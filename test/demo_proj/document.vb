#Region "Microsoft.VisualBasic::1283cab21450937d42966c05c074fc16, demo_proj\document.vb"

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

    ' Module document
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region


''' <summary>
''' The javascript html document api
''' </summary>
Module document

#Region "JavaScript html document Api"

    ' integer type in these imports javascript api is the object memory pointer
    ' in your webbrowser programs' memory

    Declare Function DOMById Lib "document" Alias "getElementById" (id As String) As Integer
    Declare Function setText Lib "document" Alias "writeElementText" (node As Integer, text As String) As Integer
    Declare Function createElement Lib "document" Alias "createElement" (tagName As String) As Integer
    Declare Function setAttribute Lib "document" Alias "setAttribute" (node As Integer, attr As String, value As String) As Integer
    Declare Function appendChild Lib "document" Alias "appendChild" (parent As Integer, node As Integer) As Integer

#End Region
End Module

