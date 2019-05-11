Module loopTest4

    Sub Main()

        Do
            Call test()
        Loop While test
    End Sub

    Private Function test() As Boolean
        Return True
    End Function
End Module
