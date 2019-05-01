Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository

Namespace Symbols

    Public Class ReferenceSymbol : Implements IDeclaredObject

        Public Property [Module] As String Implements IDeclaredObject.Module
        Public Property Symbol As String Implements IKeyedEntity(Of String).Key
        Public Property Type As SymbolType

        Public Overrides Function ToString() As String
            If Type = SymbolType.Operator OrElse Type = SymbolType.Api Then
                Return Symbol
            Else
                Return [Module] & "." & Symbol
            End If
        End Function

        Public Shared Widening Operator CType(func As FuncSignature) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .Type = SymbolType.Func,
                .[Module] = func.Module,
                .Symbol = func.Name
            }
        End Operator

        Public Shared Widening Operator CType(g As DeclareGlobal) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .Type = SymbolType.GlobalVariable,
                .[Module] = g.Module,
                .Symbol = g.name
            }
        End Operator
    End Class

    Public Enum SymbolType As Byte
        ''' <summary>
        ''' 用户在源代码之中所定义的一个用户函数
        ''' </summary>
        Func = 0
        ''' <summary>
        ''' 从JavaScript外部导入的一个Api函数引用
        ''' </summary>
        Api
        ''' <summary>
        ''' WebAssembly内部的运算符函数
        ''' </summary>
        [Operator]
        ''' <summary>
        ''' 是一个全局变量
        ''' </summary>
        GlobalVariable
    End Enum
End Namespace