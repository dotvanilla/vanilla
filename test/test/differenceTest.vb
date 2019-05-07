Imports test.testDifference

Module ClassStructureDifferenceTest

    Sub Main()

        Dim c1 As New circleClass With {.r = 100, .x = 1, .y = 1}
        ' just assign the memory pointer
        Dim c2 = c1

        Dim s1 As New circleStruct With {.y = 99, .x = .y, .r = .x * .y}
        ' value assign of structre is value copy
        ' this statement will create a new circlestruct object
        Dim s2 = s1

    End Sub


    Sub New()
        Call Main()
    End Sub
End Module

Namespace testDifference

    Public Class circleClass

        Public x, y As Double
        Public r As Integer

    End Class

    Public Structure circleStruct
        Public x, y As Double
        Public r As Integer
    End Structure

End Namespace