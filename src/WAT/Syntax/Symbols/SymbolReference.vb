Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class SymbolReference : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class
End Namespace