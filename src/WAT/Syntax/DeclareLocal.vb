Namespace Syntax

    Public Class DeclareLocal : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As CodeAnalysis.WATType
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Overrides Function ToSExpression(env As CodeAnalysis.Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace