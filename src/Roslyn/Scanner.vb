Imports System.IO
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.Roslyn.VBLanguageParser
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports TypeSchema = VanillaBasic.WebAssembly.CodeAnalysis.TypeSchema

''' <summary>
''' the syntax parser of the VisualBasic source file.
''' </summary>
Public NotInheritable Class Scanner

    Public ReadOnly Property Workspace As Workspace

    Sub New(proj As vbproj.Project)
        Workspace = New Workspace(proj.RootNamespace)
        Workspace.AssemblyInfo = proj.AssemblyInfo
    End Sub

    Private Function GetCodeModules(vb As [Variant](Of FileInfo, String), [global] As ProjectEnvironment) As ModuleBlockSyntax()
        Dim syntax As CompilationUnitSyntax = VisualBasicSyntaxTree _
            .ParseText(vb.SolveStream) _
            .GetRoot
        Dim modules As ModuleBlockSyntax() = syntax.Members _
            .OfType(Of ModuleBlockSyntax) _
            .ToArray

        ' [global].Imports.NamespaceList = syntax.Imports.Select(Function(i) i)

        Return modules
    End Function

    Public Function AddModules(vb As [Variant](Of FileInfo, String)) As Scanner
        Dim [global] As New ProjectEnvironment(Workspace)

        For Each [module] As ModuleBlockSyntax In GetCodeModules(vb, [global])
            Call ParseModule(code:=[module], [global]:=[global])
        Next

        Return Me
    End Function

    Private Sub ParseModule(code As ModuleBlockSyntax, [global] As ProjectEnvironment)
        Dim moduleName As String = code.ModuleStatement.Identifier.ValueText
        Dim env As New Environment(moduleName, container:=[global])
        Dim isPublic As Boolean = False
        Dim exports As New List(Of String)
        Dim closure As FunctionDeclare

        For Each func As MethodBlockSyntax In code.Members.OfType(Of MethodBlockSyntax)
            isPublic = False
            closure = func.RunParser(isPublic, env)
            Workspace.AddStaticMethod(closure)

            If isPublic Then
                exports.Add(closure.Name)
            End If
        Next

        Dim standardModule As New TypeSchema With {
            .IsStandardModule = True,
            .Name = moduleName,
            .ExportApi = exports.ToArray,
            .[Namespace] = [global].FullName
        }

        Call [global].Workspace.Types.Add(standardModule.FullName, standardModule)
    End Sub
End Class
