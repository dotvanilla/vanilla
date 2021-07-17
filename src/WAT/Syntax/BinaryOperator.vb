Imports VanillaBasic.WebAssembly.CodeAnalysis

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
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace