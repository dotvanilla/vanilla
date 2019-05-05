Imports test.structuretest

Namespace structuretest

    Public Structure circle
        Dim x, y!
        Dim radius!
        Dim id As String
    End Structure
End Namespace

Module testStrucutre

    Declare Sub print Lib "console" Alias "log" (data As String, Optional color$ = "blue")

    Sub New()

        Dim circle As New circle With {.id = "A", .x = 1, .y = 2}

        ' for structure
        ' each value assign will make a memory copy
        Dim copy = circle

        copy.y = 100
        circle.y = 500


        Call print(copy.y)
        Call print(circle.y)

        Dim arrayTest = {copy, circle}

        Dim a = arrayTest(0)
        Dim b = arrayTest(1)

        arrayTest(0).radius = -100

        print(arrayTest(0).radius)
        print(a.radius)

    End Sub

End Module