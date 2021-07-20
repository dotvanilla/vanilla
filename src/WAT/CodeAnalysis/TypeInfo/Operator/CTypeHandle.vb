Imports VanillaBasic.WebAssembly.Compiler
Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis.TypeInfo.Operator

    Public Module CTypeHandle

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="leftGeneric">左边的数组表达式的元素的类型</param>
        ''' <param name="right"></param>
        ''' <param name="context"></param>
        ''' <returns></returns>
        Public Function CastArray(leftGeneric As WATType, right As ArrayLiteral, context As Environment) As WATSyntax
            For i As Integer = 0 To right.Size - 1
                ' cast each array element
                right(i) = CTypeHandle.[CType](leftGeneric, right(i), context)
            Next

            Return right
        End Function

        ''' <summary>
        ''' ``CType`` operator to webassembly 
        ''' ``Datatype conversions, truncations, reinterpretations, promotions, and demotions`` feature.
        ''' 
        ''' > https://github.com/WebAssembly/design/blob/master/Semantics.md
        ''' </summary>
        ''' <param name="left"></param>
        ''' <returns></returns>
        Public Function [CType](left As WATType, right As WATSyntax, context As Environment) As WATSyntax
            Dim rightTypeInfer As WATType = right.Type

            If left.UnderlyingWATType = WATElements.array AndAlso TypeOf right Is ArrayLiteral Then
                Return CastArray(left.Generic(Scan0), right, context)
            ElseIf left.UnderlyingWATType = WATElements.any OrElse left.UnderlyingWATType = rightTypeInfer.UnderlyingWATType Then
                ' Conversion is not required when
                '
                ' 1. left accept any type
                ' 2. left is the same type as right
                Return right
            ElseIf right.isLiteralNothing Then
                ' nothing 可以赋值给任意类型
                Return LiteralValue.Nothing(left)
            ElseIf TypeOf right Is LiteralValue AndAlso IsDirectCastCapable(left.UnderlyingWATType, rightTypeInfer.UnderlyingWATType) Then
                ' 如果是常数表达式的话，则直接修改常数表达式的类型
                Return New LiteralValue(DirectCast(right, LiteralValue).Value, left) With {
                    .Annotation = $"DirectCast From {rightTypeInfer.ToString}"
                }
            End If

            Select Case left.UnderlyingWATType
                Case WATElements.i32
                    If left Is WATType.boolean Then
                        Return CTypeHandle.CBool(right, context)
                    Else
                        Return CTypeHandle.CInt(right, context)
                    End If
                Case WATElements.i64
                    Return CTypeHandle.CLng(right, context)
                Case WATElements.f32
                    Return CTypeHandle.CSng(right, context)
                Case WATElements.f64
                    Return CTypeHandle.CDbl(right, context)
                Case WATElements.string
                    ' 左边是字符串类型，但是右边不是字符串或者整形数
                    ' 则说明是一个需要将目标转换为字符串的操作
                    Return right.AnyToString(context)
                    'Case WATElements.intptr
                    '    ' 左边是一个内存指针，则右边如果是非基础类型的话
                    '    ' 可以直接赋值
                    '    If rightTypeInfer.UnderlyingWATType Like TypeExtensions.NumberOrders Then
                    '        ' 是基础类型
                    '        ' 则抛出错误
                    '        Throw New InvalidCastException
                    '    Else
                    '        ' 不需要转换，直接赋值
                    '        Return right
                    '    End If
                Case Else
                    'If rightTypeInfer.UnderlyingWATType = WATElements.intptr Then
                    '    If Not left.UnderlyingWATType Like TypeExtensions.NumberOrders Then
                    '        ' right side is intptr 
                    '        ' and left is reference type
                    '        ' cast directly
                    '        Call $"direct cast intptr to reference type {left}".__DEBUG_ECHO
                    '        Return right
                    '    End If
                    'End If

                    Throw New InvalidCastException($"{rightTypeInfer} -> {left}")
            End Select
        End Function

        Public Function [CBool](exp As WATSyntax, context As Environment) As WATSyntax
            Dim type As WATType = exp.Type
            Dim operator$ = Nothing

            Select Case type.UnderlyingWATType
                Case WATElements.i32
                    ' 直接转换
                    Return exp
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        Public Function [CInt](exp As WATSyntax, context As Environment) As WATSyntax
            Dim type As WATType = exp.Type
            Dim operator$

            Select Case type.UnderlyingWATType
                Case WATElements.i32
                    Return exp
                Case WATElements.i64
                    [operator] = "i32.wrap/i64"
                Case WATElements.f32
                    [operator] = "i32.trunc_s/f32"
                Case WATElements.f64
                    [operator] = "i32.trunc_s/f64"
                Case Else
                    Throw New NotImplementedException
            End Select

            Return New UnaryOperator([operator], exp)
        End Function

        Public Function [CLng](exp As WATSyntax, context As Environment) As WATSyntax
            Dim type As WATType = exp.Type
            Dim operator$

            Select Case type.UnderlyingWATType
                Case WATElements.i32
                    [operator] = "i64.extend_s/i32"
                Case WATElements.i64
                    Return exp
                Case WATElements.f32
                    [operator] = "i64.trunc_s/f32"
                Case WATElements.f64
                    [operator] = "i64.trunc_s/f64"
                Case Else
                    Throw New NotImplementedException
            End Select

            Return New UnaryOperator([operator], exp)
        End Function

        Public Function [CSng](exp As WATSyntax, context As Environment) As WATSyntax
            Dim type As WATType = exp.Type
            Dim operator$

            Select Case type.UnderlyingWATType
                Case WATElements.i32
                    [operator] = "f32.convert_s/i32"
                Case WATElements.i64
                    [operator] = "f32.convert_s/i64"
                Case WATElements.f32
                    Return exp
                Case WATElements.f64
                    [operator] = "f32.demote/f64"
                Case Else
                    Throw New NotImplementedException
            End Select

            Return New UnaryOperator([operator], exp)
        End Function

        Public Function [CDbl](exp As WATSyntax, context As Environment) As WATSyntax
            Dim type As WATType = exp.Type
            Dim operator$

            Select Case type.UnderlyingWATType
                Case WATElements.i32
                    [operator] = "f64.convert_s/i32"
                Case WATElements.i64
                    [operator] = "f64.convert_s/i64"
                Case WATElements.f32
                    [operator] = "f64.promote/f32"
                Case WATElements.f64
                    Return exp
                Case Else
                    Throw New NotImplementedException(type.UnderlyingWATType.Description)
            End Select

            Return New UnaryOperator([operator], exp)
        End Function
    End Module
End Namespace