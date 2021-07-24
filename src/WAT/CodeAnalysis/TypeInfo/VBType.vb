Namespace CodeAnalysis

    ''' <summary>
    ''' Type in .NET Framework.
    ''' 
    ''' (在这里为了兼容用户定义的类型以及.NET Framework之中的系统类型)
    ''' </summary>
    Public Class VBType

        Public ReadOnly Property FullName As String
        ''' <summary>
        ''' 用户定义的类型的时候，这个属性是空值
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Type As Type

        Public ReadOnly Property IsknownType As Boolean
            Get
                Return Not Type Is Nothing
            End Get
        End Property

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

        Public Function MakeArrayType() As VBType
            Throw New NotImplementedException()
        End Function

        Public Function AsGeneric(container As Type) As VBType
            If Type Is Nothing Then
                Return container.MakeGenericType(Type.GetType(FullName))
            Else
                Return container.MakeGenericType(Type)
            End If
        End Function

        Public Shared Widening Operator CType(type As Type) As VBType
            Return New VBType(type)
        End Operator
    End Class
End Namespace