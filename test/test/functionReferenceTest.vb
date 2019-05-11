Module functionReferenceTest

    Sub Main()

        Call Main()

        Dim x As Integer = Math.Exp(9)
        Dim y As Integer = Math.E ^ 9
        Dim y2 = Math.Pow(Math.E, 9)

        Dim g = Global.System.Math.Cos(0.5)

        Dim z = x ^ 2

    End Sub
End Module
