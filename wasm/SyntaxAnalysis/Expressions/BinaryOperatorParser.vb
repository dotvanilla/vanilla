#Region "Microsoft.VisualBasic::f3fddfcf827b7176d414f844c5001493, Symbols\Parser\Expressions\BinaryOperatorParser.vb"

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

    '     Module BinaryOperatorParser
    ' 
    '         Function: (+2 Overloads) BinaryStack, IsPredicate
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Module BinaryOperatorParser

        ''' <summary>
        ''' NOTE: div between two integer will convert to double div automatic. 
        ''' </summary>
        ''' <param name="expression"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function BinaryStack(expression As BinaryExpressionSyntax, symbols As SymbolTable) As Expression
            Dim left = expression.Left.ValueExpression(symbols)
            Dim right = expression.Right.ValueExpression(symbols)
            Dim op$ = expression.OperatorToken.ValueText

            Return BinaryStack(left, right, op, symbols)
        End Function

        ''' <summary>
        ''' NOTE: div between two integer will convert to double div automatic. 
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        Public Function BinaryStack(left As Expression, right As Expression, op$, symbols As SymbolTable) As Expression
            Dim type As TypeAbstract

            If op = "/" Then
                ' require type conversion if left and right is integer
                ' 对于除法，必须要首先转换为浮点型才能够完成运算
                left = CTypeHandle.CDbl(left, symbols)
                right = CTypeHandle.CDbl(right, symbols)
                type = New TypeAbstract("f64")
            ElseIf op = "&" Then
                Return symbols.StringAppend(left, right)
            ElseIf op = "Is" Then
                Return symbols.IsPredicate(left, right)
            Else
                ' 其他的运算符则需要两边的类型保持一致
                ' 往高位转换
                ' i32 -> f32 -> i64 -> f64
                Dim lt = left.TypeInfer(symbols)
                Dim rt = right.TypeInfer(symbols)
                Dim li = TypeExtensions.NumberOrders(lt.type)
                Dim ri = TypeExtensions.NumberOrders(rt.type)

                If li > ri Then
                    type = lt
                Else
                    type = rt
                End If

                left = CTypeHandle.CType(type, left, symbols)
                right = CTypeHandle.CType(type, right, symbols)
            End If

            Dim funcOpName$

            If TypeExtensions.wasmOpName.ContainsKey(op) Then
                funcOpName = TypeExtensions.wasmOpName(op)

                If type = TypeAlias.boolean Then
                    funcOpName = $"i32.{funcOpName}"
                Else
                    funcOpName = $"{type}.{funcOpName}"
                End If
            Else
                funcOpName = TypeExtensions.Compares(type.raw, op)
            End If

            ' 需要根据类型来决定操作符函数的类型来源
            Return New FuncInvoke With {
                .parameters = {left, right},
                .refer = New ReferenceSymbol With {
                    .Symbol = funcOpName,
                    .type = SymbolType.Operator
                },
                .[operator] = True
            }
        End Function

        <Extension>
        Public Function IsPredicate(symbols As SymbolTable, left As Expression, right As Expression) As Expression
            If left.IsLiteralNothing OrElse right.IsLiteralNothing Then
                ' xxx is nothing / nothing is xxx
                ' 因为nothing总是i32类型，所以在这里应该生成的是是否等于i32 0的判断
                Return New FuncInvoke With {
                    .[operator] = True,
                    .parameters = {left, right},
                    .refer = New ReferenceSymbol With {
                        .Symbol = TypeExtensions.Compares("i32", "="),
                        .Type = SymbolType.Operator
                    }
                }
            Else
                ' a is b
                Throw New NotImplementedException
            End If
        End Function
    End Module
End Namespace
