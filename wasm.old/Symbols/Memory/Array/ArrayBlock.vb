#Region "Microsoft.VisualBasic::77e8d4badc8212b0e77afc54bebf2af5, Symbols\Memory\Array\ArrayBlock.vb"

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
'         Properties: elements, elementSizeOf, itemOffset, length, sizeOf
'                     type
' 
'         Constructor: (+1 Overloads) Sub New
'         Function: [AddressOf], GetEnumerator, IEnumerable_GetEnumerator, ToSExpression, TypeInfer
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' ##### 2019-05-19
    ''' Uncaught (in promise) RangeError: start offset of Float32Array should be a multiple of 4
    ''' </remarks>
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
        Public Property length As Expression
        Public Property elements As Expression()

        Dim symbols As SymbolTable

        ''' <summary>
        ''' Byte size of this array object
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property sizeOf As Expression
            Get
                ' class_id + length + elements
                Return Literal.IntPtr(4 + 4) + elementSizeOf * New IntPtr(length)
            End Get
        End Property

        ''' <summary>
        ''' 元素类型所占用的内存空间大小
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property elementSizeOf As Integer
            Get
                Return Types.sizeOf(type.generic(Scan0), symbols)
            End Get
        End Property

        Sub New(symbols As SymbolTable)
            Me.symbols = symbols
        End Sub

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function [AddressOf]() As Expression
            Return memoryPtr.TryCast(Of Expression)
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            For Each symbol In [AddressOf].GetSymbolReference
                Yield symbol
            Next

            For Each item As Expression In elements.SafeQuery
                For Each symbol In item.GetSymbolReference
                    Yield symbol
                Next
            Next
        End Function

        ''' <summary>
        ''' 返回的是内存之中的首位置
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function ToSExpression() As String
            Return Me.AddressOf.ToSExpression
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of Expression) Implements IEnumerable(Of Expression).GetEnumerator
            ' 在这里还需要写入一些基础信息
            ' https://vanillavb.app/#array_impl
            Dim class_id As Expression = Literal.i32(type.generic(Scan0).class_id)
            Dim getLocal As GetLocalVariable = memoryPtr.TryCast(Of GetLocalVariable)

            Yield New SetLocalVariable(getLocal.var, Allocate.Call(sizeOf, Literal.i32(TypeAlias.array)))
            Yield New CommentText($"class_id/typealias_enum i32 data: {class_id}/{type.ToString}")
            ' 类型枚举值
            Yield BitConverter.save("i32", memoryPtr, class_id)
            ' 数组的元素数量
            Yield BitConverter.save("i32", ArrayBlock.IndexOffset(memoryPtr, 4), length)

            Yield New CommentText("End of byte marks meta data, start write data blocks")

            For Each expression As Expression In elements.SafeQuery
                If TypeOf expression Is UserObject Then
                    With DirectCast(expression, UserObject)
                        If .Meta.isStruct Then
                            For Each internal As Expression In .AsEnumerable
                                Yield internal
                            Next
                        Else
                            ' 在这里只允许结构体类型，但是出现了class引用
                            ' 应该是程序哪里出错了
                            Throw New InvalidOperationException
                        End If
                    End With
                Else
                    Yield expression
                End If
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
