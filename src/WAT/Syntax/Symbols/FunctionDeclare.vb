Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class FunctionDeclare : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType

        Public Property parameters As DeclareLocal()
        Public Property locals As DeclareLocal()
        Public Property body As WATSyntax()

        Sub New(type As WATType)
            Me.Type = type
        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace