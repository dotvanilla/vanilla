Imports System.Runtime.CompilerServices
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Module FunctionWriter

        <Extension>
        Public Function ToSExpression(api As FunctionDeclare, workspace As Workspace) As String

        End Function

        <Extension>
        Public Function ToSExpression(list As IEnumerable(Of FunctionDeclare), workspace As Workspace) As String
            Return list.Select(Function(api) api.ToSExpression(workspace)).JoinBy(vbCrLf & vbCrLf)
        End Function
    End Module
End Namespace