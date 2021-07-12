﻿Imports System.Linq.Expressions
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module Expressionparser

        <Extension>
        Public Function ParseValue(expression As ExpressionSyntax, context As Environment) As WATSyntax
            Select Case expression.GetType
                Case GetType(InvocationExpressionSyntax)
                    Return DirectCast(expression, InvocationExpressionSyntax).ParseFunctionInvoke(context)
                Case GetType(MemberAccessExpressionSyntax)
                    Return DirectCast(expression, MemberAccessExpressionSyntax).ParseReference(context)

                Case Else
                    Throw New NotImplementedException(expression.GetType.FullName)
            End Select
        End Function

        <Extension>
        Private Function ParseFunctionInvoke(calls As InvocationExpressionSyntax, context As Environment) As WATSyntax
            Dim par As New Dictionary(Of String, WATSyntax)
            Dim target As WATSyntax = calls.Expression.ParseValue(context)

            For Each arg As ArgumentSyntax In calls.ArgumentList.Arguments

            Next

            Return New FunctionInvoke() With {
                .Arguments = par,
                .Func = target
            }
        End Function
    End Module
End Namespace