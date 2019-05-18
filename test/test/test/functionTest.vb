#Region "Microsoft.VisualBasic::a544cd4ca28629afc836b6cf20b3550a, test\functionTest.vb"

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

    ' Module optionalParameterTest
    ' 
    '     Sub: calls
    ' 
    ' Module functionTest
    ' 
    '     Function: outputError
    ' 
    '     Sub: calls, extensionFunctiontest, Main
    ' 
    ' Module ExportAPiModule
    ' 
    '     Function: outputError
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices

Module optionalParameterTest

    <Extension>
    Public Declare Function print Lib "console" Alias "log" (info As String, Optional color$ = "green", Optional size& = 99) As Integer

    Sub calls()

        Dim obj$ = "Hello"

        Call obj.print
        Call obj.print(size:=-99)

        Call print(obj & "909090")

        Call "size".print(size:=88)
        Call "not sure".print(size:=77.555, color:="red")

    End Sub

End Module

Module functionTest

    <Extension>
    Public Declare Function print Lib "console" Alias "log" (info As String) As Integer

    Public Function outputError() As Single
        ' this err api should reference to ExportAPiModule
        Call err("this is message")

        Return -0.0001
    End Function

    Public Sub calls()

        ' use default
        Call Main()
        Call Main(obj:=99999.9, args:="Another string value")

        Call outputError()

        Dim x = outputError() + ExportAPiModule.outputError

        ' call method in another module
        Call optionalParameterTest.calls()
        ' call itself
        Call calls()
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
End Module

Module ExportAPiModule


    Public Declare Sub err Lib "console" Alias "error" (message As Object)

    Public Function outputError() As Long
        Call err("this is message")

        Return -10.0001
    End Function

End Module
