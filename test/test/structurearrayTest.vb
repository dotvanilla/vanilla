Imports test.structureArrayElement

Module structurearrayTest

    Sub createarray()

        Dim a As circle() = {New circle With {.radius = 100}, New circle With {.x = 1, .y = .x, .radius = 999}}

        Dim b As rectangle() = {New rectangle With {.x = 1, .y = 1}}

    End Sub

End Module

Namespace structureArrayElement

    Public Structure circle
        Dim x, y As Integer
        Dim radius As Double
    End Structure

    Public Class rectangle

        Public x, y As Integer
        Public w, h As Integer

        Public fill As String = "red"

    End Class
End Namespace