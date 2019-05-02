Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' 一个用户自定义类型的实例对象
    ''' </summary>
    Public Class UserObject : Inherits Expression

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ToSExpression() As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace