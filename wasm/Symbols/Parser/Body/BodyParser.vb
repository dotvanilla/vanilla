﻿#Region "Microsoft.VisualBasic::fbf786b3a7cbe5d130a825a4bec7b8a6, Symbols\Parser\Body\BodyParser.vb"

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

'     Module BodyParser
' 
'         Function: AssignVariable, (+2 Overloads) FirstArgument, LocalDeclare, ParseStatement, ValueAssign
'                   ValueReturn
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Symbols.Parser

    ''' <summary>
    ''' Parser of the function body
    ''' </summary>
    Module BodyParser

        ''' <summary>
        ''' 解析VB.NET的一条命令语句，命令语句不会产生任何值
        ''' </summary>
        ''' <param name="statement"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function ParseStatement(statement As StatementSyntax, symbols As SymbolTable) As [Variant](Of Expression, Expression())
            Select Case statement.GetType
                Case GetType(LocalDeclarationStatementSyntax)
                    Return DirectCast(statement, LocalDeclarationStatementSyntax).LocalDeclare(symbols).ToArray
                Case GetType(AssignmentStatementSyntax)
                    Return DirectCast(statement, AssignmentStatementSyntax).ValueAssign(symbols)
                Case GetType(ReturnStatementSyntax)
                    Return DirectCast(statement, ReturnStatementSyntax).ValueReturn(symbols)
                Case GetType(WhileBlockSyntax)
                    Return DirectCast(statement, WhileBlockSyntax).DoWhile(symbols).ToArray
                Case GetType(MultiLineIfBlockSyntax)
                    Return DirectCast(statement, MultiLineIfBlockSyntax).IfBlock(symbols)
                Case GetType(ForBlockSyntax)
                    Return DirectCast(statement, ForBlockSyntax).ForLoop(symbols).ToArray
                Case GetType(CallStatementSyntax)
                    Return DirectCast(statement, CallStatementSyntax).Invocation.ValueExpression(symbols)
                Case GetType(ExpressionStatementSyntax)
                    Return DirectCast(statement, ExpressionStatementSyntax).Expression.ValueExpression(symbols)
                Case GetType(DoLoopBlockSyntax)
                    Return DirectCast(statement, DoLoopBlockSyntax).DoLoop(symbols).ToArray
                Case Else
                    Throw New NotImplementedException(statement.GetType.FullName)
            End Select
        End Function

        <Extension>
        Public Function ValueReturn(returnValue As ReturnStatementSyntax, symbols As SymbolTable) As Expression
            Dim value As Expression = returnValue.Expression.ValueExpression(symbols)
            Dim returnType = symbols _
                .GetFunctionSymbol(symbols.currentModuleLabel, symbols.currentFuncSymbol) _
                .result

            If returnType = TypeAlias.array AndAlso TypeOf value Is ArraySymbol Then
                ' 需要在这里生成写入数组元素对象到内存之中的表达式
                value = DirectCast(value, ArraySymbol).writeArray(symbols, returnType)
            Else
                value = CTypeHandle.CType(returnType, value, symbols)
            End If

            Return New ReturnValue With {
                .Internal = value
            }
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function FirstArgument(args As ArgumentListSyntax, symbols As SymbolTable, define As NamedValue(Of TypeAbstract)) As Expression
            Return args.Arguments _
                .First _
                .Argument(symbols, define)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function FirstArgument(args As SeparatedSyntaxList(Of ArgumentSyntax), symbols As SymbolTable, define As NamedValue(Of TypeAbstract)) As Expression
            Return args.First.Argument(symbols, define)
        End Function

        <Extension>
        Public Function ValueAssign(assign As AssignmentStatementSyntax, symbols As SymbolTable) As Expression
            If TypeOf assign.Left Is IdentifierNameSyntax Then
                Return symbols.AssignVariable(assign)
            ElseIf TypeOf assign.Left Is InvocationExpressionSyntax Then
                ' 对数组的赋值操作
                Dim left = DirectCast(assign.Left, InvocationExpressionSyntax)
                Dim right = assign.Right.ValueExpression(symbols)

                Return left.setArrayListElement(right, symbols)
            ElseIf TypeOf assign.Left Is MemberAccessExpressionSyntax Then
                Dim left = DirectCast(assign.Left, MemberAccessExpressionSyntax)
                Dim right = assign.Right.ValueExpression(symbols)

                Return symbols.memberAssign(left, right)
            Else
                Throw New NotImplementedException(assign.Left.GetType.FullName)
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="left">左边的对象可能是一个变量名或者一个函数所产生的新对象</param>
        ''' <param name="right"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Private Function memberAssign(symbols As SymbolTable, left As MemberAccessExpressionSyntax, right As Expression) As Expression
            Dim memberName = left.Name.objectName

            If TypeOf left.Expression Is InvocationExpressionSyntax Then
                Dim producer As InvocationExpressionSyntax = left.Expression
                Dim func$ = DirectCast(producer.Expression, IdentifierNameSyntax).objectName
                Dim parameters = producer.ArgumentList

                If symbols.IsAnyObject(func) Then
                    Dim type As TypeAbstract = symbols.GetUnderlyingType(func)

                    If type = TypeAlias.array Then
                        ' 是引用的数组之中的某一个元素
                        ' 因为只有class或者structure类型才可能有成员
                        ' 所以在这里就直接取class类型了
                        Dim ofElement As TypeAbstract = type.generic(Scan0)
                        Dim meta As ClassMeta = symbols.FindByClassId(ofElement.class_id)
                        Dim fieldType As TypeAbstract = meta(memberName).type

                        ' 如果是结构体，则内存的位置是数组之中的位置
                        ' 反之，如果是class引用，则从数组之中取出intptr指针之后得到内存地址
                        Dim array = symbols.GetObjectReference(func)
                        ' 数组之中的元素
                        Dim element As Expression = IMemoryObject.IndexOffset(array, 8)
                        Dim index As Expression = parameters.FirstArgument(symbols, "i".param("i32"))

                        right = CTypeHandle.CType(fieldType, right, symbols)

                        If meta.isStruct Then
                            ' 直接在内存里面写
                            ' 结构体是实际上存储在数组中的
                            element = IMemoryObject.IndexOffset(element, BinaryStack(index, Literal.i32(meta.sizeOf), "*", symbols))
                        Else
                            ' intptr实际上为i32，只有4个字节
                            element = IMemoryObject.IndexOffset(element, BinaryStack(index, Literal.i32(4), "*", symbols))
                            ' 然后读取即可得到对象的位置
                            element = BitConverter.Loadi32(element)
                        End If

                        ' 然后计算出field offset， 然后存储数据即可
                        Dim location As Expression = IMemoryObject.IndexOffset(element, meta.GetFieldOffset(memberName))
                        Dim save As Expression = BitConverter.save(fieldType, location, right)

                        Return save
                    ElseIf type = TypeAlias.list Then
                        ' 是引用的列表之中的某一个元素
                        Throw New NotImplementedException
                    Else
                        ' 字典引用？
                        Throw New NotImplementedException
                    End If
                Else
                    Throw New NotImplementedException
                End If
            Else
                Dim objName As String = left.Expression.ToString

                If objName Like symbols.ModuleNames Then
                    ' 是对一个模块全局变量的引用
                    Dim [global] As DeclareGlobal = symbols.FindModuleGlobal(objName, memberName)
                    Dim rightValue As Expression = CTypeHandle.CType(
                        left:=[global].TypeInfer(symbols),
                        right:=right,
                        symbols:=symbols
                    )

                    Return New SetGlobalVariable With {
                        .[module] = [global].module,
                        .var = [global].name,
                        .value = rightValue
                    }

                ElseIf symbols.GetUnderlyingType(objName) = GetType(DictionaryBase).FullName Then
                    Dim key = symbols.StringConstant(memberName)

                    Return New FuncInvoke(JavaScriptImports.Dictionary.SetValue) With {
                        .parameters = {
                            symbols.GetObjectReference(objName),
                            key, right
                        }
                    }
                Else
                    ' 设置实例对象的成员字段的值
                    Return objName.SetMemberField(memberName, right, symbols)
                End If
            End If
        End Function

        <Extension>
        Private Function AssignVariable(symbols As SymbolTable, assign As AssignmentStatementSyntax) As Expression
            Dim var = DirectCast(assign.Left, IdentifierNameSyntax).objectName
            Dim right = assign.Right.ValueExpression(symbols)
            Dim typeLeft = symbols.GetUnderlyingType(var)
            Dim op$ = assign.OperatorToken.ValueText
            Dim left As Expression = symbols.GetObjectReference(var)

            Select Case op
                Case "*="
                    right = BinaryStack(left, right, "*", symbols)
                Case "+="
                    right = BinaryStack(left, right, "+", symbols)
                Case "-="
                    right = BinaryStack(left, right, "-", symbols)
                Case "/="
                    right = BinaryStack(left, right, "/", symbols)
                Case "="
                    ' do nothing
                Case Else
                    Throw New NotImplementedException
            End Select

            Dim valueRight As Expression = CTypeHandle.CType(typeLeft, right, symbols)

            If TypeOf valueRight Is ArraySymbol AndAlso typeLeft = TypeAlias.array Then
                valueRight = DirectCast(valueRight, ArraySymbol).writeArray(symbols, typeLeft)
            End If

            If symbols.IsLocal(var) Then
                Return New SetLocalVariable With {
                    .var = var,
                    .value = valueRight
                }
            Else
                ' 在这里的全局变量没有添加模块名称引用
                ' 则需要重新查找一遍
                Dim [global] As DeclareGlobal = symbols.FindModuleGlobal(Nothing, var)

                Return New SetGlobalVariable With {
                    .var = var,
                    .value = valueRight,
                    .[module] = [global].module
                }
            End If
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="statement"></param>
        ''' <param name="symbols"></param>
        ''' <returns>May be contains multiple local variables</returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Public Function LocalDeclare(statement As LocalDeclarationStatementSyntax, symbols As SymbolTable) As IEnumerable(Of Expression)
            Return statement.Declarators.ParseDeclarator(symbols, Nothing)
        End Function
    End Module
End Namespace
