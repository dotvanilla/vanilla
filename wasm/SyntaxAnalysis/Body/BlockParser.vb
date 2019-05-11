#Region "Microsoft.VisualBasic::7aedc23f3f0e916492869918d9c1d00d, SyntaxAnalysis\Body\BlockParser.vb"

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

    '     Module BlockParser
    ' 
    '         Function: AutoDropValueStack, controlVariable, ctlGetLocal, DoLoop, DoWhile
    '                   ForLoop, IfBlock, ParseBlockInternal, parseForLoopTest, (+2 Overloads) whileCondition
    '                   whileLoopInternal
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.Blocks
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Module BlockParser

        <Extension>
        Public Function IfBlock(doIf As MultiLineIfBlockSyntax, symbols As SymbolTable) As Expression
            Dim test As New BooleanSymbol With {
                .Condition = doIf _
                    .IfStatement _
                    .Condition _
                    .ValueExpression(symbols)
            }
            Dim elseBlock As Expression()
            Dim thenBlock As Expression() = doIf.Statements _
                .ParseBlockInternal(symbols) _
                .ToArray

            If Not doIf.ElseBlock Is Nothing Then
                elseBlock = doIf.ElseBlock _
                    .Statements _
                    .ParseBlockInternal(symbols) _
                    .ToArray
            Else
                elseBlock = {}
            End If

            Return New IfBlock With {
                .condition = test,
                .[then] = thenBlock,
                .[else] = elseBlock
            }
        End Function

        ''' <summary>
        ''' Convert for loop for while loop
        ''' </summary>
        ''' <param name="forBlock"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Iterator Function ForLoop(forBlock As ForBlockSyntax, symbols As SymbolTable) As IEnumerable(Of Expression)
            Dim control As Expression = forBlock.controlVariable(symbols)
            Dim init = forBlock.ForStatement.FromValue.ValueExpression(symbols)
            Dim final = forBlock.ForStatement.ToValue.ValueExpression(symbols)
            Dim stepValue As Expression

            ' set for loop variable init value
            If TypeOf control Is DeclareLocal Then
                Yield New SetLocalVariable With {
                    .var = DirectCast(control, DeclareLocal).name,
                    .value = CTypeHandle.CType(control.TypeInfer(symbols), init, symbols)
                }
            Else
                Yield control
            End If

            If forBlock.ForStatement.StepClause Is Nothing Then
                ' 默认是1
                stepValue = New LiteralExpression(1, control.TypeInfer(symbols))
            Else
                stepValue = forBlock.ForStatement _
                    .StepClause _
                    .StepValue _
                    .ValueExpression(symbols)
            End If

            Yield New CommentText With {
                .text = forBlock.ForStatement.ToString
            }

            Dim block As New [Loop] With {
                .guid = $"block_{symbols.NextGuid}",
                .loopID = $"loop_{symbols.NextGuid}"
            }
            Dim break As New br_if With {
                .blockLabel = block.guid,
                .condition = parseForLoopTest(control, stepValue, final, symbols)
            }
            Dim [next] As New br With {.blockLabel = block.loopID}
            Dim internal As New List(Of Expression)
            Dim controlVar = control.ctlGetLocal
            Dim doStep = BinaryOperatorParser.BinaryStack(controlVar, stepValue, "+", symbols)

            internal += break
            internal += forBlock.Statements.ParseBlockInternal(symbols)
            ' 更新循环控制变量的值
            internal += New CommentText With {
                .text = $"For loop control step: {stepValue.ToSExpression}"
            }
            internal += New SetLocalVariable With {.var = controlVar.var, .value = doStep}
            internal += [next]
            internal += New CommentText With {
                .text = $"For Loop Next On {[next].blockLabel}"
            }

            block.internal = internal

            Yield block
        End Function

        <Extension>
        Private Function ctlGetLocal(control As Expression) As GetLocalVariable
            If TypeOf control Is DeclareLocal Then
                Return New GetLocalVariable With {
                    .var = DirectCast(control, DeclareLocal).name
                }
            Else
                Return control
            End If
        End Function

        Private Function parseForLoopTest(control As Expression, [step] As Expression, [to] As Expression, symbols As SymbolTable) As BooleanSymbol
            Dim ctlVar As GetLocalVariable = control.ctlGetLocal
            Dim ctrlTest As BooleanSymbol

            If TypeOf control Is DeclareLocal Then
                With DirectCast(control, DeclareLocal)
                    Call symbols.AddLocal(.ByRef)
                End With
            End If

            ' for i = 0 to 10 step 1
            ' equals to
            '
            ' if i >= 10 then
            '    break
            ' end if

            ' for i = 10 to 0 step -1
            ' equals to
            '
            ' if i <= 0 then
            '    break
            ' end if

            If TypeOf [step] Is LiteralExpression Then
                With DirectCast([step], LiteralExpression)
                    If .Sign > 0 Then
                        ctrlTest = BooleanSymbol.BinaryCompares(ctlVar, [to], ">", symbols)
                    Else
                        ctrlTest = BooleanSymbol.BinaryCompares(ctlVar, [to], "<", symbols)
                    End If
                End With
            Else
                ctrlTest = BooleanSymbol.BinaryCompares(ctlVar, [to], "=", symbols)
            End If

            Return ctrlTest
        End Function

        <Extension>
        Private Function controlVariable(forBlock As ForBlockSyntax, symbols As SymbolTable) As Expression
            Dim control = forBlock.ForStatement.ControlVariable

            If TypeOf control Is VariableDeclaratorSyntax Then
                Dim declareCtl = DirectCast(control, VariableDeclaratorSyntax) _
                    .ParseDeclarator(symbols, Nothing, isConst:=False) _
                    .First

                Return declareCtl
            Else
                ' reference a local variable
                Throw New NotImplementedException
            End If
        End Function

        <Extension>
        Public Iterator Function AutoDropValueStack(lineSymbols As [Variant](Of Expression, Expression()), symbols As SymbolTable) As IEnumerable(Of Expression)
            If lineSymbols.GetUnderlyingType.IsInheritsFrom(GetType(Expression)) Then
                lineSymbols = {lineSymbols.TryCast(Of Expression)}
            End If

            For Each line In lineSymbols.TryCast(Of Expression())
                If Not TypeOf line Is ReturnValue AndAlso line.TypeInfer(symbols) <> "void" Then
                    ' https://github.com/WebAssembly/wabt/issues/1067
                    '
                    ' required a drop if target produce values
                    Yield New drop With {.expression = line}
                Else
                    Yield line
                End If
            Next
        End Function

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
            Dim condition As Expression

            If [do] Is Nothing Then
                Dim [loop] As LoopStatementSyntax = doLoopBlock.LoopStatement

                ' do 
                '  xxxxx
                ' loop xxxx
                Yield New CommentText With {.text = "Do ... Loop"}

                If [loop].WhileOrUntilClause Is Nothing Then
                    ' do .... loop
                    ' 无条件判断的无限循环表达式
                    For Each line As Expression In whileLoopInternal(Nothing, doLoopBlock.Statements, symbols)
                        Yield line
                    Next
                Else
                    Throw New NotImplementedException
                End If
            Else
                condition = [do] _
                    .Condition _
                    .whileCondition(symbols)

                If [do].WhileOrUntilKeyword.ValueText = "Until" Then
                    Throw New NotImplementedException
                Else
                    Yield New CommentText With {.text = doLoopBlock.DoStatement.ToString}

                    For Each line In condition.whileLoopInternal(doLoopBlock.Statements, symbols)
                        Yield line
                    Next
                End If
            End If
        End Function

        <Extension>
        Private Iterator Function whileLoopInternal(condition As Expression, statements As SyntaxList(Of StatementSyntax), symbols As SymbolTable) As IEnumerable(Of Expression)
            Dim block As New [Loop] With {
                .guid = $"block_{symbols.NextGuid}",
                .loopID = $"loop_{symbols.NextGuid}"
            }
            Dim internal As New List(Of Expression)

            Yield New CommentText With {.text = $"Start Do While Block {block.guid}"}

            ' 如果condition是空值的时候，则是类似于do xxxx loop这样的无退出条件的无限循环
            If Not condition Is Nothing Then
                ' 有条件的退出循环
                internal += New br_if With {
                    .blockLabel = block.guid,
                    .condition = condition
                }
            End If

            internal += statements.ParseBlockInternal(symbols)
            internal += New br With {.blockLabel = block.loopID}

            block.internal = internal

            Yield block
            Yield New CommentText With {.text = $"End Loop {block.loopID}"}
        End Function

        <Extension>
        Public Iterator Function DoWhile(whileBlock As WhileBlockSyntax, symbols As SymbolTable) As IEnumerable(Of Expression)
            Dim condition As Expression = whileBlock.whileCondition(symbols)

            For Each line In condition.whileLoopInternal(whileBlock.Statements, symbols)
                Yield line
            Next
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function whileCondition(whileBlock As WhileBlockSyntax, symbols As SymbolTable) As Expression
            Return whileBlock.WhileStatement.Condition.whileCondition(symbols)
        End Function

        <Extension>
        Private Function whileCondition(expression As ExpressionSyntax, symbols As SymbolTable) As Expression
            Dim condition As Expression = expression.ValueExpression(symbols)

            Return New BooleanSymbol With {
                .Condition = condition,
                .[IsNot] = True
            }
        End Function
    End Module
End Namespace
