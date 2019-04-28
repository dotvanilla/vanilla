Public Class ClassTest

    Dim fieldName As String

    Public Property arrayProperty As Double()

    Public Overrides Function ToString() As String
        Dim s As String = ""

        For i As Integer = 0 To arrayProperty.Length - 1
            s = s & " " & arrayProperty(i)
        Next

        Return $"{fieldName}: {s}"
    End Function

End Class


Public Module Runtest

    Declare Sub print Lib "console" Alias "log" (data As Object)

    Sub test()

        Dim s As New ClassTest With {.arrayProperty = {55, 55, 555, 5}}

        Call print(s.arrayProperty)
        Call print("Object instance method test: " & s.ToString)

    End Sub

End Module