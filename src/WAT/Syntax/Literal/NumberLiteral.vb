
Imports VanillaBasic.WebAssembly.CodeAnalysis
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

        Sub New(number As Object, type As WATType)
            Call MyBase.New(number, type)
        End Sub
    End Class
End Namespace