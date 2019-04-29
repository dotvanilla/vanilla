#Region "Microsoft.VisualBasic::b2d0c073fa33ea5eff9ce14d8269a605, Symbols\Parser\Expressions\ExpressionParser.vb"

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

    '     Module ExpressionParse
    ' 
    '         Function: (+2 Overloads) BinaryStack, ConstantExpression, (+2 Overloads) CreateArray, IsPredicate, ParenthesizedStack
    '                   ReferVariable, StringConstant, UnaryExpression, UnaryValue, ValueCType
    '                   ValueExpression
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Wasm.Compiler
Imports Wasm.Symbols.Blocks

Namespace Symbols.Parser

    Module ExpressionParse

        <Extension>
        Public Function ValueExpression(value As ExpressionSyntax, symbols As SymbolTable) As Expression
            Select Case value.GetType
                Case GetType(BinaryExpressionSyntax)
                    Return DirectCast(value, BinaryExpressionSyntax).BinaryStack(symbols)
                Case GetType(ParenthesizedExpressionSyntax)
                    Return DirectCast(value, ParenthesizedExpressionSyntax).ParenthesizedStack(symbols)
                Case GetType(LiteralExpressionSyntax)
                    Return DirectCast(value, LiteralExpressionSyntax).ConstantExpression(Nothing, symbols)
                Case GetType(IdentifierNameSyntax)
                    Return DirectCast(value, IdentifierNameSyntax).ReferVariable(symbols)
                Case GetType(InvocationExpressionSyntax)
                    Return DirectCast(value, InvocationExpressionSyntax).FunctionInvoke(symbols)
                Case GetType(UnaryExpressionSyntax)
                    Return DirectCast(value, UnaryExpressionSyntax).UnaryExpression(symbols)
                Case GetType(CTypeExpressionSyntax)
                    Return DirectCast(value, CTypeExpressionSyntax).ValueCType(symbols)
                Case GetType(MemberAccessExpressionSyntax)
                    Return DirectCast(value, MemberAccessExpressionSyntax).MemberExpression(symbols)
                Case GetType(InterpolatedStringExpressionSyntax)
                    Return DirectCast(value, InterpolatedStringExpressionSyntax).StringExpression(symbols)
                Case GetType(CollectionInitializerSyntax)
                    Return DirectCast(value, CollectionInitializerSyntax).CreateArray(symbols)
                Case GetType(ObjectCreationExpressionSyntax)
                    Return DirectCast(value, ObjectCreationExpressionSyntax).CreateObject(symbols)
                Case GetType(ArrayCreationExpressionSyntax)
                    Return DirectCast(value, ArrayCreationExpressionSyntax).CreateArray(symbols)
                Case Else
                    Throw New NotImplementedException(value.GetType.FullName)
            End Select
        End Function

        <Extension>
        Public Function CreateArray(newArray As ArrayCreationExpressionSyntax, symbols As SymbolTable) As Expression
            Dim type = AsTypeHandler.GetType(newArray.Type, symbols)
            Dim arrayType As TypeAbstract = New TypeAbstract(type).MakeArrayType

            If newArray.ArrayBounds Is Nothing Then
                Dim array As ArraySymbol = newArray.Initializer.CreateArray(symbols)
                array.type = arrayType
                Return array
            Else
                Dim bounds As Expression = newArray.ArrayBounds _
                    .Arguments _
                    .First _
                    .GetExpression _
                    .ValueExpression(symbols)

                Return New Array With {
                    .size = bounds,
                    .type = arrayType
                }
            End If
        End Function

        <Extension>
        Public Function CreateArray(newArray As CollectionInitializerSyntax, symbols As SymbolTable) As Expression
            Dim elements = newArray.Initializers _
                .Select(Function(value)
                            Return value.ValueExpression(symbols)
                        End Function) _
                .ToArray
            Dim array As New ArraySymbol With {
                .Initialize = elements
            }

            Return array
        End Function

        <Extension>
        Public Function ValueCType(cast As CTypeExpressionSyntax, symbols As SymbolTable) As Expression
            Dim value As Expression = cast.Expression.ValueExpression(symbols)
            Dim castToType As New TypeAbstract(cast.Type.GetType(symbols))

            Return CTypeHandle.CType(castToType, value, symbols)
        End Function

        ''' <summary>
        ''' 可能是常量，也可能是一个变量引用
        ''' </summary>
        ''' <param name="unary"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function UnaryExpression(unary As UnaryExpressionSyntax, symbols As SymbolTable) As Expression
            Dim op As String = unary.OperatorToken.ValueText
            Dim right = unary.Operand.ValueExpression(symbols)

            ' not 也是一个单目运算符
            If op = "Not" Then
                Return New BooleanSymbol With {
                    .Condition = right,
                    .[IsNot] = True
                }
            Else
                Dim left = New LiteralExpression With {
                   .type = right.TypeInfer(symbols),
                   .value = 0
                }
                Dim opFunc As New ReferenceSymbol With {
                    .IsOperator = True,
                    .Symbol = $"{left.type}.{TypeExtensions.wasmOpName(op)}"
                }

                Return New FuncInvoke With {
                    .parameters = {left, right},
                    .refer = opFunc,
                    .[operator] = True
                }
            End If
        End Function

        ''' <summary>
        ''' 常量表达式
        ''' </summary>
        ''' <param name="unary"></param>
        ''' <returns></returns>
        <Extension>
        Public Function UnaryValue(unary As UnaryExpressionSyntax) As String
            Dim op$ = unary.OperatorToken.ValueText
            Dim valueToken = DirectCast(unary.Operand, LiteralExpressionSyntax)
            Dim value$ = valueToken.Token.ValueText

            Return op & value
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ReferVariable(name As IdentifierNameSyntax, symbols As SymbolTable) As Expression
            Dim var As String = name.objectName

            If symbols.IsLocal(var) Then
                Return New GetLocalVariable With {
                    .var = var
                }
            Else
                Return New GetGlobalVariable With {
                    .var = var
                }
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="[const]"></param>
        ''' <param name="wasmType"></param>
        ''' <param name="memory">内存设备</param>
        ''' <returns></returns>
        <Extension>
        Public Function ConstantExpression([const] As LiteralExpressionSyntax, wasmType As TypeAbstract, memory As SymbolTable) As Expression
            Dim value As Object = [const].Token.Value
            Dim type As TypeAbstract = wasmType

            If value Is Nothing Then
                ' 是空值常量，则直接返回整形数0表示空指针
                value = 0
                type = New TypeAbstract(TypeAlias.any)
            ElseIf type Is Nothing Then
                type = New TypeAbstract(value.GetType)
            End If

            Select Case type.type
                Case TypeAlias.string
                    Return memory.StringConstant(value)
                Case TypeAlias.boolean
                    ' 在常量下，逻辑值是i32 1 或者 0
                    If value = True Then
                        value = 1
                    Else
                        value = 0
                    End If
                Case Else
                    ' do nothing
            End Select

            Return New LiteralExpression With {
                .type = type,
                .value = value
            }
        End Function

        <Extension>
        Public Function StringConstant(memory As SymbolTable, str As String) As LiteralExpression
            Dim intPtr As Object = str

            Call memory.stringValue(intPtr)

            Return New LiteralExpression With {
               .type = New TypeAbstract("string"),
               .value = intPtr
            }
        End Function

        <Extension>
        Public Function ParenthesizedStack(parenthesized As ParenthesizedExpressionSyntax, symbols As SymbolTable) As Parenthesized
            Return New Parenthesized With {
                .Internal = parenthesized.Expression.ValueExpression(symbols)
            }
        End Function

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
                    .IsOperator = True
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
                        .IsOperator = True
                    }
                }
            Else
                ' a is b
                Throw New NotImplementedException
            End If
        End Function
    End Module
End Namespace
