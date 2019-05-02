#Region "Microsoft.VisualBasic::cc35c9421bcc4046cbb1aed75cfc3352, DeclareHelpers.vb"

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

' Module DeclareHelpers
' 
'     Function: isExportObject, (+3 Overloads) objectName, (+4 Overloads) param
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Wasm.TypeInfo

Module DeclareHelpers

    <Extension>
    Friend Function isExportObject(method As MethodStatementSyntax) As Boolean
        If method.Modifiers.Count = 0 Then
            ' by default is public in VB.NET
            Return True
        Else
            For i As Integer = 0 To method.Modifiers.Count - 1
                If method.Modifiers _
                    .Item(i) _
                    .ValueText _
                    .TextEquals("Public") Then

                    Return True
                End If
            Next

            Return False
        End If
    End Function

    <Extension>
    Public Function param(name$, type$) As NamedValue(Of TypeAbstract)
        Return New NamedValue(Of TypeAbstract) With {
            .Name = name,
            .Value = New TypeAbstract(type)
        }
    End Function

    <Extension>
    Public Function param(name$, type As TypeAbstract) As NamedValue(Of TypeAbstract)
        Return New NamedValue(Of TypeAbstract) With {
            .Name = name,
            .Value = type
        }
    End Function

    <Extension>
    Public Function param(name$, type As TypeAlias) As NamedValue(Of TypeAbstract)
        Return New NamedValue(Of TypeAbstract) With {
            .Name = name,
            .Value = New TypeAbstract(type)
        }
    End Function

    ''' <summary>
    ''' S-Expression of the function parameter
    ''' </summary>
    ''' <param name="a"></param>
    ''' <returns></returns>
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Friend Function param(a As NamedValue(Of TypeAbstract)) As String
        Return $"(param ${a.Name} {a.typefit})"
    End Function

    <Extension>
    Friend Function objectName(name As IdentifierNameSyntax) As String
        Return name.Identifier.ValueText.Trim("["c, "]"c)
    End Function

    <Extension>
    Friend Function objectName(name As SimpleNameSyntax) As String
        Return name.Identifier.ValueText.Trim("["c, "]"c)
    End Function

    <Extension>
    Friend Function objectName(name As SyntaxToken) As String
        Return name.Text.Trim("["c, "]"c)
    End Function
End Module
