#Region "Microsoft.VisualBasic::fa9a83b59ad049339af09c2ccb13bebd, SyntaxAnalysis\StringParser.vb"

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

    '     Module StringParser
    ' 
    '         Function: AnyToString, getContent, StringAppend, StringExpression
    ' 
    '         Sub: addRequired, stringValue
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Module StringParser

        ''' <summary>
        ''' VB string concatenation
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="left"></param>
        ''' <param name="right"></param>
        ''' <returns></returns>
        <Extension>
        Public Function StringAppend(symbols As SymbolTable, left As Expression, right As Expression) As Expression
            Dim append = JavaScriptImports.String.Append

            ' try add required imports symbol
            Call symbols.addRequired(append)

            Return New FuncInvoke With {
                .parameters = {left, right},
                .refer = append,
                .[operator] = False
            }
        End Function

        ''' <summary>
        ''' 尝试添加编程所需的一些基本的API，例如字符串操作，数组操作等
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="symbol"></param>
        <Extension>
        Public Sub addRequired(symbols As SymbolTable, symbol As ImportSymbol)
            Dim ref$ = symbol.name

            If Not ref Like symbols.requires Then
                symbols.requires.Add(ref)
                symbols.AddImports(symbol)
            End If
        End Sub

        <Extension>
        Friend Sub stringValue(symbols As SymbolTable, ByRef value As Object)
            ' 是字符串类型，需要做额外的处理
            value = symbols.memory.AddString(value)

            Call symbols.addRequired(JavaScriptImports.String.Replace)
            Call symbols.addRequired(JavaScriptImports.String.Append)
            Call symbols.addRequired(JavaScriptImports.String.Length)
            Call symbols.addRequired(JavaScriptImports.String.IndexOf)
        End Sub

        <Extension>
        Public Function StringExpression(str As InterpolatedStringExpressionSyntax, symbols As SymbolTable) As Expression
            Dim tokens As InterpolatedStringContentSyntax() = str.Contents.ToArray
            Dim text As Expression = tokens(Scan0).getContent(symbols)
            Dim partval As Expression

            For Each part As InterpolatedStringContentSyntax In tokens.Skip(1)
                partval = part.getContent(symbols)
                text = symbols.StringAppend(text, partval)
            Next

            Return text
        End Function

        <Extension>
        Private Function getContent(str As InterpolatedStringContentSyntax, symbols As SymbolTable) As Expression
            If TypeOf str Is InterpolatedStringTextSyntax Then
                Dim value As Object

                value = DirectCast(str, InterpolatedStringTextSyntax).TextToken.ValueText
                symbols.stringValue(value)

                Return New LiteralExpression With {
                    .type = New TypeAbstract(TypeAlias.string),
                    .value = value
                }
            Else
                Return DirectCast(str, InterpolationSyntax) _
                    .Expression _
                    .ValueExpression(symbols) _
                    .AnyToString(symbols)
            End If
        End Function

        <Extension>
        Public Function AnyToString(value As Expression, symbols As SymbolTable) As Expression
            Dim type$ = value.TypeInfer(symbols).typefit
            Dim toString = JavaScriptImports.ToString(type)

            symbols.addRequired(toString)
            value = New FuncInvoke With {
                .[operator] = False,
                .refer = toString,
                .parameters = {value}
            }

            Return value
        End Function
    End Module
End Namespace
