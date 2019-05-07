Imports test.testDifference

Module ClassStructureDifferenceTest

    Sub Main()

        Dim c1 As New circleClass With {.r = 100, .x = 1, .y = 1}
        ' just assign the memory pointer
        Dim c2 = c1

        c1.r = -99999

        ' Console.WriteLine(c1.r)
        ' Console.WriteLine(c2.r)

        Dim s1 As New circleStruct With {.y = 99, .x = .y, .r = .x * .y}
        ' value assign of structre is value copy
        ' this statement will create a new circlestruct object
        Dim s2 = s1

        s1.r = -88888

        '  Console.WriteLine(s1.r)
        '  Console.WriteLine(s2.r)

        ' Console.WriteLine(s1.x)

        Call modifyTest(s1)

        ' Console.WriteLine(s1.x)

        '  Pause()
    End Sub

    Private Sub modifyTest(s As circleStruct)
        s.x = 2222229999
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