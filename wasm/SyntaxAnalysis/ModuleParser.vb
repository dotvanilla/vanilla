#Region "Microsoft.VisualBasic::41b89f30b5b36a6f5212e4cc387dcb7e, SyntaxAnalysis\ModuleParser.vb"

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

'     Module ModuleParser
' 
'         Function: AddCustomTypes, AsConstructor, (+2 Overloads) CreateModule, CreateModuleInternal, CreateUnitModule
'                   (+2 Overloads) isConst, Join, ParseDeclares, parseEnums, ParseEnums
' 
'         Sub: parseGlobals, parseImports
' 
' 
' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Public Module ModuleParser

        ''' <summary>
        ''' Only join module members functions
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="main"></param>
        ''' <returns></returns>
        <Extension>
        Private Function Join(symbols As SymbolTable, main As ModuleBlockSyntax, enumConstants As EnumSymbol()) As SymbolTable
            Dim members = main.Members.OfType(Of MethodBlockSyntax)
            Dim moduleLabel$ = main.BlockStatement.Identifier.objectName

            If symbols Is Nothing Then
                Return New SymbolTable(moduleLabel, members, enumConstants)
            Else
                For Each [const] As EnumSymbol In enumConstants
                    Call symbols.AddEnumType([const])
                Next

                Return symbols.AddFunctionDeclares(members, moduleLabel)
            End If
        End Function

        <Extension>
        Public Function ParseEnums(root As CompilationUnitSyntax) As EnumSymbol()
            ' 添加常数申明
            Return root.Members _
                .OfType(Of EnumBlockSyntax) _
                .parseEnums() _
                .ToArray
        End Function

        ''' <summary>
        ''' This function have some limitations: 
        ''' 
        ''' 1. <paramref name="vbcode"/> should only have 1 module define in it and no class/strucutre was allowed.
        ''' 2. Only allows numeric algorithm code in the modules
        ''' 3. Not supports string and other non-primitive type.
        ''' </summary>
        ''' <param name="vbcode">This parameter can be file path or file text content.</param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function CreateModule(vbcode As [Variant](Of FileInfo, String)) As ModuleSymbol
            Return CreateUnitModule(VisualBasicSyntaxTree.ParseText(vbcode.SolveStream).GetRoot)
        End Function

        <Extension>
        Public Function CreateUnitModule(vbcode As CompilationUnitSyntax) As ModuleSymbol
            Dim project As ModuleBlockSyntax() = vbcode.Members _
                .OfType(Of ModuleBlockSyntax) _
                .ToArray
            Dim enums As EnumSymbol()
            Dim symbols As SymbolTable = vbcode.AddCustomTypes(New SymbolTable)

            For Each main As ModuleBlockSyntax In project
                enums = vbcode.ParseEnums
                symbols = main.ParseDeclares(symbols, enums)
            Next

            ' 解析成员函数的具体定义内容
            Return project.CreateModule(symbols, Nothing)
        End Function

        <Extension>
        Public Function AddCustomTypes(vbcode As CompilationUnitSyntax, symbols As SymbolTable) As SymbolTable
            ' 因为class的解析，对于field对象而言是放在符号表的
            ' global对象之中的，所以在这里class的解析要优先于
            ' module的解析，否则会因为module的global混入class的fields
            ' 产生错误的类型定义结果
            For Each container As NamespaceBlockSyntax In vbcode.Members.OfType(Of NamespaceBlockSyntax)
                Dim name$ = container.NamespaceStatement.Name.ToString
                Dim types As ClassMeta() = container _
                    .Members _
                    .EnumerateTypes(name, symbols) _
                    .ToArray

                symbols.AddClass(types)
            Next

            Call symbols.AddClass(vbcode.Members.EnumerateTypes(Nothing, symbols))
            Call symbols.ClearLocals()
            Call symbols.ClearGlobals(includeConst:=False)

            Return symbols
        End Function

        <Extension>
        Friend Function CreateModule(modules As IEnumerable(Of ModuleBlockSyntax), symbols As SymbolTable, label$) As ModuleSymbol
            Dim project As New ModuleSymbol
            Dim part As ModuleSymbol

            ' 然后再进行具体的函数解析就不出错了
            For Each [module] As ModuleBlockSyntax In modules
                part = ModuleParser.CreateModuleInternal([module], symbols)
                project = project.Join(part)
            Next

            If Not label.StringEmpty Then
                project.LabelName = label
            End If

            project.globalStarter = New FuncSymbol() With {
                .isExtensionMethod = False,
                .locals = {},
                .[module] = "global",
                .name = "initializer",
                .parameters = {},
                .result = TypeAbstract.void,
                .body = symbols.GetGlobalStarter.ToArray
            }

            Return project
        End Function

        ''' <summary>
        ''' 解析模块内的成员函数或者全局变量的声明
        ''' </summary>
        ''' <param name="main"></param>
        ''' <param name="symbols"></param>
        ''' <param name="enums"></param>
        ''' <returns></returns>
        <Extension>
        Public Function ParseDeclares(main As ModuleBlockSyntax, symbols As SymbolTable, enums As EnumSymbol()) As SymbolTable
            Dim symbolTable As SymbolTable = symbols.Join(main, enums)
            Dim moduleName As String = main.ModuleStatement _
                .Identifier _
                .objectName

            ' 添加declare导入
            Call main.Members _
                .OfType(Of DeclareStatementSyntax) _
                .parseImports(moduleName, symbolTable)
            ' 添加内部模块变量
            Call main.Members _
                .OfType(Of FieldDeclarationSyntax) _
                .parseGlobals(moduleName, symbolTable)

            Return symbolTable
        End Function

        ''' <summary>
        ''' This function have some limitations: 
        ''' 
        ''' 1. <paramref name="main"/> should only have 1 module define in it and no class/strucutre was allowed.
        ''' 2. Only allows numeric algorithm code in the modules
        ''' 3. Not supports string and other non-primitive type.
        ''' </summary>
        ''' <returns></returns>
        ''' 
        <Extension>
        Friend Function CreateModuleInternal(main As ModuleBlockSyntax, symbolTable As SymbolTable) As ModuleSymbol
            Dim functions As New List(Of FuncSymbol)
            Dim exports As New List(Of ExportSymbolExpression)
            Dim moduleName As String = main.ModuleStatement _
                .Identifier _
                .objectName

            ' Assign module labels for find function 
            ' and global variables
            symbolTable.context.moduleLabel = moduleName

            For Each method In main.Members.OfType(Of MethodBlockSyntax)
                functions += method.ParseFunction(moduleName, symbolTable)
                symbolTable.ClearLocals()

                If method.SubOrFunctionStatement.isExportObject Then
                    exports += New ExportSymbolExpression With {
                        .Name = functions.Last.name.Trim("$"c),
                        .target = functions.Last,
                        .type = "func",
                        .[module] = moduleName
                    }
                End If
            Next

            Dim start As FuncSymbol = main.Members _
                .OfType(Of ConstructorBlockSyntax) _
                .FirstOrDefault _
                .AsConstructor(symbolTable)

            Return New ModuleSymbol With {
                .InternalFunctions = functions,
                .LabelName = moduleName,
                .Exports = exports,
                .[Imports] = symbolTable.GetAllImports.ToArray,
                .Globals = symbolTable.GetAllGlobals.ToArray,
                .memory = symbolTable,
                .start = New Start With {
                    .constructors = New List(Of FuncSymbol) From {start}
                }
            }
        End Function

        <Extension>
        Private Function AsConstructor(ctor As ConstructorBlockSyntax, symbols As SymbolTable) As FuncSymbol
            Dim moduleNew As (locals As DeclareLocal(), body As Expression()) = Nothing

            If Not ctor Is Nothing Then
                moduleNew = ctor.Statements.ToArray.FunctionBody({}, symbols)
            End If

            Return New FuncSymbol() With {
                .body = moduleNew.body,
                .locals = moduleNew.locals,
                .isExtensionMethod = False,
                .[module] = symbols.context.moduleLabel,
                .name = "constructor",
                .parameters = {},
                .result = TypeAbstract.void
            }
        End Function

        <Extension>
        Private Iterator Function parseEnums(declares As IEnumerable(Of EnumBlockSyntax)) As IEnumerable(Of EnumSymbol)
            For Each type As EnumBlockSyntax In declares
                Yield New EnumSymbol(type)
            Next
        End Function

        <Extension>
        Private Sub parseImports(declares As IEnumerable(Of DeclareStatementSyntax), moduleName$, symbolTable As SymbolTable)
            For Each api As DeclareStatementSyntax In declares
                Dim define As NamedValue(Of TypeAbstract) = api.FuncVariable(symbolTable)
                Dim apiImports As New ImportSymbol(api.ParseParameters(symbolTable)) With {
                    .name = define.Name,
                    .result = define.Value,
                    .importAlias = api.AliasName.Token.ValueText,
                    .package = api.LibraryName.Token.ValueText,
                    .[module] = moduleName,
                    .isExtensionMethod = api.IsExtensionMethod
                }

                ' add api symbols for type match in function body
                Call symbolTable.AddImports(apiImports)
            Next
        End Sub

        ''' <summary>
        ''' Parse global variables
        ''' </summary>
        ''' <param name="declares"></param>
        <Extension>
        Private Sub parseGlobals(declares As IEnumerable(Of FieldDeclarationSyntax), moduleName$, symbolTable As SymbolTable)
            For Each field As FieldDeclarationSyntax In declares
                ' 已经在函数的内部进行添加调用了
                Call field.Declarators _
                    .ParseDeclarator(symbolTable, moduleName, field.isConst) _
                    .ToArray
            Next
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Friend Function isConst(field As FieldDeclarationSyntax) As Boolean
            Return field.Modifiers.isConst
        End Function

        <Extension>
        Friend Function isConst(modifier As SyntaxTokenList) As Boolean
            Dim constTag As SyntaxToken = modifier.FirstOrDefault(Function(tag) tag.Text = "Const")
            Dim flag As Boolean = Not constTag.Text.StringEmpty

            Return flag
        End Function
    End Module
End Namespace
