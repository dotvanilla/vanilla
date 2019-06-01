Module Module1

    Dim test As New rectangle With {.x = Integer.MaxValue, .y = 10}

    Sub New()
        test.name = "test object"
        test.inner = newCircle()
    End Sub

    Private Function newCircle() As circle
        Return New circle With {.x = 100, .y = 9999, .struct = newStruct()}
    End Function

    Private Function newStruct() As structTest
        Return New structTest With {
            .array = {1, 2, 3, 4, 5},
            .name = "this is a structure!"
        }
    End Function

    Public Function getObject() As rectangle
        Return test
    End Function
End Module
