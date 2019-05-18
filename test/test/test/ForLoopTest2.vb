Module ForLoopTest2

    Sub Main()

        Dim a As Double() = New Double(1000) {}

        For i As Integer = 0 To 100
            a(i) = i ^ 2
        Next

        For i As Integer = 1000 To 1 Step -2
            a(i) = a(i) ^ (1 / 2)
        Next
    End Sub
End Module
