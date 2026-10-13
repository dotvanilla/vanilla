Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal
Imports any = Microsoft.VisualBasic.Scripting

Namespace VBLanguageParser

    Module ParseFunction

        <Extension>
        Public Function RunParser(func As MethodBlockSyntax, ByRef isPublic As Boolean, context As Environment) As FunctionDeclare
            Dim returnValue As WATType
            Dim pars As Dictionary(Of String, DeclareLocal) = func.SubOrFunctionStatement _
                .ParseParameters(context) _
                .ToDictionary(Function(x) x.Name)
            Dim methodName As String = func.SubOrFunctionStatement.Identifier.ValueText
            Dim namespace$ = context.FullName

            If func.SubOrFunctionStatement.SubOrFunctionKeyword.ValueText = "Sub" Then
                returnValue = WATType.void
            Else
                returnValue = func _
                    .SubOrFunctionStatement _
                    .AsClause _
                    .ParseAsType(env:=context)
            End If

            isPublic = func.SubOrFunctionStatement _
                .Modifiers _
                .Where(Function(w) w.ValueText = "Public") _
                .Any

            context.Symbols.Add(methodName, New DeclareLocal(returnValue))
            context = New Environment(methodName, context, returnValue) With {
                .Level = SymbolTypes.Function
            }

            For Each par As DeclareLocal In pars.Values
                context.AddLocal(par)
            Next

            Return New FunctionDeclare(returnValue) With {
                .Name = methodName,
                .[namespace] = [namespace],
                .parameters = pars.Values.ToArray,
                .body = func.Statements.LoadBody(Nothing, context),
                .locals = context.Symbols _
                    .Values _
                    .Where(Function(x)
                               ' removes function parameters and
                               ' function name
                               Return (Not x.Name.StringEmpty) AndAlso (Not pars.ContainsKey(x.Name))
                           End Function) _
                    .ToArray
            }
        End Function

        ''' <summary>
        ''' 仅解析方法的签名（名称/返回类型/参数），用于支持前向引用
        ''' </summary>
        <Extension>
        Public Function ParseSignature(func As MethodBlockSyntax, context As Environment) As FunctionDeclare
            Dim returnValue As WATType

            If func.SubOrFunctionStatement.SubOrFunctionKeyword.ValueText = "Sub" Then
                returnValue = WATType.void
            Else
                returnValue = func.SubOrFunctionStatement.AsClause.ParseAsType(env:=context)
            End If

            Dim pars As Dictionary(Of String, DeclareLocal) = func.SubOrFunctionStatement _
                .ParseParameters(context) _
                .ToDictionary(Function(x) x.Name)

            Return New FunctionDeclare(returnValue) With {
                .Name = func.SubOrFunctionStatement.Identifier.ValueText,
                .[namespace] = context.FullName,
                .parameters = pars.Values.ToArray,
                .body = {},
                .locals = {}
            }
        End Function

        <Extension>
        Friend Function LoadBody(funcBody As SyntaxList(Of StatementSyntax), ByRef locals As DeclareLocal(), context As Environment) As WATSyntax()
            Dim localList As New List(Of DeclareLocal)
            Dim body As New List(Of WATSyntax)

            For Each line As StatementSyntax In funcBody.ExceptType(Of EndBlockStatementSyntax)
                Try
                    For Each item As WATSyntax In line.Parse(context)
                        If TypeOf item Is DeclareLocal Then
                            Call localList.Add(DirectCast(item, DeclareLocal))

                            If Not context.Symbols.ContainsKey(localList.Last.Name) Then
                                Call context.AddLocal(localList.Last)
                            End If
                        End If

                        Call body.Add(item)
                    Next
                Catch ex As Exception
                    ' 宽容处理：单条语句解析失败时跳过该语句，保证整份文件仍然可以生成合法的 wast
                    Call ReportSkipped($"statement {line.GetType().Name} in {context.FullName}", ex)
                End Try
            Next

            locals = localList.ToArray

            Return body.ToArray
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function ParseParameters(method As MethodBaseSyntax, context As Environment) As IEnumerable(Of DeclareLocal)
            Return method.ParameterList _
                .Parameters _
                .Select(Function(p) ParseParameter(p, context))
        End Function

        Public Function ParseParameter(parameter As ParameterSyntax, context As Environment) As DeclareLocal
            Dim name As String = parameter.Identifier.objectName
            Dim type As WATType
            Dim [default] As WATSyntax = Nothing

            If parameter.AsClause Is Nothing Then
                type = WATType.GetUnderlyingType(
                    value:=any.GetType(Patterns.TypeCharName(name.Last)),
                    wasm:=context.Workspace
                )
                name = name.Substring(0, name.Length - 1)
            Else
                type = parameter.AsClause.ParseAsType(env:=context)
            End If

            If Not parameter.Default Is Nothing Then
                [default] = parameter.Default.GetLiteralDefault(type)
            End If

            Return New DeclareLocal(type) With {
                .Name = name,
                .DefaultValue = [default]
            }
        End Function

        <Extension>
        Public Function GetLiteralDefault([default] As EqualsValueSyntax, type As WATType) As WATSyntax
            Dim defaultText As String

            Select Case [default].Value.GetType
                Case GetType(LiteralExpressionSyntax)
                    With DirectCast([default].Value, LiteralExpressionSyntax)
                        defaultText = .Token.Value
                    End With
                Case GetType(UnaryExpressionSyntax)
                    With DirectCast([default].Value, UnaryExpressionSyntax)
                        defaultText = .UnaryValue
                    End With
                Case Else
                    Throw New NotImplementedException
            End Select

            If type Is WATType.boolean Then
                Return New LiteralValue(If(defaultText = "True", 1, 0), WATType.i32)
            ElseIf type Is WATType.f32 Then
                Return New LiteralValue(Single.Parse(defaultText), WATType.f32)
            ElseIf type Is WATType.f64 Then
                Return New LiteralValue(Double.Parse(defaultText), WATType.f64)
            ElseIf type Is WATType.i64 Then
                Return New LiteralValue(Long.Parse(defaultText), WATType.i64)
            ElseIf type Is WATType.string Then
                ' 字符串默认值在编译验收阶段降级为空指针
                Return LiteralValue.Nothing(WATType.i32)
            Else
                Return LiteralValue.Nothing(WATType.i32)
            End If
        End Function
    End Module
End Namespace