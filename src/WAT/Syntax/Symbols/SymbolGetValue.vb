Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class SymbolGetValue : Inherits SymbolReference

        Public Property isGlobal As Boolean

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            If isGlobal Then
                Return $"(get_global ${Name})"
            Else
                Return $"(get_local ${Name})"
            End If
        End Function

    End Class
End Namespace