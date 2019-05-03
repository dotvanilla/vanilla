#Region "Microsoft.VisualBasic::e0af479bd22d2c96898e6954e1855087, Symbols\Memory\ArrayBlock.vb"

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

    '     Class ArrayBlock
    ' 
    '         Properties: elements, length, sizeOf, type
    ' 
    '         Function: GetEnumerator, IEnumerable_GetEnumerator, (+2 Overloads) IndexOffset, ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public Class ArrayBlock : Inherits IMemoryObject
        Implements IEnumerable(Of Expression)

        ''' <summary>
        ''' ``ArrayOf``, 这个类型不是元素类型，而是一个完整的数组类型的定义 
        ''' </summary>
        ''' <returns></returns>
        Public Property type As TypeAbstract
        ''' <summary>
        ''' Element counts in this array object
        ''' </summary>
        ''' <returns></returns>
        Public Property length As Integer
        Public Property elements As Expression()

        ''' <summary>
        ''' Byte size of this array object
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property sizeOf As Integer
            Get
                ' class_id + length + elements
                Return 4 + 4 + Types.sizeOf(type) * length
            End Get
        End Property

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
            ' 在这里还需要写入一些基础信息
            ' https://vanillavb.app/#array_impl
            Dim class_id As Expression = Literal.i32(type.generic(Scan0).class_id)

            Yield New CommentText($"class_id/typealias_enum i32 data: {class_id}/{type.ToString}")
            ' 类型枚举值
            Yield BitConverter.save("i32", memoryPtr, class_id)
            ' 数组的元素数量
            Yield BitConverter.save("i32", ArrayBlock.IndexOffset(memoryPtr, 4), Literal.i32(length))

            Yield New CommentText("End of byte marks meta data, start write data blocks")

            For Each x As Expression In elements
                Yield x
            Next

            Yield New CommentText($"Offset object manager with {sizeOf} bytes")
            Yield New SetGlobalVariable(IMemoryObject.ObjectManager) With {
                .value = ArrayBlock.IndexOffset(Me.AddressOf, sizeOf)
            }
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
