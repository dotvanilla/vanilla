Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.Exception

    ''' <summary>
    ''' Exception handler model for WebAssembly
    ''' </summary>
    Public Class JavaScriptError : Inherits Expression

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.void
        End Function

        Public Overrides Function ToSExpression() As String
            Throw New NotImplementedException()
        End Function

        Public Overrides Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace