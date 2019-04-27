Namespace Symbols

    ''' <summary>
    ''' The object model for function/operator calls in S-Expression. 
    ''' 
    ''' (一般的函数调用表达式，也包括运算符运算)
    ''' </summary>
    Public Class FuncInvoke : Inherits Expression

        ''' <summary>
        ''' Function reference string
        ''' </summary>
        ''' <returns></returns>
        Public Property refer As String
        ''' <summary>
        ''' The argument value expression that passing to the target function
        ''' </summary>
        ''' <returns></returns>
        Public Property parameters As Expression()
        ''' <summary>
        ''' Is current function invoke is operator invoke?
        ''' </summary>
        ''' <returns></returns>
        Public Property [operator] As Boolean

        Sub New()
        End Sub

        Sub New(funcName As String)
            refer = funcName
        End Sub

        Sub New(target As FuncSignature)
            refer = target.Name
        End Sub

        Public Overrides Function ToSExpression() As String
            Dim arguments = parameters _
                .Select(Function(a)
                            Return a.ToSExpression
                        End Function) _
                .JoinBy(" ")

            If [operator] Then
                Return $"({refer} {arguments})"
            Else
                Return $"(call ${refer} {arguments})"
            End If
        End Function

        Private Shared Function typeFromOperator(refer As String) As TypeAbstract
            If refer Like TypeExtensions.Comparison Then
                ' WebAssembly comparison operator produce integer value
                Return New TypeAbstract(TypeAlias.i32)
            Else
                Return New TypeAbstract(refer.Split("."c).First)
            End If
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            If [operator] Then
                Return typeFromOperator(refer)
            Else
                Return funcTypeInfer(symbolTable)
            End If
        End Function

        ''' <summary>
        ''' 这里还需要处理一般的函数调用以及数组或者字典的一些操作
        ''' </summary>
        ''' <param name="symbolTable"></param>
        ''' <returns></returns>
        Private Function funcTypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Dim func As FuncSignature
            Dim obj As Expression

            If parameters.IsNullOrEmpty Then
                func = symbolTable.GetFunctionSymbol(Nothing, refer)
            Else
                obj = parameters(Scan0)

                ' 在这里需要对值元素类型为数组的字典引用进行一些额外的处理
                If TypeOf obj Is FuncInvoke AndAlso DirectCast(obj, FuncInvoke).refer = JavaScriptImports.Dictionary.GetValue.Name Then
                    Dim table = DirectCast(obj, FuncInvoke).parameters(0)

                    If TypeOf table Is GetLocalVariable Then
                        Dim tableObj = symbolTable.GetObjectSymbol(DirectCast(table, GetLocalVariable).var)

                        If TypeOf tableObj Is DeclareLocal Then
                            With DirectCast(tableObj, DeclareLocal)
                                If TypeExtensions.IsArray(.genericTypes(1)) Then
                                    Return .genericTypes(1).Trim("["c, "]"c)
                                Else
                                    Return .genericTypes(1)
                                End If
                            End With
                        Else
                            Throw New NotImplementedException
                        End If
                    Else
                        Throw New NotImplementedException
                    End If

                    Throw New NotImplementedException
                Else
                    func = symbolTable.GetFunctionSymbol(obj.TypeInfer(symbolTable), refer)
                End If
            End If

            Return func.result
        End Function
    End Class
End Namespace