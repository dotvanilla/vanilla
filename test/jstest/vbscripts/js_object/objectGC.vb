Module objectGC

    Dim rect As New rectangle With {.x = Integer.MaxValue, .y = 10}

    Sub New()
        rect.name = New name With {
            .name = "test object",
            .source = 888888888
        }
        rect.inner = newCircle()
    End Sub

    Dim cx As Double = 1000

    Public Function newCircle() As circle
        cx *= 2
        Return New circle With {
            .x = cx, .y = 9999,
            .nameOfX = newStruct(),
            .nameOfY = New name With {.name = "directly create a structurte"}
        }
    End Function

    Private Function newStruct() As name
        Return New name With {
        .name = "this is a structure!",
        .source = 111111
        }
    End Function

    Public Function getObject() As rectangle
        Return rect
    End Function
End Module
