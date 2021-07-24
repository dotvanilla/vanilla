Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class UnaryOperator : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.GetElementType([operator].Split("."c).First)
            End Get
        End Property

        Public Property [operator] As String
        Public Property value As WATSyntax

        Sub New(opName As String, expr As WATSyntax)
            Me.value = expr
            Me.operator = opName
        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"({[operator]} {value.ToSExpression(env, indent)})"
        End Function
    End Class
End Namespace