Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.Blocks

Namespace SyntaxAnalysis

    Module BlockDoLoopParser
        <Extension>
        Friend Iterator Function ParseBlockInternal(block As IEnumerable(Of StatementSyntax), symbols As SymbolTable) As IEnumerable(Of Expression)
            For Each statement As StatementSyntax In block
                For Each line As Expression In statement.ParseStatement(symbols).AutoDropValueStack(symbols)
                    Yield line
                Next
            Next
        End Function

        <Extension>
        Public Iterator Function DoLoop(doLoopBlock As DoLoopBlockSyntax, symbols As SymbolTable) As IEnumerable(Of Expression)
            Dim [do] As WhileOrUntilClauseSyntax = doLoopBlock.DoStatement.WhileOrUntilClause
            Dim condition As BooleanSymbol

            If [do] Is Nothing Then
                Dim [loop] As LoopStatementSyntax = doLoopBlock.LoopStatement

                ' do 
                '  xxxxx
                ' loop xxxx
                Yield New CommentText With {.text = "Do ... Loop"}

                If [loop].WhileOrUntilClause Is Nothing Then
                    ' do .... loop
                    ' 无条件判断的无限循环表达式
                    For Each line As Expression In whileLoopInternal(Nothing, doLoopBlock.Statements, symbols, True)
                        Yield line
                    Next
                Else
                    condition = [loop].WhileOrUntilClause _
                        .Condition _
                        .whileCondition(symbols)

                    If [loop].isLoopWhile Then
                        ' change nothing
                    ElseIf [loop].isLoopUntil Then
                        ' reverse the condition test
                        condition.isNot = True
                    Else
                        Throw New NotImplementedException
                    End If

                    ' 条件判断结束应该是放在最后的
                    For Each line As Expression In condition.whileLoopInternal(doLoopBlock.Statements, symbols, True)
                        Yield line
                    Next
                End If
            Else
                condition = [do] _
                    .Condition _
                    .whileCondition(symbols)

                If [do].WhileOrUntilKeyword.ValueText = "Until" Then
                    Throw New NotImplementedException
                Else
                    Yield New CommentText With {.text = doLoopBlock.DoStatement.ToString}

                    For Each line In condition.whileLoopInternal(doLoopBlock.Statements, symbols, False)
                        Yield line
                    Next
                End If
            End If
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function isLoopWhile([loop] As LoopStatementSyntax) As Boolean
            Return [loop].WhileOrUntilClause.WhileOrUntilKeyword.Value = "While"
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function isLoopUntil([loop] As LoopStatementSyntax) As Boolean
            Return [loop].WhileOrUntilClause.WhileOrUntilKeyword.Value = "Until"
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="condition"></param>
        ''' <param name="statements"></param>
        ''' <param name="symbols"></param>
        ''' <param name="isPostpositive">
        ''' 对循环条件的判断是否是后置的，即放在整个循环的末尾的
        ''' </param>
        ''' <returns></returns>
        <Extension>
        Private Iterator Function whileLoopInternal(condition As Expression,
                                                    statements As SyntaxList(Of StatementSyntax),
                                                    symbols As SymbolTable,
                                                    isPostpositive As Boolean) As IEnumerable(Of Expression)
            Dim block As New [Loop] With {
                .guid = $"block_{symbols.NextGuid}",
                .loopID = $"loop_{symbols.NextGuid}"
            }
            Dim internal As New List(Of Expression)

            ' 为exit语句所准备的
            symbols.context.blockGuid.Push(block.guid)

            Yield New CommentText With {
                .text = $"Start Do While Block {block.guid}"
            }

            ' 如果condition是空值的时候，则是类似于do xxxx loop这样的无退出条件的无限循环
            ' 这个是前置的判断，即条件判断放在循环的最开始
            If Not condition Is Nothing AndAlso Not isPostpositive Then
                ' 有条件的退出循环
                internal += New br_if With {
                    .blockLabel = block.guid,
                    .condition = condition
                }
            End If

            internal += statements.ParseBlockInternal(symbols)

            ' 这个是后置的判断，即条件判断放在循环的最末尾
            If Not condition Is Nothing AndAlso isPostpositive Then
                ' 有条件的退出循环
                internal += New br_if With {
                    .blockLabel = block.guid,
                    .condition = condition
                }
            End If

            internal += New br With {.blockLabel = block.loopID}

            block.internal = internal
            symbols.context.blockGuid.Pop()

            Yield block
            Yield New CommentText With {.text = $"End Loop {block.loopID}"}
        End Function

        <Extension>
        Public Iterator Function DoWhile(whileBlock As WhileBlockSyntax, symbols As SymbolTable) As IEnumerable(Of Expression)
            Dim condition As Expression = whileBlock.whileCondition(symbols)

            For Each line In condition.whileLoopInternal(whileBlock.Statements, symbols, False)
                Yield line
            Next
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function whileCondition(whileBlock As WhileBlockSyntax, symbols As SymbolTable) As Expression
            Return whileBlock.WhileStatement.Condition.whileCondition(symbols)
        End Function

        <Extension>
        Private Function whileCondition(expression As ExpressionSyntax, symbols As SymbolTable) As BooleanSymbol
            Dim condition As Expression = expression.ValueExpression(symbols)

            Return New BooleanSymbol With {
                .condition = condition,
                .[isNot] = True
            }
        End Function
    End Module
End Namespace