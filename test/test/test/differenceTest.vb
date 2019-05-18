#Region "Microsoft.VisualBasic::d6647dfbe428bd9300e5f60abd9fea04, test\differenceTest.vb"

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

    ' Module ClassStructureDifferenceTest
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Sub: Main, modifyTest
    ' 
    '     Class circleClass
    ' 
    ' 
    ' 
    '     Structure circleStruct
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

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
