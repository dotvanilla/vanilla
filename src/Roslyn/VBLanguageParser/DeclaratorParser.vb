
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

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
                                                 symbols As Environment,
                                                 moduleName$,
                                                 isConst As Boolean) As IEnumerable(Of WATSyntax)

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
                                        symbols As Environment,
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

        ''' <summary>
        ''' 这个函数返回WASM之中的基本数据类型
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="asClause"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 当类型申明是空的时候，应该是从其初始化值得类型来推断申明的
        ''' </remarks>
        <Extension>
        Public Function AsType(ByRef name$,
                               [asClause] As AsClauseSyntax,
                               symbols As Environment,
                               Optional initType As WATType = Nothing) As WATType

            Dim type As WATType

            If Not asClause Is Nothing Then
                If TypeOf asClause Is SimpleAsClauseSyntax Then
                    type = New WATType(GetAsType(asClause, symbols), symbols)
                ElseIf TypeOf asClause Is AsNewClauseSyntax Then
                    Dim [new] As ObjectCreationExpressionSyntax = DirectCast(asClause, AsNewClauseSyntax).NewExpression
                    Dim objType As VBType = DeclaratorParser.GetType([new].Type, symbols)

                    type = WATType.GetUnderlyingType(objType, symbols)
                Else
                    Throw New NotImplementedException
                End If
            ElseIf name.Last Like Patterns.TypeChar Then
                type = New WATType(TypeExtensions.TypeCharWasm(name.Last))
                name = name.Substring(0, name.Length - 1)
            Else
                type = initType
            End If

            Return type
        End Function

        ''' <summary>
        ''' Parse type define from [``As Type``] expression.
        ''' </summary>
        ''' <param name="[as]">``As Type``</param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetAsType([as] As SimpleAsClauseSyntax, symbols As Environment) As VBType
            If [as] Is Nothing Then
                Return GetType(Void)
            Else
                Return [GetType]([as].Type, symbols)
            End If
        End Function

        <Extension>
        Public Function PredefinedType(asType As PredefinedTypeSyntax) As Type
            Dim token$ = asType.Keyword.objectName
            ' parse from the token name
            Return Scripting.GetType(token)
        End Function

        <Extension>
        Public Function [GetType](asType As TypeSyntax, symbols As Environment) As VBType
            If TypeOf asType Is PredefinedTypeSyntax Then
                Return DirectCast(asType, PredefinedTypeSyntax).PredefinedType
            ElseIf TypeOf asType Is ArrayTypeSyntax Then
                Return DirectCast(asType, ArrayTypeSyntax).arrayType(symbols)
            ElseIf TypeOf asType Is GenericNameSyntax Then
                Dim generic = DirectCast(asType, GenericNameSyntax)
                Dim define = generic.GetGenericType(symbols)
                Dim tokenType = define.Value

                ' 在javascript之中 array 和 list是一样的
                If define.Name = "List" Then
                    Return tokenType(Scan0).listOf
                ElseIf define.Name = "Dictionary" Then
                    ' 字典对象在javascript之中则是一个任意的object
                    Return GetType(DictionaryBase)
                Else
                    Throw New NotImplementedException
                End If
            Else
                Return DirectCast(asType, IdentifierNameSyntax).definedType(symbols)
            End If
        End Function

        <Extension>
        Private Function definedType(type As IdentifierNameSyntax, symbols As Environment) As VBType
            Dim token$ = type.Identifier.objectName

            If symbols.HaveEnumType(token) Then
                Dim [const] As EnumSymbol = symbols.GetEnumType(token)
                Return [const].UnderlyingType
            ElseIf token = "Array" Then
                Return GetType(Array)
            ElseIf token = "IList" Then
                Return GetType(IList)
            Else
                ' 用户的自定义类型
                Return New VBType(token)
            End If
        End Function

        ''' <summary>
        ''' 创建一个泛型列表类型
        ''' </summary>
        ''' <param name="element"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function listOf(element As VBType) As VBType
            Dim list As Type = GetType(System.Collections.Generic.List(Of ))
            Dim raw As VBType = element.AsGeneric(container:=list)

            Return raw
        End Function

        <Extension>
        Public Function GetGenericType(generic As GenericNameSyntax, symbols As Environment) As NamedValue(Of VBType())
            Dim typeName = generic.objectName
            Dim types = generic.TypeArgumentList.Arguments
            Dim elementType As VBType() = types _
                .Select(Function(T) DeclaratorParser.GetType(T, symbols)) _
                .ToArray

            Return New NamedValue(Of VBType()) With {
                .Name = typeName,
                .Value = elementType
            }
        End Function

        <Extension>
        Private Function arrayType(type As ArrayTypeSyntax, symbols As Environment) As VBType
            Dim tokenType As VBType = [GetType](type.ElementType, symbols)
            Dim array As VBType = tokenType.MakeArrayType

            For i As Integer = 1 To type.RankSpecifiers.Count - 1
                array = array.MakeArrayType
            Next

            Return array
        End Function

        <Extension>
        Private Function ParserInternal(namedVar As ModifiedIdentifierSyntax,
                                        var As VariableDeclaratorSyntax,
                                        symbols As Environment,
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

        <Extension>
        Private Function initAutofit(namedVar As ModifiedIdentifierSyntax, ByRef type As TypeAbstract, init As Expression, symbols As SymbolTable) As Expression
            If Not init Is Nothing Then
                Select Case init.GetType
                    Case GetType(ArraySymbol)
                        With DirectCast(init, ArraySymbol)
                            If .type Is Nothing Then
                                .type = type
                            End If
                        End With
                End Select
            Else
                ' 默认是零
                init = Literal.Nothing(type)
            End If

            If type <> init.TypeInfer(symbols) Then
                If TypeOf init Is LiteralExpression Then
                    DirectCast(init, LiteralExpression).type = type
                ElseIf TypeOf init Is FuncInvoke Then
                    ' 查看是否为单目运算
                    With DirectCast(init, FuncInvoke)
                        If .IsUnary Then
                            init = .AsUnary(type)
                        End If
                    End With
                End If

                init = CTypeHandle.CType(type, init, symbols)
            End If

            If Not init Is Nothing AndAlso Not init.isLiteralNothing Then
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

                    init = symbols.writeEmptyArray(type, init)
                    type = type.MakeArrayType
                End If
            End If

            Return init
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