#Region "Microsoft.VisualBasic::905f411628039a54cba76ae384a9494c, Symbols\Expression.vb"

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

        Public ReadOnly Property isNumberLiteral As Boolean
            Get
                Return TypeOf Me Is LiteralExpression AndAlso TypeInfer(Nothing).type Like TypeExtensions.NumberOrders
            End Get
        End Property

        ''' <summary>
        ''' Current expression is a literal token expression of ``Nothing`` or ``null`` in general programing language.
        ''' </summary>
        ''' <returns></returns>
        Public Overridable ReadOnly Property isLiteralNothing As Boolean
            Get
                If Not TypeOf Me Is LiteralExpression Then
                    Return False
                Else
                    Return DirectCast(Me, LiteralExpression).isLiteralNothing
                End If
            End Get
        End Property

        Public ReadOnly Property KindText As String
            Get
                Return MyClass.GetType.FullName
            End Get
        End Property

        ''' <summary>
        ''' Debug used only, get <see cref="TypeInfer(SymbolTable)"/> value when missing <see cref="SymbolTable"/>.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property likely As TypeAbstract
            Get
                Try
                    Return TypeInfer(Nothing)
                Catch ex As Exception
                    ' 在没有符号表对象的情况下，目前无法推断出当前的这个表达式的类型
                    Return New TypeAbstract(TypeAlias.any)
                End Try
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

        Public Function ToBase() As Expression
            Return Me
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
