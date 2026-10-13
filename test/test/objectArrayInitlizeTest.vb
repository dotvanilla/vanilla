Imports test.objectarrayInitModel

Module objectArrayInitlizeTest

    Dim list(100) As circle

    Sub New()
        For i As Integer = 0 To list.Length - 1
            list(i) = New circle With {.x = VBMath.Rnd, .y = VBMath.Rnd}
        Next
    End Sub

End Module

Namespace objectarrayInitModel

    Public Class circle
        Public x, y As Single
    End Class
End Namespace