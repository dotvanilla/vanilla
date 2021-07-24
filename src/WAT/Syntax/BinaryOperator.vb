Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator

Namespace Syntax

    Public Class BinaryOperator : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return CheckWATType()
            End Get
        End Property

        Public Property [operator] As String
        Public Property left As WATSyntax
        Public Property right As WATSyntax

        Private Function CheckWATType() As WATType
            Select Case [operator]
                Case "+", "-", "*"
                    Dim left As WATType = Me.left.Type
                    Dim right As WATType = Me.right.Type

                    If left Is right Then
                        Return left
                    ElseIf left.UnderlyingWATType > right.UnderlyingWATType Then
                        Return left
                    Else
                        Return right
                    End If
                Case "AndAlso", "OrElse"
                    Return WATType.boolean
                Case "<<", ">>"
                    Return left.Type
                Case Else
                    Return WATType.boolean
            End Select
        End Function

        Public Overrides Function ToString() As String
            Return $"{Type} ({left} {[operator]} {right})"
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"({SymbolMap.GetOperator(Type, [operator])} {left.ToSExpression(env, indent)} {right.ToSExpression(env, indent)})"
        End Function
    End Class
End Namespace