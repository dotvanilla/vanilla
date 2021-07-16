﻿Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class SetGlobal : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return Value.Type
            End Get
        End Property

        Public Property Target As WATSyntax
        Public Property Value As WATSyntax

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace