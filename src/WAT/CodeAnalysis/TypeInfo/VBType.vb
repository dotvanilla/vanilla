Namespace CodeAnalysis

    ''' <summary>
    ''' 在这里为了兼容用户定义的类型以及.NET Framework之中的系统类型
    ''' </summary>
    Public Class VBType

        Public ReadOnly Property FullName As String
        ''' <summary>
        ''' 用户定义的类型的时候，这个属性是空值
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Type As Type

        Sub New(fullName As String)
            Me.FullName = fullName
        End Sub

        Sub New(type As Type)
            Me.Type = type
            Me.FullName = type.FullName
        End Sub

        Public Overrides Function ToString() As String
            Return FullName
        End Function

    End Class
End Namespace