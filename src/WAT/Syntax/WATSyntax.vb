Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    ''' <summary>
    ''' The abstract ``S-Expression`` model.(一个表达式树)
    ''' </summary>
    Public MustInherit Class WATSyntax

        Public MustOverride ReadOnly Property Type As WATType

        ''' <summary>
        ''' the comment text of current expression value
        ''' </summary>
        ''' <returns></returns>
        Public Property Annotation As String

        ''' <summary>
        ''' Current expression is a literal token expression of ``Nothing`` or ``null`` in general programing language.
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property isLiteralNothing As Boolean
            Get
                If Not TypeOf Me Is LiteralValue Then
                    Return False
                Else
                    Return DirectCast(Me, LiteralValue).isLiteralNothing
                End If
            End Get
        End Property

        Public MustOverride Function ToSExpression(env As Environment, indent As String) As String

    End Class
End Namespace