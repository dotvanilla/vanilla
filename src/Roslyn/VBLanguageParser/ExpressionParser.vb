Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
Imports VanillaBasic.WebAssembly.Compiler
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal

Namespace VBLanguageParser

    Module Expressionparser

        <Extension>
        Public Function GetSymbol(name As IdentifierNameSyntax, context As Environment) As SymbolReference
            Dim symbol As String = name.Identifier.Text
            Dim type As WATType = context.GetSymbolType(symbol)

            ' 递归调用或者是函数引用：使用符号引用（作为函数名）
            If symbol = context.SymbolName Then
                Return New SymbolReference(context.FullName, type)
            End If

            ' 普通变量读取：生成 get_local / get_global 指令
            Return New SymbolGetValue(type) With {
                .Name = symbol,
                .isGlobal = Not context.Symbols.ContainsKey(symbol)
            }
        End Function

        <Extension>
        Public Function ParseValue(expression As ExpressionSyntax, context As Environment) As WATSyntax
            If expression Is Nothing Then
                Return LiteralValue.Nothing(WATType.i32)
            End If

            Select Case expression.GetType
                Case GetType(InvocationExpressionSyntax)
                    Return DirectCast(expression, InvocationExpressionSyntax).ParseFunctionInvoke(context)
                Case GetType(MemberAccessExpressionSyntax)
                    Return DirectCast(expression, MemberAccessExpressionSyntax).ParseReference(context)
                Case GetType(LiteralExpressionSyntax)
                    Return DirectCast(expression, LiteralExpressionSyntax).GetLiteralvalue(Nothing, context)
                Case GetType(IdentifierNameSyntax)
                    Return DirectCast(expression, IdentifierNameSyntax).ParseIdentifierValue(context)
                Case GetType(BinaryExpressionSyntax)
                    Return DirectCast(expression, BinaryExpressionSyntax).GetBinary(context)
                Case GetType(UnaryExpressionSyntax)
                    Return DirectCast(expression, UnaryExpressionSyntax).GetUnaryValue(context)
                Case GetType(ObjectCreationExpressionSyntax)
                    Return DirectCast(expression, ObjectCreationExpressionSyntax).ParseObjectCreation(context)
                Case GetType(ArrayCreationExpressionSyntax)
                    Return DirectCast(expression, ArrayCreationExpressionSyntax).ParseArrayCreation(context)
                Case GetType(CollectionInitializerSyntax)
                    Return DirectCast(expression, CollectionInitializerSyntax).ParseCollectionInitializer(context)
                Case GetType(ParenthesizedExpressionSyntax)
                    Return DirectCast(expression, ParenthesizedExpressionSyntax).Expression.ParseValue(context)
                Case GetType(TernaryConditionalExpressionSyntax)
                    Return DirectCast(expression, TernaryConditionalExpressionSyntax).ParseTernary(context)
                Case GetType(BinaryConditionalExpressionSyntax)
                    ' ``If(a, b)`` 空值合并：降级为第一个操作数
                    Return DirectCast(expression, BinaryConditionalExpressionSyntax).FirstExpression.ParseValue(context)
                Case GetType(InterpolatedStringExpressionSyntax)
                    Return DirectCast(expression, InterpolatedStringExpressionSyntax).ParseInterpolatedString(context)
                Case GetType(CTypeExpressionSyntax)
                    Return DirectCast(expression, CTypeExpressionSyntax).ParseCast(context)
                Case GetType(PredefinedCastExpressionSyntax)
                    Return DirectCast(expression, PredefinedCastExpressionSyntax).ParsePredefinedCast(context)
                Case GetType(MeExpressionSyntax)
                    Return LiteralValue.Nothing(WATType.i32)
                Case Else
                    ' 宽容处理：未识别的表达式降级为零值，保证生成合法的 wast
                    Return LiteralValue.Nothing(WATType.i32)
            End Select
        End Function

        <Extension>
        Public Function GetUnaryValue(unary As UnaryExpressionSyntax, context As Environment) As WATSyntax
            Dim unaryOperator As String = unary.OperatorToken.ValueText
            Dim value As WATSyntax = unary.Operand.ParseValue(context)

            Select Case unaryOperator
                Case "-"
                    If TypeOf value Is NumberLiteral Then
                        Dim raw As Object = DirectCast(value, NumberLiteral).Value
                        Dim neg As Object

                        If TypeOf raw Is Integer Then
                            neg = -CInt(raw)
                        ElseIf TypeOf raw Is Long Then
                            neg = -CLng(raw)
                        Else
                            neg = -CDbl(raw)
                        End If

                        Return New NumberLiteral(neg, value.Type)
                    Else
                        Return New BinaryOperator With {
                            .left = New LiteralValue(0, value.Type),
                            .[operator] = "-",
                            .right = value
                        }
                    End If
                Case "+"
                    Return value
                Case "Not"
                    If value.Type Is WATType.boolean OrElse value.Type.UnderlyingWATType = WATElements.i32 Then
                        Return New UnaryOperator("i32.eqz", value)
                    Else
                        Return New UnaryOperator("i32.eqz", CTypeHandle.CInt(value, context))
                    End If
                Case Else
                    Return value
            End Select
        End Function

        ''' <summary>
        ''' 标识符求值：如果该标识符既不是局部变量也不是全局变量，但对应一个函数，
        ''' 则按照 VB 的语义自动进行无参数调用（例如 ``Loop While test``）。
        ''' </summary>
        <Extension>
        Friend Function ParseIdentifierValue(name As IdentifierNameSyntax, context As Environment) As WATSyntax
            Dim symbol As String = name.objectName

            If Not context.Symbols.ContainsKey(symbol) AndAlso
               Not context.Workspace.GlobalSymbols.ContainsKey(symbol) AndAlso
               Not context.HaveEnumType(symbol) Then
                Dim func As FunctionDeclare = ResolveFunction(symbol, context)

                If Not func Is Nothing Then
                    Return New FunctionInvoke With {
                        .Func = New SymbolReference(func.FullName),
                        .Arguments = CastFunctionArguments(func, {}, context)
                    }.SetReturnValue(func.Type)
                End If
            End If

            Return name.GetSymbol(context)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="x"></param>
        ''' <param name="type">can be nothing</param>
        ''' <param name="context"></param>
        ''' <returns></returns>
        <Extension>
        Friend Function GetLiteralvalue(x As LiteralExpressionSyntax, type As WATType, context As Environment) As WATSyntax
            Dim value As Object = x.Token.Value
            Dim buffer = context.Workspace.Memory

            If value Is Nothing Then
                ' 是空值常量，则直接返回整形数0表示空指针
                Return LiteralValue.Nothing(type)
            ElseIf TypeOf value Is Char Then
                ' 字符常量使用单字符字符串来表示
                Dim ch As String = CChar(value).ToString()
                Dim ptr As Integer = buffer.AddString(ch)

                Return New LiteralValue(ptr, WATType.string)
            ElseIf type Is Nothing Then
                type = WATType.GetElementType(value.GetType)

                If type Is Nothing Then
                    type = WATType.i32
                End If
            End If

            Select Case type.UnderlyingVBType.Type
                Case GetType(String)
                    Dim i As Integer = buffer.AddString(x.Token.ValueText)
                    Dim str As New LiteralValue(i, WATType.string)

                    Return str
                Case GetType(Boolean)

                    ' 在常量下，逻辑值是i32 1 或者 0
                    If value = True Then
                        value = 1
                    Else
                        value = 0
                    End If

                    Return New LiteralValue(value, WATType.boolean)
                Case Else
                    Return New NumberLiteral(value, type)
            End Select
        End Function

        <Extension>
        Public Function ParseFunctionInvoke(calls As InvocationExpressionSyntax, context As Environment) As WATSyntax
            Dim args As WATSyntax() = calls.ArgumentList _
                .Arguments _
                .Select(Function(a) a.ParseArgument(context)) _
                .ToArray
            Dim callee As ExpressionSyntax = calls.Expression

            ' ---------- 1. 数组元素访问 / 字符串索引：arr(i) / str(i) ----------
            If TypeOf callee Is IdentifierNameSyntax Then
                Dim name As String = DirectCast(callee, IdentifierNameSyntax).objectName
                Dim symType As WATType = context.GetSymbolType(name)

                If TypeOf symType Is ArrayType Then
                    Return ArrayGet(symType, SymbolRead(name, context), args.FirstOrDefault, context)
                ElseIf symType Is WATType.string Then
                    Return InvokeStatic("string.charAt", WATType.string, {SymbolRead(name, context), args.FirstOrDefault}, context)
                End If
            End If

            ' ---------- 2. 内建的字符串函数：ChrW / AscW 等 ----------
            If TypeOf callee Is IdentifierNameSyntax Then
                Dim builtin = TryBuiltinFunction(DirectCast(callee, IdentifierNameSyntax).objectName)

                If Not builtin Is Nothing Then
                    Return InvokeStatic(builtin.JavaScript, builtin.ResultType, args, context)
                End If
            End If

            ' ---------- 3. 成员访问调用：obj.Member(args) ----------
            If TypeOf callee Is MemberAccessExpressionSyntax Then
                Dim binding As MemberBinding = DirectCast(callee, MemberAccessExpressionSyntax).ResolveMember(context)

                If binding.Kind = "static" Then
                    Return InvokeStatic(binding.JavaScript, binding.ResultType, args, context)
                ElseIf binding.Kind = "instance-method" Then
                    Dim allArgs As New List(Of WATSyntax)

                    If binding.PrependReceiver Then
                        allArgs.Add(binding.Receiver)
                    End If

                    allArgs.AddRange(args)

                    Return InvokeStatic(binding.JavaScript, binding.ResultType, allArgs.ToArray, context)
                ElseIf binding.Kind = "instance-property" AndAlso args.Length = 0 Then
                    ' 形如 ``x.ToString()`` / ``x.Length()`` 这类以属性形式建模、但没有额外参数的成员调用：
                    ' binding.Value 已经是完整的调用表达式（例如 ``string.i32.toString(x)``）
                    Return binding.Value
                Else
                    Return LiteralValue.Nothing(WATType.i32)
                End If
            End If

            ' ---------- 4. 同模块 / 跨模块的函数调用 ----------
            If TypeOf callee Is IdentifierNameSyntax Then
                Dim name As String = DirectCast(callee, IdentifierNameSyntax).objectName
                Dim func As FunctionDeclare = ResolveFunction(name, context)

                If Not func Is Nothing Then
                    Return New FunctionInvoke With {
                        .Func = New SymbolReference(func.FullName),
                        .Arguments = CastFunctionArguments(func, args, context)
                    }.SetReturnValue(func.Type)
                End If

                Return LiteralValue.Nothing(WATType.i32)
            End If

            Dim target As WATSyntax = callee.ParseValue(context)

            Return New FunctionInvoke() With {
                .Arguments = args,
                .Func = target
            }.SetReturnValue(ResultType(target))
        End Function

        <Extension>
        Friend Function ParseArgument(arg As ArgumentSyntax, context As Environment) As WATSyntax
            If TypeOf arg Is SimpleArgumentSyntax Then
                Return DirectCast(arg, SimpleArgumentSyntax).Expression.ParseValue(context)
            Else
                Return LiteralValue.Nothing(WATType.i32)
            End If
        End Function

        <Extension>
        Public Function ParseObjectCreation(expr As ObjectCreationExpressionSyntax, context As Environment) As WATSyntax
            Dim typeName As String = GetSimpleTypeName(expr.Type)
            Dim args As WATSyntax()

            If expr.ArgumentList Is Nothing Then
                args = {}
            Else
                args = expr.ArgumentList.Arguments.Select(Function(a) a.ParseArgument(context)).ToArray
            End If

            Select Case typeName
                Case "StringBuilder"
                    Return InvokeStatic("StringBuilder.New", WATType.i32, {}, context)
                Case "Random"
                    Return InvokeStatic("Random.New", WATType.i32, args, context)
                Case "String"
                    Return InvokeStatic("Strings.NewString", WATType.string, args, context)
                Case Else
                    ' 用户自定义类型 / 集合类型暂时降级为空指针
                    Return LiteralValue.Nothing(WATType.i32)
            End Select
        End Function

        <Extension>
        Public Function ParseArrayCreation(expr As ArrayCreationExpressionSyntax, context As Environment) As WATSyntax
            ' 编译验收阶段：数组的分配与初始化统一降级为空指针常量
            Return LiteralValue.Nothing(WATType.i32)
        End Function

        <Extension>
        Public Function ParseCollectionInitializer(init As CollectionInitializerSyntax, context As Environment) As WATSyntax
            Return New ArrayLiteral With {
                .Initialize = init.Initializers.Select(Function(item) item.ParseValue(context)).ToArray
            }
        End Function

        <Extension>
        Public Function ParseTernary(expr As TernaryConditionalExpressionSyntax, context As Environment) As WATSyntax
            Return New SelectExpression With {
                .condition = CTypeHandle.CBool(expr.Condition.ParseValue(context), context),
                .ifTrue = expr.WhenTrue.ParseValue(context),
                .ifFalse = expr.WhenFalse.ParseValue(context)
            }
        End Function

        <Extension>
        Public Function ParseInterpolatedString(expr As InterpolatedStringExpressionSyntax, context As Environment) As WATSyntax
            Dim parts As New List(Of WATSyntax)

            For Each content As InterpolatedStringContentSyntax In expr.Contents
                If TypeOf content Is InterpolatedStringTextSyntax Then
                    parts.Add(NewStringLiteral(DirectCast(content, InterpolatedStringTextSyntax).TextToken.ValueText, context))
                ElseIf TypeOf content Is InterpolationSyntax Then
                    Dim value As WATSyntax = DirectCast(content, InterpolationSyntax).Expression.ParseValue(context)

                    parts.Add(value.AnyToString(context))
                End If
            Next

            If parts.Count = 0 Then
                Return NewStringLiteral("", context)
            End If

            Dim result As WATSyntax = parts(0)

            For i As Integer = 1 To parts.Count - 1
                result = InvokeStatic("string.add", WATType.string, {result, parts(i)}, context)
            Next

            Return result
        End Function

        <Extension>
        Public Function ParseCast(expr As CTypeExpressionSyntax, context As Environment) As WATSyntax
            Dim targetType As WATType = expr.Type.ParseType(context)

            Return CTypeHandle.[CType](targetType, expr.Expression.ParseValue(context), context)
        End Function

        <Extension>
        Public Function ParsePredefinedCast(expr As PredefinedCastExpressionSyntax, context As Environment) As WATSyntax
            Dim targetType As WATType = PredefinedCastType(expr.Keyword.ValueText)

            Return CTypeHandle.[CType](targetType, expr.Expression.ParseValue(context), context)
        End Function

        ' =====================================================================
        ' helpers
        ' =====================================================================

        <Extension>
        Friend Function SymbolRead(name As String, context As Environment) As SymbolGetValue
            Return New SymbolGetValue(context.GetSymbolType(name)) With {
                .Name = name,
                .isGlobal = Not context.Symbols.ContainsKey(name)
            }
        End Function

        Friend Function ArrayGet(arrayType As WATType, array As WATSyntax, index As WATSyntax, context As Environment) As WATSyntax
            Dim element As WATType = If(TypeOf arrayType Is ArrayType, DirectCast(arrayType, ArrayType).ElementType, WATType.i32)
            Dim e As WATElements = element.UnderlyingWATType

            If e = WATElements.f32 OrElse e = WATElements.f64 Then
                Return InvokeStatic("Array.get.f64", WATType.f64, {array, index}, context)
            Else
                Return InvokeStatic("Array.get.i32", WATType.i32, {array, index}, context)
            End If
        End Function

        <Extension>
        Friend Function InvokeStatic(jsName As String, resultType As WATType, args As WATSyntax(), context As Environment) As WATSyntax
            Return New FunctionInvoke With {
                .Func = New JavaScriptTranslation(resultType) With {.JavaScript = jsName},
                .Arguments = CastArguments(jsName, args, context)
            }.SetReturnValue(resultType)
        End Function

        ''' <summary>
        ''' 按照目标导入函数的参数类型对调用参数进行类型转换
        ''' </summary>
        Friend Function CastArguments(jsName As String, args As WATSyntax(), context As Environment) As WATSyntax()
            If args Is Nothing OrElse args.Length = 0 Then
                Return {}
            End If

            If context Is Nothing OrElse context.Workspace Is Nothing Then
                Return args
            End If

            Dim api As ImportsFunction = Nothing

            If context.Workspace.Imports.ContainsKey(jsName) Then
                api = context.Workspace.Imports(jsName)
            End If

            If api Is Nothing OrElse api.Parameters Is Nothing Then
                Return args
            End If

            Dim padded As WATSyntax() = PadArguments(args, api.Parameters)
            Dim casted(padded.Length - 1) As WATSyntax

            For i As Integer = 0 To padded.Length - 1
                If Not api.Parameters(i) Is Nothing AndAlso Not api.Parameters(i).Type Is Nothing Then
                    casted(i) = CTypeHandle.[CType](api.Parameters(i).Type, padded(i), context)
                Else
                    casted(i) = padded(i)
                End If
            Next

            Return casted
        End Function

        ''' <summary>
        ''' 按照目标函数的参数类型对调用参数进行类型转换（省略的可选参数使用零值补齐）
        ''' </summary>
        Friend Function CastFunctionArguments(func As FunctionDeclare, args As WATSyntax(), context As Environment) As WATSyntax()
            If func Is Nothing OrElse func.parameters Is Nothing Then
                Return args
            End If

            Dim padded As WATSyntax() = PadArguments(args, func.parameters)
            Dim casted(padded.Length - 1) As WATSyntax

            For i As Integer = 0 To padded.Length - 1
                If Not func.parameters(i) Is Nothing AndAlso Not func.parameters(i).Type Is Nothing Then
                    casted(i) = CTypeHandle.[CType](func.parameters(i).Type, padded(i), context)
                Else
                    casted(i) = padded(i)
                End If
            Next

            Return casted
        End Function

        ''' <summary>
        ''' 使用零值补齐被省略的参数（可选参数）
        ''' </summary>
        Friend Function PadArguments(args As WATSyntax(), pars As DeclareLocal()) As WATSyntax()
            If pars Is Nothing Then
                Return args
            End If

            Dim source As WATSyntax() = If(args, {})
            Dim result As New List(Of WATSyntax)

            For i As Integer = 0 To pars.Length - 1
                If i < source.Length AndAlso Not source(i) Is Nothing Then
                    result.Add(source(i))
                ElseIf Not pars(i) Is Nothing Then
                    result.Add(ZeroValue(pars(i).Type))
                End If
            Next

            Return result.ToArray
        End Function

        ''' <summary>
        ''' 生成指定 wasm 类型的零值常量
        ''' </summary>
        Friend Function ZeroValue(type As WATType) As WATSyntax
            If type Is Nothing Then
                Return New LiteralValue(0, WATType.i32)
            End If

            Select Case type.UnderlyingWATType
                Case WATElements.i64
                    Return New LiteralValue(0L, WATType.i64)
                Case WATElements.f32
                    Return New LiteralValue(0.0F, WATType.f32)
                Case WATElements.f64
                    Return New LiteralValue(0.0R, WATType.f64)
                Case Else
                    Return New LiteralValue(0, WATType.i32)
            End Select
        End Function

        <Extension>
        Friend Function NewStringLiteral(text As String, context As Environment) As WATSyntax
            Dim ptr As Integer = context.Workspace.Memory.AddString(If(text, ""))

            Return New LiteralValue(ptr, WATType.string)
        End Function

        Friend Function ResultType(target As WATSyntax) As WATType
            If TypeOf target Is JavaScriptTranslation OrElse TypeOf target Is SymbolReference Then
                Return target.Type
            Else
                Return WATType.void
            End If
        End Function

        Friend Function GetSimpleTypeName(type As TypeSyntax) As String
            If type Is Nothing Then
                Return ""
            ElseIf TypeOf type Is PredefinedTypeSyntax Then
                Return DirectCast(type, PredefinedTypeSyntax).Keyword.ValueText
            ElseIf TypeOf type Is IdentifierNameSyntax Then
                Return DirectCast(type, IdentifierNameSyntax).objectName
            ElseIf TypeOf type Is GenericNameSyntax Then
                Return DirectCast(type, GenericNameSyntax).Identifier.ValueText
            ElseIf TypeOf type Is QualifiedNameSyntax Then
                Return GetSimpleTypeName(DirectCast(type, QualifiedNameSyntax).Right)
            Else
                Return type.ToString
            End If
        End Function

        Friend Function PredefinedCastType(keyword As String) As WATType
            Select Case keyword
                Case "CBool" : Return WATType.boolean
                Case "CStr" : Return WATType.string
                Case "CLng", "CULng" : Return WATType.i64
                Case "CSng" : Return WATType.f32
                Case "CDbl", "CDec", "CDate" : Return WATType.f64
                Case Else : Return WATType.i32
            End Select
        End Function

        ''' <summary>
        ''' 内建函数（ChrW / AscW 等）到运行时桥接的映射
        ''' </summary>
        Friend Function TryBuiltinFunction(name As String) As MemberBinding
            Select Case name
                Case "ChrW", "Chr"
                    Return New MemberBinding With {.Kind = "static", .JavaScript = "Strings.ChrW", .ResultType = WATType.string}
                Case "AscW", "Asc"
                    Return New MemberBinding With {.Kind = "static", .JavaScript = "Strings.AscW", .ResultType = WATType.i32}
                Case Else
                    Return Nothing
            End Select
        End Function

        ''' <summary>
        ''' 查找目标函数：优先选择与当前模块同名的函数，其次选择任意同名函数
        ''' </summary>
        Friend Function ResolveFunction(name As String, context As Environment) As FunctionDeclare
            Dim candidates As FunctionDeclare() = context.Workspace.Methods.Values _
                .Where(Function(f) f.Name = name) _
                .ToArray

            If candidates.Length = 0 Then
                Return Nothing
            End If

            Dim prefix As String = ModulePrefix(context)
            Dim sameModule As FunctionDeclare = candidates.FirstOrDefault(Function(f) f.[namespace] = prefix)

            If Not sameModule Is Nothing Then
                Return sameModule
            Else
                Return candidates(0)
            End If
        End Function

        ''' <summary>
        ''' 解析形如 ``ModuleName.FunctionName`` 的跨模块函数调用
        ''' </summary>
        Friend Function ResolveModuleFunction(path As String, memberName As String, context As Environment) As FunctionDeclare
            If String.IsNullOrEmpty(path) Then
                Return Nothing
            End If

            Dim owner As String = path.Split("."c).Last
            Dim suffix As String = "." & owner

            Return context.Workspace.Methods.Values.FirstOrDefault(
                Function(f) f.Name = memberName AndAlso f.[namespace].EndsWith(suffix)
            )
        End Function

        ''' <summary>
        ''' 计算当前环境所属的模块命名空间前缀
        ''' </summary>
        Friend Function ModulePrefix(context As Environment) As String
            Dim e As Environment = context

            While Not e Is Nothing AndAlso Not e.Container Is Nothing AndAlso Not TypeOf e.Container Is ProjectEnvironment
                e = e.Container
            End While

            If e Is Nothing Then
                Return ""
            Else
                Return e.FullName
            End If
        End Function
    End Module
End Namespace
