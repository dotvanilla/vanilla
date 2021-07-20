Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis.TypeInfo.Operator

    Public Module CTypeHandle

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

            If left.type = TypeAlias.array AndAlso TypeOf right Is ArraySymbol Then
                Return CastArray(left.generic(Scan0), right, context)
            ElseIf left.type = TypeAlias.any OrElse left.type = rightTypeInfer.type Then
                ' Conversion is not required when
                '
                ' 1. left accept any type
                ' 2. left is the same type as right
                Return right
            ElseIf right.isLiteralNothing Then
                ' nothing 可以赋值给任意类型
                Return Literal.Nothing(left)
            ElseIf TypeOf right Is LiteralExpression AndAlso IsDirectCastCapable(left.type, rightTypeInfer.type) Then
                ' 如果是常数表达式的话，则直接修改常数表达式的类型
                Return New LiteralExpression With {
                    .Type = left,
                    .value = DirectCast(right, LiteralExpression).value
                }
            End If

            Select Case left.type
                Case TypeAlias.i32
                    Return CTypeHandle.CInt(right, context)
                Case TypeAlias.i64
                    Return CTypeHandle.CLng(right, context)
                Case TypeAlias.f32
                    Return CTypeHandle.CSng(right, context)
                Case TypeAlias.f64
                    Return CTypeHandle.CDbl(right, context)
                Case TypeAlias.string
                    ' 左边是字符串类型，但是右边不是字符串或者整形数
                    ' 则说明是一个需要将目标转换为字符串的操作
                    Return right.AnyToString(context)
                Case TypeAlias.boolean
                    Return CTypeHandle.CBool(right, context)
                Case TypeAlias.intptr
                    ' 左边是一个内存指针，则右边如果是非基础类型的话
                    ' 可以直接赋值
                    If rightTypeInfer.type Like TypeExtensions.NumberOrders Then
                        ' 是基础类型
                        ' 则抛出错误
                        Throw New InvalidCastException
                    Else
                        ' 不需要转换，直接赋值
                        Return right
                    End If
                Case Else
                    If rightTypeInfer.type = TypeAlias.intptr Then
                        If Not left.type Like TypeExtensions.NumberOrders Then
                            ' right side is intptr 
                            ' and left is reference type
                            ' cast directly
                            Call $"direct cast intptr to reference type {left}".__DEBUG_ECHO
                            Return right
                        End If
                    End If

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