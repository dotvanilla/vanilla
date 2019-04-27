Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel

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
        Return name.ValueText.Trim("["c, "]"c)
    End Function
End Module
