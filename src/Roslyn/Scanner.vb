Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.VBProj
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.Roslyn.VBLanguageParser
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.Syntax.Literal
Imports TypeSchema = VanillaBasic.WebAssembly.CodeAnalysis.TypeSchema

''' <summary>
''' the syntax parser of the VisualBasic source file.
''' </summary>
Public NotInheritable Class Scanner

    Public ReadOnly Property Workspace As Workspace

    Sub New(proj As VBProject)
        Call Me.New(proj.RootNamespace, proj.AssemblyInfo)
    End Sub

    ''' <summary>
    ''' 直接使用根命名空间以及程序集信息来构造扫描器，
    ''' 用于在无法使用 <see cref="VBProject"/> 的 ``XmlSerializer`` 反序列化时（例如 vbproj 之中包含
    ''' 无法被序列化的 ``Dictionary`` 成员）提供一个轻量级的替代构造方式。
    ''' </summary>
    Sub New(rootNamespace As String, assemblyInfo As AssemblyInfo)
        Workspace = New Workspace(If(rootNamespace, "")) With {.MSIL = AddressOf MSIL.ParseMSIL}
        Workspace.AssemblyInfo = If(assemblyInfo, New AssemblyInfo)
    End Sub

    Sub New()
        Call Me.New("", New AssemblyInfo)
    End Sub

    ''' <summary>
    ''' 解析一个 VB.NET 源代码文件（或者文件路径），将其中的所有模块/类型/枚举以及全局字段
    ''' 解析到当前的工作区之中。
    ''' </summary>
    Public Function AddModules(vb As [Variant](Of FileInfo, String)) As Scanner
        Dim source As String = vb.SolveStream

        If String.IsNullOrEmpty(source) Then
            Return Me
        End If

        Dim syntax As CompilationUnitSyntax = VisualBasicSyntaxTree.ParseText(source).GetRoot
        Dim [global] As New ProjectEnvironment(Workspace)

        [global].Imports.NamespaceList = syntax.Imports.Select(Function(i) i.ToString).ToArray

        Call ParseMembers(syntax.Members, [global])

        Return Me
    End Function

    Private Sub ParseMembers(members As IEnumerable(Of StatementSyntax), env As Environment)
        For Each member As StatementSyntax In members
            Try
                Select Case member.GetType
                    Case GetType(ModuleBlockSyntax)
                        Call ParseModule(DirectCast(member, ModuleBlockSyntax), env)
                    Case GetType(NamespaceBlockSyntax)
                        Dim ns As NamespaceBlockSyntax = DirectCast(member, NamespaceBlockSyntax)
                        Dim nsEnv As New Environment(ns.NamespaceStatement.Name.ToString, env)

                        Call ParseMembers(ns.Members, nsEnv)
                    Case GetType(EnumBlockSyntax)
                        Call ParseEnum(DirectCast(member, EnumBlockSyntax))
                    Case GetType(ClassBlockSyntax)
                        With DirectCast(member, ClassBlockSyntax)
                            Call ParseTypeBlock(.ClassStatement.Identifier.ValueText, .Members, env)
                        End With
                    Case GetType(StructureBlockSyntax)
                        With DirectCast(member, StructureBlockSyntax)
                            Call ParseTypeBlock(.StructureStatement.Identifier.ValueText, .Members, env)
                        End With
                    Case GetType(DeclareStatementSyntax)
                        Call RegisterDeclare(DirectCast(member, DeclareStatementSyntax), env)
                    Case Else
                        ' Interface / Delegate / Attributes 等其他顶层节点暂时忽略
                End Select
            Catch ex As Exception
                Call ReportSkipped($"top-level member {member.GetType().Name}", ex)
            End Try
        Next
    End Sub

    ''' <summary>
    ''' 解析 ``Module`` 块：模块级别的字段、``Declare`` 语句以及方法
    ''' </summary>
    Private Sub ParseModule(code As ModuleBlockSyntax, env As Environment)
        Dim moduleName As String = code.ModuleStatement.Identifier.ValueText.Trim("["c, "]"c)
        Dim moduleEnv As New Environment(moduleName, env)
        Dim isPublic As Boolean = False
        Dim exports As New List(Of String)
        Dim sharedSubNew As FunctionDeclare = Nothing
        Dim globalInitializers As New List(Of WATSyntax)

        For Each field As FieldDeclarationSyntax In code.Members.OfType(Of FieldDeclarationSyntax)
            Call ParseModuleFields(field, moduleEnv, globalInitializers)
        Next

        For Each decl As DeclareStatementSyntax In code.Members.OfType(Of DeclareStatementSyntax)
            Call RegisterDeclare(decl, moduleEnv)
        Next

        ' 处理模块内部嵌套的类型 / 枚举 / 子模块
        For Each nested As StatementSyntax In code.Members
            Select Case nested.GetType
                Case GetType(ModuleBlockSyntax), GetType(ClassBlockSyntax), GetType(StructureBlockSyntax),
                     GetType(EnumBlockSyntax), GetType(NamespaceBlockSyntax)
                    Call ParseMembers({nested}, moduleEnv)
            End Select
        Next

        ' 第一遍：先注册所有方法的签名，从而支持方法之间的前向引用
        For Each func As MethodBlockSyntax In code.Members.OfType(Of MethodBlockSyntax)
            Try
                Dim signature As FunctionDeclare = func.ParseSignature(moduleEnv)

                If Not (signature.Name = "New" AndAlso signature.Type Is WATType.void) Then
                    Call AddMethod(signature)
                End If
            Catch ex As Exception
                Call ReportSkipped($"signature {moduleEnv.FullName}.{func.SubOrFunctionStatement?.Identifier.ValueText}", ex)
            End Try
        Next

        For Each func As MethodBlockSyntax In code.Members.OfType(Of MethodBlockSyntax)
            Try
                isPublic = False
                Dim closure As FunctionDeclare = func.RunParser(isPublic, moduleEnv)

                If closure.Name = "New" AndAlso closure.Type Is WATType.void Then
                    sharedSubNew = closure
                Else
                    Call RegisterMethod(closure)

                    If isPublic Then
                        exports.Add(closure.Name)
                    End If
                End If
            Catch ex As Exception
                Call ReportSkipped($"method {moduleEnv.FullName}.{func.SubOrFunctionStatement?.Identifier.ValueText}", ex)
            End Try
        Next

        If sharedSubNew Is Nothing Then
            sharedSubNew = NewEmptyConstructor(moduleEnv.FullName)
        End If

        ' 将模块级别的全局字段初始化语句放入模块构造函数之中
        If globalInitializers.Count > 0 Then
            sharedSubNew.body = globalInitializers.Concat(sharedSubNew.body).ToArray
        End If

        Dim standardModule As New TypeSchema With {
            .IsStandardModule = True,
            .Name = moduleName,
            .ExportApi = exports.ToArray,
            .[Namespace] = env.FullName,
            .Initializer = sharedSubNew
        }

        If Not Workspace.Types.ContainsKey(standardModule.FullName) Then
            Workspace.Types.Add(standardModule.FullName, standardModule)
        End If
    End Sub

    ''' <summary>
    ''' 解析 ``Class`` / ``Structure`` 块（编译验收需要的简化对象模型）
    ''' </summary>
    Private Sub ParseTypeBlock(name As String, members As SyntaxList(Of StatementSyntax), env As Environment)
        Dim typeEnv As New Environment(name, env)
        Dim isPublic As Boolean = False

        ' 第一遍：注册方法签名以支持前向引用
        For Each func As MethodBlockSyntax In members.OfType(Of MethodBlockSyntax)
            Try
                Dim signature As FunctionDeclare = func.ParseSignature(typeEnv)

                If Not (signature.Name = "New" AndAlso signature.Type Is WATType.void) Then
                    Call AddMethod(signature)
                End If
            Catch ex As Exception
                Call ReportSkipped($"signature {typeEnv.FullName}.{func.SubOrFunctionStatement?.Identifier.ValueText}", ex)
            End Try
        Next

        For Each func As MethodBlockSyntax In members.OfType(Of MethodBlockSyntax)
            Try
                isPublic = False
                Dim closure As FunctionDeclare = func.RunParser(isPublic, typeEnv)

                If closure.Name = "New" AndAlso closure.Type Is WATType.void Then
                    ' 构造函数暂时忽略
                    Continue For
                End If

                Call RegisterMethod(closure)
            Catch ex As Exception
                Call ReportSkipped($"type member {typeEnv.FullName}.{func.SubOrFunctionStatement?.Identifier.ValueText}", ex)
            End Try
        Next

        Dim schema As New TypeSchema With {
            .IsStandardModule = False,
            .Name = name,
            .ExportApi = {},
            .[Namespace] = env.FullName,
            .Initializer = NewEmptyConstructor(typeEnv.FullName)
        }

        If Not Workspace.Types.ContainsKey(schema.FullName) Then
            Workspace.Types.Add(schema.FullName, schema)
        End If
    End Sub

    ''' <summary>
    ''' 解析模块级别的字段声明（``Dim`` / ``Const`` / ``Public`` / ``Private``）
    ''' </summary>
    Private Sub ParseModuleFields(field As FieldDeclarationSyntax, env As Environment, initializers As List(Of WATSyntax))
        For Each declarator As VariableDeclaratorSyntax In field.Declarators
            For Each [declare] As DeclareLocal In declarator.ParseDeclarator(env)
                If [declare].Name.StringEmpty Then
                    Continue For
                End If

                Dim value As WATSyntax = [declare].DefaultValue

                If Not value Is Nothing Then
                    ' wasm 全局量只接受常量初值，真正的初始化放入模块构造函数
                    initializers.Add(New SymbolSetValue With {
                        .Target = New SymbolReference([declare].Name),
                        .Value = CTypeHandle.[CType]([declare].Type, value, env),
                        .isGlobal = True
                    })
                End If

                If Not Workspace.GlobalSymbols.ContainsKey([declare].Name) Then
                    Workspace.GlobalSymbols.Add([declare].Name, New DeclareGlobal With {
                        .Name = [declare].Name,
                        .DeclaredType = [declare].Type,
                        .Value = ZeroLiteral([declare].Type)
                    })
                End If
            Next
        Next
    End Sub

    ''' <summary>
    ''' 解析 ``Declare Sub/Function ... Lib "..." Alias "..."`` 语句为 javascript 导入
    ''' </summary>
    Private Sub RegisterDeclare(decl As DeclareStatementSyntax, env As Environment)
        Dim name As String = decl.Identifier.ValueText

        If Workspace.Imports.ContainsKey(name) Then
            Return
        End If

        Dim library As String = "js"

        If TypeOf decl.LibraryName Is LiteralExpressionSyntax Then
            library = DirectCast(decl.LibraryName, LiteralExpressionSyntax).Token.ValueText
        End If

        Dim entry As String = name
        Dim aliasToken As SyntaxToken = decl.DescendantTokens() _
            .FirstOrDefault(Function(t) t.Kind = SyntaxKind.AliasKeyword)

        If aliasToken.RawKind <> 0 Then
            Dim ident As SyntaxToken = decl.DescendantTokens() _
                .SkipWhile(Function(t) t <> aliasToken) _
                .Skip(1) _
                .FirstOrDefault()

            If ident.RawKind <> 0 AndAlso ident.Kind = SyntaxKind.IdentifierToken Then
                entry = ident.Text
            End If
        End If

        Dim returnType As WATType = If(decl.AsClause Is Nothing, WATType.void, decl.AsClause.ParseAsType(env))
        Dim pars As DeclareLocal() = decl.ParameterList _
            .Parameters _
            .Select(Function(p) ParseFunction.ParseParameter(p, env)) _
            .ToArray

        Workspace.Imports.Add(name, New ImportsFunction(returnType) With {
            .Name = name,
            .ModuleName = library,
            .ImportsName = entry,
            .Parameters = pars
        })
    End Sub

    ''' <summary>
    ''' 解析 ``Enum`` 块
    ''' </summary>
    Private Sub ParseEnum(code As EnumBlockSyntax)
        Dim name As String = code.EnumStatement.Identifier.ValueText
        Dim underlying As WATType = GetEnumUnderlying(code)

        If Workspace.EnumVals.ContainsKey(name) Then
            Return
        End If

        Dim symbol As New EnumSymbol(name, underlying)
        Dim value As Long = 0

        For Each member As StatementSyntax In code.Members
            If TypeOf member Is EnumMemberDeclarationSyntax Then
                Dim m As EnumMemberDeclarationSyntax = DirectCast(member, EnumMemberDeclarationSyntax)

                If Not m.Initializer Is Nothing Then
                    Dim init As WATSyntax = m.Initializer.Value.ParseValue(New ProjectEnvironment(Workspace))

                    If TypeOf init Is LiteralValue Then
                        value = CLng(DirectCast(init, LiteralValue).Value)
                    End If
                End If

                symbol.Members(m.Identifier.ValueText) = New LiteralValue(value, underlying)
                value += 1
            End If
        Next

        Workspace.EnumVals.Add(name, symbol)
    End Sub

    Private Function GetEnumUnderlying(code As EnumBlockSyntax) As WATType
        Dim asClause As AsClauseSyntax = code.EnumStatement.UnderlyingType

        If asClause Is Nothing OrElse Not TypeOf asClause Is SimpleAsClauseSyntax Then
            Return WATType.i32
        End If

        Dim type As WATType = DirectCast(asClause, SimpleAsClauseSyntax).ParseAsType(New ProjectEnvironment(Workspace))

        If type.UnderlyingWATType = WATElements.i64 Then
            Return WATType.i64
        Else
            Return WATType.i32
        End If
    End Function

    Private Sub AddMethod(func As FunctionDeclare)
        If Not Workspace.Methods.ContainsKey(func.FullName) Then
            Call Workspace.AddStaticMethod(func)
        End If
    End Sub

    ''' <summary>
    ''' 注册（或者用完整解析结果覆盖）一个方法
    ''' </summary>
    Private Sub RegisterMethod(func As FunctionDeclare)
        If Workspace.Methods.ContainsKey(func.FullName) Then
            Workspace.Methods(func.FullName) = func
        Else
            Call Workspace.AddStaticMethod(func)
        End If
    End Sub

    Private Function NewEmptyConstructor(nsName As String) As FunctionDeclare
        Return New FunctionDeclare(WATType.void) With {
            .body = {},
            .locals = {},
            .Name = "SubNew",
            .[namespace] = nsName,
            .parameters = {}
        }
    End Function

    ''' <summary>
    ''' 零值常量字面量（用于 wasm 全局量的常量初值）
    ''' </summary>
    Private Function ZeroLiteral(type As WATType) As WATSyntax
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
End Class
