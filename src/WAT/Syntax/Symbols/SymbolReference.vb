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

        Public Shared Operator =(symbol As SymbolReference, name As String) As Boolean
            Return symbol.Name = name
        End Operator

        Public Shared Operator <>(symbol As SymbolReference, name As String) As Boolean
            Return Not symbol = name
        End Operator
    End Class
End Namespace