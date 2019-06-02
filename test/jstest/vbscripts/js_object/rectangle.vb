Public Structure name

    Public Const demoNameString As String = "99999999"

    Public name As String
    ' Public array As Double()
    Public source As Integer
End Structure

Public Class circle

    Public x, y As Single
    Public r As Integer = 100

    Public nameOfX As name
    Public nameOfY As name
End Class

Public Class rectangle

    Public x, y As Double
    Public w As Integer = 1000
    Public h As Integer = 1000
    Public name As String
    Public radius As Single = -99

    Public inner As circle

End Class