Imports Microsoft.VisualBasic.ComponentModel.Collection

''' <summary>
''' The compiler type alias
''' </summary>
Public Enum TypeAlias As Integer
    any
    i32
    i64
    f32
    f64
    [string]
    [boolean]
    array
    list
    table
    intptr
End Enum

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

    Public Shared ReadOnly Property primitiveTypes As Index(Of TypeAlias) = {
        TypeAlias.f32,
        TypeAlias.f64,
        TypeAlias.i32,
        TypeAlias.i64
    }

    ''' <summary>
    ''' Type symbol for generate S-Expression.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property typefit As String
        Get
            If type Like primitiveTypes Then
                Return type.ToString
            Else
                ' All of the non-primitive type is memory pointer
                Return "i32"
            End If
        End Get
    End Property

    Public Overrides Function ToString() As String
        If generic.IsNullOrEmpty Then
            Return type.Description
        Else
            Return $"{type.Description}(Of {generic.JoinBy(", ")})"
        End If
    End Function
End Class