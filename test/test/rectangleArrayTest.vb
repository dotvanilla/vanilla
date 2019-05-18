Module rectangleArrayTest

    Const i = 99

    Sub New()

        Dim a As Double()() '= New Double(100)() {}

        Dim b = a(i)

        '  a(0) = {545, 68, 456, 564}
        Dim c As Single = b(33)
    End Sub
End Module
