Imports test.arrayObjects

Module objectArrayTest

    Dim ps As Point() = {New Point With {.x = 100, .y = 500}, New Point With {.x = 1, .y = .x * 999, .name = "Hello world"}}

End Module

Namespace arrayObjects

    Public Class Point
        Public x, y As Double
        Public name As String = "ABC"
    End Class

End Namespace
