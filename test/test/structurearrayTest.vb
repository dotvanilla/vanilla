Imports test.structureArrayElement

Module structurearrayTest

    Dim r = 55
    Dim g = 66
    Dim b = 99

    Sub createarray()

        Dim a As circle() = {New circle With {.radius = 100}, New circle With {.x = 1, .y = .x, .radius = 999}}

        Dim b As rectangle() = {New rectangle With {.x = 1, .y = 1}, New rectangle With {.fill = $"rgb({r},{g},{structurearrayTest.b})"}}

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