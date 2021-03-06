﻿#Region "Microsoft.VisualBasic::68d1b4b683060512f08bc3a9f12da869, Symbols\Blocks\BooleanSymbol.vb"

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

    '     Class BooleanSymbol
    ' 
    '         Properties: [isNot], condition
    ' 
    '         Function: BinaryCompares, GetSymbolReference, ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Wasm.Compiler
Imports Wasm.SyntaxAnalysis
Imports Wasm.TypeInfo

Namespace Symbols.Blocks

    ''' <summary>
    ''' 构建生成逻辑表达式的模型
    ''' </summary>
    Public Class BooleanSymbol : Inherits Expression

        Public Property condition As Expression
        Public Property [isNot] As Boolean

        Sub New()
        End Sub

        Sub New(i32 As Expression)
            condition = i32
        End Sub

        ''' <summary>
        ''' 返回的值都是逻辑值
        ''' </summary>
        ''' <param name="symbolTable"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.boolean)
        End Function

        Public Overrides Function ToSExpression() As String
            Dim test$ = condition.ToSExpression

            If [isNot] Then
                Return $"(i32.eqz {test})"
            Else
                Return test
            End If
        End Function

        Public Shared Narrowing Operator CType(test As BooleanSymbol) As FuncInvoke
            If test.isNot Then
                Return New FuncInvoke With {
                    .[operator] = True,
                    .refer = New ReferenceSymbol With {
                        .symbol = "i32.eqz",
                        .type = SymbolType.Operator
                    },
                    .parameters = {test.condition}
                }
            Else
                Return test.condition
            End If
        End Operator

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Function BinaryCompares(left As Expression, right As Expression, op$, symbols As SymbolTable) As BooleanSymbol
            Return New BooleanSymbol With {
                .condition = BinaryOperatorParser.BinaryStack(left, right, op, symbols)
            }
        End Function

        ''' <summary>
        ''' 逻辑值操作主要是数学关系操作符判断
        ''' </summary>
        ''' <param name="op"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Widening Operator CType(op As FuncInvoke) As BooleanSymbol
            Return New BooleanSymbol With {.condition = op}
        End Operator

        Public Overrides Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Return condition.GetSymbolReference
        End Function
    End Class
End Namespace
