Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class BoolLogical : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.boolean
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace