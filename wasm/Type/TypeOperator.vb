Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Symbols

Namespace TypeInfo

    Public Module TypeOperator

        ''' <summary>
        ''' VisualBasic.NET operator to webassembly operator name
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property wasmOpName As New Dictionary(Of String, String) From {
            {"+", "add"},
            {"-", "sub"},
            {"*", "mul"},
            {"/", "div"},
            {"^", "$pow"},
            {"=", "eq"},
            {"<>", "ne"}
        }

        ''' <summary>
        ''' i32的加法运算
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property i32Add As New ReferenceSymbol With {
            .Symbol = $"i32.{wasmOpName("+")}",
            .Type = SymbolType.Operator
        }

        Public ReadOnly Property i32Multiply As New ReferenceSymbol With {
            .symbol = $"i32.{wasmOpName("*")}",
            .Type = SymbolType.Operator
        }

        Public ReadOnly Property i32Minus As New ReferenceSymbol With {
            .symbol = $"i32.{wasmOpName("-")}",
            .Type = SymbolType.Operator
        }

        Friend ReadOnly unaryOp As Index(Of String) = {
            wasmOpName("+"),
            wasmOpName("-")
        }
        Friend ReadOnly integerType As Index(Of String) = {"i32", "i64"}
        Friend ReadOnly floatType As Index(Of String) = {"f32", "f64"}

        Public ReadOnly Property Comparison As Index(Of String) = {"f32", "f64", "i32", "i64"} _
            .Select(Function(type)
                        Return {">", ">=", "<", "<=", "="}.Select(Function(op) Compares(type, op))
                    End Function) _
            .IteratesALL _
            .ToArray

        ''' <summary>
        ''' 值比较函数返回的是一个整型数
        ''' </summary>
        ''' <param name="type"></param>
        ''' <param name="op"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 
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
                Case Else
                    If type = "i32" Then
                        Return I32ByteOperator(op)
                    ElseIf type = "boolean" Then
                        Return BooleanLogical(op)
                    Else
                        Throw New NotImplementedException
                    End If
            End Select
        End Function

        Public Function I32ByteOperator(op As String) As String
            ' 有一些位相关的操作只能够执行在i32上面
            Select Case op
                Case "<<" : Return "i32.shl"
                Case ">>" : Return "i32.shr_s"
                Case "And" : Return "i32.and"
                Case "Or" : Return "i32.or"
                Case Else
                    Throw New NotImplementedException
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