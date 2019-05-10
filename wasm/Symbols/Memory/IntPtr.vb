Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public Class IntPtr : Inherits Expression

        Public Property expression As Expression

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

        Public Shared Operator *(intptr As IntPtr, n As Integer) As IntPtr
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