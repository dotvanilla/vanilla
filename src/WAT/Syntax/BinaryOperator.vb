Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class BinaryOperator : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Property [operator] As String
        Public Property left As WATSyntax
        Public Property right As WATSyntax

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace