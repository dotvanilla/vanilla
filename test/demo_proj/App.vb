#Region "Microsoft.VisualBasic::6e0a21affb714ac34e9d10d3435cd043, demo_proj\App.vb"

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

    ' Module App
    ' 
    '     Function: RunApp
    ' 
    ' /********************************************************************************/

#End Region

Imports System.ComponentModel.Composition

''' <summary>
''' This code running in webbrowser, working with javascript, not server side.
''' </summary>
''' <remarks>
''' 1. Module marked a ``Export`` attribute means this module is the main module
''' Only the public method in this module will be exports
''' 
''' 2. One project just allowed one module marked as ``Export``
''' </remarks>
<Export> Public Module App

    Dim helloWorld As String = "Hello World!"
    Dim note As String = "This message comes from a VisualBasic.NET application!"
    Dim note2 As String = "WebAssembly it works!"

    ''' <summary>
    ''' VB.NET Web frontend programming demo
    ''' </summary>
    Public Function RunApp() As Integer
        Dim textNode = document.DOMById("text")
        Dim notes = document.DOMById("notes")
        Dim message1 = document.createElement("p")
        Dim message2 = document.createElement("p")

        Call document.setText(textNode, helloWorld)
        Call document.setText(message1, note)
        Call document.setText(message2, note2)

        Call document.appendChild(notes, message1)
        Call document.appendChild(notes, message2)

        Call document.setAttribute(notes, "style", "background-color: lightgrey;")
        Call document.setAttribute(message1, "style", "font-size: 2em; color: red;")
        Call document.setAttribute(message2, "style", "font-size: 5em; color: green;")

        ' display text message on javascript console
        Call console.log("Debug text message display below:")

        Call console.warn(note)
        Call console.info(note2)
        Call console.[error]("Try to display an error message on javascript console...")

        Return 0
    End Function
End Module

