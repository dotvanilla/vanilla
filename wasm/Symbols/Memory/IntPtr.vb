#Region "Microsoft.VisualBasic::05c727878141bb16c7f8824af88c6734, Symbols\Memory\IntPtr.vb"

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

    '     Class IntPtr
    ' 
    '         Properties: expression
    ' 
    '         Constructor: (+3 Overloads) Sub New
    '         Function: Add, Minus, Multiply, ToSExpression, TypeInfer
    '         Operators: (+2 Overloads) -, (+2 Overloads) *, (+2 Overloads) +
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public Class IntPtr : Inherits Expression

        Public Property expression As Expression

        Sub New()
        End Sub

        Sub New(i As Integer)
            expression = Literal.i32(i)
        End Sub

        Sub New(value As Expression)
            expression = value
        End Sub

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.i32
        End Function

        Public Overrides Function ToSExpression() As String
            Return expression.ToSExpression
        End Function

        Public Shared Function Add(a As Expression, b As Expression) As Expression
            Return New FuncInvoke() With {
                .[operator] = True,
                .parameters = {a, b},
                .refer = i32Add
            }
        End Function

        Public Shared Function Multiply(a As Expression, b As Expression) As Expression
            Return New FuncInvoke With {
                .[operator] = True,
                .parameters = {a, b},
                .refer = i32Multiply
            }
        End Function

        Public Shared Function Minus(a As Expression, b As Expression) As Expression
            Return New FuncInvoke With {
                .[operator] = True,
                .parameters = {a, b},
                .refer = i32Minus
            }
        End Function

        Public Shared Operator +(intptr As IntPtr, offset As Integer) As IntPtr
            Return New IntPtr With {.expression = Add(intptr, Literal.i32(offset))}
        End Operator

        Public Shared Operator +(intptr As IntPtr, offset As Expression) As IntPtr
            Return New IntPtr With {.expression = Add(intptr, offset)}
        End Operator

        Public Shared Operator *(intptr As IntPtr, n As Integer) As IntPtr
            Return New IntPtr With {.expression = Multiply(intptr, Literal.i32(n))}
        End Operator

        Public Shared Operator *(n As Integer, intptr As IntPtr) As IntPtr
            Return New IntPtr With {.expression = Multiply(intptr, Literal.i32(n))}
        End Operator

        Public Shared Operator -(intptr As IntPtr, n As Integer) As IntPtr
            Return New IntPtr With {.expression = Minus(intptr, Literal.i32(n))}
        End Operator

        Public Shared Operator -(n As Integer, intptr As IntPtr) As IntPtr
            Return New IntPtr With {.expression = Minus(Literal.i32(n), intptr)}
        End Operator
    End Class
End Namespace
