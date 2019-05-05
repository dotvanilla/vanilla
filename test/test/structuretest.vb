#Region "Microsoft.VisualBasic::e214a4ed9f125f8a4ea1459b5a690911, test\structuretest.vb"

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

    '     Structure circle
    ' 
    ' 
    ' 
    ' Module testStrucutre
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: createValue
    ' 
    ' /********************************************************************************/

#End Region

Imports test.structuretest

Namespace structuretest

    Public Structure circle
        Dim x!, y!
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

        Dim arrayTest = {copy, circle, createValue()}

        Dim a = arrayTest(0)
        Dim b = arrayTest(1)

        arrayTest(0).radius = -100

        print(arrayTest(0).radius)
        print(a.radius)

    End Sub

    Private Function createValue() As circle
        Return New circle With {.id = "99999"}
    End Function

End Module
