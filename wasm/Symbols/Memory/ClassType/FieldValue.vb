﻿#Region "Microsoft.VisualBasic::7779260eaf0b8fff69ba16ffc23eebfb, Symbols\Memory\ClassType\FieldValue.vb"

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

    '     Class FieldValue
    ' 
    '         Properties: type
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' 因为在读取field值的时候，因为是从memory之中进行load操作
    ''' 导致字符串，数组等数据类型被误判为i32产生额外的或者错误的数据类型转换
    ''' 所以在这里会需要使用这个对象来保持原本的类型信息
    ''' </summary>
    Public Class FieldValue : Inherits Parenthesized

        ''' <summary>
        ''' 从内存之中读取出来的值，这个因为load函数的限制
        ''' 如果直接对表达式做类型推断的话，只能够得到4种基础类型
        ''' 所以会需要这个属性值来解决这个类型推断的bug
        ''' </summary>
        ''' <returns></returns>
        Public Property type As TypeAbstract

        ''' <summary>
        ''' 在类型推断返回原本的字段类型
        ''' </summary>
        ''' <param name="symbolTable"></param>
        ''' <returns></returns>
        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        Public Overrides Function ToSExpression() As String
            Return Internal.ToSExpression
        End Function
    End Class
End Namespace
