#Region "Microsoft.VisualBasic::05a9bf0c5b30ee6e2a46f83956f783cb, Symbols\Memory\StringSymbol.vb"

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

    '     Class StringSymbol
    ' 
    '         Properties: [string], Length, MemoryPtr
    ' 
    '         Function: [AddressOf], SizeOf, ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Symbols

    ''' <summary>
    ''' 因为wasm不支持字符串，但是支持内存对象，所以字符串使用的是一个i32类型的内存地址来表示
    ''' </summary>
    Public Class StringSymbol : Inherits Expression

        Public Property [string] As String
        Public Property MemoryPtr As Integer

        Public ReadOnly Property Length As Integer
            Get
                Return Strings.Len([string])
            End Get
        End Property

        Public Function SizeOf() As Expression
            Return New LiteralExpression With {.type = TypeAbstract.i32, .value = Length}
        End Function

        Public Function [AddressOf]() As Expression
            Return New LiteralExpression With {.type = TypeAbstract.i32, .value = MemoryPtr}
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.string)
        End Function

        Public Overrides Function ToSExpression() As String
            Return $"
    ;; String from {MemoryPtr} with {Length} bytes in memory
    (data (i32.const {MemoryPtr}) ""{[string]}\00"")"
        End Function
    End Class
End Namespace
