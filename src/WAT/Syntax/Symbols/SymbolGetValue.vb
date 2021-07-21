Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class SymbolGetValue : Inherits SymbolReference

        Public Property isGlobal As Boolean

        Sub New(Optional type As WATType = Nothing)
            Call MyBase.New(type)
        End Sub

        Sub New(local As DeclareLocal)
            Call MyBase.New(local.Type)

            isGlobal = False
            Name = local.Name
        End Sub

        Sub New(symbol As SymbolReference)
            Call MyBase.New(symbol.Type)

            isGlobal = False
            Name = symbol.Name
        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            If isGlobal Then
                Return $"(get_global ${Name})"
            Else
                Return $"(get_local ${Name})"
            End If
        End Function

    End Class
End Namespace