Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module IfBlockParser

        ''' <summary>
        ''' 多行 If 块（包含 ``ElseIf`` 链以及 ``Else`` 分支）的解析
        ''' </summary>
        <Extension>
        Public Function IfBlock(doif As MultiLineIfBlockSyntax, context As Environment) As [If]
            Return BuildIf(
                condition:=doif.IfStatement.Condition,
                thenStatements:=doif.Statements,
                elseIfs:=doif.ElseIfBlocks.ToList,
                elseBlock:=doif.ElseBlock,
                context:=context
            )
        End Function

        Private Function BuildIf(condition As ExpressionSyntax,
                                 thenStatements As SyntaxList(Of StatementSyntax),
                                 elseIfs As IList(Of ElseIfBlockSyntax),
                                 elseBlock As ElseBlockSyntax,
                                 context As Environment) As [If]

            Dim conditionLogical As New BooleanLogical(condition.ParseValue(context))
            Dim thenClosure As Closure = thenStatements.ParseClosure(context)
            Dim elseClosure As Closure = Nothing

            If elseIfs.Count > 0 Then
                Dim nested As [If] = BuildIf(
                    condition:=elseIfs(0).ElseIfStatement.Condition,
                    thenStatements:=elseIfs(0).Statements,
                    elseIfs:=elseIfs.Skip(1).ToList,
                    elseBlock:=elseBlock,
                    context:=context
                )

                elseClosure = New Closure
                elseClosure.multipleLines = {nested}
            ElseIf Not elseBlock Is Nothing Then
                elseClosure = elseBlock.Statements.ParseClosure(context)
            End If

            If elseClosure Is Nothing Then
                Return New [If] With {
                    .condition = conditionLogical,
                    .[then] = thenClosure
                }
            Else
                Return New IfElse With {
                    .condition = conditionLogical,
                    .[then] = thenClosure,
                    .[else] = elseClosure
                }
            End If
        End Function
    End Module
End Namespace
