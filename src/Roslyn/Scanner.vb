Imports System.IO
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

''' <summary>
''' the syntax parser of the VisualBasic source file.
''' </summary>
Public NotInheritable Class Scanner

    Public ReadOnly Property Workspace As Workspace

    Private Sub New()
    End Sub

    Public Shared Function GetCodeModules(vb As [Variant](Of FileInfo, String)) As ModuleBlockSyntax()
        Dim syntax As CompilationUnitSyntax = VisualBasicSyntaxTree.ParseText(vb.SolveStream).GetRoot
        Dim modules As ModuleBlockSyntax() = syntax.Members.OfType(Of ModuleBlockSyntax).ToArray

        Return modules
    End Function

    Public Sub ParseModule(code As ModuleBlockSyntax)
        For Each func As MethodBlockSyntax In code.Members.OfType(Of MethodBlockSyntax)
            Call Workspace.Methods.Add(ParseFunction(func))
        Next
    End Sub

    Public Function ParseFunction(func As MethodBlockSyntax) As WATSyntax

    End Function

End Class
