Imports Microsoft.VisualBasic.ComponentModel.Collection

Public Module Types

    Public ReadOnly Property [string] As New TypeAbstract(TypeAlias.string, {})
    Public ReadOnly Property [boolean] As New TypeAbstract(TypeAlias.boolean, {})

    Public ReadOnly Property primitiveTypes As Index(Of TypeAlias) = {
        TypeAlias.f32,
        TypeAlias.f64,
        TypeAlias.i32,
        TypeAlias.i64
    }

    Public Function ParseAliasName(fullName As String) As TypeAlias
        Select Case fullName
            Case "i32", "System.Int32"
                Return TypeAlias.i32
            Case "i64", "System.Int64"
                Return TypeAlias.i64
            Case "f32", "System.Single"
                Return TypeAlias.f32
            Case "f64", "System.Double"
                Return TypeAlias.f64
            Case "boolean", "System.Boolean"
                Return TypeAlias.boolean
            Case "void", "System.Void"
                Return TypeAlias.void
            Case "any", "System.Object"
                Return TypeAlias.any
            Case "intptr", "System.IntPtr"
                Return TypeAlias.intptr
            Case Else
                Throw New NotImplementedException(fullName)
        End Select
    End Function
End Module
