#Region "Microsoft.VisualBasic::f7bffbeb627f7815f55cbbd157e4a5a6, Type\CTypeHandle.vb"

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

    '     Module CTypeHandle
    ' 
    '         Function: [CBool], [CDbl], [CInt], [CLng], [CSng]
    '                   [CType], CastArray, CTypeInvoke, DefaultOf, EqualOfType
    '                   IsDirectCastCapable, (+2 Overloads) typefit
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.Parser

Namespace TypeInfo

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
        ''' 
        ''' </summary>
        ''' <param name="leftGeneric">左边的数组表达式的元素的类型</param>
        ''' <param name="right"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        Public Function CastArray(leftGeneric As TypeAbstract, right As ArraySymbol, symbols As SymbolTable) As Expression
            For i As Integer = 0 To right.Initialize.Length - 1
                ' cast each array element
                right(i) = [CType](leftGeneric, right(i), symbols)
            Next

            ' change the type of the expression in right side
            right.type = leftGeneric.MakeArrayType
            ' 因为类型变了，原来的api可能没有用了
            ' 在这里需要新导入api
            ' symbols.doArrayImports(leftGeneric)

            Return right
        End Function

        Public Function IsDirectCastCapable(left As TypeAlias, right As TypeAlias) As Boolean
            If left.ToString Like integerType Then
                Return right.ToString Like integerType
            ElseIf left.ToString Like floatType Then
                Return right.ToString Like floatType
            End If

            Return False
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

            If left.type = TypeAlias.array AndAlso TypeOf right Is ArraySymbol Then
                Return CastArray(left.generic(Scan0), right, symbols)
            ElseIf left.type = TypeAlias.any OrElse left.type = rightTypeInfer.type Then
                ' Conversion is not required when
                '
                ' 1. left accept any type
                ' 2. left is the same type as right
                Return right
            ElseIf right.IsLiteralNothing Then
                ' nothing 可以赋值给任意类型
                Return DefaultOf(left)
            ElseIf TypeOf right Is LiteralExpression AndAlso IsDirectCastCapable(left.type, rightTypeInfer.type) Then
                ' 如果是常数表达式的话，则直接修改常数表达式的类型
                Return New LiteralExpression With {
                    .type = left,
                    .value = DirectCast(right, LiteralExpression).value
                }
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
                Case TypeAlias.boolean
                    Return CTypeHandle.CBool(right, symbols)
                Case TypeAlias.intptr
                    ' 左边是一个内存指针，则右边如果是非基础类型的话
                    ' 可以直接赋值
                    If rightTypeInfer.type Like TypeExtensions.NumberOrders Then
                        ' 是基础类型
                        ' 则抛出错误
                        Throw New InvalidCastException
                    Else
                        ' 不需要转换，直接赋值
                        Return right
                    End If
                Case Else
                    Throw New InvalidCastException($"{rightTypeInfer} -> {left}")
            End Select
        End Function

        ''' <summary>
        ''' 好像只需要直接返回零就可以了...
        ''' </summary>
        ''' <param name="type"></param>
        ''' <returns></returns>
        Public Function DefaultOf(type As TypeAbstract) As Expression
            If type.isprimitive Then
                Return New LiteralExpression With {.value = 0, .type = type}
            ElseIf type = TypeAlias.boolean Then
                ' 逻辑值默认为False
                Return New LiteralExpression With {.value = 0, .type = type}
            Else
                Return New LiteralExpression With {.value = 0, .type = type}
            End If
        End Function

        Public Function [CBool](exp As Expression, symbols As SymbolTable) As Expression
            Dim type = exp.TypeInfer(symbols)
            Dim operator$ = Nothing

            Select Case type.type
                Case TypeAlias.i32
                    ' 直接转换
                    Return exp
                Case Else
                    Throw New NotImplementedException
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
                .refer = New ReferenceSymbol With {
                    .Type = SymbolType.Operator,
                    .Symbol = [operator]
                },
                .[operator] = True,
                .parameters = {exp}
            }
        End Function
    End Module
End Namespace
