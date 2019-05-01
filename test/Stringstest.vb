#Region "Microsoft.VisualBasic::a636e4642c34a2cdf5b10b7ff9b53c7d, Stringstest.vb"

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

    ' Module Stringstest
    ' 
    '     Function: Hello, Main, World
    ' 
    '     Sub: stringmemberTest
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Module Stringstest

    ' imports console.log api from javascript
    <Extension>
    Public Declare Function Print Lib "console" Alias "log" (text As String) As Integer

    Dim C# = 8888888888888
    Dim a = 99
    Dim b As Integer = 100

    Public Sub stringmemberTest()
        Call Print(C + Hello.Replace("AAA", Nothing).Trim.Length)

        Dim length As Long = Hello.Length
        Dim lenPlus100 = b + length


    End Sub

    Public Function Main() As String
        Dim str As String = Hello() & " " & World()
        Dim format$ = $"let {a} + {b} / {C} = {a + b / C}"

        Call Print(str)
        Call Print(format)

        Return str
    End Function

    Public Function Hello() As String
        Return "Hello"
    End Function

    Public Function World() As String
        Return "World"
    End Function
End Module
