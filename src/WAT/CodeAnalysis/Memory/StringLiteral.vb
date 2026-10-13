Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal

Namespace CodeAnalysis.Memory

    ''' <summary>
    ''' a string literal value
    ''' </summary>
    ''' <remarks>
    ''' just supports ASCII chars!
    ''' </remarks>
    Public Class StringLiteral : Inherits MemoryObject

        ''' <summary>
        ''' the string literal value
        ''' </summary>
        ''' <returns></returns>
        Public Property Value As String

        Public Overrides ReadOnly Property sizeOf As WATSyntax
            Get
                Return New LiteralValue(nchars)
            End Get
        End Property

        ''' <summary>
        ''' 字符串在数据段之中占用的<strong>字节</strong>数目（UTF-8 编码之后的大小）
        ''' </summary>
        ''' <remarks>
        ''' 注意：这里必须是 UTF-8 编码之后得到的字节数，而不是字符个数。
        ''' 非 ASCII 字符（例如中文）在 UTF-8 之中会占用多个字节，
        ''' 如果按照字符个数来计算内存偏移量，则会导致后续字符串的内存地址发生重叠。
        ''' </remarks>
        Public ReadOnly Property nchars As Integer
            Get
                If String.IsNullOrEmpty(Value) Then
                    Return 0
                Else
                    Return System.Text.Encoding.UTF8.GetByteCount(Value)
                End If
            End Get
        End Property

        Public Function ToSExpression() As String
            Dim lines As New List(Of String)
            Dim nchars As Integer = Me.nchars

            lines += $""
            lines += $";; String from {MemoryPtr} with {nchars} bytes in memory"

            If Not Annotation.StringEmpty Then
                lines += ";;"
                lines += ";; " & Annotation
                lines += ";;"
            End If

            lines += $"(data (i32.const {DirectCast(MemoryPtr, StaticPtr).Scan0}) ""{Escape(Value)}\00"")"

            Return lines _
                .Select(Function(line) "    " & line) _
                .JoinBy(ASCII.LF)
        End Function

        ''' <summary>
        ''' 将字符串转义为 wasm text 格式之中的字节串（反斜杠/引号/控制字符以及非 ASCII 字符）
        ''' </summary>
        Private Shared Function Escape(value As String) As String
            If value Is Nothing Then
                Return ""
            End If

            Dim sb As New System.Text.StringBuilder(value.Length)

            For Each ch As Char In value
                Dim code As Integer = AscW(ch)

                Select Case ch
                    Case "\"c
                        Call sb.Append("\\")
                    Case """"c
                        Call sb.Append("\""")
                    Case ChrW(10)
                        Call sb.Append("\0a")
                    Case ChrW(13)
                        Call sb.Append("\0d")
                    Case ChrW(9)
                        Call sb.Append("\09")
                    Case Else
                        If code < 32 OrElse code >= 128 Then
                            ' 非 ASCII 字符按照 UTF-8 字节序列进行转义
                            Dim bytes As Byte() = System.Text.Encoding.UTF8.GetBytes({ch})

                            For Each b As Byte In bytes
                                Call sb.Append("\" & b.ToString("x2"))
                            Next
                        Else
                            Call sb.Append(ch)
                        End If
                End Select
            Next

            Return sb.ToString
        End Function
    End Class
End Namespace