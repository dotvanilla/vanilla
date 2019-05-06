#Region "Microsoft.VisualBasic::655771f0fcb764bb8971c27634e406fb, demo_proj\array.vb"

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

    ' Module array
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Sub: arrayMemberTest, listArray
    ' 
    ' /********************************************************************************/

#End Region

Public Module array

    Dim stringArray As String()

    Sub New()
        ' initialize of the global array
        stringArray = {"sfghnsmfhsdjkfh", "sdjkfhsdjkfhsdjkfhsdjkfhs", "djkfhsdjkfsdfsdfsd"}

    End Sub

    Private Sub arrayMemberTest()
        Dim len = stringArray.Length


    End Sub

    Sub listArray()
        Dim list As New List(Of Double) From {6, 54, 68, 988, 9654, 65, 464}
    End Sub

End Module
