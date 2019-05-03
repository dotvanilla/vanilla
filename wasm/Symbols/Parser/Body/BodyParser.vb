#Region "Microsoft.VisualBasic::867cc8dd9a09370d7769c20bc31b54b3, Symbols\Parser\Body\BodyParser.vb"

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
    '         Function: AssignVariable, (+2 Overloads) FirstArgument, LocalDeclare, ParseExpression, ValueAssign
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
                Dim objName = left.Expression.ToString
                Dim memberName = left.Name.objectName

                If objName Like symbols.ModuleNames Then
                    ' 是对一个模块全局变量的引用
                    Dim [global] = symbols.FindModuleGlobal(objName, memberName)
                    Dim rightValue As Expression = CTypeHandle.CType(
                        left:=[global].TypeInfer(symbols),
                        right:=right,
                        symbols:=symbols
                    )

                    Return New SetGlobalVariable With {
                        .[module] = [global].Module,
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

            Else
                Throw New NotImplementedException(assign.Left.GetType.FullName)
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

            Dim valueRight = CTypeHandle.CType(typeLeft, right, symbols)

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
                    .[module] = [global].Module
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
