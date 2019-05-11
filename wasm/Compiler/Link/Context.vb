#Region "Microsoft.VisualBasic::8c07973096698977d400b413a51dfdac, Compiler\Link\Context.vb"

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

    '     Class Context
    ' 
    '         Properties: [object], blockGuid, currentBlockGuid, funcSymbol, moduleLabel
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language.Default
Imports Wasm.Symbols.MemoryObject

Namespace Compiler

    ''' <summary>
    ''' 当前的程序代码的上下文
    ''' </summary>
    Public Class Context

        ''' <summary>
        ''' 当前所进行解析的函数的名称
        ''' </summary>
        ''' <returns></returns>
        Public Property funcSymbol As String
        ''' <summary>
        ''' 当前的代码块的guid，这个是用来退出特定的block所需要的
        ''' </summary>
        ''' <returns></returns>
        Public Property blockGuid As New Stack(Of String)
        ''' <summary>
        ''' 当前的VisualBasic模块的名称
        ''' </summary>
        ''' <returns></returns>
        Public Property moduleLabel() As DefaultValue(Of String)
        ''' <summary>
        ''' 对象初始化或者匿名对象引用的时候使用
        ''' </summary>
        ''' <returns></returns>
        Public Property [object] As UserObject

        Public ReadOnly Property currentBlockGuid As String
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return blockGuid.Peek
            End Get
        End Property

    End Class
End Namespace
