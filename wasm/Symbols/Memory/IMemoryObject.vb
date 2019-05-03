﻿#Region "Microsoft.VisualBasic::147ab78a1ff059397e25c0a2e996c2ea, Symbols\Memory\IMemoryObject.vb"

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
'         Function: [AddressOf]
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
            .[Module] = "global",
            .name = NameOf(ObjectManager),
            .type = TypeAbstract.i32
        }

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        Public Function [AddressOf]() As Expression
            If memoryPtr Like GetType(Integer) Then
                Return Literal.i32(memoryPtr.TryCast(Of Integer))
            Else
                Return memoryPtr.TryCast(Of Expression)
            End If
        End Function
    End Class
End Namespace
