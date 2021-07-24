Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.WASM

Namespace VBLanguageParser

    Module ForLoopBlockParser

        ''' <summary>
        ''' Convert for loop for while loop
        ''' </summary>
        ''' <param name="forBlock"></param>
        ''' <param name="context"></param>
        ''' <returns></returns>
        <Extension>
        Public Function ParseForLoop(forBlock As ForBlockSyntax, context As Environment) As [For]
            Dim control As WATSyntax = forBlock.controlVariable(context)
            Dim init = forBlock.ForStatement.FromValue.ParseValue(context)
            Dim final = forBlock.ForStatement.ToValue.ParseValue(context)
            Dim stepValue As WATSyntax
            Dim forLoop As New [For] With {
                .Annotation = forBlock.ForStatement.ToString,
                .control = control
            }

            If forBlock.ForStatement.StepClause Is Nothing Then
                ' 默认是1
                stepValue = New LiteralValue(1, control.Type)
            Else
                stepValue = forBlock.ForStatement _
                    .StepClause _
                    .StepValue _
                    .ParseValue(context)
            End If

            Dim break As New br_if With {
                .blockLabel = forLoop.guid,
                .condition = parseForLoopTest(control, stepValue, final, context)
            }
            Dim [next] As New br With {.blockLabel = forLoop.loopID}
            Dim internal As New List(Of WATSyntax)
            Dim controlVar = control.ctlGetLocal
            Dim doStep = BinaryParser.BinaryStack(controlVar, stepValue, "+", context)
            Dim locals As DeclareLocal() = Nothing

            internal += break
            internal += forBlock.Statements.LoadBody(locals, context)
            ' 更新循环控制变量的值
            internal += New CommentText With {
                .Text = $"For loop control step: {stepValue.ToSExpression}"
}
            internal += New SetLocalVariable With {.var = controlVar.var, .Value = doStep}
            internal += [next]
            internal += New CommentText With {
                .Text = $"For Loop Next On {[next].blockLabel}"
            }

            block.internal = internal

            Yield block
        End Function

        <Extension>
        Private Function ctlGetLocal(control As Expression) As GetLocalVariable
            If TypeOf control Is DeclareLocal Then
                Return New GetLocalVariable With {
                    .var = DirectCast(control, DeclareLocal).Name
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
        Private Function controlVariable(forBlock As ForBlockSyntax, symbols As Environment) As WATSyntax
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