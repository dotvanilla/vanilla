Namespace CodeAnalysis

    Public Class WATType

        Public ReadOnly Property UnderlyingWATType As WATElements
        Public ReadOnly Property UnderlyingVBType As Type

        Public Shared ReadOnly Property i32 As New WATType(WATElements.i32)
        Public Shared ReadOnly Property i64 As New WATType(WATElements.i64)
        Public Shared ReadOnly Property f32 As New WATType(WATElements.f32)
        Public Shared ReadOnly Property f64 As New WATType(WATElements.f64)
        Public Shared ReadOnly Property void As New WATType(GetType(Void))

        Private Sub New(elementType As Type)
            UnderlyingVBType = elementType

            If elementType Is GetType(Void) Then
                UnderlyingWATType = WATElements.void
            Else
                UnderlyingWATType = WATElements.i32
            End If
        End Sub

        Private Sub New(elementType As WATElements)
            UnderlyingWATType = elementType

            Select Case elementType
                Case WATElements.f32 : UnderlyingVBType = GetType(Single)
                Case WATElements.f64 : UnderlyingVBType = GetType(Double)
                Case WATElements.i32 : UnderlyingVBType = GetType(Integer)
                Case WATElements.i64 : UnderlyingVBType = GetType(Long)
            End Select
        End Sub

        Public Shared Function GetUnderlyingType(value As Type, wasm As Workspace) As WATType
            Select Case value
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
                    Throw New InvalidProgramException(value.FullName)
            End Select
        End Function

        Public Overrides Function ToString() As String
            Return $"({UnderlyingWATType.Description})"
        End Function

    End Class
End Namespace
