#Region "Microsoft.VisualBasic::6178c29893dfabda4cc96e23e9366b7b, Type\Types.vb"

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

    ' Module Types
    ' 
    '     Properties: [boolean], [string], primitiveTypes
    ' 
    '     Function: ParseAliasName
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Collection

Public Module Types

    Public ReadOnly Property [string] As New TypeAbstract(TypeAlias.string, {})
    Public ReadOnly Property [boolean] As New TypeAbstract(TypeAlias.boolean, {})

    Public ReadOnly Property primitiveTypes As Index(Of TypeAlias) = {
        TypeAlias.f32,
        TypeAlias.f64,
        TypeAlias.i32,
        TypeAlias.i64
    }

    Public Function ParseAliasName(fullName As String) As TypeAlias
        Select Case fullName
            Case "i32", "System.Int32"
                Return TypeAlias.i32
            Case "i64", "System.Int64"
                Return TypeAlias.i64
            Case "f32", "System.Single"
                Return TypeAlias.f32
            Case "f64", "System.Double"
                Return TypeAlias.f64
            Case "boolean", "System.Boolean"
                Return TypeAlias.boolean
            Case "void", "System.Void"
                Return TypeAlias.void
            Case "any", "System.Object"
                Return TypeAlias.any
            Case "intptr", "System.IntPtr"
                Return TypeAlias.intptr
            Case Else
                Throw New NotImplementedException(fullName)
        End Select
    End Function
End Module

