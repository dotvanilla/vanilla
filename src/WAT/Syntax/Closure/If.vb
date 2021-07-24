Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class [If] : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.void
            End Get
        End Property

        Public Property condition As BooleanLogical
        Public Property [then] As Closure

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"
    (if {condition.ToSExpression(env, indent)} 
        (then
            {Closure.InternalBlock([then], env, "        ")}
        ) 
    )"
        End Function
    End Class

    Public Class IfElse : Inherits [If]

        Public Property [else] As Closure

        Private Function getElseCode(env As Environment, indent As String) As String
            Return $"(else
        {Closure.InternalBlock(Me.else, env, indent & "        ")}
    )"
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"
    (if {condition.ToSExpression(env, indent)} 
        (then
            {Closure.InternalBlock([then], env, "        ")}
        ) {getElseCode(env, indent)}
    )"
        End Function

    End Class

    Public Class [ElseIf] : Inherits IfElse

        Public Property elseIfs As [If]()

    End Class

End Namespace