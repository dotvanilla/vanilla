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

End Module
