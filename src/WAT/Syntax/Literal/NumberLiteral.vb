
Imports stdNum = System.Math

Namespace Syntax.Literal

    Public Class NumberLiteral : Inherits LiteralValue

        Public ReadOnly Property Sign As Integer
            Get
                Return stdNum.Sign(CDbl(Value))
            End Get
        End Property

        Sub New(i As Integer)
            Call MyBase.New(i)
        End Sub
    End Class
End Namespace