Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.vbproj
Imports VanillaBasic.Roslyn

Module Test

    Sub Main()
        Call ParseSimple()
    End Sub

    Sub ParseSimple()
        Dim vb As Project = Project.Load("D:\vanilla\test\SimpleHelloWorld\SimpleHelloWorld.vbproj")
        Dim code = New Scanner(vb).AddModules("D:\vanilla\src\UnitTest\Program.vb")

        Pause()
    End Sub
End Module
