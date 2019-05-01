#Region "Microsoft.VisualBasic::78ee317cc130329067d01977137d7770, dictionarytest.vb"

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

    ' Module dictionarytest
    ' 
    '     Sub: addValues
    ' 
    ' /********************************************************************************/

#End Region

Module dictionarytest

    Sub addValues()
        Dim table As New Dictionary(Of String, Double()) From {
            {"A", {56, 7, 56, 7}},
            {"GGGGGG", {353, 53, 48593, 465, 46}}
        }

        table = New Dictionary(Of String, Double())

        table("GG") = {44, 44}
        table.Add("AAAAAAA", {345, 654, 6, 5465445})

        table!XYZ = {89898}

        Dim z As Integer = table!ABC(999)
        Dim zz As Long = table("UI*")(100)

        table.Remove("QQQQQQ")

        Dim v As Double() = table("5678")

        Dim index As String() = table.Keys.ToArray
        Dim matrix As Double()() = table.Values.ToArray

    End Sub
End Module

