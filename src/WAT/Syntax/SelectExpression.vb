Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    ''' <summary>
    ''' 三元条件表达式 ``If(condition, trueValue, falseValue)``，
    ''' 映射为 wasm 的 ``(select trueValue falseValue condition)`` 指令。
    ''' </summary>
    Public Class SelectExpression : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return ifTrue.Type
            End Get
        End Property

        Public Property condition As WATSyntax
        Public Property ifTrue As WATSyntax
        Public Property ifFalse As WATSyntax

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(select {ifTrue.ToSExpression(env, indent)} {ifFalse.ToSExpression(env, indent)} {condition.ToSExpression(env, indent)})"
        End Function
    End Class
End Namespace
