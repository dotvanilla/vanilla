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

        Public MustOverride Function ToSExpression(env As Environment, indent As String) As String

    End Class
End Namespace