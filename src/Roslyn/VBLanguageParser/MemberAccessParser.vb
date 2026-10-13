Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal

Namespace VBLanguageParser

    ''' <summary>
    ''' 成员访问的解析结果
    ''' </summary>
    Public Class MemberBinding
        ''' <summary>
        ''' static | instance-method | instance-property | constant | enum | unsupported
        ''' </summary>
        Public Property Kind As String
        Public Property JavaScript As String
        Public Property ResultType As WATType
        Public Property Receiver As WATSyntax
        Public Property PrependReceiver As Boolean
        Public Property Value As WATSyntax
    End Class

    Module MemberAccessParser

        ''' <summary>
        ''' 可以作为静态访问根的类型/命名空间名称
        ''' </summary>
        Private ReadOnly staticRoots As String() = {
            "Console", "Math", "String", "Strings", "Integer", "Long", "Short", "Byte", "SByte",
            "UShort", "UInteger", "ULong", "Single", "Double", "Boolean", "Object", "Char",
            "Convert", "Encoding", "StringBuilder", "Random", "Global", "System",
            "Date", "Decimal", "Guid", "Environment", "Int32", "Int64", "Activator"
        }

        <Extension>
        Public Function ParseReference(indexer As MemberAccessExpressionSyntax, context As Environment) As WATSyntax
            Dim binding As MemberBinding = indexer.ResolveMember(context)

            Select Case binding.Kind
                Case "constant", "enum", "instance-property"
                    Return binding.Value
                Case "static", "instance-method"
                    ' 没有被调用的方法引用：返回可调用的 javascript 映射
                    Return New JavaScriptTranslation(binding.ResultType) With {.JavaScript = binding.JavaScript}
                Case Else
                    Return LiteralValue.Nothing(WATType.i32)
            End Select
        End Function

        <Extension>
        Public Function ResolveMember(indexer As MemberAccessExpressionSyntax, context As Environment) As MemberBinding
            Dim memberName As String = indexer.Name.Identifier.ValueText
            Dim path As String = Nothing
            Dim isPath As Boolean = TryStaticPath(indexer.Expression, context, path)

            If isPath Then
                ' ---------- 枚举成员 ----------
                If context.HaveEnumType(path) Then
                    Dim [enum] = context.GetEnumType(path)
                    Dim value = [enum].GetMember(memberName)

                    If Not value Is Nothing Then
                        Return New MemberBinding With {.Kind = "enum", .Value = value, .ResultType = value.Type}
                    End If
                End If

                Dim normalized As String = NormalizePath(path & "." & memberName)

                ' ---------- 静态常量 ----------
                Dim constBinding = TryStaticConstant(normalized, context)
                If Not constBinding Is Nothing Then
                    Return constBinding
                End If

                ' ---------- 静态方法 ----------
                Dim api = TryStaticApi(normalized)
                If Not api Is Nothing Then
                    Return api
                End If

                ' ---------- 跨模块函数调用 Module.Function ----------
                Dim func = Expressionparser.ResolveModuleFunction(path, memberName, context)
                If Not func Is Nothing Then
                    Return New MemberBinding With {.Kind = "static", .JavaScript = func.FullName, .ResultType = func.Type}
                End If
            End If

            ' ---------- 实例成员 ----------
            Dim receiver As WATSyntax = indexer.Expression.ParseValue(context)
            Dim instanceBinding = ResolveInstance(memberName, receiver, context)

            If Not instanceBinding Is Nothing Then
                Return instanceBinding
            End If

            Return New MemberBinding With {.Kind = "unsupported"}
        End Function

        ' =====================================================================
        ' helpers
        ' =====================================================================

        Private Function TryStaticPath(expr As ExpressionSyntax, context As Environment, ByRef path As String) As Boolean
            Select Case expr.GetType
                Case GetType(IdentifierNameSyntax)
                    Dim name As String = DirectCast(expr, IdentifierNameSyntax).objectName

                    If Array.IndexOf(staticRoots, name) >= 0 OrElse IsKnownType(name, context) Then
                        path = name
                        Return True
                    Else
                        Return False
                    End If
                Case GetType(MemberAccessExpressionSyntax)
                    Dim ma As MemberAccessExpressionSyntax = DirectCast(expr, MemberAccessExpressionSyntax)
                    Dim leftPath As String = Nothing

                    If TryStaticPath(ma.Expression, context, leftPath) Then
                        path = leftPath & "." & ma.Name.Identifier.ValueText
                        Return True
                    Else
                        Return False
                    End If
                Case Else
                    Return False
            End Select
        End Function

        Private Function IsKnownType(name As String, context As Environment) As Boolean
            Return context.Workspace.Types.Values.Any(Function(t) t.Name = name)
        End Function

        Private Function NormalizePath(path As String) As String
            Dim p As String = path

            For Each prefix As String In {"Global.", "System.", "Microsoft.VisualBasic."}
                While p.StartsWith(prefix)
                    p = p.Substring(prefix.Length)
                End While
            Next

            Return p
        End Function

        Private Function TryStaticConstant(name As String, context As Environment) As MemberBinding
            Select Case name
                Case "Math.PI" : Return ConstF64(System.Math.PI)
                Case "Math.E" : Return ConstF64(System.Math.E)
                Case "Math.Tau" : Return ConstF64(System.Math.PI * 2)
                Case "Integer.MaxValue" : Return ConstI32(Integer.MaxValue)
                Case "Integer.MinValue" : Return ConstI32(Integer.MinValue)
                Case "Short.MaxValue" : Return ConstI32(Short.MaxValue)
                Case "Short.MinValue" : Return ConstI32(Short.MinValue)
                Case "Byte.MaxValue" : Return ConstI32(Byte.MaxValue)
                Case "Byte.MinValue" : Return ConstI32(Byte.MinValue)
                Case "SByte.MaxValue" : Return ConstI32(SByte.MaxValue)
                Case "SByte.MinValue" : Return ConstI32(SByte.MinValue)
                Case "Long.MaxValue" : Return ConstI64(Long.MaxValue)
                Case "Long.MinValue" : Return ConstI64(Long.MinValue)
                Case "Single.MaxValue" : Return ConstF32(Single.MaxValue)
                Case "Single.MinValue" : Return ConstF32(Single.MinValue)
                Case "Double.MaxValue" : Return ConstF64(Double.MaxValue)
                Case "Double.MinValue" : Return ConstF64(Double.MinValue)
                Case "String.Empty" : Return ConstString("", context)
                Case "Boolean.TrueString" : Return ConstString("True", context)
                Case "Boolean.FalseString" : Return ConstString("False", context)
                Case Else : Return Nothing
            End Select
        End Function

        Private Function TryStaticApi(name As String) As MemberBinding
            Select Case name
                Case "Console.WriteLine", "Console.Write"
                    Return StaticBinding("Console.WriteLine", WATType.void)
                Case "Math.Pow" : Return StaticBinding("Math.Pow", WATType.f64)
                Case "Math.Sqrt" : Return StaticBinding("Math.Sqrt", WATType.f64)
                Case "Math.Exp" : Return StaticBinding("Math.Exp", WATType.f64)
                Case "Math.Log" : Return StaticBinding("Math.Log", WATType.f64)
                Case "Math.Log10" : Return StaticBinding("Math.Log10", WATType.f64)
                Case "Math.Cos" : Return StaticBinding("Math.Cos", WATType.f64)
                Case "Math.Sin" : Return StaticBinding("Math.Sin", WATType.f64)
                Case "Math.Tan" : Return StaticBinding("Math.Tan", WATType.f64)
                Case "Math.Floor" : Return StaticBinding("Math.Floor", WATType.f64)
                Case "Math.Ceiling" : Return StaticBinding("Math.Ceiling", WATType.f64)
                Case "Math.Round" : Return StaticBinding("Math.Round", WATType.f64)
                Case "Math.Abs" : Return StaticBinding("Math.Abs", WATType.f64)
                Case "Math.Min" : Return StaticBinding("Math.Min", WATType.f64)
                Case "Math.Max" : Return StaticBinding("Math.Max", WATType.f64)
                Case "Math.Random" : Return StaticBinding("Math.Random", WATType.f64)
                Case "Encoding.UTF8.GetBytes" : Return StaticBinding("Encoding.UTF8.GetBytes", WATType.i32)
                Case "Encoding.UTF8.GetString" : Return StaticBinding("Encoding.UTF8.GetString", WATType.string)
                Case "Convert.ToBase64String" : Return StaticBinding("Convert.ToBase64String", WATType.string)
                Case "Convert.FromBase64String" : Return StaticBinding("Convert.FromBase64String", WATType.i32)
                Case "Convert.ToString" : Return StaticBinding("Convert.ToString", WATType.string)
                Case "String.Concat" : Return StaticBinding("Strings.Concat", WATType.string)
                Case "String.Join" : Return StaticBinding("Strings.Join", WATType.string)
                Case Else : Return Nothing
            End Select
        End Function

        Private Function ResolveInstance(memberName As String, receiver As WATSyntax, context As Environment) As MemberBinding
            Dim recvType As WATType = If(receiver Is Nothing, Nothing, receiver.Type)
            Dim isString As Boolean = recvType Is WATType.string
            Dim isArray As Boolean = TypeOf recvType Is ArrayType
            Dim isFloat As Boolean = Not recvType Is Nothing AndAlso
                (recvType.UnderlyingWATType = WATElements.f32 OrElse recvType.UnderlyingWATType = WATElements.f64)
            Dim isNumeric As Boolean = Not recvType Is Nothing AndAlso
                (recvType.UnderlyingWATType = WATElements.i32 OrElse recvType.UnderlyingWATType = WATElements.i64 OrElse
                 recvType.UnderlyingWATType = WATElements.f32 OrElse recvType.UnderlyingWATType = WATElements.f64 OrElse
                 recvType.UnderlyingWATType = WATElements.boolean)

            Select Case memberName
                Case "Length"
                    If isArray Then
                        Return InstanceProperty("Array.length", WATType.i32, receiver, context)
                    Else
                        Return InstanceProperty("string.length", WATType.i32, receiver, context)
                    End If
                Case "Count"
                    Return InstanceProperty("Array.length", WATType.i32, receiver, context)
                Case "ToString"
                    If isString Then
                        Return New MemberBinding With {.Kind = "instance-property", .Value = receiver, .ResultType = WATType.string}
                    ElseIf isNumeric OrElse recvType Is Nothing Then
                        Dim js As String = If(isFloat, "string.f64.toString", "string.i32.toString")

                        Return InstanceProperty(js, WATType.string, receiver, context)
                    Else
                        Return InstanceMethod("StringBuilder.ToString", WATType.string, receiver)
                    End If
                Case "Append" : Return InstanceMethod("StringBuilder.Append", WATType.i32, receiver)
                Case "AppendLine" : Return InstanceMethod("StringBuilder.AppendLine", WATType.i32, receiver)
                Case "Clear" : Return InstanceMethod("StringBuilder.Clear", WATType.i32, receiver)
                Case "Replace" : Return InstanceMethod("string.replace", WATType.string, receiver)
                Case "Trim" : Return InstanceMethod("string.trim", WATType.string, receiver)
                Case "Substring" : Return InstanceMethod("string.substr", WATType.string, receiver)
                Case "IndexOf" : Return InstanceMethod("string.indexOf", WATType.i32, receiver)
                Case "ToUpper" : Return InstanceMethod("string.upper", WATType.string, receiver)
                Case "ToLower" : Return InstanceMethod("string.lower", WATType.string, receiver)
                Case "Equals" : Return InstanceMethod("string.equals", WATType.i32, receiver)
                Case "Next" : Return InstanceMethod("Random.Next", WATType.i32, receiver)
                Case "NextDouble" : Return InstanceMethod("Random.NextDouble", WATType.f64, receiver)
                Case Else : Return Nothing
            End Select
        End Function

        Private Function StaticBinding(jsName As String, resultType As WATType) As MemberBinding
            Return New MemberBinding With {.Kind = "static", .JavaScript = jsName, .ResultType = resultType}
        End Function

        Private Function InstanceMethod(jsName As String, resultType As WATType, receiver As WATSyntax) As MemberBinding
            Return New MemberBinding With {
                .Kind = "instance-method",
                .JavaScript = jsName,
                .ResultType = resultType,
                .Receiver = receiver,
                .PrependReceiver = True
            }
        End Function

        Private Function InstanceProperty(jsName As String, resultType As WATType, receiver As WATSyntax, context As Environment) As MemberBinding
            Return New MemberBinding With {
                .Kind = "instance-property",
                .JavaScript = jsName,
                .ResultType = resultType,
                .Receiver = receiver,
                .Value = Expressionparser.InvokeStatic(jsName, resultType, {receiver}, context)
            }
        End Function

        Private Function ConstF64(v As Double) As MemberBinding
            Return New MemberBinding With {.Kind = "constant", .Value = New LiteralValue(v, WATType.f64), .ResultType = WATType.f64}
        End Function

        Private Function ConstF32(v As Single) As MemberBinding
            Return New MemberBinding With {.Kind = "constant", .Value = New LiteralValue(v, WATType.f32), .ResultType = WATType.f32}
        End Function

        Private Function ConstI32(v As Integer) As MemberBinding
            Return New MemberBinding With {.Kind = "constant", .Value = New LiteralValue(v, WATType.i32), .ResultType = WATType.i32}
        End Function

        Private Function ConstI64(v As Long) As MemberBinding
            Return New MemberBinding With {.Kind = "constant", .Value = New LiteralValue(v, WATType.i64), .ResultType = WATType.i64}
        End Function

        Private Function ConstString(s As String, context As Environment) As MemberBinding
            Return New MemberBinding With {
                .Kind = "constant",
                .Value = Expressionparser.NewStringLiteral(s, context),
                .ResultType = WATType.string
            }
        End Function
    End Module
End Namespace
