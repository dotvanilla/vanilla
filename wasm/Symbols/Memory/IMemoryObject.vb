Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public MustInherit Class IMemoryObject : Inherits Expression

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        Public Property memoryPtr As Integer

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        Public Function [AddressOf]() As LiteralExpression
            Return New LiteralExpression With {
                .type = TypeAbstract.i32,
                .value = memoryPtr
            }
        End Function
    End Class
End Namespace