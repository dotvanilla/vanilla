Module loopTest3

    Sub Main()
        Do

            If assert() > 9 Then
                Exit Do
            End If

        Loop
    End Sub


    Private Function assert() As Double
        Return 8
    End Function
End Module
