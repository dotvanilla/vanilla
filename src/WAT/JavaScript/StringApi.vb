Namespace JavaScript

    ''' <summary>
    ''' ``System.String`` 相关的运行时桥接。
    ''' </summary>
    Public Class StringApi

        ''' <summary>
        ''' ``a &amp; b`` 字符串连接
        ''' </summary>
        Public Declare Function [Add] Lib "string" Alias "string.add" (a As String, b As String) As String

        ''' <summary>
        ''' ``s.Length``
        ''' </summary>
        Public Declare Function Length Lib "string" Alias "string.length" (s As String) As Integer

        ''' <summary>
        ''' ``s(i)``，返回单个字符（以单字符字符串表示）
        ''' </summary>
        Public Declare Function CharAt Lib "string" Alias "string.charAt" (s As String, index As Integer) As String

        ''' <summary>
        ''' ``s.Substring(start, len)``
        ''' </summary>
        Public Declare Function Substring Lib "string" Alias "string.substr" (s As String, start As Integer, len As Integer) As String

        ''' <summary>
        ''' ``s.Replace(find, replacement)``
        ''' </summary>
        Public Declare Function Replace Lib "string" Alias "string.replace" (s As String, find As String, replacement As String) As String

        ''' <summary>
        ''' ``s.Trim()``
        ''' </summary>
        Public Declare Function Trim Lib "string" Alias "string.trim" (s As String) As String

        ''' <summary>
        ''' ``s.IndexOf(find)``
        ''' </summary>
        Public Declare Function IndexOf Lib "string" Alias "string.indexOf" (s As String, find As String) As Integer

        ''' <summary>
        ''' ``s.ToUpper()``
        ''' </summary>
        Public Declare Function ToUpper Lib "string" Alias "string.upper" (s As String) As String

        ''' <summary>
        ''' ``s.ToLower()``
        ''' </summary>
        Public Declare Function ToLower Lib "string" Alias "string.lower" (s As String) As String

        ''' <summary>
        ''' ``s.Equals(other)`` / ``s = other``
        ''' </summary>
        Public Shadows Declare Function Equals Lib "string" Alias "string.equals" (a As String, b As String) As Integer

    End Class
End Namespace
