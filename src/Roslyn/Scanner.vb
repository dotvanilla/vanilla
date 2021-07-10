Imports System.IO
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.Syntax

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

    Public Shared Function ParseModule()

    End Function

    Public Shared Function ParseFunction(func As MethodBlockSyntax) As WATSyntax

    End Function

End Class
