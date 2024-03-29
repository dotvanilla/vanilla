﻿Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class DeclareLocal : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType
        Public Property DefaultValue As WATSyntax

        Sub New(type As WATType)
            Me.Type = type
        End Sub

        Public Overrides Function ToString() As String
            Return ToSExpression(Nothing, Nothing)
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(local ${Name} {Type.UnderlyingWATType.Description})"
        End Function

        Public Function GetParameterExpression() As String
            Return $"(param ${Name} {Type.UnderlyingWATType.Description})"
        End Function
    End Class
End Namespace