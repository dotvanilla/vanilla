Module arrayTest2

    Dim data As Double()

    Sub New()
        data = New Double() {24, 23, 424, 2423, 4534, 5353, 55, 55, 55, 55, 5555, 5}
    End Sub

    Declare Function print Lib "console" Alias "log" (x As Double)

    Function returnArrayTest() As Single()
        Dim x As Double = data(1)

        Return {x, 0, 35, 78345, 34, 534, 53}
    End Function

    Function readTest() As Single

        Dim x As Long = data(9999)



        For i As Integer = 0 To data.Length - 1
            print(data(i))
        Next

        Return x
    End Function

    Sub setValueTest(x As Integer)

        data(x + 1) = x * 2

        print(data(x * 99))

    End Sub

End Module
