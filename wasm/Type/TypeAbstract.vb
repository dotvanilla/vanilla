''' <summary>
''' Type model in WebAssembly compiler
''' </summary>
Public Class TypeAbstract

    Public ReadOnly Property type As TypeAlias
    ''' <summary>
    ''' Generic type arguments in VisualBasic.NET language.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property generic As String()
    ''' <summary>
    ''' The raw definition: <see cref="System.Type.FullName"/>
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property raw As String

    ''' <summary>
    ''' Type symbol for generate S-Expression.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property typefit As String
        Get
            Return CTypeHandle.typefit(type)
        End Get
    End Property

    Sub New(type As Type)

    End Sub

    Sub New(fullName As String)
        Select Case fullName
            Case "i32", "System.Int32"
                type = TypeAlias.i32
            Case "i64", "System.Int64"
                type = TypeAlias.i64
            Case "f32", "System.Single"
                type = TypeAlias.f32
            Case "f64", "System.Double"
                type = TypeAlias.f64
            Case "boolean", "System.Boolean"
                type = TypeAlias.boolean
            Case Else
                Throw New NotImplementedException
        End Select
    End Sub

    Sub New([alias] As TypeAlias, Optional generic$() = Nothing)
        Me.type = [alias]
        Me.generic = generic
    End Sub

    Public Overrides Function ToString() As String
        If generic.IsNullOrEmpty Then
            Return type.Description
        Else
            Return $"{type.Description}(Of {generic.JoinBy(", ")})"
        End If
    End Function

    Public Shared Operator <>(type As TypeAbstract, name$) As Boolean
        Return type.type.ToString <> name
    End Operator

    Public Shared Operator =(type As TypeAbstract, name$) As Boolean
        Return type.type.ToString = name
    End Operator
End Class