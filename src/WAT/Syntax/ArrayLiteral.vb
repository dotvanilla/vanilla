Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class ArrayLiteral : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Public Property Initialize As WATSyntax()

        Public ReadOnly Property Size As Integer
            Get
                Return Initialize.Length
            End Get
        End Property

        Default Public Property Item(i As Integer) As WATSyntax
            Get
                Return Initialize(i)
            End Get
            Set
                Initialize(i) = Value
            End Set
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace