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
            Dim result As String = api.Type.UnderlyingWATType.Description
            Dim buildBody As String() = api.body _
                .Select(Function(line)
                            Return line.ToSExpression(Nothing, Nothing)
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
        Public Function ToSExpression(list As IEnumerable(Of FunctionDeclare), workspace As Workspace) As String
            Return list _
                .GroupBy(Function(fun) fun.namespace) _
                .Select(Function(group)
                            Dim str As New StringBuilder($";; functions in [{group.Key}]" & vbCrLf)

                            Call str.AppendLine()

                            For Each api As FunctionDeclare In group
                                Call str.AppendLine(api.ToSExpression(workspace))
                            Next

                            Call str.AppendLine()

                            Return str.ToString
                        End Function) _
                .JoinBy(ASCII.LF)
        End Function
    End Module
End Namespace