﻿#Region "Microsoft.VisualBasic::6535f48d4f407f5de6e0efed7327af3a, Type\Models\TypeAlias.vb"

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

    '     Enum TypeAlias
    ' 
    '         [boolean], [string], any, array, f32
    '         f64, i32, i64, intptr, list
    '         table
    ' 
    '  
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace TypeInfo

    ''' <summary>
    ''' The compiler type alias
    ''' </summary>
    Public Enum TypeAlias As Integer
        ''' <summary>
        ''' Function or expression have no value returns
        ''' </summary>
        void = -1
        any
        i32
        i64
        f32
        f64
        [string]
        [boolean]
        ''' <summary>
        ''' Fix length array in WebAssembly runtime
        ''' </summary>
        array
        ''' <summary>
        ''' Array list in javascript runtime
        ''' </summary>
        list
        ''' <summary>
        ''' Javascript object
        ''' </summary>
        table

        ''' <summary>
        ''' 所有用户自定义的引用类型
        ''' </summary>
        intptr
    End Enum
End Namespace
