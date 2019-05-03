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

        Default Public ReadOnly Property Field(name As String) As DeclareGlobal
            Get
                Return Fields.FirstOrDefault(Function(v) v.name = name)
            End Get
        End Property

        ''' <summary>
        ''' 对象实例在内存之中只有存储状态数据的字段栈位置，方法，属性之类的不需要加载到内存之中
        ''' 在这里为了方便内存分配而需要从这个属性得到计算的字段位宽来进行内存指针偏移量的计算
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property sizeOf As Integer
            Get
                Return Aggregate field As DeclareGlobal
                       In Fields
                       Let width = Types.sizeOf(field.type)
                       Into Sum(width)
            End Get
        End Property

        Public ReadOnly Property Reference As ReferenceSymbol
            Get
                Return New ReferenceSymbol With {
                    .[Module] = [Module],
                    .Symbol = ClassName,
                    .Type = SymbolType.Type
                }
            End Get
        End Property

        Public Function GetFieldOffset(name As String) As Integer
            Dim offset As Integer

            For Each field As DeclareGlobal In Fields
                If field.name = name Then
                    Exit For
                Else
                    offset += Types.sizeOf(field.type)
                End If
            Next

            Return offset
        End Function

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