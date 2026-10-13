' 控制台测试程序：新建 VB 控制台项目后，把两个文件放进去运行即可
Imports System.Text

Module Program

    Sub Main()
        Console.WriteLine("========== 经典示例 ==========")
        ShowDemo("Man")      ' 3 字节整除 → 无填充
        ShowDemo("Ma")       ' 剩 2 字节 → 1 个 '='
        ShowDemo("M")        ' 剩 1 字节 → 2 个 '='

        Console.WriteLine("========== 中文（UTF-8 多字节）==========")
        ShowDemo("你好，Base64！")

        Console.WriteLine("========== 边界情况 ==========")
        ShowDemo("")

        Console.WriteLine("========== 与 .NET 内置实现交叉验证 ==========")
        ' 说明：Convert.ToBase64String 只用来“对答案”，模块实现本身没有用它
        Dim rnd As New Random(2026)
        Dim allPass As Boolean = True
        For len As Integer = 0 To 60
            Dim sb As New StringBuilder()
            For k As Integer = 1 To len
                ' 生成 32~&HD7FE 的随机字符（避开代理区，保证 UTF-8 可无损往返）
                sb.Append(ChrW(rnd.Next(32, &HD7FF)))
            Next
            Dim text As String = sb.ToString()

            Dim mine As String = Base64Codec.EncodeText(text)
            Dim standard As String = Convert.ToBase64String(Encoding.UTF8.GetBytes(text))

            If mine <> standard OrElse Base64Codec.DecodeText(mine) <> text Then
                allPass = False
                Console.WriteLine("长度 " & len & " 的结果与标准不一致！")
            End If
        Next
        Console.WriteLine("长度 0~60 共 61 组随机数据全部通过: " & allPass.ToString())
    End Sub

    Private Sub ShowDemo(text As String)
        Dim encoded As String = Base64Codec.EncodeText(text)
        Dim decoded As String = Base64Codec.DecodeText(encoded)
        Console.WriteLine("原文     : [" & text & "]")
        Console.WriteLine("Base64   : [" & encoded & "]")
        Console.WriteLine("解码还原 : [" & decoded & "]")
        Console.WriteLine("还原一致 : " & (decoded = text).ToString())
        Console.WriteLine(New String("-"c, 46))
    End Sub

End Module
