Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Wasm

Public NotInheritable Class TypeEquality : Implements IEqualityComparer(Of TypeAbstract)

    ReadOnly arrayList As Index(Of TypeAlias) = {TypeAlias.array, TypeAlias.list}

    Public Shared ReadOnly Property Test As New TypeEquality

    Private Sub New()
    End Sub

    Public Overloads Function Equals(x As TypeAbstract, y As TypeAbstract) As Boolean Implements IEqualityComparer(Of TypeAbstract).Equals
        If x.type <> y.type Then
            Return False
        End If

        If x.type Like arrayList AndAlso x.type = y.type Then
            Return Equals(x.generic(Scan0), y.generic(Scan0))
        End If

        Return True
    End Function

    Public Overloads Function GetHashCode(obj As TypeAbstract) As Integer Implements IEqualityComparer(Of TypeAbstract).GetHashCode
        Return obj.GetHashCode
    End Function
End Class
