﻿Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class WASMModule : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace