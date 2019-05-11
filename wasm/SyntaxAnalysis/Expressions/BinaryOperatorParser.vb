#Region "Microsoft.VisualBasic::ce528d688d1a075c44ecbe4c512ab4fc, SyntaxAnalysis\Expressions\BinaryOperatorParser.vb"

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
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols
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

        <Extension>
        Private Function highOrderTransfer(symbols As SymbolTable, ByRef left As Expression, ByRef right As Expression) As TypeAbstract
            Dim type As TypeAbstract

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

            Return type
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
            ElseIf op Like TypeOperator.comparisonOp Then
                Return symbols.DoComparison(left, right, op)
            ElseIf op Like TypeOperator.LogicalOperators Then
                Return symbols.DoLogical(left, right, op)
            Else
                type = symbols.highOrderTransfer(left, right)
            End If

            Dim func As [Variant](Of String, ImportSymbol)

            If TypeOperator.IsValidDirectMapOperator(op) Then
                func = TypeOperator.GetDirectMapOperator(op)

                If type = TypeAlias.boolean Then
                    func = $"i32.{func}"
                Else
                    func = $"{type}.{func}"
                End If
            ElseIf op = "^" Then
                func = JavaScriptImports.Math.Pow
            Else
                func = TypeOperator.Compares(type.raw, op)
            End If

            ' 需要根据类型来决定操作符函数的类型来源
            If func Like GetType(ImportSymbol) Then
                Return func _
                    .TryCast(Of ImportSymbol) _
                    .FunctionInvoke(
                        CTypeHandle.CDbl(left, symbols),
                        CTypeHandle.CDbl(right, symbols)
                    )
            Else
                Return New FuncInvoke With {
                    .parameters = {left, right},
                    .refer = New ReferenceSymbol With {
                        .symbol = func,
                        .type = SymbolType.Operator
                    },
                    .[operator] = True
                }
            End If
        End Function

        <Extension>
        Public Function DoLogical(symbols As SymbolTable, left As Expression, right As Expression, op$) As Expression
            left = CTypeHandle.CBool(left, symbols)
            right = CTypeHandle.CBool(right, symbols)

            Return New FuncInvoke With {
                .[operator] = True,
                .parameters = {left, right},
                .refer = New ReferenceSymbol With {
                    .symbol = TypeOperator.BooleanLogical(op),
                    .type = SymbolType.LogicalOperator
                }
            }
        End Function

        <Extension>
        Public Function DoComparison(symbols As SymbolTable, left As Expression, right As Expression, op$) As Expression
            Dim type As TypeAbstract = symbols.highOrderTransfer(left, right)

            Return New FuncInvoke With {
                .[operator] = True,
                .parameters = {left, right},
                .refer = New ReferenceSymbol With {
                    .symbol = TypeOperator.Compares(type.typefit, op),
                    .type = SymbolType.LogicalOperator
                }
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
                        .symbol = TypeOperator.Compares("i32", "="),
                        .type = SymbolType.LogicalOperator
                    }
                }
            Else
                ' a is b
                Throw New NotImplementedException
            End If
        End Function
    End Module
End Namespace
