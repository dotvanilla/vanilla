Imports System.ComponentModel

Namespace CodeAnalysis

    Public Enum WATElements
        <Description("i32")> void
        i32
        i64
        f32
        f64

        <Description("i32")> any
        <Description("i32")> [string]
        <Description("i32")> array
        <Description("i32")> list
        <Description("i32")> table
    End Enum
End Namespace