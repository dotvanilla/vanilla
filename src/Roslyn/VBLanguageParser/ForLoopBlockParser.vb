Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
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
            Dim control As DeclareLocal = forBlock.ParseControlVariable(context)
            Dim init As WATSyntax = forBlock.ForStatement.FromValue.ParseValue(context)
            Dim final As WATSyntax = forBlock.ForStatement.ToValue.ParseValue(context)
            Dim stepValue As WATSyntax
            Dim forLoop As New [For] With {
                .Annotation = forBlock.ForStatement.ToString,
                .control = control
            }

            If forBlock.ForStatement.StepClause Is Nothing Then
                ' 默认是1（必须是 NumberLiteral，因为下面需要依据步长的符号来决定循环的退出条件）
                stepValue = New NumberLiteral(1, control.Type)
            Else
                stepValue = forBlock.ForStatement _
                    .StepClause _
                    .StepValue _
                    .ParseValue(context)
            End If

            Dim break As New br_if With {
                .blockLabel = forLoop.guid,
                .condition = ParseForLoopTest(control, stepValue, final, context),
                .Annotation = .condition.ToString
            }
            Dim [next] As New br With {.blockLabel = forLoop.loopID}
            Dim internal As New List(Of WATSyntax)
            Dim controlVar As SymbolGetValue = control.ForControlGetLocal
            Dim doStep As WATSyntax = BinaryParser.BinaryStack(controlVar, stepValue, "+", context)
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
            forLoop.initFrom = init
            forLoop.stepvalue = stepValue

            For Each symbol As DeclareLocal In forLoop.locals
                Call context.AddLocal(symbol)
            Next

            Return forLoop
        End Function

        <Extension>
        Private Function ForControlGetLocal(control As WATSyntax) As SymbolGetValue
            If TypeOf control Is DeclareLocal Then
                Return New SymbolGetValue(control.Type) With {
                    .Name = DirectCast(control, DeclareLocal).Name
                }
            Else
                Return control
            End If
        End Function

        Private Function ParseForLoopTest(control As WATSyntax, [step] As WATSyntax, [to] As WATSyntax, context As Environment) As BooleanLogical
            Dim ctlVar As SymbolGetValue = control.ForControlGetLocal
            Dim ctrlTest As BooleanLogical

            If TypeOf control Is DeclareLocal Then
                Call context.AddLocal(DirectCast(control, DeclareLocal))
            End If

            If TypeOf ctlVar Is SymbolGetValue Then
                DirectCast(ctlVar, SymbolGetValue).isGlobal = False
            End If

            ' VB 的 For 循环语义等价于：
            '
            '   step > 0 :  while i <= to   （即 i >  to 的时候退出）
            '   step < 0 :  while i >= to   （即 i <  to 的时候退出）
            '
            ' NOTE: 这里**必须**使用 ">"/"<" 作为退出条件，而不能使用 "="。
            ' 使用 "=" 的时候，如果循环的起始值已经大于结束值（例如
            ' ``For k As Integer = 1 To len`` 且 ``len = 0``），循环变量永远不可能
            ' 等于结束值，循环就永远无法退出 —— 表现为运行期死循环。
            If StepSign([step]) < 0 Then
                ctrlTest = BinaryParser.BinaryCompares(ctlVar, [to], "<", context)
            Else
                ctrlTest = BinaryParser.BinaryCompares(ctlVar, [to], ">", context)
            End If

            Return ctrlTest
        End Function

        ''' <summary>
        ''' 判断 For 循环步长的符号：返回负数表示递减循环。
        ''' 在无法于编译期确定步长的时候，默认为递增（这可以保证循环一定能够终止）。
        ''' </summary>
        Private Function StepSign([step] As WATSyntax) As Integer
            If [step] Is Nothing Then
                Return 1
            ElseIf TypeOf [step] Is NumberLiteral Then
                Return DirectCast([step], NumberLiteral).Sign
            ElseIf TypeOf [step] Is LiteralValue Then
                Dim value As Object = DirectCast([step], LiteralValue).Value

                If TypeOf value Is Integer Then
                    Return Math.Sign(CInt(value))
                ElseIf TypeOf value Is Long Then
                    Return Math.Sign(CLng(value))
                ElseIf TypeOf value Is Single Then
                    Return Math.Sign(CSng(value))
                ElseIf TypeOf value Is Double Then
                    Return Math.Sign(CDbl(value))
                End If
            End If

            Return 1
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