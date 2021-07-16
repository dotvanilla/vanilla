Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class SymbolSetValue : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return Value.Type
            End Get
        End Property

        Public Property Target As WATSyntax
        Public Property Value As WATSyntax
        Public Property isGlobal As Boolean

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Dim target As String = Me.Target.ToSExpression(env, indent)
            Dim value As String = Me.Value.ToSExpression(env, indent)

            If isGlobal Then
                Return $"(set_global {target} {value})"
            Else
                Return $"(set_local {target} {value})"
            End If
        End Function
    End Class
End Namespace