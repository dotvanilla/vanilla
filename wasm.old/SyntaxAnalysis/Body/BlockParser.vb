#Region "Microsoft.VisualBasic::45141c51213650d2858624888718b71d, SyntaxAnalysis\Body\BlockParser.vb"

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

    '     Module BlockParser
    ' 
    '         Function: AutoDropValueStack, IfBlock
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.Blocks

Namespace SyntaxAnalysis

    Module BlockParser

        <Extension>
        Public Function IfBlock(doIf As MultiLineIfBlockSyntax, symbols As SymbolTable) As Expression
            Dim test As New BooleanSymbol With {
                .condition = doIf _
                    .IfStatement _
                    .Condition _
                    .ValueExpression(symbols)
            }
            Dim elseBlock As Expression()
            Dim thenBlock As Expression() = doIf.Statements _
                .ParseBlockInternal(symbols) _
                .ToArray

            If Not doIf.ElseBlock Is Nothing Then
                elseBlock = doIf.ElseBlock _
                    .Statements _
                    .ParseBlockInternal(symbols) _
                    .ToArray
            Else
                elseBlock = {}
            End If

            Return New IfBlock With {
                .condition = test,
                .[then] = thenBlock,
                .[else] = elseBlock
            }
        End Function

        <Extension>
        Public Iterator Function AutoDropValueStack(lineSymbols As [Variant](Of Expression, Expression()), symbols As SymbolTable) As IEnumerable(Of Expression)
            If lineSymbols.GetUnderlyingType.IsInheritsFrom(GetType(Expression)) Then
                lineSymbols = {lineSymbols.TryCast(Of Expression)}
            End If

            For Each line In lineSymbols.TryCast(Of Expression())
                If Not TypeOf line Is ReturnValue AndAlso line.TypeInfer(symbols) <> "void" Then
                    ' https://github.com/WebAssembly/wabt/issues/1067
                    '
                    ' required a drop if target produce values
                    Yield New drop With {.expression = line}
                Else
                    Yield line
                End If
            Next
        End Function
    End Module
End Namespace
