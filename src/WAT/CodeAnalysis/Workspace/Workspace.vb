Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports VanillaBasic.WebAssembly.Compiler
Imports VanillaBasic.WebAssembly.JavaScript
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
        Public Property [Imports] As New Dictionary(Of String, ImportsFunction)

        Default Public ReadOnly Property PublicApi(export As ExportSymbol) As FunctionDeclare
            Get
                Return Methods(export.target.Name)
            End Get
        End Property

        Sub New(defaultNamespace As String)
            Me.DefaultNamespace = defaultNamespace

            Call Library.Imports(Of JavaScript.Math)(Me)
        End Sub

        Public Sub AddStaticMethod(func As FunctionDeclare)
            Methods.Add(func.FullName, func)
        End Sub

        Public Iterator Function GetPublicApi() As IEnumerable(Of ExportSymbol)
            For Each type As TypeSchema In Types.Values
                For Each name As String In type.ExportApi
                    Yield New ExportSymbol(Methods($"{type.FullName}.{name}"))
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