Imports test.arrayObjects

Module objectArrayTest

    Dim ps As Point() = {
        New Point With {.x = 100, .y = 500},
        New Point With {.x = 1, .y = .x * 999, .name = "Hello world"}
    }

    Declare Function dump Lib "console" Alias "log" (obj As Point)
    Declare Function println Lib "console" Alias "log" (info As String)

    Sub New()
        Dim item As Point

        For i As Integer = 0 To ps.Length - 1
            item = ps(i)

            Call println(info:=$"#{i} addressOf:=&{item}, name:={item.name}")
            Call dump(item)
        Next

    End Sub

    Public Function GetPoints() As Point()
        Return ps
    End Function

End Module

Namespace arrayObjects

    Public Class Point
        Public x, y As Double
        Public name As String = "ABC"
    End Class

End Namespace
