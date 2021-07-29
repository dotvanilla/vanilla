Imports System.ComponentModel

Namespace CodeAnalysis

    ''' <summary>
    ''' The compiler type alias
    ''' </summary>
    Public Enum WATElements

        ''' <summary>
        ''' Function or expression have no value returns
        ''' </summary>
        <Description("i32")> void
        i32
        i64
        f32
        f64

        <Description("i32")> any
        <Description("i32")> [string]
        <Description("i32")> [boolean]
        ''' <summary>
        ''' Fix length array in WebAssembly runtime
        ''' </summary>
        <Description("i32")> array
        ''' <summary>
        ''' Array list in javascript runtime
        ''' </summary>
        <Description("i32")> list
        ''' <summary>
        ''' Javascript object
        ''' </summary>
        <Description("i32")> table
        ''' <summary>
        ''' 所有用户自定义的引用类型
        ''' </summary>
        <Description("i32")> intptr
    End Enum
End Namespace