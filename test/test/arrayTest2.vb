Module arrayTest2

    Dim data As Double()

    Sub New()
        data = New Double() {24, 23, 424, 2423, 4534, 5353, 55, 55, 55, 55, 5555, 5}
    End Sub

    Declare Function print Lib "console" Alias "log" (x As Double)

    Sub readTest()
        For i As Integer = 0 To data.Length - 1
            print(data(i))
        Next
    End Sub

    Sub setValueTest(x As Integer)

        data(x + 1) = x * 2

        print(data(x * 99))

    End Sub

End Module
