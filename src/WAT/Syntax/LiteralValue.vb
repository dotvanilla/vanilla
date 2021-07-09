Public Class LiteralValue : Inherits WATSyntax

    Public Overrides ReadOnly Property Type As WATType
    Public ReadOnly Property Value As Object

    Sub New(value As Object)
        Me.Value = value
    End Sub

    Public Shared Function GetUnderlyingType(value As Object) As WATType
        If value Is Nothing Then
            Return Nothing
        Else
            Select Case value.GetType
                Case GetType(Integer), GetType(UInteger), GetType(Byte), GetType(SByte), GetType(Short), GetType(UShort)
                    Return WATType.i32

                Case GetType(Long), GetType(ULong)
                    Return WATType.i64

                Case GetType(Single)
                    Return WATType.f32

                Case GetType(Double), GetType(Decimal)
                    Return WATType.f64

                Case GetType(String)

                Case Else
                    Throw New InvalidProgramException(value.GetType.FullName)
            End Select
        End If
    End Function

    Public Overrides Function ToSExpression(indent As String) As String
        Throw New NotImplementedException()
    End Function
End Class
