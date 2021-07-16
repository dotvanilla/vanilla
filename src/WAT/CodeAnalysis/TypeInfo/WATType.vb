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

    Public Class WATType

        Public ReadOnly Property UnderlyingWATType As WATElements
        Public ReadOnly Property UnderlyingVBType As VBType

        Public Shared ReadOnly Property i32 As New WATType(WATElements.i32)
        Public Shared ReadOnly Property i64 As New WATType(WATElements.i64)
        Public Shared ReadOnly Property f32 As New WATType(WATElements.f32)
        Public Shared ReadOnly Property f64 As New WATType(WATElements.f64)
        Public Shared ReadOnly Property void As New WATType(GetType(Void))
        Public Shared ReadOnly Property [string] As New WATType(WATElements.string)

        Public ReadOnly Property IsUserType As Boolean
            Get
                Return UnderlyingVBType.Type Is Nothing
            End Get
        End Property

        Protected Sub New(elementType As Type)
            UnderlyingVBType = New VBType(elementType)

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
                Case WATElements.f32 : UnderlyingVBType = New VBType(GetType(Single))
                Case WATElements.f64 : UnderlyingVBType = New VBType(GetType(Double))
                Case WATElements.i32,
                     WATElements.string,
                     WATElements.table,
                     WATElements.list,
                     WATElements.array

                    UnderlyingVBType = New VBType(GetType(Integer))

                Case WATElements.i64 : UnderlyingVBType = New VBType(GetType(Long))
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
