Imports System.Runtime.CompilerServices
Imports System.Text
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Module FunctionWriter

        <Extension>
        Public Function ToSExpression(api As FunctionDeclare, workspace As Workspace) As String
            Dim par As String = api.parameters.Select(Function(a) a.GetParameterExpression).JoinBy(" ")
            Dim result As String = api.Type.UnderlyingWATType.ToString
            Dim buildBody As String() = api.body _
                .Select(Function(line)
                            Return Drop.AutoDropValueStack(line).ToSExpression(Nothing, Nothing)
                        End Function) _
                .ToArray

            If result = "void" Then
                result = ""
            Else
                result = $"(result {result})"
            End If

            Return $"(func ${api.namespace}.{api.Name} {par} {result}
    ;; {api.ToString}
    {buildBody.JoinBy(ASCII.LF)}
)"
        End Function

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