Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal

Namespace VBLanguageParser

    Module Expressionparser

        <Extension>
        Public Function GetSymbol(name As IdentifierNameSyntax, context As Environment) As SymbolReference
            Dim symbol As String = name.Identifier.Text
            Dim type As WATType = context.GetSymbolType(symbol)

            ' 可能是递归调用
            If symbol = context.SymbolName Then
                symbol = context.FullName
            End If

            Return New SymbolReference(symbol, type)
        End Function

        <Extension>
        Public Function ParseValue(expression As ExpressionSyntax, context As Environment) As WATSyntax
            Select Case expression.GetType
                Case GetType(InvocationExpressionSyntax)
                    Return DirectCast(expression, InvocationExpressionSyntax).ParseFunctionInvoke(context)
                Case GetType(MemberAccessExpressionSyntax)
                    Return DirectCast(expression, MemberAccessExpressionSyntax).ParseReference(context)
                Case GetType(LiteralExpressionSyntax)
                    Return DirectCast(expression, LiteralExpressionSyntax).GetLiteralvalue(Nothing, context)
                Case GetType(IdentifierNameSyntax)
                    Return DirectCast(expression, IdentifierNameSyntax).GetSymbol(context)
                Case GetType(BinaryExpressionSyntax)
                    Return DirectCast(expression, BinaryExpressionSyntax).GetBinary(context)
                Case GetType(UnaryExpressionSyntax)
                    Return DirectCast(expression, UnaryExpressionSyntax).GetUnaryValue(context)
                Case Else
                    Throw New NotImplementedException(expression.GetType.FullName)
            End Select
        End Function

        <Extension>
        Public Function GetUnaryValue(unary As UnaryExpressionSyntax, context As Environment) As WATSyntax
            Dim unaryOperator As String = unary.OperatorToken.ValueText
            Dim value As WATSyntax = unary.Operand.ParseValue(context)

            If TypeOf value Is LiteralValue Then
                Dim constVal As NumberLiteral = DirectCast(value, NumberLiteral)

                Select Case unaryOperator
                    Case "-"
                        Return New NumberLiteral(-1 * constVal.Value)
                    Case "+"
                        Return constVal
                    Case Else
                        Throw New NotImplementedException
                End Select
            Else
                Return New UnaryOperator(unaryOperator, value)
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="type">can be nothing</param>
        ''' <param name="context"></param>
        ''' <returns></returns>
        <Extension>
        Friend Function GetLiteralvalue(x As LiteralExpressionSyntax, type As WATType, context As Environment) As WATSyntax
            Dim value As Object = x.Token.Value
            Dim buffer = context.Workspace.Memory

            If value Is Nothing Then
                ' 是空值常量，则直接返回整形数0表示空指针
                Return LiteralValue.Nothing(type)
            ElseIf type Is Nothing Then
                type = WATType.GetElementType(value.GetType)
            End If

            Select Case type.UnderlyingVBType.Type
                Case GetType(String)
                    Dim i As Integer = buffer.AddString(x.Token.ValueText)
                    Dim str As New LiteralValue(i, WATType.string)

                    Return str
                Case GetType(Boolean)

                    ' 在常量下，逻辑值是i32 1 或者 0
                    If value = True Then
                        value = 1
                    Else
                        value = 0
                    End If

                    Return New LiteralValue(value, WATType.boolean)
                Case Else
                    Return New NumberLiteral(value, type)
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
            }.SetReturnValue(context.GetSymbolType(target))
        End Function
    End Module
End Namespace