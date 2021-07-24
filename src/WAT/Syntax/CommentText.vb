Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class CommentText : Inherits WATSyntax

        Public Property Text As String

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.void
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"{indent};;{Text}"
        End Function
    End Class
End Namespace