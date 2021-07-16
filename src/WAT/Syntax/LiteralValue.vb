Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class LiteralValue : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
        Public ReadOnly Property Value As Object

        Sub New(value As Object)
            Me.Value = value
            Me.Type = WATType.GetElementType(value.GetType)
        End Sub

        Sub New(intptr As Integer, type As WATType)
            Me.Value = intptr
            Me.Type = type
        End Sub

        Public Shared Function GetUnderlyingType(value As Object, wasm As Workspace) As WATType
            If value Is Nothing Then
                Return Nothing
            Else
                Return WATType.GetUnderlyingType(value.GetType, wasm)
            End If
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Select Case Type
                Case WATType.i32, WATType.string
                    Return $"(i32.const {Value})"
                Case WATType.i64
                    Return $"(i64.const {Value})"
                Case WATType.f32
                    Return $"(f32.const {Value})"
                Case WATType.f64
                    Return $"(f64.const {Value})"
                Case Else
                    Throw New InvalidCastException
            End Select
        End Function
    End Class
End Namespace