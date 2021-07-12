Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class FunctionInvoke : Inherits WATSyntax

        ''' <summary>
        ''' the value type of the function returns
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property Type As WATType

        Public Property Func As WATSyntax
        Public Property Arguments As Dictionary(Of String, WATSyntax)

        Sub New()

        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace