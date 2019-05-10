#Region "Microsoft.VisualBasic::5aa50f91daa3296c1dba9fdeaabbd6f5, Symbols\Parser\Body\DeclaratorParser.vb"

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

'     Module DeclaratorParser
' 
'         Function: GetInitialize, (+2 Overloads) ParseDeclarator, ParserInternal
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.JavaScriptImports
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Module DeclaratorParser

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
                                                 moduleName$,
                                                 isConst As Boolean) As IEnumerable(Of Expression)

            For Each var As VariableDeclaratorSyntax In names
                For Each [declare] As DeclareLocal In var.ParseDeclarator(symbols, moduleName, isConst)
                    If moduleName.StringEmpty Then
                        If Not [declare].init Is Nothing Then
                            Yield [declare].SetLocal
                        End If

                        Call symbols.AddLocal([declare])
                    End If
                Next
            Next
        End Function

        <Extension>
        Friend Function ParseDeclarator(var As VariableDeclaratorSyntax,
                                        symbols As SymbolTable,
                                        moduleName$,
                                        isConst As Boolean) As IEnumerable(Of DeclareLocal)
            Dim fieldNames = var.Names

            Return fieldNames _
                .Select(Function(namedVar)
                            Return namedVar.ParserInternal(var, symbols, moduleName, isConst)
                        End Function) _
                .Where(Function(x) Not x Is Nothing) _
                .ToArray
        End Function

        <Extension>
        Private Function ParserInternal(namedVar As ModifiedIdentifierSyntax,
                                        var As VariableDeclaratorSyntax,
                                        symbols As SymbolTable,
                                        moduleName$,
                                        isConst As Boolean) As DeclareLocal

            Dim name$ = namedVar.Identifier.objectName
            Dim type As TypeAbstract = Nothing
            Dim init As Expression = Nothing

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

                Call symbols.AddGlobal(name, type, moduleName, init, isConst)

                Return Nothing
            Else
                If Not init Is Nothing Then
                    init = CTypeHandle.CType(type, init, symbols)

                    If TypeOf init Is ArraySymbol Then
                        Dim array As ArraySymbol = init

                        If array.type = TypeAlias.array Then
                            init = array.writeArray(symbols, array.type)
                        Else
                            ' 是一个list，需要导入额外的javascript api来完成功能
                            Call symbols.doArrayImports(DirectCast(init, ArraySymbol).type)
                        End If
                    End If
                Else
                    If Not namedVar.ArrayBounds Is Nothing Then
                        ' 这是一个VB6版本的数组申明语法
                        init = namedVar.ArrayBounds _
                            .Arguments _
                            .First _
                            .GetExpression _
                            .ValueExpression(symbols)

                        init = symbols.memory.AllocateArrayBlock(type, init)
                        type = type.MakeArrayType
                    End If
                End If

                Return New DeclareLocal With {
                    .name = name,
                    .type = type,
                    .init = init,
                    .isConst = isConst
                }
            End If
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
