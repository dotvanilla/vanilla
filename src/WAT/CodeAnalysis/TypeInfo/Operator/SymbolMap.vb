﻿Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection

Namespace CodeAnalysis.TypeInfo.Operator

    Public Module SymbolMap

        ReadOnly SymbolMaps As New Dictionary(Of String, String) From {
            {"+", "add"},
            {"-", "sub"},
            {"*", "mul"},
            {"/", "div"}
        }

        Public ReadOnly Property LogicalOperators As Index(Of String) = {"And", "Or", "AndAlso", "OrElse"}
        Public ReadOnly Property ComparisonOperators As Index(Of String) = {">", ">=", "<", "<=", "=", "<>"}

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function IsValidDirectMapOperator(op As String) As Boolean
            Return SymbolMaps.ContainsKey(op)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetDirectMapOperator(op As String) As String
            Return SymbolMaps(op)
        End Function

        Public Function GetOperator(type As WATType, [operator] As String) As String
            If SymbolMaps.ContainsKey([operator]) Then
                Return $"{type.UnderlyingWATType.Description}.{SymbolMaps([operator])}"
            ElseIf [operator] Like LogicalOperators Then
                Return BooleanLogical([operator])
            Else
                Return Compares(type.UnderlyingWATType.ToString, [operator])
            End If
        End Function

        Public Function BooleanLogical(op As String) As String
            Select Case op
                Case "And", "AndAlso"
                    ' 逻辑与是乘法操作
                    Return $"i32.{SymbolMaps("*")}"
                Case "Or", "OrElse"
                    ' 逻辑或是加法操作
                    Return $"i32.{SymbolMaps("+")}"
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

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
                    ElseIf type = NameOf(WATElements.boolean) Then
                        Return $"i32.eq"
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
    End Module
End Namespace