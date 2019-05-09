#Region "Microsoft.VisualBasic::9b0c752978d83cbe94051a26a5f63192, test\DeclareTest.vb"

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

    ' Module DeclareTest
    ' 
    '     Function: localDeclareTest
    ' 
    ' /********************************************************************************/

#End Region

Module DeclareTest

    Const INF As Integer = Integer.MaxValue

    Dim MN As Long = -99
    Dim E%, F&
    Dim L As Single = 90
    Dim A, B, C As Double, GG As Single, Z&
    Dim uniqueGlobalName As String

    Private Function localDeclareTest() As Single
        Dim XYY! = 888999 + DeclareTest.A
        Dim MN2 As Long = -99, L As Single = 90
        Dim A, B, C As Double
        Dim GG As Single = DeclareTest.GG
        Dim Z! = DeclareTest.Z * 99
        Dim E%, F&

        C = 5000

        ' set global test 1
        DeclareTest.C = C * (DeclareTest.C + 1)
        ' set global test 2
        uniqueGlobalName = $"Hello: {C}"

        ' this should be true
        Dim globalNameRefere = (MN + DeclareTest.MN) = MN * 2

        If Not globalNameRefere Then
            ' this value should never returns
            Return -100
        End If

        Return (MN / MN2 + L + (A * DeclareTest.B) + B + C) * GG / Z * E * F / CLng(DeclareTest.C)
    End Function

End Module
