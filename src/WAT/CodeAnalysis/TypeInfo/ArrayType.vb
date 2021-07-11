Namespace CodeAnalysis

    Public Class ArrayType : Inherits WATType

        Sub New(predefinedType As WATElements)
            Call MyBase.New(predefinedType)
        End Sub

        Sub New(elementType As WATType)
            Call MyBase.New(elementType)
        End Sub

        Public Overrides Function ToString() As String
            Return $"{MyBase.ToString}[]"
        End Function

    End Class
End Namespace