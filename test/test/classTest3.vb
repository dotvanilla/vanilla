Imports test.testNamespace

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