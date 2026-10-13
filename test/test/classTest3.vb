#Region "Microsoft.VisualBasic::ea826dfc471256ad41cb8a64605b652a, test\test\classTest3.vb"

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

    ' Module classArrayTest
    ' 
    '     Function: produceObject
    ' 
    '     Sub: initializeArray
    ' 
    ' Module classTest3
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    '     Class circle
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports test.testNamespace

Module classArrayTest

    Dim circles As circle()
    Dim str As String = "SSSSSS"

    Private Function produceObject() As circle
        Return New circle With {.x = 1, .radius = .x * .y * .z, .id = "AAAAAAAAAA"}
    End Function

    Public Sub initializeArray()
        Dim c2 As New circle With {.radius = 100}

        circles = {New circle With {.x = 1, .y = .x, .z = .x}, c2, produceObject()}
    End Sub

End Module

Module classTest3

    Dim circle As circle

    Sub New()
        ' radius is default initalize value 999 
        circle = New circle With {
            .x = 1,
            .y = .x,
            .z = .x + .y
        }
    End Sub

End Module


Namespace testNamespace

    Public Class circle
        Public x, y, z As Single
        Public radius As Double = 999
        Public id As String = "ABC"
    End Class
End Namespace
