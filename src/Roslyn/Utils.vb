Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
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
                Dim system As Type = predefinedTypes(DirectCast(type, PredefinedTypeSyntax).Keyword.ValueText)
                Dim elementType As WATType = WATType.GetUnderlyingType(system, env.Workspace)

                Return elementType
            Case GetType(ArrayTypeSyntax)
                Return New ArrayType(DirectCast(type, ArrayTypeSyntax).ElementType.ParseType(env))

            Case Else
                Throw New NotImplementedException(type.GetType.FullName)
        End Select
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

    <Extension>
    Friend Function objectName(x As ModifiedIdentifierSyntax) As String
        Return x.Identifier.objectName
    End Function

    ''' <summary>
    ''' 常量表达式
    ''' </summary>
    ''' <param name="unary"></param>
    ''' <returns></returns>
    <Extension>
    Public Function UnaryValue(unary As UnaryExpressionSyntax) As String
        Dim op$ = unary.OperatorToken.ValueText
        Dim valueToken = DirectCast(unary.Operand, LiteralExpressionSyntax)
        Dim value$ = valueToken.Token.ValueText

        Return op & value
    End Function
End Module
