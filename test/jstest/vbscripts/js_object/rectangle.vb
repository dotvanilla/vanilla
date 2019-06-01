Public Class structTest

    Public name As String
    ' Public array As Double()

End Class

Public Class circle

    Public x, y As Single
    Public r As Integer = 100

    Public struct As structTest

End Class

Public Class rectangle

    Public x, y As Double
    Public w As Integer = 1000
    Public h As Integer = 1000
    Public name As String
    Public radius As Single = -99

    Public inner As circle

End Class