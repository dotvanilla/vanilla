Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.Syntax

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
                Return New LiteralValue(Value.Length)
            End Get
        End Property

        Public ReadOnly Property nchars As Integer
            Get
                Return Value.Length
            End Get
        End Property

        Public Function ToSExpression() As String
            Dim lines As New List(Of String)
            Dim nchars As Integer = Strings.Len(Value)

            lines += $""
            lines += $";; String from {MemoryPtr} with {nchars} bytes in memory"

            If Not Annotation.StringEmpty Then
                lines += ";;"
                lines += ";; " & Annotation
                lines += ";;"
            End If

            lines += $"(data (i32.const {DirectCast(MemoryPtr, StaticPtr).Scan0}) ""{Value}\00"")"

            Return lines _
                .Select(Function(line) "    " & line) _
                .JoinBy(ASCII.LF)
        End Function
    End Class
End Namespace