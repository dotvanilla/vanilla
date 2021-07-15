
Imports System.Runtime.CompilerServices
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.Memory

Namespace Compiler

    Module StringWriter

        <Extension>
        Public Function StringExpressions(buffer As MemoryBuffer) As String()
            Return buffer _
                .Where(Function(m) TypeOf m Is StringLiteral) _
                .Select(Function(str) DirectCast(str, StringLiteral).ToSExpression) _
                .ToArray
        End Function
    End Module
End Namespace