Imports test.nestedTypes

Module nestedTest

    Dim line As line

    Sub New()

        line = New line With {.a = New point With {.x = 99, .y = 88}}

        line.b = New point With {.x = 100, .y = 50000, .tag = "ABC"}

    End Sub

    Private Function newPoint() As point
        Return New point With {.x = -1, .y = -1}
    End Function

    Sub copytest()
        line = New line With {.a = newPoint()}
    End Sub
End Module

Namespace nestedTypes

    Public Structure point
        Public x, y As Single
        Public tag As String
    End Structure

    Public Class line
        Public a, b As point
        Public name As String = "this is a line"
    End Class
End Namespace