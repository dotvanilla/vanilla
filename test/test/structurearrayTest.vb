Imports test.structureArrayElement

Module structurearrayTest

    Dim r = 55
    Dim g = 66
    Dim b = 99

    Sub createarray()

        Dim a As circle() = {
            New circle With {.radius = 100},
            New circle With {.x = 1, .y = .x, .radius = CDbl(999) + rectangle.Max},
            globalCircle,
            createStruct()
        }

    End Sub

    Sub fillArraytest()

        ' syntax 1
        Dim a(100) As circle

        ' syntax2
        a = New circle(20) {}

        For i As Integer = 0 To a.Length - 1
            a(i) = New circle With {.x = 1, .y = 1, .radius = i + 0.1}
        Next

    End Sub

    Dim globalCircle As circle

    Sub New()
        globalCircle = New circle With {.radius = rectangle.Max}
    End Sub

    Private Function createStruct() As circle
        Return New circle
    End Function

    Sub createClassArray()
        Dim b As rectangle() = {
           New rectangle With {.x = 1, .y = 1},
           New rectangle With {.fill = $"rgb({r},{g},{structurearrayTest.b})"}
       }
    End Sub

End Module

Namespace structureArrayElement

    Public Structure circle
        Dim x, y As Integer
        Dim radius As Double
    End Structure

    Public Class rectangle

        Public x, y As Integer
        Public w, h As Integer

        Public fill As String = "red"

        Public Const Max As Long = Long.MaxValue

    End Class
End Namespace