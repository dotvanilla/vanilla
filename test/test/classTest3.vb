﻿Imports test.testNamespace

Module classArrayTest

    Dim circles As circle()

    Public Sub initializeArray()
        Dim c2 As New circle With {.radius = 100}

        circles = {New circle With {.x = 1, .y = .x, .z = .x}, c2}
    End Sub

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
    End Class
End Namespace