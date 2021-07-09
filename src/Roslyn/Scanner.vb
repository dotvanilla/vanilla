Imports System.IO
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language

''' <summary>
''' the syntax parser of the VisualBasic source file.
''' </summary>
Public NotInheritable Class Scanner

    Private Sub New()
    End Sub

    Public Shared Function GetCodeModules(vb As [Variant](Of FileInfo, String)) As ModuleBlockSyntax()
        Dim syntax As CompilationUnitSyntax = VisualBasicSyntaxTree.ParseText(vb.SolveStream).GetRoot
        Dim modules As ModuleBlockSyntax() = syntax.Members.OfType(Of ModuleBlockSyntax).ToArray

        Return modules
    End Function

End Class
