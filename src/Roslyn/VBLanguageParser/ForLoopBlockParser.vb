Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal
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
            Dim control As WATSyntax = forBlock.ParseControlVariable(context)
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
                .Text = $"For loop control step: {stepValue.ToSExpression(context, "")}"
}
            internal += New SymbolSetValue With {.Target = New SymbolReference(controlVar.Name), .Value = doStep}
            internal += [next]
            internal += New CommentText With {
                .Text = $"For Loop Next On {[next].blockLabel}"
            }

            forLoop.multipleLines = internal
            forLoop.locals = locals

            Return forLoop
        End Function

        <Extension>
        Private Function ctlGetLocal(control As WATSyntax) As SymbolGetValue
            If TypeOf control Is DeclareLocal Then
                Return New SymbolGetValue With {
                    .Name = DirectCast(control, DeclareLocal).Name
                }
            Else
                Return control
            End If
        End Function

        Private Function parseForLoopTest(control As WATSyntax, [step] As WATSyntax, [to] As WATSyntax, context As Environment) As BooleanLogical
            Dim ctlVar As SymbolGetValue = control.ctlGetLocal
            Dim ctrlTest As BooleanLogical

            If TypeOf control Is DeclareLocal Then
                Call context.AddLocal(DirectCast(control, DeclareLocal))
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

            If TypeOf [step] Is NumberLiteral Then
                With DirectCast([step], NumberLiteral)
                    If .Sign > 0 Then
                        ctrlTest = BinaryParser.BinaryCompares(ctlVar, [to], ">", context)
                    Else
                        ctrlTest = BinaryParser.BinaryCompares(ctlVar, [to], "<", context)
                    End If
                End With
            Else
                ctrlTest = BinaryParser.BinaryCompares(ctlVar, [to], "=", context)
            End If

            Return ctrlTest
        End Function

        <Extension>
        Private Function ParseControlVariable(forBlock As ForBlockSyntax, context As Environment) As WATSyntax
            Dim control As VisualBasicSyntaxNode = forBlock.ForStatement.ControlVariable

            If TypeOf control Is VariableDeclaratorSyntax Then
                Dim declareCtl = DirectCast(control, VariableDeclaratorSyntax) _
                    .ParseDeclarator(context) _
                    .First

                Return declareCtl
            Else
                ' reference a local variable
                Throw New NotImplementedException
            End If
        End Function
    End Module
End Namespace