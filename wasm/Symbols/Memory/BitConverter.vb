﻿#Region "Microsoft.VisualBasic::129d914fa48b106c36bffbefd84923eb, Symbols\Memory\BitConverter.vb"

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

    '     Module BitConverter
    ' 
    '         Function: [addressOf], (+2 Overloads) load, Loadf32, Loadf64, Loadi32
    '                   Loadi64, (+2 Overloads) save
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' WebAssembly Linear Memory Accesses
    ''' </summary>
    Module BitConverter

        Public Function Loadi32(intptr As [Variant](Of Integer, Expression)) As Expression
            Return load("i32", intptr)
        End Function

        Public Function Loadi64(intptr As [Variant](Of Integer, Expression)) As Expression
            Return load("i64", intptr)
        End Function

        Public Function Loadf32(intptr As [Variant](Of Integer, Expression)) As Expression
            Return load("f32", intptr)
        End Function

        Public Function Loadf64(intptr As [Variant](Of Integer, Expression)) As Expression
            Return load("f64", intptr)
        End Function

        ''' <summary>
        ''' 从内存之中读取指定位置的字节块，然后返回对应的数值
        ''' </summary>
        ''' <param name="type">只有4种基础的数据类型</param>
        ''' <param name="intptr">内存的位置</param>
        ''' <returns></returns>
        Public Function load(type$, intptr As [Variant](Of Integer, Expression)) As Expression
            Return New FuncInvoke With {
                .[operator] = True,
                .parameters = {intptr.[addressOf]()},
                .refer = New ReferenceSymbol With {
                    .Symbol = $"{type}.load",
                    .Type = SymbolType.Operator
                }
            }
        End Function

        Public Function load(type As TypeAbstract, intptr As [Variant](Of Integer, Expression)) As Expression
            Return load(type.typefit, intptr)
        End Function

        ''' <summary>
        ''' 请注意，这个写内存的函数并不包括自定类型转换，需要在调用之前就完成类型转换操作
        ''' </summary>
        ''' <param name="type"></param>
        ''' <param name="intptr"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Function save(type As TypeAbstract, intptr As [Variant](Of Integer, Expression), value As Expression) As Expression
            Return save(type.typefit, intptr, value)
        End Function

        <Extension>
        Private Function [addressOf](intptr As [Variant](Of Integer, Expression)) As Expression
            If intptr Like GetType(Integer) Then
                Return Literal.i32(intptr)
            Else
                Return intptr.TryCast(Of Expression)
            End If
        End Function

        ''' <summary>
        ''' 将数据写入指定位置的内存之中
        ''' </summary>
        ''' <param name="type$"></param>
        ''' <param name="intptr"></param>
        ''' <param name="value"></param>
        ''' <returns></returns>
        Public Function save(type$, intptr As [Variant](Of Integer, Expression), value As Expression) As Expression
            Return New FuncInvoke With {
                .[operator] = True,
                .parameters = {intptr.[addressOf](), value},
                .refer = New ReferenceSymbol With {
                    .Symbol = $"{type}.store",
                    .Type = SymbolType.Operator
                }
            }
        End Function
    End Module
End Namespace
