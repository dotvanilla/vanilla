Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public MustInherit Class WATSyntax

        Public MustOverride ReadOnly Property Type As WATType

        Public MustOverride Function ToSExpression(env As Environment, indent As String) As String

    End Class
End Namespace