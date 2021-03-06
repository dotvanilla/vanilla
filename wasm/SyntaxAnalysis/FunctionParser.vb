﻿#Region "Microsoft.VisualBasic::33eab20bb7ba038b64ad05ea4f56bc14, SyntaxAnalysis\FunctionParser.vb"

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

    '     Module FunctionParser
    ' 
    '         Function: FunctionBody, (+2 Overloads) FuncVariable, ParseFunction, ParseParameter, (+3 Overloads) ParseParameters
    '                   runParser
    ' 
    '         Sub: addImplicitReturns
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Module FunctionParser

        <Extension>
        Public Function FuncVariable(method As MethodBlockSyntax, symbols As SymbolTable) As NamedValue(Of TypeAbstract)
            Dim name As String = method.SubOrFunctionStatement.Identifier.objectName
            Dim returns As RawType

            If method.SubOrFunctionStatement.SubOrFunctionKeyword.Text = "Sub" Then
                returns = GetType(System.Void)
            Else
                Dim funcAs = method.SubOrFunctionStatement.AsClause

                If funcAs Is Nothing Then
                    ' 定义为function，但是忘记申明函数的返回类型了
                    ' 默认返回i32数据类型？
                    returns = GetType(Integer)
                Else
                    returns = GetAsType(funcAs, symbols)
                End If

            End If

            Return New NamedValue(Of TypeAbstract) With {
                .Name = name,
                .Value = New TypeAbstract(returns, symbols)
            }
        End Function

        <Extension>
        Public Function FuncVariable(api As DeclareStatementSyntax, symbols As SymbolTable) As NamedValue(Of TypeAbstract)
            Dim name As String = api.Identifier.objectName
            Dim returns As RawType = GetAsType(api.AsClause, symbols)

            Return New NamedValue(Of TypeAbstract) With {
                .Name = name,
                .Value = New TypeAbstract(returns, symbols)
            }
        End Function

        ''' <summary>
        ''' 解析出函数的参数列表
        ''' </summary>
        ''' <param name="api"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ParseParameters(api As DeclarationStatementSyntax, symbols As SymbolTable) As NamedValue(Of TypeAbstract)()
            Return DirectCast(api, MethodBaseSyntax).ParseParameters(symbols:=symbols).ToArray
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function ParseParameters(method As MethodBaseSyntax, symbols As SymbolTable) As IEnumerable(Of NamedValue(Of TypeAbstract))
            Return method.ParameterList _
                .Parameters _
                .Select(Function(p) ParseParameter(p, symbols))
        End Function

        ''' <summary>
        ''' 解析出函数的参数列表
        ''' </summary>
        ''' <param name="method"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function ParseParameters(method As MethodBlockSyntax, symbols As SymbolTable) As NamedValue(Of TypeAbstract)()
            Return method.BlockStatement.ParseParameters(symbols).ToArray
        End Function

        ''' <summary>
        ''' 解析一个执行函数
        ''' </summary>
        ''' <param name="method"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function ParseFunction(method As MethodBlockSyntax, moduleName$, symbols As SymbolTable) As FuncSymbol
            Dim parameters = method.ParseParameters(symbols)
            Dim body As StatementSyntax() = method.Statements.ToArray
            Dim funcVar = method.FuncVariable(symbols)

            ' using for return value ctype operation
            symbols.context.funcSymbol = funcVar.Name
            ' using for distinguish function and global variables that 
            ' between different VisualBasic modules
            symbols.context.moduleLabel = moduleName

            ' the function parameter treated as local variable
            ' in function body
            For Each arg As NamedValue(Of TypeAbstract) In parameters
                Call symbols.AddLocal(arg)
            Next

            Dim paramIndex As Index(Of String) = parameters.Keys
            Dim funcBody = body.FunctionBody(paramIndex, symbols)
            Dim func As New FuncSymbol(funcVar) With {
                .parameters = parameters,
                .body = funcBody.body,
                .[module] = moduleName,
                .locals = funcBody.locals
            }

            If func.result <> "void" Then
                If func.body.Length > 0 Then
                    If Not TypeOf func.body.Last Is ReturnValue Then
                        Call func.addImplicitReturns
                    End If
                Else
                    ' VB之中的空函数这是自动返回零
                    Call func.addImplicitReturns
                End If
            End If

            Return func
        End Function

        <Extension>
        Friend Function FunctionBody(bodyStatements As StatementSyntax(),
                                     paramIndex As Index(Of String),
                                     symbols As SymbolTable) As (locals As DeclareLocal(), body As Expression())

            Dim runParser As Func(Of StatementSyntax, IEnumerable(Of Expression)) = symbols.runParser
            Dim funcBody As Expression() = bodyStatements _
                .ExceptType(Of EndBlockStatementSyntax) _
                .Select(Function(s)
                            Return runParser(s)
                        End Function) _
                .IteratesALL _
                .ToArray
            Dim locals As DeclareLocal() = symbols _
                .GetAllLocals _
                .Where(Function(v)
                           ' removes function parameters from declare locals
                           Return Not v.name Like paramIndex
                       End Function) _
                .ToArray

            Return (locals, funcBody)
        End Function

        <Extension>
        Private Sub addImplicitReturns(func As FuncSymbol)
            Dim implicitReturn As New ReturnValue With {
                .Internal = New LiteralExpression With {
                    .type = func.result,
                    .value = 0
                }
            }

            ' 自动添加一个返回语句，如果最后一行没有返回表达式的话？
            Call func.body.Add(implicitReturn)
        End Sub

        <Extension>
        Private Function runParser(symbols As SymbolTable) As Func(Of StatementSyntax, IEnumerable(Of Expression))
            Return Function(statement As StatementSyntax)
                       Return statement.ParseStatement(symbols).AutoDropValueStack(symbols)
                   End Function
        End Function

        Public Function ParseParameter(parameter As ParameterSyntax, symbols As SymbolTable) As NamedValue(Of TypeAbstract)
            Dim name = parameter.Identifier.Identifier.objectName
            Dim type As RawType
            Dim default$ = Nothing

            If parameter.AsClause Is Nothing Then
                type = Scripting.GetType(Patterns.TypeCharName(name.Last))
                name = name.Substring(0, name.Length - 1)
            Else
                type = GetAsType(parameter.AsClause, symbols)
            End If

            If Not parameter.Default Is Nothing Then
                Select Case parameter.Default.Value.GetType
                    Case GetType(LiteralExpressionSyntax)
                        With DirectCast(parameter.Default.Value, LiteralExpressionSyntax)
                            [default] = .Token.Value
                        End With
                    Case GetType(UnaryExpressionSyntax)
                        With DirectCast(parameter.Default.Value, UnaryExpressionSyntax)
                            [default] = .UnaryValue
                        End With
                    Case Else
                        Throw New NotImplementedException
                End Select
            End If

            Return New NamedValue(Of TypeAbstract) With {
                .Name = name,
                .Value = New TypeAbstract(type, symbols),
                .Description = [default]
            }
        End Function
    End Module
End Namespace
