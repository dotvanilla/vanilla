Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' The class object meta data object model. 
    ''' </summary>
    Public Class ClassMeta : Inherits IMemoryObject
        Implements IDeclaredObject

        Public Property [Module] As String Implements IDeclaredObject.Module
        Public Property ClassName As String Implements IKeyedEntity(Of String).Key

        ''' <summary>
        ''' 字段
        ''' </summary>
        ''' <returns></returns>
        Public Property Fields As DeclareGlobal()
        ''' <summary>
        ''' 方法和属性
        ''' </summary>
        ''' <returns></returns>
        Public Property Methods As FuncSymbol()

        Public ReadOnly Property Reference As ReferenceSymbol
            Get
                Return New ReferenceSymbol With {
                    .[Module] = [Module],
                    .Symbol = ClassName,
                    .Type = SymbolType.Type
                }
            End Get
        End Property

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.intptr, Reference)
        End Function

        ''' <summary>
        ''' 返回这个元数据在内存之中的位置
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToSExpression() As String
            Return Me.AddressOf.ToSExpression
        End Function
    End Class
End Namespace