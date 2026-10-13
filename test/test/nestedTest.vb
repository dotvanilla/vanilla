Imports test.nestedTypes

Module nestedTest

    Dim line As line

    Sub New()

        line = New line With {.a = New point With {.x = 99, .y = 88}}

        line.b = New point With {.x = 100, .y = 50000, .tag = "ABC"}

    End Sub

    Public Function getArray() As line()
        Return {
            New line With {.a = newPoint(), .b = New point With {.tag = "GG"}},
            New line With {.b = New point, .a = newPoint(), .name = New [nameOf] With {.name = "#2", .source = 2}}
        }
    End Function

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
        Public name As [nameOf]
    End Class

    Public Class [nameOf]
        Public name As String = "this is a line"
        Public source As Integer = -99999
    End Class
End Namespace