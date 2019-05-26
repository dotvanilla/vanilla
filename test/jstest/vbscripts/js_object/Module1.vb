Module Module1

    Dim test As New rectangle With {.x = Integer.MaxValue, .y = 10}

    Sub New()
        test.name = "test object"
    End Sub

    Public Function getObject() As rectangle
        Return test
    End Function
End Module
