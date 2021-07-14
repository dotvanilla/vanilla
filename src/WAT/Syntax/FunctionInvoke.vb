Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class FunctionInvoke : Inherits WATSyntax

        ''' <summary>
        ''' the value type of the function returns
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property Type As WATType

        Public Property Func As WATSyntax
        Public Property Arguments As WATSyntax()

        Sub New()

        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Dim arguments As String() = Me.Arguments.Select(Function(a) a.ToSExpression(env, indent)).ToArray
            Dim calls As String = $"(call ${Func.ToSExpression(env, indent)} {arguments.JoinBy(" ")})"

            Return calls
        End Function
    End Class
End Namespace