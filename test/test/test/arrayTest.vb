﻿#Region "Microsoft.VisualBasic::b6d1353b4078c5307f6dd2922c3bb997, test\test\arrayTest.vb"

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

    ' Module arrayTest
    ' 
    '     Constructor: (+1 Overloads) Sub New
    ' 
    '     Function: arrayLoop, createArray, testListAdd
    ' 
    '     Sub: arrayDeclares
    ' 
    ' /********************************************************************************/

#End Region

Module arrayTest

    Declare Function debug Lib "console" Alias "log" (any As String()) As Integer
    Declare Function print Lib "console" Alias "log" (any As String) As Integer


    Declare Sub log Lib "console" Alias "log" (any As Object)

    Public arrayLength = 9999

    Dim xxl As List(Of String)
    Dim ints2 As Integer()

    Sub New()
        ints2 = {456, 2, 387, 456, 4641, 231, 23, 13, 1, 2.3}
    End Sub

    Public Function arrayLoop()
        ' convert to string and display
        Dim ints As Integer() = {1, 2, 3, 4, 5, 6, 7, 88}

        ints2 = ints

        Call print(ints2.Length)

        For i As Integer = 0 To ints.Length
            Call print(ints(i))
        Next
    End Function

    Public Function testListAdd()
        Dim l As List(Of String) = New List(Of String) From {"Hello", "World"}

        xxl = l

        Call l.Add("yes")
        Call print(l(2))

        Call log(l)
    End Function

    Public Sub arrayDeclares()
        Dim syntax2 As Double() = New Double() {23, 42, 42, 4}
        Dim syntax3(arrayLength - 5) As Double
        Dim len = 999
        Dim syntax1 As Double() = New Double(len - 1) {}

        Call log(syntax2)
        Call log(syntax3)
        Call log(syntax1)
    End Sub

    Public Function createArray()
        Dim str As String() = {"333333", "AAAAA", "XXXXX", "534535", "asdajkfsdhjkf"}
        Dim strAtFirst$ = str(0)

        Call debug(str)
        Call print(str(3))

        str(4) = "Hello world"

        Call debug(str)
        Call print(str(4))

        Call log(str)
    End Function
End Module
