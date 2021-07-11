Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class DeclareLocal : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function

        Public Function GetParameterExpression() As String
            Return $"(param ${Name} {Type.UnderlyingWATType})"
        End Function
    End Class
End Namespace