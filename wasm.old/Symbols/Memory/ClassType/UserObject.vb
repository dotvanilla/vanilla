#Region "Microsoft.VisualBasic::d2610e5a3f1ca8ccc2cdbb3d29a794e1, Symbols\Memory\ClassType\UserObject.vb"

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

    '     Class UserObject
    ' 
    '         Properties: Initialize, Meta, UnderlyingType, width
    ' 
    '         Function: GetEnumerator, IEnumerable_GetEnumerator, ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' The user object abstract model.
    ''' 一个用户自定义类型的实例对象
    ''' </summary>
    Public Class UserObject : Inherits IMemoryObject
        Implements IEnumerable(Of Expression)

        Public Property UnderlyingType As TypeAbstract
        Public Property Meta As ClassMeta
        ''' <summary>
        ''' 当前的这个用户类型在内存之中所占用的字节大小
        ''' </summary>
        ''' <returns></returns>
        Public Property width As Integer
        Public Property Initialize As Expression()

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return UnderlyingType
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function ToSExpression() As String
            Return Me.memoryPtr.TryCast(Of Expression).ToSExpression
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            For Each symbol In Me.AddressOf.GetSymbolReference
                Yield symbol
            Next

            For Each item In Initialize
                For Each symbol In item.GetSymbolReference
                    Yield symbol
                Next
            Next
        End Function

        ''' <summary>
        ''' 这个枚举函数返回的是初始化语句
        ''' </summary>
        ''' <returns></returns>
        Public Iterator Function GetEnumerator() As IEnumerator(Of Expression) Implements IEnumerable(Of Expression).GetEnumerator
            For Each init As Expression In Initialize
                Yield init
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
