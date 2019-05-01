Imports Wasm.Compiler

Namespace Symbols

    ''' <summary>
    ''' Start符号相当于VB.NET模块的``Sub New``构造函数
    ''' </summary>
    Public Class Start : Inherits FuncSymbol

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.void
        End Function

        Public Overrides Function ToSExpression() As String
            Return MyBase.buildBody
        End Function
    End Class
End Namespace