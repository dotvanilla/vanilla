Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class Closure : Inherits WATSyntax

        Public Property locals As DeclareLocal()
        Public Property guid As String = App.GetNextUniqueName("WASMClosure_br_")
        Public Property multipleLines As WATSyntax()

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.void
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function

        ''' <summary>
        ''' line joins
        ''' </summary>
        ''' <param name="block"></param>
        ''' <param name="env"></param>
        ''' <param name="indent"></param>
        ''' <returns></returns>
        Friend Shared Function InternalBlock(block As Closure, env As Environment, indent As String) As String
            Return block.multipleLines _
                .SafeQuery _
                .Select(Function(line)
                            ' 嵌套代码块之中的表达式语句同样需要自动 drop，否则会造成堆栈不平衡
                            Return indent & WASM.drop.AutoDropValueStack(line).ToSExpression(env, indent)
                        End Function) _
                .JoinBy(ASCII.LF)
        End Function
    End Class
End Namespace