Public Class EnumSymbol : Inherits WATSyntax

    Public Overrides ReadOnly Property Type As WATType
    Public ReadOnly Property Name As String
    Public ReadOnly Property Members As Dictionary(Of String, LiteralValue)

    Sub New()

    End Sub

    Public Overrides Function ToSExpression(indent As String) As String
        Throw New NotImplementedException()
    End Function
End Class
