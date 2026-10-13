Namespace JavaScript

    ''' <summary>
    ''' ``System.Text`` 命名空间相关的 BCL 桥接：
    ''' ``Encoding.UTF8`` 的编解码、``StringBuilder`` 以及 ``Strings``/``Char`` 相关的字符串工具。
    ''' 
    ''' 说明：为了兼容 wasm 的基本类型系统，数组在这里统一使用 ``Integer``（线性内存指针）来表示。
    ''' </summary>
    Public Class Text

        ''' <summary>
        ''' ``Encoding.UTF8.GetBytes(text) -> Byte()``，返回线性内存之中的字节数组指针
        ''' </summary>
        Public Declare Function GetBytes Lib "Encoding" Alias "Encoding.UTF8.GetBytes" (text As String) As Integer

        ''' <summary>
        ''' ``Encoding.UTF8.GetString(bytes) -> String``
        ''' </summary>
        Public Declare Function GetString Lib "Encoding" Alias "Encoding.UTF8.GetString" (bytes As Integer) As String

        ''' <summary>
        ''' ``New StringBuilder()``，返回 javascript 侧的构建器对象句柄
        ''' </summary>
        Public Declare Function NewStringBuilder Lib "StringBuilder" Alias "StringBuilder.New" () As Integer

        ''' <summary>
        ''' ``sb.Append(text)``
        ''' </summary>
        Public Declare Function Append Lib "StringBuilder" Alias "StringBuilder.Append" (sb As Integer, text As String) As Integer

        ''' <summary>
        ''' ``sb.Append(char)``
        ''' </summary>
        Public Declare Function AppendChar Lib "StringBuilder" Alias "StringBuilder.AppendChar" (sb As Integer, c As String) As Integer

        ''' <summary>
        ''' ``sb.AppendLine(text)``
        ''' </summary>
        Public Declare Function AppendLine Lib "StringBuilder" Alias "StringBuilder.AppendLine" (sb As Integer, text As String) As Integer

        ''' <summary>
        ''' ``sb.ToString()``
        ''' </summary>
        Public Shadows Declare Function ToString Lib "StringBuilder" Alias "StringBuilder.ToString" (sb As Integer) As String

        ''' <summary>
        ''' ``sb.Clear()``
        ''' </summary>
        Public Declare Function Clear Lib "StringBuilder" Alias "StringBuilder.Clear" (sb As Integer) As Integer

        ''' <summary>
        ''' ``String.Empty``
        ''' </summary>
        Public Declare Function Empty Lib "Strings" Alias "String.Empty" () As String

        ''' <summary>
        ''' ``ChrW(code)``，返回单字符字符串
        ''' </summary>
        Public Declare Function ChrW Lib "Strings" Alias "Strings.ChrW" (code As Integer) As String

        ''' <summary>
        ''' ``New String(c, n)``
        ''' </summary>
        Public Declare Function NewString Lib "Strings" Alias "Strings.NewString" (c As String, n As Integer) As String

        ''' <summary>
        ''' ``String.Concat(a, b)``
        ''' </summary>
        Public Declare Function Concat Lib "Strings" Alias "Strings.Concat" (a As String, b As String) As String

        ''' <summary>
        ''' ``String.Join(separator, array)``
        ''' </summary>
        Public Declare Function Join Lib "Strings" Alias "Strings.Join" (separator As String, array As Integer) As String

        ''' <summary>
        ''' ``AscW(c)``
        ''' </summary>
        Public Declare Function AscW Lib "Strings" Alias "Strings.AscW" (text As String) As Integer

        ''' <summary>
        ''' ``i32`` 数值到字符串的转换（对应旧运行时的 ``string.toString``）
        ''' </summary>
        Public Declare Function I32ToString Lib "string" Alias "string.i32.toString" (x As Integer) As String

        ''' <summary>
        ''' ``f64`` 数值到字符串的转换
        ''' </summary>
        Public Declare Function F64ToString Lib "string" Alias "string.f64.toString" (x As Double) As String

    End Class
End Namespace
