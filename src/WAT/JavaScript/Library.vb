Imports System.Reflection
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace JavaScript

    Public Class Library

        Public Shared Iterator Function GetJavaScriptApi(Of T As Class)() As IEnumerable(Of ImportsFunction)
            Dim methodList As MethodInfo() = GetType(T).GetMethods

            For Each api As MethodInfo In methodList

            Next
        End Function

        Public Shared Sub [Imports](Of T As Class)(workspace As Workspace)
            For Each api As ImportsFunction In GetJavaScriptApi(Of T)()
                Call workspace.Imports.Add(api.Name, api)
            Next
        End Sub
    End Class
End Namespace