Imports test.arrayObjects

Module objectArrayTest

    Declare Sub dump Lib "console" Alias "log" (obj As Point)
    Declare Sub println Lib "console" Alias "log" (info As String)
    Declare Sub warning Lib "console" Alias "warn" (info As String)
    Declare Sub viewLine Lib "console" Alias "log" (line As line)

    Dim ps As Point() = {
        New Point With {.x = 100, .y = 500},
        New Point With {.x = 1, .y = .x * 999, .name = "Hello world"},
        New Point With {.name = "b:Point"},
        New Point With {.name = "c:Point"}
    }

    Dim lines As line()

    ''' <summary>
    ''' no bug
    ''' </summary>
    Sub New()
        Dim item As Point

        For i As Integer = 0 To ps.Length - 1
            item = ps(i)

            Call println(info:=$"#{i} addressOf:=&{item}, name:={item.name}")
            Call dump(item)
        Next

        lines = {
            New line With {.a = ps(0), .b = ps(1)},
            New line With {.a = ps(2), .b = ps(3)}
        }
    End Sub

    ''' <summary>
    ''' no temp variable when access array element
    ''' </summary>
    Public Sub printArray()
        Call modifyArray()

        For i As Integer = 0 To ps.Length - 1
            Call println(info:=$"#{i} addressOf:=&{ps(i)}, name:={ps(i).name}")
            Call println(info:=$"   [x,y]:=[{ps(i).x},{ps(i).y}]")
            Call dump(ps(i))
        Next

        For j As Integer = 0 To lines.Length - 1
            Call viewLine(lines(j))
        Next
    End Sub

    Private Sub modifyArray()
        For i As Integer = 0 To ps.Length - 1
            ps(i).x = VBMath.Rnd
            ps(i).y = VBMath.Rnd * 100

            Call warning($"Assign a random [x,y]:=[{ps(i).x},{ps(i).y}]")
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

    Public Class line
        Public a, b As Point
    End Class

End Namespace
