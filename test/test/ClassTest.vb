#Region "Microsoft.VisualBasic::48a45b23233e09a57cc93df867ca857b, test\ClassTest.vb"

    ' Author:
    ' 
    '       xieguigang (I@xieguigang.me)
    '       asuka (evia@lilithaf.me)
    '       wasm project (developer@vanillavb.app)
    ' 
    ' Copyright (c) 2019 developer@vanillavb.app, VanillaBasic(https://vanillavb.app)
    ' 
    ' 
    ' MIT License
    ' 
    ' 
    ' Permission is hereby granted, free of charge, to any person obtaining a copy
    ' of this software and associated documentation files (the "Software"), to deal
    ' in the Software without restriction, including without limitation the rights
    ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ' copies of the Software, and to permit persons to whom the Software is
    ' furnished to do so, subject to the following conditions:
    ' 
    ' The above copyright notice and this permission notice shall be included in all
    ' copies or substantial portions of the Software.
    ' 
    ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    ' SOFTWARE.



    ' /********************************************************************************/

    ' Summaries:

    '     Class CircleModel
    ' 
    ' 
    ' 
    ' Module Runtest
    ' 
    '     Function: globalMemberTest, returnObjecttest
    ' 
    '     Sub: test
    ' 
    ' /********************************************************************************/

#End Region

Imports test.moduleContainer.name1

Namespace moduleContainer.name1
    Public Class CircleModel

        Public nodeName As String = "55555"

        Public x As Integer
        Public y As Integer
        Public radius As Double

    End Class
End Namespace

''' <summary>
''' Class in root namespace
''' </summary>
Public Class RectangleModel

    Public name As String = "[55555555]"

    Public x As Integer
    Public y As Integer
    Public w As Integer
    Public h As Integer

End Class

Public Module Runtest

    Declare Sub print Lib "console" Alias "log" (data As String)

    Dim globalObject As CircleModel
    Dim globalObject2 As RectangleModel

    Sub New()
        globalObject2 = New RectangleModel With {.h = 500, .w = 900}
        globalObject2.name = globalObject.nodeName
    End Sub

    Function globalMemberTest()
        globalObject = returnObjecttest(777)


        Call print($"y of the globalobject is {globalObject.y}")
        ' full name reference test
        Call print($"y of the globalobject is {Runtest.globalObject.y}")

        Return globalObject.x * globalObject.radius
    End Function

    Sub test()

        Dim s As New CircleModel With {.radius = 100001, .x = -1, .y = 1.0009, .nodeName = "{55, 55, 555, 5}"}

        ' object field reference test
        Call print(s.radius)

        Dim c As CircleModel = returnObjecttest()

        ' y should be zero

        Call print($"y is {c.y}")
        Call print($"min distance of two circle center is (a.radius+b.radius) {s.radius + c.radius }")


        ' set field value test
        c.y = -99.999

        Call print($"y after update is {c.y}")

    End Sub

    Private Function returnObjecttest(Optional radius As Long = 99999) As CircleModel
        ' field y is not initialized at here
        Return New CircleModel With {
            .nodeName = "XXXXXXXXXX!",
            .radius = radius,
            .x = radius + 1
        }
    End Function

End Module
