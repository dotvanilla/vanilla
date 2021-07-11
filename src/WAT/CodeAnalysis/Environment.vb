Namespace CodeAnalysis

    ''' <summary>
    ''' local environment/module environment for find symbol reference
    ''' </summary>
    Public Class Environment

        Public ReadOnly Property [global] As ProjectEnvironment
            Get
                If Container Is Nothing Then
                    Return Me
                Else
                    Return Container.global
                End If
            End Get
        End Property

        Public ReadOnly Property FullName As String
        Public ReadOnly Property Container As Environment

        Sub New(name As String, Optional container As Environment = Nothing)
            Me.Container = container
            Me.FullName = If(container Is Nothing, name, $"{container.FullName}.{name}")
        End Sub

    End Class
End Namespace