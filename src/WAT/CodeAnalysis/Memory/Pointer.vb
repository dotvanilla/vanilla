Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis.Memory

    Public MustInherit Class MemoryPtr

    End Class

    Public Class StaticPtr : Inherits MemoryPtr

        Public Property Scan0 As Integer

    End Class

    Public Class InstancePtr : Inherits MemoryPtr

        Public Property Scan0 As WATSyntax

    End Class
End Namespace