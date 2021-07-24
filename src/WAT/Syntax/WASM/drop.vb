Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax.WASM

    ''' <summary>
    ''' drop unused stack value
    ''' </summary>
    ''' <remarks>
    ''' Your imported function $array_push has a return value of i32. You does not consume 
    ''' this value in the then block. Your if block is declared as void or []. You need to 
    ''' consume the i32 (for example with a drop) or change the return type of the if 
    ''' expression to (result i32).
    ''' 
    ''' https://github.com/WebAssembly/wabt/issues/1067
    ''' </remarks>
    Public Class drop : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.void
            End Get
        End Property

        Public Property Value As WATSyntax

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(drop {Value.ToSExpression(env, indent)})"
        End Function

        Private Shared Function CanBeDrop(line As WATSyntax) As Boolean
            If TypeOf line Is ReturnValue Then
                Return False
            ElseIf TypeOf line Is [If] Then
                Return False
            ElseIf TypeOf line Is IfElse Then
                Return False
            ElseIf TypeOf line Is [ElseIf] Then
                Return False
            ElseIf TypeOf line Is SymbolSetValue Then
                Return False
            Else
                Return Not (line.Type Is WATType.void OrElse line.Type.UnderlyingWATType = WATElements.void)
            End If
        End Function

        Public Shared Function AutoDropValueStack(line As WATSyntax) As WATSyntax
            If CanBeDrop(line) Then
                ' https://github.com/WebAssembly/wabt/issues/1067
                '
                ' required a drop if target produce values
                Return New drop With {.Value = line}
            Else
                Return line
            End If
        End Function
    End Class
End Namespace