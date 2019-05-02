#Region "Microsoft.VisualBasic::c1284cdb482b26105b5b58ff14869169, Symbols\Memory\StringSymbol.vb"

' Author:
' 
'       xieguigang (I@xieguigang.me)
'       asuka (evia@lilithaf.me)
'       wasm project (developer@vanillavb.app)
' 
' Copyright (c) 2019 developer@vanillavb.app, VanillaBasic(https://vanillavb.app)
' 
' 
' MIT License
' 
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.



' /********************************************************************************/

' Summaries:

'     Class StringSymbol
' 
'         Properties: [string], Length, MemoryPtr
' 
'         Function: [AddressOf], SizeOf, ToSExpression, TypeInfer
' 
'     Class ArrayBlock
' 
'         Properties: elements, length, memoryPtr, type
' 
'         Function: GetEnumerator, IEnumerable_GetEnumerator, IndexOffset, ToSExpression, TypeInfer
' 
' 
' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' 因为wasm不支持字符串，但是支持内存对象，所以字符串使用的是一个i32类型的内存地址来表示
    ''' </summary>
    Public Class StringSymbol : Inherits Expression

        Public Property [string] As String
        Public Property MemoryPtr As Integer

        Public ReadOnly Property Length As Integer
            Get
                Return Strings.Len([string])
            End Get
        End Property

        Public Function SizeOf() As Expression
            Return New LiteralExpression With {.type = TypeAbstract.i32, .value = Length}
        End Function

        Public Function [AddressOf]() As Expression
            Return New LiteralExpression With {.type = TypeAbstract.i32, .value = MemoryPtr}
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.string)
        End Function

        Public Overrides Function ToSExpression() As String
            Return $"
    ;; String from {MemoryPtr} with {Length} bytes in memory
    (data (i32.const {MemoryPtr}) ""{[string]}\00"")"
        End Function
    End Class

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
