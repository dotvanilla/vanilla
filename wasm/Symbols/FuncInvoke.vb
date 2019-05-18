#Region "Microsoft.VisualBasic::faea70aa560294175e10e83abacdd125, Symbols\FuncInvoke.vb"

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

    '     Class FuncInvoke
    ' 
    '         Properties: [operator], IsUnary, parameters, refer
    ' 
    '         Constructor: (+4 Overloads) Sub New
    '         Function: AsUnary, funcTypeInfer, GetSymbolReference, InternalApiReturnType, ToSExpression
    '                   typeFromOperator, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' The object model for function/operator calls in S-Expression. 
    ''' 
    ''' (一般的函数调用表达式，也包括运算符运算)
    ''' </summary>
    Public Class FuncInvoke : Inherits Expression

        ''' <summary>
        ''' Function reference string, [funcName => module/type]
        ''' </summary>
        ''' <returns></returns>
        Public Property refer As ReferenceSymbol
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

        ''' <summary>
        ''' 只有加减两种情况，并且第一个参数为零常数
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsUnary As Boolean
            Get
                If Not [operator] Then
                    Return False
                ElseIf parameters.Length = 0 Then
                    Return False
                ElseIf Not parameters(Scan0).IsNumberLiteral Then
                    Return False
                Else
                    Dim first As LiteralExpression = parameters(Scan0)

                    If Val(first.value) <> 0.0 Then
                        Return False
                    End If
                End If

                Dim op$ = refer.symbol.Split("."c).Last

                Return op Like TypeOperator.unaryOp
            End Get
        End Property

        Sub New()
        End Sub

        Sub New(module$, funcName As String)
            refer = New ReferenceSymbol With {
                .symbol = funcName,
                .[module] = [module],
                .type = SymbolType.Func
            }
        End Sub

        ''' <summary>
        ''' 这个构造函数用于生成<see cref="refer"/>引用符号
        ''' </summary>
        ''' <param name="target"></param>
        Sub New(target As FuncSignature)
            refer = New ReferenceSymbol With {
                .symbol = target.name,
                .[module] = target.module,
                .type = SymbolType.Func
            }
        End Sub

        Sub New(target As ImportSymbol)
            refer = New ReferenceSymbol With {
                .symbol = target.name,
                .[module] = If(target.definedInModule, target.module, Nothing),
                .type = SymbolType.Api
            }
        End Sub

        Public Function AsUnary(type As TypeAbstract) As LiteralExpression
            If Not IsUnary Then
                Throw New InvalidCastException
            End If

            If refer.symbol.Split("."c).Last = "add" Then
                ' 直接返回第二个参数
                Return parameters(1)
            Else
                Dim number$

                ' 为第二个参数添加一个符号
                With DirectCast(parameters(1), LiteralExpression)
                    number = "-" & .value
                End With

                Return New LiteralExpression With {
                    .type = type,
                    .value = number
                }
            End If
        End Function

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
            If refer Like TypeOperator.Comparison Then
                ' WebAssembly comparison operator produce integer value
                Return New TypeAbstract(TypeAlias.boolean)
            ElseIf refer.Split("."c).Last = "store" Then
                ' 写内存操作符没有返回值
                Return TypeAbstract.void
            Else
                Return New TypeAbstract(refer.Split("."c).First)
            End If
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            If [operator] Then
                Return typeFromOperator(refer.symbol)
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
                ' 参数是空的，则直接查找函数的返回值
                func = symbolTable.GetFunctionSymbol(refer.module, refer.symbol)
            Else
                ' 如果参数不是空的，则可能是用户定义的类型的方法
                ' 或者是拓展函数调用
                obj = parameters(Scan0)

                ' 在这里需要对值元素类型为数组的字典引用进行一些额外的处理
                If TypeOf obj Is FuncInvoke AndAlso DirectCast(obj, FuncInvoke).refer.symbol = JavaScriptImports.Dictionary.GetValue.name Then
                    Dim table = DirectCast(obj, FuncInvoke).parameters(0)

                    If TypeOf table Is GetLocalVariable Then
                        Dim tableObj = symbolTable.GetObjectSymbol(DirectCast(table, GetLocalVariable).var)

                        If TypeOf tableObj Is DeclareLocal Then
                            'With DirectCast(tableObj, DeclareLocal)
                            '    If TypeExtensions.IsArray(.genericTypes(1)) Then
                            '        Return .genericTypes(1).Trim("["c, "]"c)
                            '    Else
                            '        Return .genericTypes(1)
                            '    End If
                            'End With
                        Else
                            Throw New NotImplementedException
                        End If
                    Else
                        Throw New NotImplementedException
                    End If

                    Throw New NotImplementedException
                Else
                    If refer.module Like symbolTable.ModuleNames Then
                        func = symbolTable.FindModuleMemberFunction(refer.module, refer.symbol)
                    ElseIf refer.type = SymbolType.Api AndAlso refer.module.StringEmpty Then
                        ' 是外部导入的Api，但是没有模块名称
                        ' 则说明是内部定义的Api函数
                        Return InternalApiReturnType(refer)
                    Else
                        Dim context$ = obj.TypeInfer(symbolTable).type.Description
                        func = symbolTable.GetFunctionSymbol(context, refer.symbol)
                    End If
                End If
            End If

            Return func.result
        End Function

        Private Shared Function InternalApiReturnType(refer As ReferenceSymbol) As TypeAbstract
            Dim tokens As String() = refer.symbol.Split("."c)
            Dim typeToken = tokens(0).Split("_"c)
            Dim type$ = typeToken.Last
            Dim genericType$ = typeToken.First

            Select Case type
                Case "string"
                    Select Case tokens(1)
                        Case "add", "replace", "trim"
                            Return New TypeAbstract(TypeAlias.string)
                        Case "length", "indexOf"
                            Return New TypeAbstract(TypeAlias.i32)
                        Case Else
                            Throw New NotImplementedException(refer.symbol)
                    End Select
                Case "array", "list"
                    Select Case tokens(1)
                        Case "push"
                            Return New TypeAbstract(type).MakeArrayType
                        Case "length"
                            Return New TypeAbstract(TypeAlias.i32)
                        Case "get"
                            Return New TypeAbstract(genericType)
                        Case "set"
                            Return TypeAbstract.void
                        Case Else
                            Throw New NotImplementedException(refer.symbol)
                    End Select
                Case "Math"
                    Select Case tokens(1)
                        Case "exp", "pow", "cos", "sin", "ceil", "random"
                            Return TypeAbstract.f64
                        Case Else
                            Throw New NotImplementedException(refer.symbol)
                    End Select
                Case Else
                    Throw New NotImplementedException(refer.symbol)
            End Select
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            For Each parameter In parameters
                For Each symbol In parameter.GetSymbolReference
                    Yield symbol
                Next
            Next
        End Function
    End Class
End Namespace
