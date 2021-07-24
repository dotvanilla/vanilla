Namespace CodeAnalysis

    ''' <summary>
    ''' Type model in WebAssembly compiler
    ''' </summary>
    Public Class WATType

        Public ReadOnly Property UnderlyingWATType As WATElements
        Public ReadOnly Property UnderlyingVBType As VBType
        Public ReadOnly Property Generic As WATType()

        Public Shared ReadOnly Property i32 As New WATType(WATElements.i32)
        Public Shared ReadOnly Property i64 As New WATType(WATElements.i64)
        Public Shared ReadOnly Property f32 As New WATType(WATElements.f32)
        Public Shared ReadOnly Property f64 As New WATType(WATElements.f64)
        Public Shared ReadOnly Property void As New WATType(GetType(Void))
        Public Shared ReadOnly Property [string] As New WATType(WATElements.string)
        Public Shared ReadOnly Property [boolean] As New WATType(GetType(Boolean))

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

        Sub New(fullName As String)
            UnderlyingVBType = New VBType(fullName)
            UnderlyingWATType = WATElements.i32
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

        Public Shared Function GetUnderlyingType(value As VBType, wasm As Environment) As WATType
            If value.IsknownType Then
                Return GetUnderlyingType(value.Type, wasm.Workspace)
            Else
                Return New WATType(value.FullName)
            End If
        End Function

        Public Shared Function GetUnderlyingType(value As Type, wasm As Workspace) As WATType
            Dim type As WATType = GetElementType(value)

            If type Is Nothing Then
                Throw New InvalidProgramException(value.FullName)
            End If

            Return type
        End Function

        Public Shared Function GetElementType(name As String) As WATType
            Select Case name
                Case NameOf(i32) : Return i32
                Case NameOf(i64) : Return i64
                Case NameOf(f32) : Return f32
                Case NameOf(f64) : Return f64
                Case Else
                    Throw New NotImplementedException(name)
            End Select
        End Function

        Public Shared Function GetElementType(value As Type) As WATType
            Static byrefString As Type = Type.GetType("System.String&")

            Select Case value
                Case GetType(Integer), GetType(UInteger),
                     GetType(Byte), GetType(SByte),
                     GetType(Short), GetType(UShort)

                    Return WATType.i32

                Case GetType(Long), GetType(ULong)
                    Return WATType.i64

                Case GetType(Single)
                    Return WATType.f32

                Case GetType(Double), GetType(Decimal)
                    Return WATType.f64

                Case GetType(String), byrefString
                    Return WATType.string

                Case GetType(Void)
                    Return WATType.void

                Case GetType(Boolean)
                    Return WATType.boolean

                Case Else
                    Return Nothing
            End Select
        End Function

        Public Overrides Function ToString() As String
            Dim WASM As WATElements = UnderlyingWATType

            If UnderlyingWATType = WATElements.void Then
                WASM = WATElements.i32
            End If

            Return $"({WASM.Description})"
        End Function

        Public Shared Operator =(a As WATType, b As WATType) As Boolean
            Return (a.UnderlyingWATType = b.UnderlyingWATType) AndAlso (a.UnderlyingVBType Is b.UnderlyingVBType)
        End Operator

        Public Shared Operator <>(a As WATType, b As WATType) As Boolean
            Return Not a = b
        End Operator
    End Class
End Namespace
