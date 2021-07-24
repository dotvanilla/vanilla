#Region "Microsoft.VisualBasic::632c54480d6d2efe9b592c94e1729dd5, Symbols\Memory\StringSymbol.vb"

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
'         Properties: [string], Length
' 
'         Function: SizeOf, ToSExpression, TypeInfer
' 
' 
' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Net.Http
Imports Microsoft.VisualBasic.Text
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' 因为wasm不支持字符串，但是支持内存对象，所以字符串使用的是一个i32类型的内存地址来表示
    ''' </summary>
    Public Class StringSymbol : Inherits IMemoryObject

        Public Property [string] As String

        Public ReadOnly Property Length As Integer
            Get
                Return Strings.Len([string])
            End Get
        End Property

        Public ReadOnly Property base64_decode As String
            Get
                Return [string].DecodeBase64
            End Get
        End Property

        Public Function SizeOf() As Expression
            Return New LiteralExpression With {.type = TypeAbstract.i32, .value = Length}
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.string)
        End Function

        Public Overrides Function ToSExpression() As String
            Dim lines As New List(Of String)

            lines += $""
            lines += $";; String from {memoryPtr} with {Length} bytes in memory"

            If Not comment.StringEmpty Then
                lines += ";;"
                lines += ";; " & comment
                lines += ";;"
            End If

            lines += $"(data (i32.const {memoryPtr}) ""{[string]}\00"")"

            Return lines _
                .Select(Function(line) "    " & line) _
                .JoinBy(ASCII.LF)
        End Function
    End Class
End Namespace
