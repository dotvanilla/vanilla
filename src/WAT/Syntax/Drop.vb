Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Your imported function $array_push has a return value of i32. You does not consume 
    ''' this value in the then block. Your if block is declared as void or []. You need to 
    ''' consume the i32 (for example with a drop) or change the return type of the if 
    ''' expression to (result i32).
    ''' 
    ''' https://github.com/WebAssembly/wabt/issues/1067
    ''' </remarks>
    Public Class Drop : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.void
            End Get
        End Property

        Public Property Value As WATSyntax

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(drop {Value.ToSExpression(env, indent)})"
        End Function

        Public Shared Function AutoDropValueStack(line As WATSyntax) As WATSyntax
            If Not TypeOf line Is ReturnValue AndAlso (line.Type Is WATType.void OrElse line.Type.UnderlyingWATType = WATElements.void) Then
                ' https://github.com/WebAssembly/wabt/issues/1067
                '
                ' required a drop if target produce values
                Return New Drop With {.Value = line}
            Else
                Return line
            End If
        End Function
    End Class
End Namespace