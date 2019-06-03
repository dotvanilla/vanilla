Module numberArray

    Public Function createVector() As Integer()
        Return {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}
    End Function

    Public Function namesVector() As String()
        Return {"AAAAAAAA", "BBBBBBBB", "CCCCCCCCCC", "DDDDDDDDDD", "EE", "FFFFFFF", "GGGGGGGG"}
    End Function

    Public Function structures() As name()
        Dim names = namesVector()

        Return {
            New name With {.name = names(0), .source = 0},
            New name With {.name = names(1), .source = 1},
            New name With {.name = names(2), .source = 2}
        }
    End Function
End Module
