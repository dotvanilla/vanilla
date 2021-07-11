Namespace CodeAnalysis

    ''' <summary>
    ''' 用于解析一个独立的代码文件的环境对象
    ''' </summary>
    Public Class ProjectEnvironment : Inherits Environment

        Public ReadOnly Property Workspace As Workspace

        Sub New(workspace As Workspace)
            Me.Workspace = workspace
        End Sub

    End Class
End Namespace