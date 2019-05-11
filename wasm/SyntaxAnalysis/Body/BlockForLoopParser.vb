Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.Blocks
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Module BlockForLoopParser

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
    End Module
End Namespace