#Region "Microsoft.VisualBasic::ec4c727b5edc2821b9965242cfdff0e8, Symbols\Blocks\Block.vb"

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

    '     Class AbstractBlock
    ' 
    '         Properties: guid
    ' 
    '         Function: GetDeclareLocals
    ' 
    '     Class Block
    ' 
    '         Properties: internal
    ' 
    '         Function: InternalBlock
    ' 
    '     Class ExpressionGroup
    ' 
    '         Properties: group
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Text
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.Blocks

    Public MustInherit Class AbstractBlock : Inherits Expression

        ''' <summary>
        ''' The label of this block
        ''' </summary>
        ''' <returns></returns>
        Public Property guid As String

        ''' <summary>
        ''' By default no declares, returns an empty array 
        ''' </summary>
        ''' <returns></returns>
        Friend Overridable Iterator Function GetDeclareLocals() As IEnumerable(Of DeclareLocal)
            ' empty
        End Function
    End Class

    Public MustInherit Class Block : Inherits AbstractBlock

        Public Property internal As Expression()

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Function InternalBlock(block As IEnumerable(Of Expression), indent As String) As String
            Return block _
                .SafeQuery _
                .Select(Function(line)
                            Return indent & line.ToSExpression
                        End Function) _
                .JoinBy(ASCII.LF)
        End Function
    End Class

    ''' <summary>
    ''' 这个和<see cref="Block"/>类型的语法结构不一样，只是单纯的将表达式分组
    ''' </summary>
    Public Class ExpressionGroup : Inherits Expression
        Implements IEnumerable(Of Expression)

        Public Property group As Expression()

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.void
        End Function

        Public Overrides Function ToSExpression() As String
            Return Block.InternalBlock(group, "    ")
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            For Each item In group
                For Each symbol In item.GetSymbolReference
                    Yield symbol
                Next
            Next
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of Expression) Implements IEnumerable(Of Expression).GetEnumerator
            For Each line In group
                Yield line
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
