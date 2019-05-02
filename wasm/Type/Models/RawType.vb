Imports Microsoft.VisualBasic.Language

Namespace TypeInfo

    ''' <summary>
    ''' Type in .NET Framework
    ''' </summary>
    Public Class RawType

        Dim raw As [Variant](Of String, Type)

        Public Overrides Function ToString() As String
            If raw Like GetType(String) Then
                Return raw
            Else
                Return raw.TryCast(Of Type).FullName
            End If
        End Function

        Public Shared Widening Operator CType(type As Type) As RawType
            Return New RawType With {.raw = type}
        End Operator

        Public Shared Widening Operator CType(name As String) As RawType
            Return New RawType With {.raw = name}
        End Operator

    End Class
End Namespace