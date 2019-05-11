Module loopTest4

    Sub Main()

        Do
            Call test()
        Loop While test
    End Sub

    Private Function test(Optional reverse As Boolean = False) As Boolean
        Return reverse OrElse True
    End Function
End Module
