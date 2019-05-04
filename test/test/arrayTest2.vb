#Region "Microsoft.VisualBasic::cf7979c9df7b6232629e37267fb95937, test\arrayTest2.vb"

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

    ' Module arrayTest2
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    '     Function: readTest, returnArrayTest
    ' 
    '     Sub: setValueTest
    ' 
    ' /********************************************************************************/

#End Region

Module arrayTest2

    Dim data As Double()

    Sub New()
        data = New Double() {24, 23, 424, 2423, 4534, 5353, 55, 55, 55, 55, 5555, 5}
    End Sub

    Declare Function print Lib "console" Alias "log" (x As Double)

    Function returnArrayTest() As Single()
        Dim x As Double = data(1)

        Return {x, 0, 35, 78345, 34, 534, 53, data.Length}
    End Function

    Function readTest() As Single

        Dim x As Long = data(9999)



        For i As Integer = 0 To data.Length - 1
            print(data(i))
        Next

        Return x
    End Function

    Sub setValueTest(x As Integer)

        data(x + 1) = x * 2 / (data.Length - 1)

        print(data(x * 99))

    End Sub

End Module

