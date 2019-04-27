#Region "Microsoft.VisualBasic::4d110052ab099c97dca2ee13efd79903, Symbols\Parser\CTypeParser.vb"

' Author:
' 
'       xieguigang (I@xieguigang.me)
'       asuka (evia@lilithaf.me)
' 
' Copyright (c) 2019 GCModeller Cloud Platform
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

'     Module CTypeParser
' 
'         Function: typefit
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Wasm.Symbols

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
    Public Function [CType](left As String, right As Expression, symbols As SymbolTable) As Expression
        Dim rightTypeInfer As TypeAbstract = right.TypeInfer(symbols)
        Dim rightIsI32 As Boolean = rightTypeInfer = "i32" OrElse rightTypeInfer = GetType(Integer).FullName
        Dim isArrayType As Boolean = IsArray(left)

        ' is any type in typescript
        If left = "object" OrElse left = "any" OrElse left = GetType(Object).FullName Then
            ' 可以传递任意类型给左边
            Return right
        ElseIf left Like stringType AndAlso rightTypeInfer Like stringType Then
            Return right
        End If

        ' is a javascript object table
        If left = GetType(DictionaryBase).FullName AndAlso TypeOf right Is ArrayTable Then
            Return right
        End If
        If rightIsI32 AndAlso IsArray(left) Then
            Return right
        End If

        If left = rightTypeInfer Then
            Return right
        ElseIf left Like stringType AndAlso rightIsI32 Then
            Return right
        ElseIf rightTypeInfer Like stringType AndAlso left = "i32" Then
            Return right
        ElseIf left = booleanType AndAlso rightIsI32 Then
            Return right
        ElseIf isArrayType AndAlso (TypeOf right Is ArraySymbol OrElse TypeOf right Is Symbols.Array) Then
            ' is javascript array type
            Return right
        End If

        Select Case left
            Case "i32", DotNet.Integer
                Return TypeExtensions.CInt(right, symbols)
            Case "i64", DotNet.Long
                Return TypeExtensions.CLng(right, symbols)
            Case "f32", DotNet.Single
                Return TypeExtensions.CSng(right, symbols)
            Case "f64", DotNet.Double
                Return TypeExtensions.CDbl(right, symbols)
            Case "char*", DotNet.String, DotNet.Char
                ' 左边是字符串类型，但是右边不是字符串或者整形数
                ' 则说明是一个需要将目标转换为字符串的操作
                Return right.AnyToString(symbols)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function [CInt](exp As Expression, symbols As SymbolTable) As Expression
        Dim type = exp.TypeInfer(symbols)
        Dim operator$

        Select Case type
            Case "i32", DotNet.Integer
                Return exp
            Case "i64", DotNet.Long
                [operator] = "i32.wrap/i64"
            Case "f32", DotNet.Single
                [operator] = "i32.trunc_s/f32"
            Case "f64", DotNet.Double
                [operator] = "i32.trunc_s/f64"
            Case Else
                Throw New NotImplementedException
        End Select

        Return CTypeInvoke([operator], exp)
    End Function

    Public Function [CLng](exp As Expression, symbols As SymbolTable) As Expression
        Dim type = exp.TypeInfer(symbols)
        Dim operator$

        Select Case type
            Case "i32", DotNet.Integer
                [operator] = "i64.extend_s/i32"
            Case "i64", DotNet.Long
                Return exp
            Case "f32", DotNet.Single
                [operator] = "i64.trunc_s/f32"
            Case "f64", DotNet.Double
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

        Select Case type
            Case "i32", DotNet.Integer
                [operator] = "f64.convert_s/i32"
            Case "i64", DotNet.Long
                [operator] = "f64.convert_s/i64"
            Case "f32", DotNet.Single
                [operator] = "f64.promote/f32"
            Case "f64", DotNet.Double
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
