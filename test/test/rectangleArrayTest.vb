Module rectangleArrayTest

    Sub New()

        Dim d As Integer()
        Dim a As Double()() = New Double(100)() {}

        Dim b = a(3)

        '  a(0) = {545, 68, 456, 564}
        Dim c As Single = b(33)

        d(88) = b(5)
    End Sub
End Module
