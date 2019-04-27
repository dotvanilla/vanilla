Imports Microsoft.VisualBasic.ComponentModel.Collection

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

    End Sub

    Sub New([alias] As TypeAlias, generic$())
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
End Class