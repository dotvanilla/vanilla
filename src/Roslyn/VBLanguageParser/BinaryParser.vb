Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module BinaryParser

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function BinaryCompares(left As WATSyntax, right As WATSyntax, op$, context As Environment) As BooleanLogical
            Return New BooleanLogical(BinaryParser.BinaryStack(left, right, op, context))
        End Function

        <Extension>
        Public Function FixSymbol(data As WATSyntax, context As Environment) As WATSyntax
            If Not TypeOf data Is SymbolReference Then
                Return data
            End If

            Dim symbolName As String = DirectCast(data, SymbolReference).Name

            If context.Level = SymbolTypes.Function Then
                If context.Symbols.ContainsKey(symbolName) Then
                    Return New SymbolGetValue(DirectCast(data, SymbolReference)) With {.isGlobal = False}
                ElseIf symbolName <> context.SymbolName Then
                    Return New SymbolGetValue(DirectCast(data, SymbolReference)) With {.isGlobal = True}
                End If
            End If

            Throw New NotImplementedException
        End Function

        <Extension>
        Public Function GetBinary(bin As BinaryExpressionSyntax, context As Environment) As WATSyntax
            Dim left As WATSyntax = bin.Left.ParseValue(context).FixSymbol(context)
            Dim right As WATSyntax = bin.Right.ParseValue(context).FixSymbol(context)
            Dim opName As String = bin.OperatorToken.ValueText

            Return BinaryStack(left, right, opName, context)
        End Function

        ''' <summary>
        ''' NOTE: div between two integer will convert to double div automatic. 
        ''' </summary>
        ''' <returns></returns>
        Public Function BinaryStack(left As WATSyntax, right As WATSyntax, op$, context As Environment) As WATSyntax
            Dim type As WATType

            If op = "&" OrElse (op = "+" AndAlso (left.Type Is WATType.string OrElse right.Type Is WATType.string)) Then
                ' 字符串连接：a & b / a + b
                Return MakeStringConcat(left, right, context)
            ElseIf op = "/" Then
                ' require type conversion if left and right is integer
                ' 对于除法，必须要首先转换为浮点型才能够完成运算
                left = CTypeHandle.CDbl(left, context)
                right = CTypeHandle.CDbl(right, context)

                Return New BinaryOperator With {
                    .left = left,
                    .[operator] = op,
                    .right = right
                }
            ElseIf op = "^" Then
                ' 幂运算 -> Math.Pow
                Return MakePower(left, right, context)
            ElseIf op = "Is" OrElse op = "IsNot" Then
                ' ``Is`` / ``IsNot`` 引用比较：按 i32 指针比较处理
                Return New BooleanLogical(New BinaryOperator With {
                    .left = ReferenceOperand(left, context),
                    .right = ReferenceOperand(right, context),
                    .[operator] = If(op = "Is", "=", "<>"),
                    .Annotation = op
                })
            ElseIf op.isBinaryBool(left, right) Then
                ' 布尔逻辑运算（And / Or / AndAlso / OrElse）
                Return New BooleanLogical(context.DoLogical(left, right, op))
            ElseIf op Like ComparisonOperators Then
                Return context.DoComparison(left, right, op)
            ElseIf op = "\" OrElse op = "Mod" OrElse op = "<<" OrElse op = ">>" OrElse
                   op = "And" OrElse op = "Or" OrElse op = "Xor" Then
                ' 整型位运算 / 整除 / 取模：两个操作数统一转换为 i32
                Return New BinaryOperator With {
                    .left = CTypeHandle.CInt(left, context),
                    .[operator] = op,
                    .right = CTypeHandle.CInt(right, context),
                    .Annotation = "i32 bit operation"
                }
            Else
                type = context.highOrderTransfer(left, right)
            End If

            Return New BinaryOperator With {
                .left = left,
                .right = right,
                .[operator] = op,
                .Annotation = "Binary"
            }
        End Function

        <Extension>
        Public Function DoComparison(context As Environment, left As WATSyntax, right As WATSyntax, op$) As WATSyntax
            Dim type As WATType = context.highOrderTransfer(left, right)
            Dim opSymbol As String = SymbolMap.Compares(type.UnderlyingWATType.Description, op)
            Dim result As WATSyntax

            If opSymbol Is Nothing Then
                ' 可能是i32位运算
                result = New BinaryOperator With {
                    .[operator] = op,
                    .left = left,
                    .right = right,
                    .Annotation = "i32 bit operation"
                }
            Else
                result = New BinaryOperator With {
                    .[operator] = op,
                    .left = left,
                    .right = right,
                    .Annotation = "logical comparision"
                }
                result = New BooleanLogical(result)
            End If

            Return result
        End Function

        <Extension>
        Private Function highOrderTransfer(context As Environment, ByRef left As WATSyntax, ByRef right As WATSyntax) As WATType
            Dim type As WATType

            ' 其他的运算符则需要两边的类型保持一致
            ' 往高位转换
            ' i32 -> f32 -> i64 -> f64
            Dim lt = left.Type
            Dim rt = right.Type
            Dim li = TypeExtensions.NumberOrders(lt.UnderlyingWATType)
            Dim ri = TypeExtensions.NumberOrders(rt.UnderlyingWATType)

            If li > ri Then
                type = lt
            Else
                type = rt
            End If

            left = CTypeHandle.CType(type, left, context)
            right = CTypeHandle.CType(type, right, context)

            Return type
        End Function

        ''' <summary>
        ''' 产生的是一个逻辑值表达式
        ''' </summary>
        ''' <param name="left"></param>
        ''' <param name="right"></param>
        ''' <param name="op$"></param>
        ''' <returns></returns>
        <Extension>
        Public Function DoLogical(context As Environment, left As WATSyntax, right As WATSyntax, op$) As WATSyntax
            left = CTypeHandle.CBool(left, context)
            right = CTypeHandle.CBool(right, context)

            Return New BinaryOperator With {
                .left = left,
                .right = right,
                .[operator] = op
            }
        End Function

        <Extension>
        Private Function isBinaryBool(op$, left As WATSyntax, right As WATSyntax) As Boolean
            Return op Like LogicalOperators AndAlso (left.Type Is WATType.boolean AndAlso right.Type Is WATType.boolean)
        End Function

        ''' <summary>
        ''' 引用比较的操作数：``Nothing`` 直接使用 i32 零值，其他值统一转换为 i32 指针
        ''' </summary>
        Private Function ReferenceOperand(value As WATSyntax, context As Environment) As WATSyntax
            If value Is Nothing OrElse value.isLiteralNothing Then
                Return New LiteralValue(0, WATType.i32)
            Else
                Return CTypeHandle.CInt(value, context)
            End If
        End Function

        ''' <summary>
        ''' 生成字符串连接调用 ``string.add``
        ''' </summary>
        Private Function MakeStringConcat(left As WATSyntax, right As WATSyntax, context As Environment) As WATSyntax
            Return New FunctionInvoke With {
                .Func = New JavaScriptTranslation(WATType.string) With {.JavaScript = "string.add"},
                .Arguments = {
                    CTypeHandle.[CType](WATType.string, left, context),
                    CTypeHandle.[CType](WATType.string, right, context)
                }
            }.SetReturnValue(WATType.string)
        End Function

        ''' <summary>
        ''' 生成幂运算调用 ``Math.Pow``（f64）
        ''' </summary>
        Private Function MakePower(left As WATSyntax, right As WATSyntax, context As Environment) As WATSyntax
            Return New FunctionInvoke With {
                .Func = New JavaScriptTranslation(WATType.f64) With {.JavaScript = "Math.Pow"},
                .Arguments = {
                    CTypeHandle.CDbl(left, context),
                    CTypeHandle.CDbl(right, context)
                }
            }.SetReturnValue(WATType.f64)
        End Function
    End Module
End Namespace