#Region "Microsoft.VisualBasic::8e68958bb7eddcf082f44b67b18803ff, Symbols\Parser\Expressions\MemberCallsParser.vb"

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

    '     Module MemberCallsParser
    ' 
    '         Function: ExpressionMember, InvokeMemberFunc, MemberExpression, SimpleMember
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Wasm.Compiler

Namespace Symbols.Parser

    Module MemberCallsParser

        ''' <summary>
        ''' 目标直接是一个名字
        ''' </summary>
        ''' <param name="ref"></param>
        ''' <param name="symbols"></param>
        ''' <param name="memberName$"></param>
        ''' <returns></returns>
        Public Function SimpleMember(ref As SimpleNameSyntax, symbols As SymbolTable, memberName$) As Expression
            Dim [const] As EnumSymbol
            Dim objName$ = ref.objectName

            If symbols.HaveEnumType(objName) Then
                [const] = symbols.GetEnumType(objName)

                Return New LiteralExpression With {
                    .type = New TypeAbstract([const].type),
                    .value = [const].Members(memberName)
                }
            ElseIf (Not symbols.IsAnyObject(objName)) AndAlso (Not objName Like symbols.ModuleNames) Then
                Dim func = symbols.GetFunctionSymbol(Nothing, objName)
                Dim funcValue As Expression = New FuncInvoke(func) With {.parameters = {}}

                ' 可能是一个拓展函数或者函数返回值的成员调用
                If func.TypeInfer(symbols).type = TypeAlias.string Then
                    Dim member = symbols.GetFunctionSymbol("string", memberName)

                    Return New FuncInvoke(member) With {
                        .parameters = {funcValue}
                    }
                End If

                Throw New NotImplementedException
            Else
                ' 数组长度，属性，无参数的方法调用等
                Dim func = symbols.GetFunctionSymbol(objName, memberName)

                If Not func Is Nothing Then
                    Return func.InvokeMemberFunc(objName, symbols)
                Else
                    Dim obj = symbols.GetObjectReference(objName)

                    If obj Is Nothing AndAlso objName Like symbols.ModuleNames Then
                        ' 在这里objName是一个模块名称
                        ' 所以objName会作为对象查找的context
                        Return symbols.FindModuleGlobal(objName, memberName).GetReference
                    Else
                        ' 是引用的模块成员
                        If TypeOf obj Is GetGlobalVariable Then
                            Return obj
                        End If
                    End If
                End If

                Throw New NotImplementedException
            End If
        End Function

        ''' <summary>
        ''' 无参数的成员函数或者模块函数调用语法
        ''' 
        ''' 虽然是无参数，但是可能会存在可选默认参数，在这里需要做下额外的处理
        ''' </summary>
        ''' <param name="func"></param>
        ''' <param name="objName"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Private Function InvokeMemberFunc(func As FuncSignature, objName$, symbols As SymbolTable) As Expression
            ' 可能是模块的成员函数
            ' 也可能是拓展函数
            If symbols.IsAnyObject(objName) Then
                Return New FuncInvoke(func) With {
                   .parameters = {symbols.GetObjectReference(objName)}
                }
            ElseIf objName Like symbols.ModuleNames Then
                ' 是一个无参数的模块成员函数调用
                Return New FuncInvoke(func) With {
                   .parameters = {}
                }
            Else
                Throw New NotImplementedException
            End If
        End Function

        ''' <summary>
        ''' 成员属性，字段或者没有参数的函数的调用
        ''' </summary>
        ''' <param name="ref"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function MemberExpression(ref As MemberAccessExpressionSyntax, symbols As SymbolTable) As Expression
            Dim memberName = ref.Name.objectName

            If TypeOf ref.Expression Is SimpleNameSyntax Then
                Return SimpleMember(ref.Expression, symbols, memberName)
            Else
                Return ref.Expression.ValueExpression(symbols).ExpressionMember(memberName, symbols)
            End If
        End Function

        ''' <summary>
        ''' 目标是一个值表达式
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Extension>
        Public Function ExpressionMember(obj As Expression, memberName$, symbols As SymbolTable) As Expression
            Dim type As TypeAbstract = obj.TypeInfer(symbols)
            Dim func = symbols.GetFunctionSymbol(type.type.Description, memberName)

            If Not func Is Nothing AndAlso func.parameters.Length = 1 Then
                ' func (obj)
                Return New FuncInvoke(func) With {
                    .parameters = {obj}
                }
            Else
                ' object field

            End If

            'If symbols.GetObjectSymbol(objName).IsArray AndAlso memberName = "Length" Then
            '    ' 可能是获取数组长度
            '    Return New FuncInvoke With {
            '        .refer = JavaScriptImports.ArrayLength.Name,
            '        .parameters = {
            '            New GetLocalVariable With {.var = objName}
            '        }
            '    }
            'ElseIf symbols.GetObjectSymbol(objName).type Like TypeExtensions.stringType Then
            '    ' 是字符串的一些对象方法
            '    Dim api As ImportSymbol = JavaScriptImports.GetStringMethod(memberName)

            '    Call symbols.addRequired(api)

            '    Return New FuncInvoke With {
            '        .refer = api.Name,
            '        .parameters = {
            '            New GetLocalVariable With {.var = objName}
            '        }
            '    }
            'Else
            '    Throw New NotImplementedException(ref.ToString)
            'End If

            Throw New NotImplementedException
        End Function
    End Module
End Namespace
