
Imports Microsoft.VisualBasic.Linq
Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class ImportsFunction : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType

        Public Property Parameters As DeclareLocal()
        Public Property ModuleName As String
        Public Property ImportsName As String

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Dim arguments As String() = Parameters _
                .SafeQuery _
                .Select(Function(a) a.GetParameterExpression) _
                .ToArray

            Return $"(func ${Name} (import ""{ModuleName}"" ""{ImportsName}"") {arguments.JoinBy(" ")} (result {Type.UnderlyingWATType.Description}))"
        End Function
    End Class
End Namespace