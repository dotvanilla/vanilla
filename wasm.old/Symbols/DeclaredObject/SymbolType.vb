#Region "Microsoft.VisualBasic::6d26a0b9ab578bea8213f6b13d9ed901, Symbols\DeclaredObject\SymbolType.vb"

    ' Author:
    ' 
    '       xieguigang (I@xieguigang.me)
    '       asuka (evia@lilithaf.me)
    '       wasm project (developer@vanillavb.app)
    ' 
    ' Copyright (c) 2019 developer@vanillavb.app, VanillaBasic(https://vanillavb.app)
    ' 
    ' 
    ' MIT License
    ' 
    ' 
    ' Permission is hereby granted, free of charge, to any person obtaining a copy
    ' of this software and associated documentation files (the "Software"), to deal
    ' in the Software without restriction, including without limitation the rights
    ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ' copies of the Software, and to permit persons to whom the Software is
    ' furnished to do so, subject to the following conditions:
    ' 
    ' The above copyright notice and this permission notice shall be included in all
    ' copies or substantial portions of the Software.
    ' 
    ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    ' SOFTWARE.



    ' /********************************************************************************/

    ' Summaries:

    '     Enum SymbolType
    ' 
    '         [Operator], Api, GlobalVariable, LocalVariable, LogicalOperator
    '         Type
    ' 
    '  
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

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
        ''' 因为i32类型会与逻辑值混淆，所以在这里使用这个标记来区分函数的返回值
        ''' </summary>
        LogicalOperator
        ''' <summary>
        ''' 是一个全局变量
        ''' </summary>
        GlobalVariable
        LocalVariable
        ''' <summary>
        ''' 引用的是一个用户自定义的类型
        ''' </summary>
        Type
    End Enum
End Namespace
