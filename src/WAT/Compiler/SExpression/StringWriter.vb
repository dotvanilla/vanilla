
Imports System.Runtime.CompilerServices
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.Memory
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Public Module StringWriter

        <Extension>
        Public Function StringExpressions(buffer As MemoryBuffer) As String()
            Return buffer _
                .Where(Function(m) TypeOf m Is StringLiteral) _
                .Select(Function(str) DirectCast(str, StringLiteral).ToSExpression) _
                .ToArray
        End Function

        <Extension>
        Public Function AnyToString(value As WATSyntax, context As Environment) As WATSyntax
            'Dim type$ = value.Type
            'Dim toString = JavaScriptImports.ToString(type)

            'context.addRequired(toString)
            'value = New FuncInvoke With {
            '    .[operator] = False,
            '    .refer = toString,
            '    .parameters = {value}
            '}

            'Return value
            Throw New NotImplementedException
        End Function
    End Module
End Namespace