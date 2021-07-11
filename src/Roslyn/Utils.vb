Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis

Module Utils

    <Extension>
    Friend Function SolveStream(vbcode As [Variant](Of FileInfo, String)) As String
        If vbcode Like GetType(String) Then
            Return CType(vbcode, String).SolveStream
        Else
            Return CType(vbcode, FileInfo).FullName.SolveStream
        End If
    End Function

    <Extension>
    Public Function ParseAsType([as] As SimpleAsClauseSyntax, env As Environment) As WATType
        Return [as].Type.ParseType(env)
    End Function

    <Extension>
    Public Function ParseType(type As TypeSyntax, env As Environment) As WATType
        Static predefinedTypes As New Dictionary(Of String, Type) From {
            {"Integer", GetType(Integer)}
        }

        Select Case type.GetType
            Case GetType(PredefinedTypeSyntax)
                Return WATType.GetUnderlyingType(predefinedTypes(DirectCast(type, PredefinedTypeSyntax).Keyword.ValueText), env.Workspace)

            Case GetType(ArrayTypeSyntax)
                Return New ArrayType(DirectCast(type, ArrayTypeSyntax).ElementType.ParseType(env))

            Case Else
                Throw New NotImplementedException(type.GetType.FullName)
        End Select
    End Function
End Module
