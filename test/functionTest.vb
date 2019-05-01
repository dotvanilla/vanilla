#Region "Microsoft.VisualBasic::1f9b32ff2092269cccc46ba830af4ef4, functionTest.vb"

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

    ' Module functionTest
    ' 
    '     Function: outputError
    ' 
    '     Sub: calls, extensionFunctiontest, Main
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Module functionTest

    <Extension>
    Declare Function print Lib "console" Alias "log" (info As String) As Integer
    Declare Sub err Lib "console" Alias "error" (message As Object)

    Public Sub calls()

        ' use default
        Call Main()
        Call Main(obj:=999999, args:="Another string value")

        Call outputError()

    End Sub

    Public Sub extensionFunctiontest()
        Call "345566777777".print
    End Sub


    Public Sub Main(Optional args As String = "This is the optional parameter value", Optional obj As Integer = -100, Optional f As Boolean = True)
        Call print(False = f)
        Call print(False <> f)
        Call print(Not (False = f))
        Call print(Not f)
        Call print(args)
        Call print(obj)
        Call print(True)
        Call print(args Is Nothing)
    End Sub

    Public Function outputError() As Single
        Call err("this is message")

        Return -0.0001
    End Function

End Module
