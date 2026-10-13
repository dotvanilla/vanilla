Namespace JavaScript

    ''' <summary>
    ''' ``System.Convert`` 相关的 BCL 桥接。
    ''' 数组统一使用 ``Integer``（线性内存指针）表示。
    ''' </summary>
    Public Class Convert

        ''' <summary>
        ''' ``Convert.ToBase64String(bytes) -> String``
        ''' </summary>
        Public Declare Function ToBase64String Lib "Convert" Alias "Convert.ToBase64String" (bytes As Integer) As String

        ''' <summary>
        ''' ``Convert.FromBase64String(text) -> Byte()``
        ''' </summary>
        Public Declare Function FromBase64String Lib "Convert" Alias "Convert.FromBase64String" (text As String) As Integer

        ''' <summary>
        ''' ``Convert.ToString(value)``
        ''' </summary>
        Public Declare Function ToString Lib "Convert" Alias "Convert.ToString" (value As Integer) As String

    End Class
End Namespace
