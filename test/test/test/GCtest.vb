Imports test.GCObjects

Module GCtest

    Sub New()
        Dim c As New circle With {.x = 1, .y = 11, .radius = 999}
    End Sub
End Module

Namespace GCObjects

    Public Class circle
        Public x, y As Integer
        Public radius As Single
    End Class
End Namespace