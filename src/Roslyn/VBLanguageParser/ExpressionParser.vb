Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module Expressionparser

        <Extension>
        Public Function GetSymbol(name As IdentifierNameSyntax) As SymbolReference
            Return New SymbolReference With {.Name = name.Identifier.Text}
        End Function

        <Extension>
        Public Function ParseValue(expression As ExpressionSyntax, context As Environment) As WATSyntax
            Select Case expression.GetType
                Case GetType(InvocationExpressionSyntax)
                    Return DirectCast(expression, InvocationExpressionSyntax).ParseFunctionInvoke(context)
                Case GetType(MemberAccessExpressionSyntax)
                    Return DirectCast(expression, MemberAccessExpressionSyntax).ParseReference(context)
                Case GetType(LiteralExpressionSyntax)
                    Return DirectCast(expression, LiteralExpressionSyntax).GetLiteralvalue(context)
                Case GetType(IdentifierNameSyntax)
                    Return DirectCast(expression, IdentifierNameSyntax).GetSymbol
                Case GetType(BinaryExpressionSyntax)
                    Return DirectCast(expression, BinaryExpressionSyntax).GetBinary(context)

                Case Else
                    Throw New NotImplementedException(expression.GetType.FullName)
            End Select
        End Function

        <Extension>
        Private Function GetBinary(bin As BinaryExpressionSyntax, context As Environment) As WATSyntax
            Dim left As WATSyntax = bin.Left.ParseValue(context)
            Dim right As WATSyntax = bin.Right.ParseValue(context)
            Dim opName As String = bin.OperatorToken.ValueText

            Return BinaryStack(left, right, opName, context)
        End Function

        Public ReadOnly Property LogicalOperators As Index(Of String) = {"And", "Or", "AndAlso", "OrElse"}

        ''' <summary>
        ''' NOTE: div between two integer will convert to double div automatic. 
        ''' </summary>
        ''' <returns></returns>
        Public Function BinaryStack(left As WATSyntax, right As WATSyntax, op$, context As Environment) As WATSyntax
            If op = "/" Then
                ' require type conversion if left and right is integer
                ' 对于除法，必须要首先转换为浮点型才能够完成运算
                left = CTypeHandle.CDbl(left, context)
                right = CTypeHandle.CDbl(right, context)

                Return New BinaryOperator With {
                    .left = left,
                    .[operator] = op,
                    .right = right
                }
            ElseIf 
            End If
        End Function

        <Extension>
        Private Function GetLiteralvalue(x As LiteralExpressionSyntax, context As Environment) As WATSyntax
            Dim value As Object = x.Token.Value
            Dim buffer = context.Workspace.Memory

            Select Case value.GetType
                Case GetType(String)
                    Dim i As Integer = buffer.AddString(x.Token.ValueText)
                    Dim str As New LiteralValue(i, WATType.string)

                    Return str
                Case Else
                    Return New LiteralValue(value)
            End Select
        End Function

        <Extension>
        Private Function ParseFunctionInvoke(calls As InvocationExpressionSyntax, context As Environment) As WATSyntax
            Dim par As New Dictionary(Of String, WATSyntax)
            Dim target As WATSyntax = calls.Expression.ParseValue(context)
            Dim i As Integer = 0

            For Each arg As ArgumentSyntax In calls.ArgumentList.Arguments
                Select Case arg.GetType
                    Case GetType(SimpleArgumentSyntax)
                        par("$" & i) = DirectCast(arg, SimpleArgumentSyntax).Expression.ParseValue(context)
                    Case Else
                        Throw New NotImplementedException
                End Select

                i += 1
            Next

            Return New FunctionInvoke() With {
                .Arguments = par.Values.ToArray,
                .Func = target
            }
        End Function
    End Module
End Namespace