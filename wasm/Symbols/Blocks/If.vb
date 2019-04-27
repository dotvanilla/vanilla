#Region "Microsoft.VisualBasic::acacb417f9d7907d257585db04326f57, Symbols\Blocks\If.vb"

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
    '         Properties: [Else], [Then], Condition
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Symbols.Blocks

    Public Class IfBlock : Inherits AbstractBlock

        Public Property Condition As BooleanSymbol
        Public Property [Then] As Expression()
        Public Property [Else] As Expression()

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract("void")
        End Function

        Public Overrides Function ToSExpression() As String
            Dim else$ = ""

            If Not Me.Else.IsNullOrEmpty Then
                [else] = $"(else
        {Block.InternalBlock(Me.[Else], "        ")}
    )"
            End If

            Return $"
(if {Condition} 
    (then
        {Block.InternalBlock([Then], "        ")}
    ) {[else]}
)"
        End Function
    End Class
End Namespace
