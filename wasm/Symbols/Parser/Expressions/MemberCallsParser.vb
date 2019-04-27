Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax

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
            ElseIf Not symbols.IsAnyObject(objName) Then
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
        End Function
    End Module
End Namespace