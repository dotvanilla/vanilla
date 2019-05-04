Namespace Symbols

    Public Enum SymbolType As Byte
        ''' <summary>
        ''' 用户在源代码之中所定义的一个用户函数
        ''' </summary>
        Func = 0
        ''' <summary>
        ''' 从JavaScript外部导入的一个Api函数引用
        ''' </summary>
        Api
        ''' <summary>
        ''' WebAssembly内部的运算符函数
        ''' </summary>
        [Operator]
        ''' <summary>
        ''' 是一个全局变量
        ''' </summary>
        GlobalVariable
        ''' <summary>
        ''' 引用的是一个用户自定义的类型
        ''' </summary>
        Type
    End Enum
End Namespace