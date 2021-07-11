Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis.Memory

    Public MustInherit Class MemoryPtr

    End Class

    Public Class StaticPtr : Inherits MemoryPtr

        Public Property Scan0 As Integer

        Sub New(i As Integer)
            Scan0 = i
        End Sub

        Public Overrides Function ToString() As String
            Return Scan0.ToHexString
        End Function

    End Class

    Public Class InstancePtr : Inherits MemoryPtr

        Public Property Scan0 As WATSyntax

    End Class
End Namespace