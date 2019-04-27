#Region "Microsoft.VisualBasic::c15b617dab4f72922b1b92f5484c7013, Symbols\Parser\BodyParser.vb"

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
    '         Function: AssignVariable, GetInitialize, LocalDeclare, (+2 Overloads) ParseDeclarator, ParseExpression
    '                   ValueAssign, ValueReturn
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports Wasm.Compiler
Imports Wasm.Symbols.JavaScriptImports

Namespace Symbols.Parser

    ''' <summary>
    ''' Parser of the function body
    ''' </summary>
    Module BodyParser

        <Extension>
        Public Function ParseExpression(statement As StatementSyntax, symbols As SymbolTable) As [Variant](Of Expression, Expression())
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
                .GetFunctionSymbol(Nothing, symbols.currentFuncSymbol) _
                .result

            value = CTypeHandle.CType(returnType, value, symbols)

            Return New ReturnValue With {
                .Internal = value
            }
        End Function

        <Extension>
        Public Function ValueAssign(assign As AssignmentStatementSyntax, symbols As SymbolTable) As Expression
            If TypeOf assign.Left Is IdentifierNameSyntax Then
                Return symbols.AssignVariable(assign)
            ElseIf TypeOf assign.Left Is InvocationExpressionSyntax Then
                ' 对数组的赋值操作
                Dim left = DirectCast(assign.Left, InvocationExpressionSyntax)
                Dim arrayName = DirectCast(left.Expression, IdentifierNameSyntax).objectName
                Dim index As Expression = left.ArgumentList _
                    .Arguments _
                    .First _
                    .Argument(symbols, "a".param("i32"))
                Dim arraySymbol As New GetLocalVariable With {.var = arrayName}

                Call symbols.addRequired(JavaScriptImports.SetArrayElement)

                Return New FuncInvoke(JavaScriptImports.SetArrayElement.Name) With {
                    .parameters = {
                        arraySymbol, index, assign.Right.ValueExpression(symbols)
                    }
                }
            ElseIf TypeOf assign.Left Is MemberAccessExpressionSyntax Then
                Dim left = DirectCast(assign.Left, MemberAccessExpressionSyntax)
                Dim right = assign.Right.ValueExpression(symbols)
                Dim objName = left.Expression.ToString
                Dim memberName = left.Name.objectName

                If symbols.GetUnderlyingType(objName) = GetType(DictionaryBase).FullName Then
                    Dim key = symbols.StringConstant(memberName)

                    Return New FuncInvoke(JavaScriptImports.Dictionary.SetValue) With {
                        .parameters = {
                            symbols.GetObjectReference(objName),
                            key, right
                        }
                    }
                Else
                    Throw New NotImplementedException
                End If

            Else
                Throw New NotImplementedException(assign.Left.GetType.FullName)
            End If
        End Function

        <Extension>
        Private Function AssignVariable(symbols As SymbolTable, assign As AssignmentStatementSyntax) As Expression
            Dim var = DirectCast(assign.Left, IdentifierNameSyntax).objectName
            Dim left As Expression
            Dim right = assign.Right.ValueExpression(symbols)
            Dim typeLeft = symbols.GetUnderlyingType(var)
            Dim op$ = assign.OperatorToken.ValueText

            If symbols.IsLocal(var) Then
                left = New GetLocalVariable(var)
            Else
                left = New GetGlobalVariable(var)
            End If

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
                Return New SetGlobalVariable With {
                    .var = var,
                    .value = valueRight
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

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="names"></param>
        ''' <param name="symbols"></param>
        ''' <param name="moduleName">这个参数是空值表示局部变量，反之表示为模块全局变量</param>
        ''' <returns></returns>
        <Extension>
        Friend Iterator Function ParseDeclarator(names As IEnumerable(Of VariableDeclaratorSyntax),
                                                 symbols As SymbolTable,
                                                 moduleName$) As IEnumerable(Of Expression)

            For Each var As VariableDeclaratorSyntax In names
                For Each [declare] As DeclareLocal In var.ParseDeclarator(symbols, moduleName)
                    If moduleName.StringEmpty Then
                        If Not [declare].init Is Nothing Then
                            Yield [declare].SetLocal

                            'If TypeOf [declare].init Is ArrayTable Then
                            '    With DirectCast([declare].init, ArrayTable)
                            '        [declare].genericTypes = { .key, .type}
                            '    End With
                            'ElseIf TypeOf [declare].init Is ArraySymbol Then
                            '    With DirectCast([declare].init, ArraySymbol)
                            '        [declare].genericTypes = { .type}
                            '    End With
                            'ElseIf TypeOf [declare].init Is Array Then
                            '    With DirectCast([declare].init, Array)
                            '        [declare].genericTypes = { .type}
                            '    End With
                            'Else
                            '    [declare].genericTypes = {[declare].type}
                            'End If
                        End If

                        Call symbols.AddLocal([declare])
                    End If
                Next
            Next
        End Function

        <Extension>
        Friend Iterator Function ParseDeclarator(var As VariableDeclaratorSyntax,
                                                 symbols As SymbolTable,
                                                 moduleName As String) As IEnumerable(Of DeclareLocal)
            Dim fieldNames = var.Names
            Dim type As TypeAbstract = Nothing
            Dim init As Expression = Nothing

            For Each namedVar As ModifiedIdentifierSyntax In fieldNames
                Dim name$ = namedVar.Identifier.objectName

                If name.Last Like Patterns.TypeChar Then
                    type = New TypeAbstract(Patterns.TypeCharName(name.Last))
                    name = name.Substring(0, name.Length - 1)
                Else
                    type = Nothing
                End If

                If Not var.Initializer Is Nothing Then
                    init = var.Initializer.GetInitialize(symbols, type)
                    type = name.AsType(var.AsClause, symbols, init.TypeInfer(symbols))
                Else
                    init = Nothing

                    If type Is Nothing Then
                        type = name.AsType(var.AsClause, symbols)
                    End If

                    If TypeOf var.AsClause Is AsNewClauseSyntax Then
                        init = DirectCast(var.AsClause, AsNewClauseSyntax).NewExpression.AsNewObject(type, symbols)
                    End If
                End If

                If Not init Is Nothing Then
                    Select Case init.GetType
                        Case GetType(ArraySymbol)
                            With DirectCast(init, ArraySymbol)
                                If .type Is Nothing Then
                                    .type = type
                                End If
                            End With
                    End Select
                End If

                If Not moduleName.StringEmpty Then
                    If init Is Nothing Then
                        ' 默认是零
                        init = New LiteralExpression(0, type)
                    ElseIf type <> init.TypeInfer(symbols) Then
                        If TypeOf init Is LiteralExpression Then
                            DirectCast(init, LiteralExpression).type = type
                        ElseIf TypeOf init Is FuncInvoke Then
                            ' 查看是否为单目运算
                            With DirectCast(init, FuncInvoke)
                                If .IsUnary Then
                                    init = .AsUnary(type)
                                End If
                            End With
                        Else
                            Throw New InvalidExpressionException("Global variable its initialize value only supports constant value!")
                        End If
                    End If

                    Call symbols.AddGlobal(name, type, moduleName, init)
                Else
                    If Not init Is Nothing Then
                        init = CTypeHandle.CType(type, init, symbols)

                        If TypeOf init Is ArraySymbol Then
                            Call symbols.doArrayImports
                        End If
                    Else
                        If Not namedVar.ArrayBounds Is Nothing Then
                            ' 这是一个VB6版本的数组申明语法
                            type = type.MakeArrayType
                            init = namedVar.ArrayBounds _
                                .Arguments _
                                .First _
                                .GetExpression _
                                .ValueExpression(symbols)

                            init = New Array With {.type = type, .size = init}
                        End If
                    End If

                    Yield New DeclareLocal With {
                        .name = name,
                        .type = type,
                        .init = init
                    }
                End If
            Next
        End Function

        <Extension>
        Public Function GetInitialize(init As EqualsValueSyntax, symbols As SymbolTable, type As TypeAbstract) As Expression
            Dim val As ExpressionSyntax = init.Value

            If TypeOf val Is LiteralExpressionSyntax Then
                If type Is Nothing Then
                    Return val.ValueExpression(symbols)
                Else
                    With DirectCast(val, LiteralExpressionSyntax)
                        Return .ConstantExpression(type, symbols)
                    End With
                End If
            Else
                Return val.ValueExpression(symbols)
            End If
        End Function
    End Module
End Namespace
