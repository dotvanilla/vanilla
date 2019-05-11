﻿#Region "Microsoft.VisualBasic::e251020d8f82546dcb970d8081abf220, Type\TypeExtensions.vb"

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

    '     Module TypeExtensions
    ' 
    '         Properties: Comparison, Convert2Wasm, i32Add, i32Minus, i32Multiply
    '                     NumberOrders, wasmOpName
    ' 
    '         Function: ArrayElement, Compares, IsArray, TypeCharWasm
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage

' how it works
' 
' vb source => codeDOM => wast model => wast => wasm

Namespace TypeInfo

    Module TypeExtensions

        ''' <summary>
        ''' 在进行类型转换的是否，会需要使用这个索引来判断类型的优先度，同时，也可以使用这个索引来判断类型是否为基础类型
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NumberOrders As Index(Of TypeAlias) = {
            TypeAlias.i32,
            TypeAlias.f32,
            TypeAlias.i64,
            TypeAlias.f64
        }

        ''' <summary>
        ''' Webassembly之中，逻辑值是一个32位整型数
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Convert2Wasm As New Dictionary(Of Type, String) From {
            {GetType(Boolean), Types.booleanType},
            {GetType(Integer), "i32"},
            {GetType(Long), "i64"},
            {GetType(Single), "f32"},
            {GetType(Double), "f64"},
            {GetType(String), "string"}, ' 实际上这是一个integer类型
            {GetType(Char), "string"},
            {GetType(System.Void), "void"},
            {GetType(Object), "any"}
        }

        Public Function IsArray(type As String) As Boolean
            ' instr是从1开始的
            Dim p = InStr(type, "[]") - 1
            Dim lastIndex = (type.Length - 2)

            Return p = lastIndex
        End Function

        ''' <summary>
        ''' get array element type name
        ''' </summary>
        ''' <param name="fullName"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function ArrayElement(fullName As String) As String
            Return fullName.Substring(Scan0, fullName.Length - 2)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function TypeCharWasm(c As Char) As String
            Return Convert2Wasm(Scripting.GetType(Patterns.TypeCharName(c)))
        End Function
    End Module
End Namespace
