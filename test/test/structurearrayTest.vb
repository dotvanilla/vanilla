Imports test.structureArrayElement

Module structurearrayTest

    Sub createarray()

        Dim a As circle() = {New circle With {.radius = 100}, New circle With {.x = 1, .y = .x, .radius = 999}}

    End Sub

End Module

Namespace structureArrayElement

    Public Structure circle
        Dim x, y As Integer
        Dim radius As Double
    End Structure
End Namespace