#Region "Microsoft.VisualBasic::add454f5ee84a8b7d90a7bf5a709a4ac, Symbols\Blocks\Loop.vb"

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
'     Class [Loop]
' 
'         Properties: loopID
' 
'         Function: ToSExpression, TypeInfer
' 
'     Class br
' 
'         Properties: blockLabel
' 
'         Function: ToSExpression, TypeInfer
' 
'     Class br_if
' 
'         Properties: condition
' 
'         Function: ToSExpression, TypeInfer
' 
'     Class drop
' 
'         Properties: expression
' 
'         Function: ToSExpression, TypeInfer
' 
' 
' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.Blocks

    Public Class [Loop] : Inherits Block

        Public Property loopID As String

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract("void")
        End Function

        Public Overrides Function ToSExpression() As String
            Return $"
(block ${guid} 
    (loop ${loopID}

        {InternalBlock(internal, "        ")}

    )
)"
        End Function
    End Class

    Public Class br : Inherits Expression

        Public Property blockLabel As String

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract("void")
        End Function

        Public Overrides Function ToSExpression() As String
            Return $"(br ${blockLabel})"
        End Function
    End Class

    Public Class br_if : Inherits br

        ''' <summary>
        ''' Is a logical expression
        ''' </summary>
        ''' <returns></returns>
        Public Property condition As BooleanSymbol

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract("void")
        End Function

        Public Overrides Function ToSExpression() As String
            Return $"(br_if ${blockLabel} {condition})"
        End Function
    End Class

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Your imported function $array_push has a return value of i32. You does not consume 
    ''' this value in the then block. Your if block is declared as void or []. You need to 
    ''' consume the i32 (for example with a drop) or change the return type of the if 
    ''' expression to (result i32).
    ''' 
    ''' https://github.com/WebAssembly/wabt/issues/1067
    ''' </remarks>
    Public Class drop : Inherits Expression

        Public Property expression As Expression

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract("void")
        End Function

        Public Overrides Function ToSExpression() As String
            Return $"(drop {expression.ToSExpression})"
        End Function
    End Class
End Namespace
