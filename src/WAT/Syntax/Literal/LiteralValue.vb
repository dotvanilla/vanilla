Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax.Literal

    Public Class LiteralValue : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
        Public ReadOnly Property Value As Object

        ''' <summary>
        ''' Current literal expression is a literal token expression of 
        ''' ``Nothing`` or ``null`` in general programing language.
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property isLiteralNothing As Boolean
            Get
                Select Case Type.UnderlyingWATType
                    Case WATElements.f32, WATElements.f64, WATElements.i32, WATElements.i64, WATElements.void, WATElements.string
                        ' 基础类型总是有一个默认值零的
                        ' 所以不是Nothing
                        Return False
                    Case Else
                        ' 对于其他类型，只要是零就表示可能是Nothing
                        Return Value = 0
                End Select
            End Get
        End Property

        Sub New(value As Object)
            Me.Value = value
            Me.Type = WATType.GetElementType(value.GetType)
        End Sub

        Sub New(any As Object, type As WATType)
            Me.Value = any
            Me.Type = type
        End Sub

        Sub New(intptr As Integer, type As WATType)
            Me.Value = intptr
            Me.Type = type
        End Sub

        Public Overrides Function ToString() As String
            Return ToSExpression(Nothing, Nothing)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="type">
        ''' default is object type
        ''' </param>
        ''' <returns></returns>
        Public Shared Function [Nothing](Optional type As WATType = Nothing) As LiteralValue
            If type Is Nothing Then
                type = WATType.any
            End If

            Return New LiteralValue(0, type) With {
                .Annotation = $"Nothing(Of {type.ToString})"
            }
        End Function

        Public Shared Function GetUnderlyingType(value As Object, wasm As Workspace) As WATType
            If value Is Nothing Then
                Return Nothing
            Else
                Return WATType.GetUnderlyingType(value.GetType, wasm)
            End If
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Select Case Type
                Case WATType.i32, WATType.string
                    Return $"(i32.const {Value})"
                Case WATType.i64
                    Return $"(i64.const {Value})"
                Case WATType.f32
                    Return $"(f32.const {Value})"
                Case WATType.f64
                    Return $"(f64.const {Value})"
                Case Else
                    Throw New InvalidCastException
            End Select
        End Function
    End Class
End Namespace