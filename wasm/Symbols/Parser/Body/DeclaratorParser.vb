Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports Wasm.Compiler
Imports Wasm.Symbols.JavaScriptImports

Namespace Symbols.Parser

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
                                                 moduleName$) As IEnumerable(Of Expression)

            For Each var As VariableDeclaratorSyntax In names
                For Each [declare] As DeclareLocal In var.ParseDeclarator(symbols, moduleName)
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
                                        moduleName As String) As IEnumerable(Of DeclareLocal)
            Dim fieldNames = var.Names

            Return fieldNames _
                .Select(Function(namedVar)
                            Return namedVar.ParserInternal(var, symbols, moduleName)
                        End Function) _
                .Where(Function(x) Not x Is Nothing) _
                .ToArray
        End Function

        <Extension>
        Private Function ParserInternal(namedVar As ModifiedIdentifierSyntax, var As VariableDeclaratorSyntax, symbols As SymbolTable, moduleName As String) As DeclareLocal
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

                Call symbols.AddGlobal(name, type, moduleName, init)

                Return Nothing
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

                Return New DeclareLocal With {
                    .name = name,
                    .type = type,
                    .init = init
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