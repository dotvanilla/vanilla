Public MustInherit Class WATSyntax

    Public MustOverride ReadOnly Property Type As WATType

    Public MustOverride Function ToSExpression(indent As String) As String

End Class
