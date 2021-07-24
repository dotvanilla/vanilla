Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax.WASM

    Public Class br_if : Inherits br

        ''' <summary>
        ''' Is a logical expression
        ''' </summary>
        ''' <returns></returns>
        Public Property condition As BooleanLogical

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"
    ;; {Annotation}
    {indent}(br_if ${blockLabel} {condition.ToSExpression(env, indent)})"
        End Function
    End Class
End Namespace