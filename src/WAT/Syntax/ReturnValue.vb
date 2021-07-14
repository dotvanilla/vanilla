Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class ReturnValue : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return Value.Type
            End Get
        End Property

        Public Property Value As WATSyntax

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(return {Value.ToSExpression(env, indent)})"
        End Function
    End Class
End Namespace