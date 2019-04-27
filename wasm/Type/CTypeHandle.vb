﻿#Region "Microsoft.VisualBasic::ef1c24ebb8ac8e6c13e1fdb2fe591766, Type\CTypeHandle.vb"

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

    ' Module CTypeHandle
    ' 
    '     Function: [CDbl], [CInt], [CLng], [CSng], [CType]
    '               CTypeInvoke, (+2 Overloads) typefit
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Wasm.Symbols
Imports Wasm.Symbols.Parser

''' <summary>
''' Type cast operation in WebAssembly runtime
''' </summary>
Module CTypeHandle

    ''' <summary>
    ''' Convert type alias in compiler to WebAssembly primitive type.
    ''' (这个自动类型转换应该是仅在生成``S-Expression``的时候使用)
    ''' </summary>
    ''' <param name="type"></param>
    ''' <returns></returns>
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function typefit(type As TypeAlias) As String
        If type Like primitiveTypes Then
            Return type.ToString
        ElseIf type = TypeAlias.void Then
            Return "void"
        Else
            ' All of the non-primitive type is memory pointer
            Return "i32"
        End If
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Public Function typefit(arg As NamedValue(Of TypeAbstract)) As String
        Return arg.Value.typefit
    End Function

    ''' <summary>
    ''' ``CType`` operator to webassembly 
    ''' ``Datatype conversions, truncations, reinterpretations, promotions, and demotions`` feature.
    ''' 
    ''' > https://github.com/WebAssembly/design/blob/master/Semantics.md
    ''' </summary>
    ''' <param name="left"></param>
    ''' <returns></returns>
    Public Function [CType](left As TypeAbstract, right As Expression, symbols As SymbolTable) As Expression
        Dim rightTypeInfer As TypeAbstract = right.TypeInfer(symbols)

        If left.type = TypeAlias.any Then
            ' left accept any type
            Return right
        End If

        Select Case left.type
            Case TypeAlias.i32
                Return CTypeHandle.CInt(right, symbols)
            Case TypeAlias.i64
                Return CTypeHandle.CLng(right, symbols)
            Case TypeAlias.f32
                Return CTypeHandle.CSng(right, symbols)
            Case TypeAlias.f64
                Return CTypeHandle.CDbl(right, symbols)
            Case TypeAlias.string
                ' 左边是字符串类型，但是右边不是字符串或者整形数
                ' 则说明是一个需要将目标转换为字符串的操作
                Return right.AnyToString(symbols)
            Case Else
                Throw New InvalidCastException($"{left} -> {rightTypeInfer}")
        End Select
    End Function

    Public Function [CInt](exp As Expression, symbols As SymbolTable) As Expression
        Dim type = exp.TypeInfer(symbols)
        Dim operator$

        Select Case type.type
            Case TypeAlias.i32
                Return exp
            Case TypeAlias.i64
                [operator] = "i32.wrap/i64"
            Case TypeAlias.f32
                [operator] = "i32.trunc_s/f32"
            Case TypeAlias.f64
                [operator] = "i32.trunc_s/f64"
            Case Else
                Throw New NotImplementedException
        End Select

        Return CTypeInvoke([operator], exp)
    End Function

    Public Function [CLng](exp As Expression, symbols As SymbolTable) As Expression
        Dim type = exp.TypeInfer(symbols)
        Dim operator$

        Select Case type.type
            Case TypeAlias.i32
                [operator] = "i64.extend_s/i32"
            Case TypeAlias.i64
                Return exp
            Case TypeAlias.f32
                [operator] = "i64.trunc_s/f32"
            Case TypeAlias.f64
                [operator] = "i64.trunc_s/f64"
            Case Else
                Throw New NotImplementedException
        End Select

        Return CTypeInvoke([operator], exp)
    End Function

    Public Function [CSng](exp As Expression, symbols As SymbolTable) As Expression
        Dim type As TypeAbstract = exp.TypeInfer(symbols)
        Dim operator$

        Select Case type.type
            Case TypeAlias.i32
                [operator] = "f32.convert_s/i32"
            Case TypeAlias.i64
                [operator] = "f32.convert_s/i64"
            Case TypeAlias.f32
                Return exp
            Case TypeAlias.f64
                [operator] = "f32.demote/f64"
            Case Else
                Throw New NotImplementedException
        End Select

        Return CTypeInvoke([operator], exp)
    End Function

    Public Function [CDbl](exp As Expression, symbols As SymbolTable) As Expression
        Dim type = exp.TypeInfer(symbols)
        Dim operator$

        Select Case type.type
            Case TypeAlias.i32
                [operator] = "f64.convert_s/i32"
            Case TypeAlias.i64
                [operator] = "f64.convert_s/i64"
            Case TypeAlias.f32
                [operator] = "f64.promote/f32"
            Case TypeAlias.f64
                Return exp
            Case Else
                Throw New NotImplementedException
        End Select

        Return CTypeInvoke([operator], exp)
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Private Function CTypeInvoke(operator$, exp As Expression) As Expression
        Return New FuncInvoke With {
            .refer = [operator],
            .[operator] = True,
            .parameters = {exp}
        }
    End Function
End Module
