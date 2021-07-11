Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.vbproj
Imports VanillaBasic.Roslyn

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

        Pause()
    End Sub
End Module
