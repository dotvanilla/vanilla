#Region "Microsoft.VisualBasic::198316464c83929c621b34cf54d78f16, SyntaxAnalysis\Expressions\ExpressionParser.vb"

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

    '     Module ExpressionParser
    ' 
    '         Function: [TryCast], ConstantExpression, CreateArray, ParenthesizedStack, ReferVariable
    '                   StringConstant, UnaryExpression, UnaryValue, ValueCType, ValueExpression
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.Blocks
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Module ExpressionParser

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
                Case GetType(PredefinedCastExpressionSyntax)
                    Return DirectCast(value, PredefinedCastExpressionSyntax).TryCast(symbols)
                Case GetType(PredefinedTypeSyntax)
                    Return New TypeSymbol With {
                        .type = New TypeAbstract(DirectCast(value, PredefinedTypeSyntax).PredefinedType)
                    }
                Case Else
                    Throw New NotImplementedException(value.GetType.FullName)
            End Select
        End Function

        ''' <summary>
        ''' CInt, CLng, CSng, etc
        ''' </summary>
        ''' <param name="cast"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function [TryCast](cast As PredefinedCastExpressionSyntax, symbols As SymbolTable) As Expression
            Dim value As Expression = cast.Expression.ValueExpression(symbols)
            Dim castTo$ = cast.Keyword.ValueText

            Select Case castTo
                Case "CInt"
                    Return CTypeHandle.CInt(value, symbols)
                Case "CDbl"
                    Return CTypeHandle.CDbl(value, symbols)
                Case "CLng"
                    Return CTypeHandle.CLng(value, symbols)
                Case "CSng"
                    Return CTypeHandle.CSng(value, symbols)
                Case "CBool"
                    Return CTypeHandle.CBool(value, symbols)
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        <Extension>
        Public Function CreateArray(newArray As CollectionInitializerSyntax, symbols As SymbolTable) As ArraySymbol
            Dim elements = newArray.Initializers _
                .Select(Function(value)
                            Return value.ValueExpression(symbols)
                        End Function) _
                .ToArray
            Dim elementType As TypeAbstract = elements _
                .Select(Function(e) e.TypeInfer(symbols)) _
                .TopMostFrequent(TypeEquality.Test)
            Dim array As New ArraySymbol With {
                .Initialize = elements,
                .type = elementType.MakeArrayType
            }

            ' 导入数组操作所需要的外部api
            ' Call symbols.doArrayImports(elementType)

            Return array
        End Function

        ''' <summary>
        ''' CType(..., ...)
        ''' </summary>
        ''' <param name="cast"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function ValueCType(cast As CTypeExpressionSyntax, symbols As SymbolTable) As Expression
            Dim value As Expression = cast.Expression.ValueExpression(symbols)
            Dim raw As RawType = cast.Type.GetType(symbols)
            Dim castToType As New TypeAbstract(raw, symbols)

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
                    .condition = right,
                    .[isNot] = True
                }
            Else
                Dim left = New LiteralExpression With {
                   .type = right.TypeInfer(symbols),
                   .value = 0
                }
                Dim opFunc As New ReferenceSymbol With {
                    .type = SymbolType.Operator,
                    .symbol = $"{left.type}.{TypeExtensions.wasmOpName(op)}"
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

        ''' <summary>
        ''' 目标对象<paramref name="name"/>可能是局部变量，全局变量，也可能是一个没有任何参数输入的函数调用表达式
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ReferVariable(name As IdentifierNameSyntax, symbols As SymbolTable) As Expression
            Dim var As String = name.objectName
            Dim ref As Expression
            Dim type As TypeAbstract

            If symbols.IsAnyObject(var) Then
                If symbols.IsLocal(var) Then
                    ref = New GetLocalVariable With {
                        .var = var
                    }
                Else
                    ref = symbols.FindModuleGlobal(Nothing, var).GetReference
                End If
            Else
                ' 是一个不需要输入任何参数的函数调用的表达式
                ref = symbols.InvokeFunction(var, Nothing)
            End If

            type = ref.TypeInfer(symbols)

            If type = TypeAlias.intptr Then
                Dim meta = symbols.FindByClassId(type.class_id)

                If meta.isStruct Then
                    ' 如果是值类型的结构体的话，则需要拷贝原来的数据
                    ' 然后传递新指针即可
                    Return type.Clone(ref, symbols)
                Else
                    Return ref
                End If
            Else
                Return ref
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
    End Module
End Namespace
