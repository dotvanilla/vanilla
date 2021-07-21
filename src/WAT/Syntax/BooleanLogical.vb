Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class BooleanLogical : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.boolean
            End Get
        End Property

        Public Property expression As WATSyntax

        Sub New(b As WATSyntax)
            expression = b
        End Sub

        Public Overrides Function ToString() As String
            Return "(boolean) " & expression.ToString
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return expression.ToSExpression(env, indent)
        End Function
    End Class
End Namespace