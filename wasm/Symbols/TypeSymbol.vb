Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols

    Public Class TypeSymbol : Inherits Expression

        Public Property type As TypeAbstract

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        Public Overrides Function ToSExpression() As String
            Return type.ToString
        End Function
    End Class
End Namespace