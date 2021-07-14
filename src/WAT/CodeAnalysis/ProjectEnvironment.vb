Namespace CodeAnalysis

    ''' <summary>
    ''' 用于解析一个独立的代码文件的环境对象
    ''' </summary>
    Public Class ProjectEnvironment : Inherits Environment

        Public Overrides ReadOnly Property Workspace As Workspace
        Public ReadOnly Property [Imports] As New NamespaceContext

        Sub New(workspace As Workspace)
            Call MyBase.New(workspace.DefaultNamespace, Nothing)

            Me.Workspace = workspace
        End Sub

    End Class
End Namespace