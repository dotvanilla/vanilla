Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis

    ''' <summary>
    ''' 编译整个完整的VB项目的工作区对象
    ''' </summary>
    Public Class Workspace

        Public ReadOnly Property DefaultNamespace As String

        Public Property Methods As New Dictionary(Of String, FunctionDeclare)
        Public Property EnumVals As New Dictionary(Of String, EnumSymbol)
        Public Property Types

        Sub New(defaultNamespace As String)
            Me.DefaultNamespace = defaultNamespace
        End Sub

        Public Sub AddStaticMethod(func As FunctionDeclare)
            Methods.Add(func.Name, func)
        End Sub

        Public Overloads Function [GetType](name As String, [imports] As NamespaceContext) As WATType

        End Function

    End Class

    Public Class NamespaceContext

    End Class
End Namespace