Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Linq
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal
Imports VanillaBasic.WebAssembly.Syntax.WASM

Namespace Compiler

    Module FunctionWriter

        <Extension>
        Public Function ToSExpression(func As FunctionDeclare, workspace As Workspace) As String
            If func.Name = "EncodeBytes" Then
                Call System.Console.Error.WriteLine($"[emit] {func.namespace}.{func.Name} body={If(func.body Is Nothing, -1, func.body.Length)}")
            End If

            Dim par As String = func.parameters.Select(Function(a) a.GetParameterExpression).JoinBy(" ")
            Dim result As String
            Dim buildBody As String() = func.body _
                .Select(Function(line)
                            Return drop.AutoDropValueStack(line).ToSExpression(Nothing, "    ")
                        End Function) _
                .ToArray

            If func.Type.UnderlyingWATType = WATElements.void Then
                result = ""
            Else
                result = $"(result {func.Type.UnderlyingWATType.Description})"
                func.FixImplictReturns(buildBody)
            End If

            Dim locals As String() = func.locals _
                .SafeQuery _
                .Select(Function(local)
                            Return local.ToSExpression(Nothing, "")
                        End Function) _
                .ToArray

            Return $"
    ;; {func.ToString}
    (func ${func.namespace}.{func.Name} {par} {result}

        {locals.JoinBy(SExpressionEngine.Indent)}

        {buildBody.JoinBy(SExpressionEngine.Indent & "    ")}
    )"
        End Function

        <Extension>
        Private Sub FixImplictReturns(func As FunctionDeclare, ByRef buildBody As String())
            If func.body Is Nothing OrElse func.body.Length = 0 OrElse Not TypeOf func.body.Last() Is ReturnValue Then
                Dim rtvl As ReturnValue
                Dim value As Object

                Select Case func.Type.UnderlyingWATType
                    Case WATElements.i64
                        value = 0&
                    Case WATElements.f32
                        value = 0!
                    Case WATElements.f64
                        value = 0#
                    Case Else
                        ' i32 / boolean / string / array / 用户自定义类型等统一使用 i32 零值
                        value = 0%
                End Select

                rtvl = New ReturnValue(New LiteralValue(value))
                buildBody = c(buildBody, {rtvl.ToSExpression(Nothing, "")}).ToArray
            End If
        End Sub

        <Extension>
        Public Iterator Function ToSExpression(list As IEnumerable(Of FunctionDeclare), workspace As Workspace) As IEnumerable(Of String)
            For Each group In list.GroupBy(Function(fun) fun.namespace)
                Dim str As New StringBuilder($";; functions in [{group.Key}]" & vbCrLf)

                Call str.AppendLine()

                For Each api As FunctionDeclare In group
                    Call str.AppendLine(api.ToSExpression(workspace))
                Next

                Call str.AppendLine()

                Yield str.ToString
            Next
        End Function
    End Module
End Namespace