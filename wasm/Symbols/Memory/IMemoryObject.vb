#Region "Microsoft.VisualBasic::36dfc84c52f6219ff01c6cdcbee69804, Symbols\Memory\IMemoryObject.vb"

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

    '     Class IMemoryObject
    ' 
    '         Properties: memoryPtr, ObjectManager
    ' 
    '         Function: [AddressOf], (+2 Overloads) IndexOffset
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public MustInherit Class IMemoryObject : Inherits Expression

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 如果是一个表达式，则是一个动态资源，反之为字符串之类的静态资源
        ''' </remarks>
        Public Property memoryPtr As [Variant](Of Integer, Expression)

        Public Shared ReadOnly Property ObjectManager As New DeclareGlobal With {
            .init = Literal.i32(1),
            .[module] = "global",
            .name = NameOf(ObjectManager),
            .type = TypeAbstract.i32
        }

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        Public Overridable Function [AddressOf]() As Expression
            If memoryPtr Like GetType(Integer) Then
                Return Literal.i32(memoryPtr.TryCast(Of Integer))
            Else
                Return memoryPtr.TryCast(Of Expression)
            End If
        End Function

        Public Overrides Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Return Me.AddressOf.GetSymbolReference
        End Function

        Public Shared Function IndexOffset(array As Expression, offset As Integer) As Expression
            Return IndexOffset(array, Literal.i32(offset))
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
