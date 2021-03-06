﻿#Region "Microsoft.VisualBasic::abbc4bc6b387aa8cea1a1392c7f0f35b, test\test\rectangleArrayTest2.vb"

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

    ' Module rectangleArrayTest2
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    ' Class MyCircle
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Module rectangleArrayTest2

    Sub New()

        Dim d As Integer()
        Dim a As MyCircle()() = New MyCircle(100 - 99)() {}

        Dim b = a(3)

        '  a(0) = {545, 68, 456, 564}
        Dim c As Single = b(33).r

        d(88) = b(5).x + b(0).y
    End Sub
End Module

Public Class MyCircle
    Public x, y, r As Double
End Class
