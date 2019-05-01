Imports Wasm.Compiler

Namespace Symbols

    ''' <summary>
    ''' Start符号相当于VB.NET模块的``Sub New``构造函数
    ''' </summary>
    Public Class Start : Inherits FuncSymbol

        Sub New(moduleLabel As String)
            Me.Module = moduleLabel
            Me.Name = "new"
            Me.parameters = {}
            Me.result = TypeAbstract.void
        End Sub

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.void
        End Function

        Public Overrides Function ToSExpression() As String
            Return MyBase.buildBody
        End Function
    End Class
End Namespace