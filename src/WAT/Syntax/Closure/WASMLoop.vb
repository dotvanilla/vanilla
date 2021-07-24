Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class WASMLoop : Inherits Closure

        Public Property loopID As String = $"loop_{App.NextTempName}"

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"
    (block ${guid} 
        (loop ${loopID}

            {Closure.InternalBlock(Me, env, $"{indent}        ")}

        )
    )"
        End Function

    End Class
End Namespace