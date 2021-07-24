Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.vbproj
Imports VanillaBasic.Roslyn
Imports VanillaBasic.WebAssembly.Compiler

Module Test

    Sub Main()
        Call ParseSimple()
    End Sub

    Sub ParseSimple()
        Dim vb As Project = Project.Load("D:\vanilla\test\SimpleHelloWorld\SimpleHelloWorld.vbproj")
        Dim wasm As New Scanner(vb)

        For Each file As String In vb.EnumerateSourceFiles(skipAssmInfo:=True, fullName:=True)
            wasm.AddModules(file)
        Next

        Call wasm.Workspace.ToSExpression.SaveTo("./demo.wat")

        Pause()
    End Sub
End Module
