Namespace CodeAnalysis

    Public Class TypeSchema

        Public Property Name As String
        Public Property [Namespace] As String
        Public Property ExportApi As String()

        Public ReadOnly Property FullName As String
            Get
                Return $"{[Namespace]}.{Name}"
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return FullName
        End Function

    End Class
End Namespace