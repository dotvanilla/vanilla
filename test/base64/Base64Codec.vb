' 手写 Base64 编码/解码模块（教学版）
' 不使用 Convert.ToBase64String / Convert.FromBase64String，
' 只用数组、循环、整数位运算等基础能力，从零实现完整算法。
Imports System.Text

Public Module Base64Codec

    ' =====================================================================
    ' Base64 字母表：64 个字符，恰好覆盖 6 位二进制数的全部取值(0~63)。
    ' 这就是“每 6 位一组”的由来：2^6 = 64。
    ' 顺序由标准 RFC 4648 规定，不能打乱，否则别人无法解码。
    ' =====================================================================
    Private Const Alphabet As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"

    ' =====================================================================
    ' 对外接口（文本层）
    ' Base64 本质上处理的是“字节”而不是“字符”，所以分两层：
    '     文本 --(UTF-8)--> 字节 --(Base64核心)--> Base64字符串
    ' =====================================================================

    ''' <summary>把普通文本编码为 Base64 字符串</summary>
    Public Function EncodeText(text As String) As String
        ' 纯英文：1 字符 = 1 字节；中文：1 字符通常 = 3 字节（UTF-8）
        Dim bytes As Byte() = Encoding.UTF8.GetBytes(text)
        Return EncodeBytes(bytes)
    End Function

    ''' <summary>把 Base64 字符串解码还原为普通文本</summary>
    Public Function DecodeText(base64 As String) As String
        Dim bytes As Byte() = DecodeBytes(base64)
        ' 必须用与编码时相同的字符编码，否则非 ASCII 文本会变乱码
        Return Encoding.UTF8.GetString(bytes)
    End Function

    ' =====================================================================
    ' 核心算法（字节层）
    ' =====================================================================

    ' 编码思路（以 "Man" 为例，M=77, a=97, n=110）：
    '
    '   字节:      01001101  01100001  01101110        3 字节 = 24 位
    '   重新分组:  010011 | 010110 | 000101 | 101110    4 组 × 6 位
    '   十进制:       19       22       5       46
    '   查字母表:     T        W       F       u
    '   结果:      "TWFu"
    '
    Public Function EncodeBytes(data As Byte()) As String
        If data Is Nothing OrElse data.Length = 0 Then Return String.Empty

        Dim sb As New StringBuilder()

        ' ---------- 主循环：每次处理完整的 3 个字节 ----------
        Dim i As Integer = 0
        While i + 3 <= data.Length
            ' 先把字节放进 Integer 再做位运算
            ' （注意：VB 中 Byte << 16 会按 Byte 截断，必须先转 Integer！）
            Dim b1 As Integer = data(i)
            Dim b2 As Integer = data(i + 1)
            Dim b3 As Integer = data(i + 2)

            ' 3 个字节按“高位在前”拼成一个 24 位整数：
            ' b1 占第 23~16 位，b2 占第 15~8 位，b3 占第 7~0 位
            Dim combined As Integer = (b1 << 16) Or (b2 << 8) Or b3

            ' 从 24 位里自高向低取出 4 个 6 位组：
            '   >>18 后取低 6 位 → 第 23~18 位
            '   >>12 后取低 6 位 → 第 17~12 位
            '   >>6  后取低 6 位 → 第 11~6 位
            '   不移位取低 6 位  → 第 5~0 位
            ' &H3F = 二进制 00111111，And 它等于“只保留最低 6 位”
            sb.Append(Alphabet((combined >> 18) And &H3F))
            sb.Append(Alphabet((combined >> 12) And &H3F))
            sb.Append(Alphabet((combined >> 6) And &H3F))
            sb.Append(Alphabet(combined And &H3F))

            i += 3
        End While

        ' ---------- 尾部：不足 3 字节的部分 ----------
        Dim remaining As Integer = data.Length - i

        If remaining = 1 Then
            ' 剩 1 字节(8位)：低位补 4 个 0 → 12 位 → 2 个 6 位组
            ' 输出 2 个字符 + 2 个 '=' 凑满一组。例："M" → "TQ=="
            Dim b1 As Integer = data(i)
            Dim combined As Integer = b1 << 16      ' 相当于低位跟了 16 个 0
            sb.Append(Alphabet((combined >> 18) And &H3F))
            sb.Append(Alphabet((combined >> 12) And &H3F))
            sb.Append("==")
        ElseIf remaining = 2 Then
            ' 剩 2 字节(16位)：低位补 2 个 0 → 18 位 → 3 个 6 位组
            ' 输出 3 个字符 + 1 个 '='。例："Ma" → "TWE="
            Dim b1 As Integer = data(i)
            Dim b2 As Integer = data(i + 1)
            Dim combined As Integer = (b1 << 16) Or (b2 << 8)
            sb.Append(Alphabet((combined >> 18) And &H3F))
            sb.Append(Alphabet((combined >> 12) And &H3F))
            sb.Append(Alphabet((combined >> 6) And &H3F))
            sb.Append("=")
        End If
        ' remaining = 0：正好整除，无需填充

        Return sb.ToString()
    End Function

    ' 解码思路：编码的逆过程。每 4 个字符查表还原出 4 个 6 位数，
    ' 拼回 24 位，再拆出最多 3 个字节；'=' 标记被补零截断的部分。
    Public Function DecodeBytes(base64 As String) As Byte()
        If base64 Is Nothing Then Throw New ArgumentNullException("base64")
        If base64.Length = 0 Then Return New Byte() {}

        ' 4 个字符一组，所以总长度必须是 4 的倍数
        If base64.Length Mod 4 <> 0 Then
            Throw New ArgumentException("Base64 字符串的长度必须是 4 的倍数。")
        End If

        ' 简化校验：'=' 只允许出现在最后两个字符的位置
        For j As Integer = 0 To base64.Length - 3
            If base64(j) = "="c Then
                Throw New ArgumentException("填充符 '=' 只能出现在末尾。")
            End If
        Next

        ' 统计末尾 '=' 个数（0、1 或 2）
        Dim padCount As Integer = 0
        If base64(base64.Length - 1) = "="c Then padCount += 1
        If padCount >= 1 AndAlso base64(base64.Length - 2) = "="c Then padCount += 1

        ' 还原后的总字节数：组数 × 3 - 填充数
        '   "xxxx"→3字节   "xxx="→2字节   "xx=="→1字节
        Dim totalBytes As Integer = base64.Length \ 4 * 3 - padCount
        Dim result(totalBytes - 1) As Byte
        Dim outPos As Integer = 0

        Dim i As Integer = 0
        While i < base64.Length
            ' ---------- 取一组 4 个字符，查表转成 6 位数值 ----------
            Dim v1 As Integer = CharToValue(base64(i))
            Dim v2 As Integer = CharToValue(base64(i + 1))

            ' 第 3、4 个字符可能是 '='（只出现在最后一组）。
            ' 它们对应的位本来就是编码时补的 0，所以按 0 处理即可。
            Dim c3 As Char = base64(i + 2)
            Dim c4 As Char = base64(i + 3)
            Dim v3 As Integer = If(c3 = "="c, 0, CharToValue(c3))
            Dim v4 As Integer = If(c4 = "="c, 0, CharToValue(c4))

            ' ---------- 4 个 6 位组拼回 24 位（与编码互逆）----------
            Dim combined As Integer = (v1 << 18) Or (v2 << 12) Or (v3 << 6) Or v4

            ' ---------- 从 24 位里自高向低拆出最多 3 个字节 ----------
            ' &HFF = 二进制 11111111，And 它等于“只保留最低 8 位”
            result(outPos) = CByte((combined >> 16) And &HFF)
            outPos += 1

            If c3 <> "="c Then      ' 第 3 个字符有效 → 第 2 个字节存在
                result(outPos) = CByte((combined >> 8) And &HFF)
                outPos += 1
            End If

            If c4 <> "="c Then      ' 第 4 个字符有效 → 第 3 个字节存在
                result(outPos) = CByte(combined And &HFF)
                outPos += 1
            End If

            i += 4
        End While

        Return result
    End Function

    ' 反向查表：字符 → 字母表下标(0~63)。
    ' 下标恰好就是该字符代表的 6 位数值，例如 'A'→0、'a'→26、'/'→63。
    ' （工程上可用 128 长度的数组做 O(1) 反查，这里用线性查找便于理解）
    Private Function CharToValue(c As Char) As Integer
        For i As Integer = 0 To Alphabet.Length - 1
            If Alphabet(i) = c Then Return i
        Next
        Throw New ArgumentException("遇到非法的 Base64 字符: " & c)
    End Function

End Module
