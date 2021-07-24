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

        Sub New(func As FunctionDeclare, ParamArray arguments As WATSyntax())
            Me.Func = New SymbolReference(func.FullName)
            Me.Arguments = arguments
        End Sub

        ''' <summary>
        ''' set the result type of the target function
        ''' </summary>
        ''' <param name="rtvl"></param>
        ''' <returns></returns>
        Public Function SetReturnValue(rtvl As WATType) As FunctionInvoke
            _Type = rtvl
            Return Me
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Dim arguments As String() = Me.Arguments _
                .Select(Function(a)
                            Return a.ToSExpression(env, indent)
                        End Function) _
                .ToArray
            Dim calls As String = $"(call {Func.ToSExpression(env, indent)} {arguments.JoinBy(" ")})"

            Return calls
        End Function
    End Class
End Namespace