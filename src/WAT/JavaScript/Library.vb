Imports System.Reflection
Imports VanillaBasic.WebAssembly.Syntax

Namespace JavaScript

    Public Class Library

        Public Shared Iterator Function [Imports](Of T As Class)() As IEnumerable(Of ImportsFunction)
            Dim methodList As MethodInfo() = GetType(T).GetMethods

            For Each api As MethodInfo In methodList

            Next
        End Function
    End Class
End Namespace