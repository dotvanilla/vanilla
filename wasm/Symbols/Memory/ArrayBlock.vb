
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols

    Public Class ArrayBlock : Inherits Expression
        Implements IEnumerable(Of Expression)

        Public Property type As TypeAbstract
        Public Property memoryPtr As Integer
        Public Property length As Integer
        Public Property elements As Expression()

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        ''' <summary>
        ''' 返回的是内存之中的首位置
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToSExpression() As String
            Return Literal.i32(memoryPtr).ToSExpression
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of Expression) Implements IEnumerable(Of Expression).GetEnumerator
            For Each x As Expression In elements
                Yield x
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function

        ''' <summary>
        ''' 返回读写数组元素的内存的位置表达式
        ''' </summary>
        ''' <param name="array"></param>
        ''' <param name="offset"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 因为array对象和i32对象之间不方便直接相加，所以在这里单独使用这个函数来计算实际的内存位置
        ''' </remarks>
        Public Shared Function IndexOffset(array As Expression, offset As Expression) As Expression
            Return New FuncInvoke() With {
                .[operator] = True,
                .parameters = {array, offset},
                .refer = i32Add
            }
        End Function
    End Class
End Namespace