#Region "Microsoft.VisualBasic::7e47b481170c691231a1be7944f998ce, ClassTest.vb"

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

' Class ClassTest
' 
'     Properties: arrayProperty
' 
'     Function: ToString
' 
' Module Runtest
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

Public Module Runtest

    Declare Sub print Lib "console" Alias "log" (data As Object)

    Sub test()

        Dim s As New CircleModel With {.radius = 100001, .x = -1, .y = 1.0009, .nodeName = "{55, 55, 555, 5}"}

        ' object field reference test
        Call print(s.radius)

    End Sub

End Module
