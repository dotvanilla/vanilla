Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class LiteralValue : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
        Public ReadOnly Property Value As Object

        Sub New(value As Object)
            Me.Value = value
            Me.Type = WATType.GetElementType(value.GetType)
        End Sub

        Public Shared Function GetUnderlyingType(value As Object, wasm As Workspace) As WATType
            If value Is Nothing Then
                Return Nothing
            Else
                Return WATType.GetUnderlyingType(value.GetType, wasm)
            End If
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace