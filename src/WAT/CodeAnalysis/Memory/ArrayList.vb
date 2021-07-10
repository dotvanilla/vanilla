Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis.Memory

    Public Class ArrayList : Inherits MemoryObject

        Public Overrides ReadOnly Property sizeOf As WATSyntax
            Get
                Throw New NotImplementedException()
            End Get
        End Property
    End Class
End Namespace