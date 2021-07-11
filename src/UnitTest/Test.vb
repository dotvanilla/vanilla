Imports VanillaBasic.Roslyn

Module Test

    Sub Main()
        Call ParseSimple()
    End Sub

    Sub ParseSimple()
        Dim code = New Scanner().AddModules("D:\vanilla\src\UnitTest\Program.vb")

        Pause()
    End Sub
End Module
