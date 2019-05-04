
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' 因为在读取field值的时候，因为是从memory之中进行load操作
    ''' 导致字符串，数组等数据类型被误判为i32产生额外的或者错误的数据类型转换
    ''' 所以在这里会需要使用这个对象来保持原本的类型信息
    ''' </summary>
    Public Class FieldValue : Inherits Expression

        Public Property type As TypeAbstract
        ''' <summary>
        ''' 从内存之中读取出来的值，这个因为load函数的限制
        ''' 如果直接对表达式做类型推断的话，只能够得到4种基础类型
        ''' </summary>
        ''' <returns></returns>
        Public Property value As Expression

        ''' <summary>
        ''' 在类型推断返回原本的字段类型
        ''' </summary>
        ''' <param name="symbolTable"></param>
        ''' <returns></returns>
        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        Public Overrides Function ToSExpression() As String
            Return value.ToSExpression
        End Function
    End Class
End Namespace