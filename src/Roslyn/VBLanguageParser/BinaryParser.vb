Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module BinaryParser

        <Extension>
        Public Function GetBinary(bin As BinaryExpressionSyntax, context As Environment) As WATSyntax
            Dim left As WATSyntax = bin.Left.ParseValue(context)
            Dim right As WATSyntax = bin.Right.ParseValue(context)
            Dim opName As String = bin.OperatorToken.ValueText

            Return BinaryStack(left, right, opName, context)
        End Function

        ''' <summary>
        ''' NOTE: div between two integer will convert to double div automatic. 
        ''' </summary>
        ''' <returns></returns>
        Public Function BinaryStack(left As WATSyntax, right As WATSyntax, op$, context As Environment) As WATSyntax
            Dim type As WATType

            If op = "/" Then
                ' require type conversion if left and right is integer
                ' 对于除法，必须要首先转换为浮点型才能够完成运算
                left = CTypeHandle.CDbl(left, context)
                right = CTypeHandle.CDbl(right, context)

                Return New BinaryOperator With {
                    .left = left,
                    .[operator] = op,
                    .right = right
                }
            ElseIf op.isBinaryBool(left, right) Then
                Return New BooleanLogical(context.DoLogical(left, right, op))
            ElseIf op Like ComparisonOperators Then
                Return context.DoComparison(left, right, op)
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
    End Module
End Namespace