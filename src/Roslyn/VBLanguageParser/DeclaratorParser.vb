
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal

Namespace VBLanguageParser

    Module DeclaratorParser

        '''' <summary>
        '''' 
        '''' </summary>
        '''' <param name="names"></param>
        '''' <param name="symbols"></param>
        '''' <param name="moduleName">这个参数是空值表示局部变量，反之表示为模块全局变量</param>
        '''' <returns></returns>
        '<Extension>
        'Friend Iterator Function ParseDeclarator(names As IEnumerable(Of VariableDeclaratorSyntax),
        '                                         symbols As Environment,
        '                                         moduleName$,
        '                                         isConst As Boolean) As IEnumerable(Of WATSyntax)

        '    For Each var As VariableDeclaratorSyntax In names
        '        For Each [declare] As DeclareLocal In var.ParseDeclarator(symbols, moduleName, isConst)
        '            If moduleName.StringEmpty Then
        '                If Not [declare].DefaultValue Is Nothing Then
        '                    Yield [declare].SetLocal
        '                End If

        '                Call symbols.AddLocal([declare])
        '            End If
        '        Next
        '    Next
        'End Function

        <Extension>
        Friend Iterator Function ParseDeclarator(var As VariableDeclaratorSyntax,
                                                 context As Environment,
                                                 moduleName$,
                                                 isConst As Boolean) As IEnumerable(Of DeclareLocal)
            Dim fieldNames = var.Names
            Dim local As New Value(Of DeclareLocal)

            For Each namedVar In fieldNames
                If Not local = namedVar.ParserInternal(var, context, moduleName, isConst) Is Nothing Then
                    Yield CType(local, DeclareLocal)
                End If
            Next
        End Function

        <Extension>
        Private Function ParserInternal(namedVar As ModifiedIdentifierSyntax,
                                        var As VariableDeclaratorSyntax,
                                        context As Environment,
                                        moduleName$,
                                        isConst As Boolean) As DeclareLocal

            Dim name$ = namedVar.Identifier.objectName
            Dim type As WATType = Nothing
            Dim init As WATSyntax = Nothing

            If name.Last Like Patterns.TypeChar Then
                type = WATType.GetElementType(Patterns.CharToType(name.Last))
                name = name.Substring(0, name.Length - 1)
            Else
                type = Nothing
            End If

            If Not var.Initializer Is Nothing Then
                init = var.Initializer.GetInitialize(symbols, type)
                type = name.AsType(var.AsClause, symbols, init.Type)
            Else
                init = Nothing

                If type Is Nothing Then
                    type = name.AsType(var.AsClause, symbols)
                End If

                If TypeOf var.AsClause Is AsNewClauseSyntax Then
                    init = DirectCast(var.AsClause, AsNewClauseSyntax).NewExpression.AsNewObject(type, symbols)
                End If
            End If

            init = namedVar.initAutofit(type, init, symbols)

            If Not moduleName.StringEmpty Then
                Call symbols.AddGlobal(name, type, moduleName, init, isConst)
                Return Nothing
            Else
                Return New DeclareLocal With {
                    .Name = name,
                    .Type = type,
                    .init = init,
                    .isConst = isConst
                }
            End If
        End Function

        '<Extension>
        'Private Function initAutofit(namedVar As ModifiedIdentifierSyntax, ByRef type As TypeAbstract, init As Expression, symbols As SymbolTable) As Expression
        '    If Not init Is Nothing Then
        '        Select Case init.GetType
        '            Case GetType(ArraySymbol)
        '                With DirectCast(init, ArraySymbol)
        '                    If .type Is Nothing Then
        '                        .type = type
        '                    End If
        '                End With
        '        End Select
        '    Else
        '        ' 默认是零
        '        init = Literal.Nothing(type)
        '    End If

        '    If type <> init.TypeInfer(symbols) Then
        '        If TypeOf init Is LiteralExpression Then
        '            DirectCast(init, LiteralExpression).type = type
        '        ElseIf TypeOf init Is FuncInvoke Then
        '            ' 查看是否为单目运算
        '            With DirectCast(init, FuncInvoke)
        '                If .IsUnary Then
        '                    init = .AsUnary(type)
        '                End If
        '            End With
        '        End If

        '        init = CTypeHandle.CType(type, init, symbols)
        '    End If

        '    If Not init Is Nothing AndAlso Not init.isLiteralNothing Then
        '        If TypeOf init Is ArraySymbol Then
        '            Dim array As ArraySymbol = init

        '            If array.type = TypeAlias.array Then
        '                init = array.writeArray(symbols, array.type)
        '            Else
        '                ' 是一个list，需要导入额外的javascript api来完成功能
        '                Call symbols.doArrayImports(DirectCast(init, ArraySymbol).type)
        '            End If
        '        End If
        '    Else
        '        If Not namedVar.ArrayBounds Is Nothing Then
        '            ' 这是一个VB6版本的数组申明语法
        '            init = namedVar.ArrayBounds _
        '                    .Arguments _
        '                    .First _
        '                    .GetExpression _
        '                    .ValueExpression(symbols)

        '            init = symbols.writeEmptyArray(type, init)
        '            type = type.MakeArrayType
        '        End If
        '    End If

        '    Return init
        'End Function

        <Extension>
        Public Function GetInitialize(init As EqualsValueSyntax, symbols As Environment, type As WATType) As WATSyntax
            Dim val As ExpressionSyntax = init.Value

            If TypeOf val Is LiteralExpressionSyntax Then
                If type Is Nothing Then
                    Return val.ParseValue(symbols)
                Else
                    With DirectCast(val, LiteralExpressionSyntax)
                        Return .GetLiteralvalue(type, symbols)
                    End With
                End If
            Else
                Return val.ParseValue(symbols)
            End If
        End Function
    End Module
End Namespace