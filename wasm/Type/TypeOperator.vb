#Region "Microsoft.VisualBasic::0a66c5698bfadec20f6437049f396a4d, Type\TypeOperator.vb"

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

    '     Module TypeOperator
    ' 
    '         Properties: Comparison, i32Add, i32Minus, i32Multiply, LogicalOperators
    ' 
    '         Function: BooleanLogical, Compares, GetDirectMapOperator, I32ByteOperator, IsValidDirectMapOperator
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Symbols

Namespace TypeInfo

    Public Module TypeOperator

        ''' <summary>
        ''' VisualBasic.NET operator to webassembly operator name
        ''' </summary>
        ReadOnly wasmOpName As New Dictionary(Of String, String) From {
            {"+", "add"},
            {"-", "sub"},
            {"*", "mul"},
            {"/", "div"}
        }

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function IsValidDirectMapOperator(op As String) As Boolean
            Return wasmOpName.ContainsKey(op)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetDirectMapOperator(op As String) As String
            Return wasmOpName(op)
        End Function

        ''' <summary>
        ''' i32的加法运算
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property i32Add As New ReferenceSymbol With {
            .symbol = $"i32.{wasmOpName("+")}",
            .type = SymbolType.Operator
        }

        Public ReadOnly Property i32Multiply As New ReferenceSymbol With {
            .symbol = $"i32.{wasmOpName("*")}",
            .type = SymbolType.Operator
        }

        Public ReadOnly Property i32Minus As New ReferenceSymbol With {
            .symbol = $"i32.{wasmOpName("-")}",
            .type = SymbolType.Operator
        }

        Friend ReadOnly unaryOp As Index(Of String) = {wasmOpName("+"), wasmOpName("-")}
        Friend ReadOnly integerType As Index(Of String) = {"i32", "i64"}
        Friend ReadOnly floatType As Index(Of String) = {"f32", "f64"}
        Friend ReadOnly comparisonOp As Index(Of String) = {">", ">=", "<", "<=", "=", "<>"}

        ''' <summary>
        ''' 数值之间的比较运算符
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Comparison As Index(Of String) = {"f32", "f64", "i32", "i64"} _
            .Select(Function(type)
                        Return {">", ">=", "<", "<=", "="}.Select(Function(op) Compares(type, op))
                    End Function) _
            .IteratesALL _
            .ToArray

        Public ReadOnly Property LogicalOperators As Index(Of String) = {"And", "Or", "AndAlso", "OrElse"}

        ''' <summary>
        ''' 值比较函数返回的是一个整型数
        ''' </summary>
        ''' <param name="type"></param>
        ''' <param name="op"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 在这里应该产生的是一个逻辑值比较
        ''' </remarks>
        Public Function Compares(type$, op$) As String
            Select Case op
                Case ">"
                    If type Like integerType Then
                        Return $"{type}.gt_s"
                    Else
                        Return $"{type}.gt"
                    End If
                Case ">="
                    If type Like integerType Then
                        Return $"{type}.ge_s"
                    Else
                        Return $"{type}.ge"
                    End If
                Case "<"
                    If type Like integerType Then
                        Return $"{type}.lt_s"
                    Else
                        Return $"{type}.lt"
                    End If
                Case "<="
                    If type Like integerType Then
                        Return $"{type}.le_s"
                    Else
                        Return $"{type}.le"
                    End If
                Case "="
                    If type Like integerType OrElse type Like floatType Then
                        Return $"{type}.eq"
                    Else
                        Throw New NotImplementedException
                    End If
                Case "<>"
                    If type Like integerType OrElse type Like floatType Then
                        Return $"{type}.ne"
                    Else
                        Throw New NotImplementedException
                    End If
                Case Else
                    If type = "boolean" Then
                        Return BooleanLogical(op)
                    ElseIf type = "i32" Then
                        Return Nothing
                    Else
                        Throw New NotImplementedException
                    End If
            End Select
        End Function

        ''' <summary>
        ''' i32整形数的位运算符号
        ''' </summary>
        ''' <param name="op"></param>
        ''' <returns></returns>
        Public Function I32ByteOperator(op As String) As String
            ' 有一些位相关的操作只能够执行在i32上面
            Select Case op
                Case "<<" : Return "i32.shl"
                Case ">>" : Return "i32.shr_s"
                Case "And" : Return "i32.and"
                Case "Or" : Return "i32.or"
                Case "Mod" : Return "i32.rem_s"
                Case Else
                    Throw New NotImplementedException(op)
            End Select
        End Function

        Public Function BooleanLogical(op As String) As String
            Select Case op
                Case "And", "AndAlso"
                    ' 逻辑与是乘法操作
                    Return $"i32.{wasmOpName("*")}"
                Case "Or", "OrElse"
                    ' 逻辑或是加法操作
                    Return $"i32.{wasmOpName("+")}"
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
    End Module
End Namespace
