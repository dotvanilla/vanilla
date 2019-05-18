#Region "Microsoft.VisualBasic::c8a0ac0b3e1ccab24bf6f0a1f9e52351, Symbols\Blocks\If.vb"

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

    '     Class IfBlock
    ' 
    '         Properties: [else], [then], condition
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.Blocks

    Public Class IfBlock : Inherits AbstractBlock

        Public Property condition As BooleanSymbol
        Public Property [then] As ExpressionGroup
        Public Property [else] As ExpressionGroup

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract("void")
        End Function

        Public Overrides Function ToSExpression() As String
            Dim else$ = ""

            If Not Me.else.IsNullOrEmpty Then
                [else] = $"(else
        {Block.InternalBlock(Me.else, "        ")}
    )"
            End If

            Return $"
(if {condition} 
    (then
        {Block.InternalBlock([then], "        ")}
    ) {[else]}
)"
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            For Each symbol In condition.GetSymbolReference
                Yield symbol
            Next

            If Not [then].IsNullOrEmpty Then
                For Each symbol In [then].GetSymbolReference
                    Yield symbol
                Next
            End If

            If Not [else].IsNullOrEmpty Then
                For Each symbol In [else].GetSymbolReference
                    Yield symbol
                Next
            End If
        End Function
    End Class
End Namespace
