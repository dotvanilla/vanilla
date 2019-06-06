Public Module loopOnArray

    Dim rect As rectangle() = {
        New rectangle With {.h = 100, .inner = New circle With {.nameOfX = New name With {.name = "123"}}, .radius = 999, .name = New name With {.name = "first"}},
        New rectangle With {.name = New name With {.name = "second"}}
    }

    Public Declare Sub print Lib "console" Alias "log" (item As rectangle)

    Sub New()
        For i As Integer = 0 To rect.Length - 1
            Call print(rect(i))
        Next
    End Sub
End Module
