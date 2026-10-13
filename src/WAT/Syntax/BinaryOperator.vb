Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator

Namespace Syntax

    Public Class BinaryOperator : Inherits WATSyntax

        Public Overrides ReadOnly Property Type As WATType
            Get
                Return CheckWATType()
            End Get
        End Property

        Public Property [operator] As String
        Public Property left As WATSyntax
        Public Property right As WATSyntax

        Private Function CheckWATType() As WATType
            Select Case [operator]
                Case "+", "-", "*"
                    Dim left As WATType = Me.left.Type
                    Dim right As WATType = Me.right.Type

                    If left Is right Then
                        Return left
                    ElseIf left.UnderlyingWATType > right.UnderlyingWATType Then
                        Return left
                    Else
                        Return right
                    End If
                Case "AndAlso", "OrElse"
                    Return WATType.boolean
                Case "<<", ">>"
                    Return left.Type
                Case Else
                    Return WATType.boolean
            End Select
        End Function

        Public Overrides Function ToString() As String
            Return $"{Type} ({left} {[operator]} {right})"
        End Function

        Private Shared ReadOnly compareOperators As String() = {">", ">=", "<", "<=", "=", "<>"}

        Private Function IsComparison(op As String) As Boolean
            Return Array.IndexOf(compareOperators, op) >= 0
        End Function

        ''' <summary>
        ''' 比较运算符的指令变体（例如 ``i32.gt_s`` 与 ``f64.gt``）是由<strong>操作数</strong>的类型
        ''' 决定的，而不是由比较表达式的结果类型（``boolean``）决定的。以前这里直接使用
        ''' ``Type``（对比较表达式的求值结果是 ``boolean``）去生成指令，会得到 ``boolean.gt``
        ''' 这种非法的 wasm 指令。
        ''' </summary>
        Private Function ComparisonType() As WATType
            Dim l As WATType = Me.left.Type
            Dim r As WATType = Me.right.Type
            Dim t As WATType

            If l = r Then
                t = l
            ElseIf l.UnderlyingWATType > r.UnderlyingWATType Then
                t = l
            Else
                t = r
            End If

            Select Case t.UnderlyingWATType
                Case WATElements.i32, WATElements.i64, WATElements.f32, WATElements.f64
                    Return t
                Case Else
                    ' 非数值类型（例如字符串指针、boolean）退回到默认的逻辑比较语义
                    Return Type
            End Select
        End Function

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Dim opType As WATType = If(IsComparison([operator]), ComparisonType(), Type)

            Return $"({SymbolMap.GetOperator(opType, [operator])} {left.ToSExpression(env, indent)} {right.ToSExpression(env, indent)})"
        End Function
    End Class
End Namespace