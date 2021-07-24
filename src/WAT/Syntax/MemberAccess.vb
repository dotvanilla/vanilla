Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    ''' <summary>
    ''' 其实就是一个函数，例如
    ''' 
    ''' ```
    ''' a.field -> memory intptr
    ''' a.func1 -> func1(a, ...)
    ''' ```
    ''' </summary>
    Public Class MemberAccess : Inherits WATSyntax

        Public Property target As WATSyntax
        Public Property member As WATSyntax
        Public Property accessor As AccessorType

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return member.Type
            End Get
        End Property

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class

    Public Enum AccessorType
        Field
        Method
    End Enum
End Namespace