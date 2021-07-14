Namespace CodeAnalysis

    Public Class WATType

        Public ReadOnly Property UnderlyingWATType As WATElements
        Public ReadOnly Property UnderlyingVBType As Type

        Public Shared ReadOnly Property i32 As New WATType(WATElements.i32)
        Public Shared ReadOnly Property i64 As New WATType(WATElements.i64)
        Public Shared ReadOnly Property f32 As New WATType(WATElements.f32)
        Public Shared ReadOnly Property f64 As New WATType(WATElements.f64)
        Public Shared ReadOnly Property void As New WATType(GetType(Void))
        Public Shared ReadOnly Property [string] As New WATType(WATElements.string)

        Protected Sub New(elementType As Type)
            UnderlyingVBType = elementType

            If elementType Is GetType(Void) Then
                UnderlyingWATType = WATElements.void
            Else
                UnderlyingWATType = WATElements.i32
            End If
        End Sub

        Protected Sub New(clone As WATType)
            UnderlyingVBType = clone.UnderlyingVBType
            UnderlyingWATType = clone.UnderlyingWATType
        End Sub

        Protected Sub New(elementType As WATElements)
            UnderlyingWATType = elementType

            Select Case elementType
                Case WATElements.f32 : UnderlyingVBType = GetType(Single)
                Case WATElements.f64 : UnderlyingVBType = GetType(Double)
                Case WATElements.i32,
                     WATElements.string,
                     WATElements.table,
                     WATElements.list,
                     WATElements.array

                    UnderlyingVBType = GetType(Integer)

                Case WATElements.i64 : UnderlyingVBType = GetType(Long)
                Case Else
                    Throw New NotImplementedException
            End Select
        End Sub

        Public Shared Function GetUnderlyingType(value As Type, wasm As Workspace) As WATType
            Dim type As WATType = GetElementType(value)

            If type Is Nothing Then
                Throw New InvalidProgramException(value.FullName)
            End If

            Return type
        End Function

        Public Shared Function GetElementType(value As Type) As WATType
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
                    Throw New NotImplementedException
                Case Else
                    Return Nothing
            End Select
        End Function

        Public Overrides Function ToString() As String
            Return $"({UnderlyingWATType.Description})"
        End Function

        Public Shared Operator =(a As WATType, b As WATType) As Boolean
            Return (a.UnderlyingWATType = b.UnderlyingWATType) AndAlso (a.UnderlyingVBType Is b.UnderlyingVBType)
        End Operator

        Public Shared Operator <>(a As WATType, b As WATType) As Boolean
            Return Not a = b
        End Operator
    End Class
End Namespace
