Imports System.IO
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

''' <summary>
''' the syntax parser of the VisualBasic source file.
''' </summary>
Public NotInheritable Class Scanner

    Public ReadOnly Property Workspace As Workspace

    Sub New(proj As vbproj.Project)
        Workspace = New Workspace(proj.RootNamespace)
    End Sub

    Private Function GetCodeModules(vb As [Variant](Of FileInfo, String), [global] As Workspace) As ModuleBlockSyntax()
        Dim syntax As CompilationUnitSyntax = VisualBasicSyntaxTree _
            .ParseText(vb.SolveStream) _
            .GetRoot
        Dim modules As ModuleBlockSyntax() = syntax.Members _
            .OfType(Of ModuleBlockSyntax) _
            .ToArray

        Return modules
    End Function

    Public Function AddModules(vb As [Variant](Of FileInfo, String)) As Scanner
        Dim [global] As New ProjectEnvironment(Workspace)

        For Each [module] As ModuleBlockSyntax In GetCodeModules(vb, [global]:=Workspace)
            Call ParseModule(code:=[module], [global]:=[global])
        Next

        Return Me
    End Function

    Private Sub ParseModule(code As ModuleBlockSyntax, [global] As ProjectEnvironment)
        For Each func As MethodBlockSyntax In code.Members.OfType(Of MethodBlockSyntax)
            Call Workspace.AddStaticMethod(ParseFunction(func, [global]))
        Next
    End Sub

    Private Function ParseFunction(func As MethodBlockSyntax, [global] As ProjectEnvironment) As WATSyntax
        Dim returnValue As WATType

        If func.SubOrFunctionStatement.SubOrFunctionKeyword.ValueText = "Sub" Then
            returnValue = WATType.void
        Else
            returnValue = func.SubOrFunctionStatement.AsClause.ParseAsType(env:=[global])
        End If

        Return New FunctionDeclare(returnValue) With {
            .Name = func.SubOrFunctionStatement.Identifier.ValueText
        }
    End Function

End Class
