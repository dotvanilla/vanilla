Imports System.Runtime.CompilerServices
Imports Wasm.Symbols.Blocks

Namespace Symbols

    Module Extensions

        <Extension>
        Public Function IsNullOrEmpty(group As ExpressionGroup) As Boolean
            Return group Is Nothing OrElse group.group.IsNullOrEmpty
        End Function
    End Module
End Namespace