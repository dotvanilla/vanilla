Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class ReturnValue : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return Value.Type
            End Get
        End Property

        Public Property Value As WATSyntax

        Sub New()
        End Sub

        Sub New(value As WATSyntax)
            Me.Value = value
        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(return {Value.ToSExpression(env, indent)})"
        End Function

        Public Overrides Function ToString() As String
            Return $"(return {Value.ToSExpression(Nothing, Nothing)})"
        End Function
    End Class
End Namespace