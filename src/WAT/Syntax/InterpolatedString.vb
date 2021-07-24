Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class InterpolatedString : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.string
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace