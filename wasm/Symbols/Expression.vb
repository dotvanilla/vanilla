#Region "Microsoft.VisualBasic::d175dd440152883dcf1f3320767933c0, Symbols\Expression.vb"

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

    '     Class Expression
    ' 
    '         Properties: comment, IsLiteralNothing, IsNumberLiteral, KindText
    ' 
    '         Function: GetUserType, ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' The abstract ``S-Expression`` model.(一个表达式树)
    ''' </summary>
    Public MustInherit Class Expression

        Public Property comment As String

        Public ReadOnly Property IsNumberLiteral As Boolean
            Get
                Return TypeOf Me Is LiteralExpression AndAlso TypeInfer(Nothing).type Like TypeExtensions.NumberOrders
            End Get
        End Property

        Public Overridable ReadOnly Property IsLiteralNothing As Boolean
            Get
                If Not TypeOf Me Is LiteralExpression Then
                    Return False
                Else
                    Return DirectCast(Me, LiteralExpression).IsLiteralNothing
                End If
            End Get
        End Property

        Public ReadOnly Property KindText As String
            Get
                Return MyClass.GetType.FullName
            End Get
        End Property

        ''' <summary>
        ''' Get the webassembly data type of this expression that will be generated.
        ''' </summary>
        ''' <param name="symbolTable">local/global/function calls</param>
        ''' <returns></returns>
        Public MustOverride Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
        Public MustOverride Function ToSExpression() As String
        ''' <summary>
        ''' 递归的获取这个表达式树所引用的所有的符号列表
        ''' </summary>
        ''' <returns></returns>
        Public MustOverride Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)

        Public Function GetUserType(symbols As SymbolTable) As ClassMeta
            Dim type As TypeAbstract = TypeInfer(symbols)
            Dim define As ClassMeta = symbols.GetClassType(type.raw)

            Return define
        End Function

        ''' <summary>
        ''' S-Expression debug previews
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            Return ToSExpression()
        End Function

    End Class
End Namespace
