Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis

    ''' <summary>
    ''' 编译整个完整的VB项目的工作区对象
    ''' </summary>
    Public Class Workspace

        Public ReadOnly Property DefaultNamespace As String

        Public Property AssemblyInfo As AssemblyInfo
        Public Property Methods As New Dictionary(Of String, FunctionDeclare)
        Public Property EnumVals As New Dictionary(Of String, EnumSymbol)
        ''' <summary>
        ''' defined types in current project
        ''' </summary>
        ''' <returns></returns>
        Public Property Types As New Dictionary(Of String, TypeSchema)
        Public Property Memory As New MemoryBuffer

        Sub New(defaultNamespace As String)
            Me.DefaultNamespace = defaultNamespace
        End Sub

        Public Sub AddStaticMethod(func As FunctionDeclare)
            Methods.Add(func.FullName, func)
        End Sub

        Public Iterator Function GetPublicApi() As IEnumerable(Of FunctionDeclare)
            For Each type As TypeSchema In Types.Values
                For Each name As String In type.ExportApi
                    Yield Methods($"{type.FullName}.{name}")
                Next
            Next
        End Function

        Public Overloads Function [GetType](name As String, [imports] As NamespaceContext) As WATType

        End Function

        Public Overrides Function ToString() As String
            Return DefaultNamespace
        End Function

    End Class

End Namespace