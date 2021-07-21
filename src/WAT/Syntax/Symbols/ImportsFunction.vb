
Imports Microsoft.VisualBasic.Linq
Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class ImportsFunction : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType

        Public Property Parameters As DeclareLocal()
        Public Property ModuleName As String
        Public Property ImportsName As String

        Sub New(type As WATType)
            Me.Type = type
        End Sub

        Public Overrides Function ToString() As String
            Return ToSExpression(Nothing, "")
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Dim arguments As String() = Parameters _
                .SafeQuery _
                .Select(Function(a) a.GetParameterExpression) _
                .ToArray
            Dim result As String = ""

            If Type.UnderlyingWATType <> WATElements.void Then
                result = $"(result {Type.UnderlyingWATType.Description})"
            End If

            Return $"(func ${Name} (import ""{ModuleName}"" ""{ImportsName}"") {arguments.JoinBy(" ")} {result})"
        End Function
    End Class
End Namespace