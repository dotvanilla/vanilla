Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Compiler

Namespace Syntax

    Public Class FunctionDeclare : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType

        Public Property parameters As DeclareLocal()
        Public Property locals As DeclareLocal()
        Public Property body As WATSyntax()
        Public Property [namespace] As String

        Public ReadOnly Property FullName As String
            Get
                Return $"{[namespace]}.{Name}"
            End Get
        End Property

        Sub New(type As WATType)
            Me.Type = type
        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return FunctionWriter.ToSExpression(Me, env.global.Workspace)
        End Function
    End Class
End Namespace