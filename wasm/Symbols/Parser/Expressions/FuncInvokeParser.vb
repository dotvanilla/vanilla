#Region "Microsoft.VisualBasic::cd21da1a7861379e2db62617bcdb4f97, Symbols\Parser\Expressions\FuncInvokeParser.vb"

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

'     Module FuncInvokeParser
' 
'         Function: Argument, ArgumentSequence, fillParameters, FunctionInvoke, InvokeFunction
'                   ObjectInvoke, OptionalDefault
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler

Namespace Symbols.Parser

    Module FuncInvokeParser

        <Extension>
        Public Function Argument(arg As ArgumentSyntax, symbols As SymbolTable, param As NamedValue(Of TypeAbstract)) As Expression
            Dim value As Expression = arg _
                .GetExpression _
                .ValueExpression(symbols)
            Dim left As TypeAbstract = param.Value

            Return CTypeHandle.CType(left, value, symbols)
        End Function

        <Extension>
        Public Function FunctionInvoke(invoke As InvocationExpressionSyntax, symbols As SymbolTable) As FuncInvoke
            Dim reference = invoke.Expression
            Dim funcName$

            ' 得到被调用的目标函数的名称符号
            Select Case reference.GetType
                Case GetType(SimpleNameSyntax)
                    funcName = DirectCast(reference, SimpleNameSyntax).objectName
                Case GetType(IdentifierNameSyntax)
                    funcName = DirectCast(reference, IdentifierNameSyntax).objectName

                    If symbols.IsAnyObject(funcName) Then
                        Dim target = symbols.GetObjectSymbol(funcName)

                        If target.type = GetType(DictionaryBase).FullName Then
                            Return New FuncInvoke(JavaScriptImports.Dictionary.GetValue) With {
                                .parameters = {
                                    New GetLocalVariable(target.name),
                                    invoke.ArgumentList _
                                        .Arguments _
                                        .First _
                                        .Argument(symbols, "key".param("i32"))
                                }
                            }
                        Else
                            ' do nothing
                        End If
                    End If
                Case GetType(MemberAccessExpressionSyntax)
                    Dim acc = DirectCast(reference, MemberAccessExpressionSyntax)
                    Dim isKeyAccess As Boolean = acc.OperatorToken.Text = "!"
                    ' 模块或者变量名称
                    Dim target = acc.Expression
                    ' 目标函数名称
                    funcName$ = acc.Name.objectName

                    Return target.ObjectInvoke(
                        funcName,
                        invoke.ArgumentList,
                        symbols,
                        isKeyAccess
                    )
                Case GetType(InvocationExpressionSyntax)
                    Dim acc = DirectCast(reference, InvocationExpressionSyntax).FunctionInvoke(symbols)

                    If acc.TypeInfer(symbols) = "i32" Then
                        ' 返回的是一个对象引用
                        ' 在这里假设是一个数组
                        Return New FuncInvoke(JavaScriptImports.Array.GetArrayElement) With {
                            .parameters = {
                                acc,
                                invoke.ArgumentList _
                                    .Arguments _
                                    .First _
                                    .Argument(symbols, "index".param("i32"))
                            }
                        }
                    Else
                        Throw New NotImplementedException
                    End If
                Case Else
                    Throw New NotImplementedException(reference.GetType.FullName)
            End Select

            Return symbols.InvokeFunction(funcName, invoke.ArgumentList)
        End Function

        ''' <summary>
        ''' 需要判断一下target的类型
        ''' 如果是本地变量，全局变量，常量，则可能是对象实例方法或者拓展方法
        ''' </summary>
        ''' <param name="target"></param>
        ''' <param name="funcName"></param>
        ''' <param name="symbols"></param>
        ''' <param name="isKeyAccess">如果这个参数为真的时候，表示为字典值得读取操作</param>
        ''' <returns></returns>
        <Extension>
        Public Function ObjectInvoke(target As ExpressionSyntax,
                                     funcName$,
                                     argumentList As ArgumentListSyntax,
                                     symbols As SymbolTable,
                                     isKeyAccess As Boolean) As Expression

            Dim argumentFirst As Expression = Nothing
            Dim funcDeclare As FuncSignature
            Dim leftArguments As NamedValue(Of TypeAbstract)() = Nothing

            If TypeOf target Is LiteralExpressionSyntax Then
                funcDeclare = symbols.GetFunctionSymbol(Nothing, funcName)
                argumentFirst = target.ValueExpression(symbols)
                leftArguments = funcDeclare.parameters.Skip(1).ToArray
            ElseIf TypeOf target Is IdentifierNameSyntax Then
                ' 模块静态引用或者对象实例引用
                Dim name$ = DirectCast(target, IdentifierNameSyntax).objectName

                If isKeyAccess Then
                    ' 当为字典键引用的时候，函数对象肯定是查找不到的
                    Dim keyAccess As Expression = New FuncInvoke(JavaScriptImports.Dictionary.GetValue) With {
                        .parameters = {
                            target.ValueExpression(symbols),
                            symbols.StringConstant(funcName)
                        }
                    }

                    ' 因为当前的表达式被判断是一个函数调用
                    ' 所以字典的结果值应该是一个数组
                    keyAccess = New FuncInvoke(JavaScriptImports.Array.GetArrayElement) With {
                        .parameters = {
                            keyAccess,
                            argumentList.Arguments _
                                .First _
                                .Argument(symbols, "i".param("i32"))
                        }
                    }

                    Return keyAccess
                Else
                    funcDeclare = symbols.GetFunctionSymbol(name, funcName)

                    If symbols.IsAnyObject(name) Then
                        ' 是对对象实例的方法引用
                        argumentFirst = target.ValueExpression(symbols)
                        leftArguments = funcDeclare.parameters.Skip(1).ToArray
                    ElseIf name Like symbols.ModuleNames Then
                        ' 是对静态模块的方法引用
                        argumentFirst = Nothing
                        leftArguments = funcDeclare.parameters
                    ElseIf symbols.IsModuleFunction(name) Then
                        ' 方法生成值，然后再调用值对象的成员方法的
                        argumentFirst = New FuncInvoke(name) With {.parameters = {}}
                        leftArguments = funcDeclare.parameters.Skip(1).ToArray
                    End If
                End If
            Else
                Throw New NotImplementedException
            End If

            Dim arguments As Expression() = argumentList.fillParameters(leftArguments, symbols)

            If Not argumentFirst Is Nothing Then
                arguments = argumentFirst _
                    .Join(arguments) _
                    .ToArray
            End If

            Return New FuncInvoke(funcDeclare) With {
                .parameters = arguments
            }
        End Function

        <Extension>
        Private Function fillParameters(argumentList As ArgumentListSyntax, funcDeclare As NamedValue(Of TypeAbstract)(), symbols As SymbolTable) As Expression()
            Dim arg As NamedValue(Of TypeAbstract)
            Dim input As ArgumentSyntax = Nothing
            Dim arguments As New List(Of Expression)
            Dim invokeInputs As ArgumentSyntax()

            If argumentList Is Nothing Then
                invokeInputs = {}
            Else
                invokeInputs = argumentList _
                    .ArgumentSequence(funcDeclare) _
                    .ToArray
            End If

            For i As Integer = 0 To funcDeclare.Length - 1
                arg = funcDeclare(i)
                input = invokeInputs.ElementAtOrNull(i)

                If input Is Nothing Then
                    arguments += symbols.OptionalDefault(arg)
                Else
                    arguments += input.Argument(symbols, arg)
                End If
            Next

            Return arguments
        End Function

        ''' <summary>
        ''' 所有的可选参数的默认值都是一个常量
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="arg"></param>
        ''' <returns></returns>
        <Extension>
        Public Function OptionalDefault(symbols As SymbolTable, arg As NamedValue(Of TypeAbstract)) As Expression
            If arg.Value = TypeAlias.string Then
                Return symbols.StringConstant(arg.Description)
            ElseIf arg.Value = TypeAlias.boolean Then
                ' default value of boolean in VisualBasic.NET is True/False
                ' should translate to i32 1 or 0 in webassembly
                Return New LiteralExpression With {
                    .type = arg.Value,
                    .value = If(arg.Description.ParseBoolean, 1, 0)
                }
            Else
                Return New LiteralExpression With {
                    .type = arg.Value,
                    .value = arg.Description
                }
            End If
        End Function

        <Extension>
        Public Function InvokeFunction(symbols As SymbolTable, funcName$, argumentList As ArgumentListSyntax) As Expression
            Dim funcDeclare = symbols.GetFunctionSymbol(Nothing, funcName)

            If JavaScriptImports.Array.IsArrayOperation(funcDeclare) Then
                ' 是一个数组元素的读取操作
                Dim array = New GetLocalVariable With {.var = funcName}
                Dim index As Expression = argumentList _
                    .Arguments _
                    .First _
                    .Argument(symbols, funcDeclare.parameters.Last)

                Return New FuncInvoke With {
                    .refer = funcDeclare.Name,
                    .parameters = {array, index}
                }
            Else
                Dim arguments = argumentList.fillParameters(funcDeclare.parameters, symbols)

                Return New FuncInvoke(funcName) With {
                    .parameters = arguments
                }
            End If
        End Function

        <Extension>
        Private Iterator Function ArgumentSequence(arguments As ArgumentListSyntax, define As NamedValue(Of TypeAbstract)()) As IEnumerable(Of ArgumentSyntax)
            Dim input = arguments.Arguments.ToArray
            Dim a As ArgumentSyntax
            Dim check As NamedValue(Of TypeAbstract)

            For i As Integer = 0 To define.Length - 1
                a = input.ElementAtOrNull(i)
                check = define(i)

                If a Is Nothing Then
                    ' 可能是一个a:=...，并且出现在前面或者后面
                    a = input _
                        .FirstOrDefault(Function(arg)
                                            Return arg.IsNamed AndAlso DirectCast(arg, SimpleArgumentSyntax) _
                                                .NameColonEquals _
                                                .Name _
                                                .objectName _
                                                .TextEquals(check.Name)
                                        End Function)
                    Yield a
                Else
                    If DirectCast(a, SimpleArgumentSyntax).NameColonEquals Is Nothing Then
                        Yield a
                    Else
                        a = input _
                            .FirstOrDefault(Function(arg)
                                                Return arg.IsNamed AndAlso DirectCast(arg, SimpleArgumentSyntax) _
                                                    .NameColonEquals _
                                                    .Name _
                                                    .objectName _
                                                    .TextEquals(check.Name)
                                            End Function)
                        Yield a
                    End If
                End If
            Next
        End Function
    End Module
End Namespace
