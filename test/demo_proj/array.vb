Public Module array

    Dim stringArray As String()

    Sub New()
        ' initialize of the global array
        stringArray = {"sfghnsmfhsdjkfh", "sdjkfhsdjkfhsdjkfhsdjkfhs", "djkfhsdjkfsdfsdfsd"}

    End Sub

    Private Sub arrayMemberTest()
        Dim len = stringArray.Length


    End Sub

    Sub listArray()
        Dim list As New List(Of Double) From {6, 54, 68, 988, 9654, 65, 464}
    End Sub

End Module
