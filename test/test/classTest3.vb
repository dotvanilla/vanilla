Imports test.testNamespace

Module classArrayTest

    Dim circles As circle()
    Dim str As String = "SSSSSS"

    Public Sub initializeArray()
        Dim c2 As New circle With {.radius = 100}

        circles = {New circle With {.x = 1, .y = .x, .z = .x}, c2, produceObject()}
    End Sub

    Private Function produceObject() As circle
        Return New circle With {.x = 1, .radius = .x * .y * .z, .id = "AAAAAAAAAA"}
    End Function

End Module

Module classTest3

    Dim circle As circle

    Sub New()
        ' radius is default initalize value 999 
        circle = New circle With {
            .x = 1,
            .y = .x,
            .z = .x + .y
        }
    End Sub

End Module


Namespace testNamespace

    Public Class circle
        Public x, y, z As Single
        Public radius As Double = 999
        Public id As String = "ABC"
    End Class
End Namespace