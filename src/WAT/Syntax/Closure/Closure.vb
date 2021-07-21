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

        Friend Shared Function InternalBlock([then] As Closure, env As Environment, indent As String) As String
            Return [then].multipleLines _
                .SafeQuery _
                .Select(Function(line)
                            Return indent & line.ToSExpression(env, indent)
                        End Function) _
                .JoinBy(ASCII.LF)
        End Function
    End Class
End Namespace