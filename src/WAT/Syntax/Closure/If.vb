Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class [If] : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return WATType.void
            End Get
        End Property

        Public Property condition As BoolLogical
        Public Property [then] As Closure

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class IfElse : Inherits [If]

        Public Property [else] As Closure

    End Class

    Public Class [ElseIf] : Inherits IfElse

        Public Property elseIfs As [If]()

    End Class

End Namespace