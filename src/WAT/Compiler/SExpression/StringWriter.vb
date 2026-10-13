
Imports System.Runtime.CompilerServices
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.Memory
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
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

        ''' <summary>
        ''' 任意的值转换为字符串：调用运行时提供的 ``string.i32.toString`` / ``string.f64.toString`` 桥接
        ''' </summary>
        <Extension>
        Public Function AnyToString(value As WATSyntax, context As Environment) As WATSyntax
            Select Case value.Type.UnderlyingWATType
                Case WATElements.string
                    ' 已经是字符串了，不需要转换
                    Return value
                Case WATElements.f32, WATElements.f64
                    Return New FunctionInvoke With {
                        .Func = New JavaScriptTranslation(WATType.string) With {.JavaScript = "string.f64.toString"},
                        .Arguments = {CTypeHandle.CDbl(value, context)}
                    }.SetReturnValue(WATType.string)
                Case Else
                    ' i32 / boolean / i64(降级) 等整型值
                    Return New FunctionInvoke With {
                        .Func = New JavaScriptTranslation(WATType.string) With {.JavaScript = "string.i32.toString"},
                        .Arguments = {CTypeHandle.CInt(value, context)}
                    }.SetReturnValue(WATType.string)
            End Select
        End Function
    End Module
End Namespace