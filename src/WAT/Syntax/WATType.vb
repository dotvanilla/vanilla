Public Class WATType

    Public ReadOnly Property UnderlyingWATType As WATElements
    Public ReadOnly Property UnderlyingVBType As Type

    Public Shared ReadOnly Property i32 As New WATType(WATElements.i32)
    Public Shared ReadOnly Property i64 As New WATType(WATElements.i64)
    Public Shared ReadOnly Property f32 As New WATType(WATElements.f32)
    Public Shared ReadOnly Property f64 As New WATType(WATElements.f64)

    Private Sub New(elementType As WATElements)
        UnderlyingWATType = elementType

        Select Case elementType
            Case WATElements.f32 : UnderlyingVBType = GetType(Single)
            Case WATElements.f64 : UnderlyingVBType = GetType(Double)
            Case WATElements.i32 : UnderlyingVBType = GetType(Integer)
            Case WATElements.i64 : UnderlyingVBType = GetType(Long)
        End Select
    End Sub

    Public Overrides Function ToString() As String
        Return $"({UnderlyingWATType.Description})"
    End Function

End Class

Public Enum WATElements
    void
    i32
    i64
    f32
    f64
End Enum