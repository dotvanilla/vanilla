Imports test.nestedTypes

Module nestedTest

    Dim line As line

    Sub New()

        line = New line With {.a = New point With {.x = 99, .y = 88}}

        line.b = New point With {.x = 100, .y = 50000}

    End Sub
End Module

Namespace nestedTypes

    Public Class point
        Public x, y As Single
    End Class

    Public Class line
        Public a, b As point
    End Class
End Namespace