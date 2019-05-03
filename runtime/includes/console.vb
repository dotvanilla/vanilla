#Region "Microsoft.VisualBasic::2e1bb20867db55bdf773ac47328f794b, includes\console.vb"

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

    ' Module console
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Public Module console

    Public Declare Sub log Lib "console" Alias "log" (obj As Object)
    Public Declare Sub warn Lib "console" Alias "warn" (obj As Object)
    Public Declare Sub info Lib "console" Alias "info" (obj As Object)
    Public Declare Sub [error] Lib "console" Alias "error" (obj As Object)
    Public Declare Sub table Lib "console" Alias "table" (obj As Object)
    Public Declare Sub trace Lib "console" Alias "trace" (obj As Object)
    Public Declare Sub debug Lib "console" Alias "debug" (obj As Object)

End Module
