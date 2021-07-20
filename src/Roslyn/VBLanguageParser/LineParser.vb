Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module LineParser

        <Extension>
        Public Iterator Function Parse(statement As StatementSyntax, context As Environment) As IEnumerable(Of WATSyntax)
            Select Case statement.GetType
                Case GetType(LocalDeclarationStatementSyntax)
                    ' Return DirectCast(statement, LocalDeclarationStatementSyntax).LocalDeclare(symbols).ToArray
                Case GetType(AssignmentStatementSyntax)
                    ' Return DirectCast(statement, AssignmentStatementSyntax).ValueAssign(symbols)
                Case GetType(ReturnStatementSyntax)
                    Yield DirectCast(statement, ReturnStatementSyntax).GetReturnValue(context)
                    Return
                Case GetType(WhileBlockSyntax)
                    ' Return DirectCast(statement, WhileBlockSyntax).DoWhile(symbols).ToArray
                Case GetType(MultiLineIfBlockSyntax)
                    Yield DirectCast(statement, MultiLineIfBlockSyntax).IfBlock(context)
                    Return
                Case GetType(ForBlockSyntax)
                    ' Return DirectCast(statement, ForBlockSyntax).ForLoop(symbols).ToArray
                Case GetType(CallStatementSyntax)
                    Yield DirectCast(statement, CallStatementSyntax).Invocation.ParseValue(context)
                    Return
                Case GetType(ExpressionStatementSyntax)
                    Yield DirectCast(statement, ExpressionStatementSyntax).Expression.ParseValue(context)
                    Return
                Case GetType(DoLoopBlockSyntax)
                    ' Return DirectCast(statement, DoLoopBlockSyntax).DoLoop(symbols).ToArray
                Case GetType(ExitStatementSyntax)
                    ' Return DirectCast(statement, ExitStatementSyntax).ExitBlock(symbols)
                Case Else
                    Throw New NotImplementedException(statement.GetType.FullName)
            End Select

            Throw New NotImplementedException(statement.GetType.FullName)
        End Function

        <Extension>
        Private Function GetReturnValue(rtvl As ReturnStatementSyntax, context As Environment) As ReturnValue
            Dim value As WATSyntax = rtvl.Expression.ParseValue(context)
            Dim returns As New ReturnValue With {.Value = value}

            Return returns
        End Function

        <Extension>
        Public Function ParseClosure(funcBody As SyntaxList(Of StatementSyntax), context As Environment) As Closure
            Dim body As New Closure
            body.multipleLines = funcBody.LoadBody(body.locals, context)
            Return body
        End Function
    End Module
End Namespace