#Region "Microsoft.VisualBasic::e20b65e6c062d877284869c4f950d0c2, Modulemethod_test.vb"

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

    ' Module Modulemethod_test
    ' 
    '     Function: arraytypeInferTest, test, ThisIsAInternalFunction
    ' 
    '     Sub: calls
    ' 
    ' Module module2
    ' 
    '     Function: test, ThisIsAInternalFunction
    ' 
    '     Sub: Runapp
    ' 
    ' /********************************************************************************/

#End Region

Module Modulemethod_test

    Public Function arraytypeInferTest() As Long()
        Return {2342, 34, 322, 343}
    End Function

    ''' <summary>
    ''' this function overloads with <see cref="module2.test(String)"/>
    ''' </summary>
    ''' <returns></returns>
    Public Function test()
        Return -9999
    End Function

    Public Sub calls()
        Call test()
        Call module2.test($"34546734853{test()}8sdjkfsdhfsdfsdf")
        Call ThisIsAInternalFunction()
    End Sub

    Private Function ThisIsAInternalFunction() As Object
        Return "This is a internal function"
    End Function

End Module

Module module2

    Private Function ThisIsAInternalFunction() As Object
        Return "This is a internal function too"
    End Function

    Public Sub Runapp()
        Call Modulemethod_test.calls()
        Call ThisIsAInternalFunction()
    End Sub

    Public Function test(gg As String) As String()
        Return {gg, gg & "ddddd"}
    End Function
End Module
