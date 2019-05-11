#Region "Microsoft.VisualBasic::59807882fe53a93e94f6c25c4286e2c7, test\structurearrayTest.vb"

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

    ' Module structurearrayTest
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    '     Function: createStruct
    ' 
    '     Sub: createarray, createClassArray, fillArraytest
    ' 
    '     Structure circle
    ' 
    ' 
    ' 
    '     Class rectangle
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports test.structureArrayElement

Module structurearrayTest

    Dim r = 55
    Dim g = 66
    Dim b = 99

    Sub createarray()

        Dim a As circle() = {
            New circle With {.radius = 100},
            New circle With {.x = 1, .y = .x, .radius = CDbl(999) + rectangle.Max},
            globalCircle,
            createStruct()
        }

    End Sub

    Sub fillArraytest()

        ' syntax 1
        Dim a(100) As circle

        ' syntax2
        a = New circle(20) {}

        For i As Integer = 0 To a.Length - 1
            a(i) = New circle With {.x = 1, .y = 1, .radius = i + 0.1}
        Next

    End Sub

    Dim globalCircle As circle

    Sub New()
        globalCircle = New circle With {.radius = rectangle.Max}
    End Sub

    Private Function createStruct() As circle
        Return New circle
    End Function

    Sub createClassArray()
        Dim b As rectangle() = {
           New rectangle With {.x = 1, .y = 1},
           New rectangle With {.fill = $"rgb({r},{g},{structurearrayTest.b})"}
       }
    End Sub

End Module

Namespace structureArrayElement

    Public Structure circle
        Dim x, y As Integer
        Dim radius As Double
    End Structure

    Public Class rectangle

        Public x, y As Integer
        Public w, h As Integer

        Public fill As String = "red"

        Public Const Max As Long = Long.MaxValue

    End Class
End Namespace
