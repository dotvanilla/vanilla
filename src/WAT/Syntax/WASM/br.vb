Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax.WASM

    Public Class br : Inherits WATSyntax

        Public Property blockLabel As String

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.void
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(br ${blockLabel})"
        End Function
    End Class

End Namespace