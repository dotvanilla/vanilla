#Region "Microsoft.VisualBasic::b7ebfd555bd533a27ba2adb347b5c14f, Symbols\Parser\ClassParser.vb"

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

    '     Module ClassParser
    ' 
    '         Function: EnumerateTypes, Parse
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject

Namespace Symbols.Parser

    ''' <summary>
    ''' Parser for class/structure
    ''' 
    ''' 在这个模块之中，class的解析类似于module的解析，但是会需要完成下面的一些改造
    ''' 
    ''' 1. 为类型的实例方法增加一个类型实例参数
    ''' 2. 将方法之中的全局变量引用改造为实例对象的字段的引用
    ''' </summary>
    Module ClassParser

        <Extension>
        Public Function Parse(type As ClassBlockSyntax, symbolTable As SymbolTable, Optional namespace$ = Nothing) As ClassMeta
            Dim className$ = type.ClassStatement.Identifier.objectName
            Dim functions As New List(Of FuncSymbol)
            Dim fieldList As New List(Of DeclareGlobal)
            Dim fieldInitialize As New List(Of Expression)

            For Each field As FieldDeclarationSyntax In type _
                .Members _
                .OfType(Of FieldDeclarationSyntax)

                fieldInitialize += field.Declarators _
                    .ParseDeclarator(symbolTable, className) _
                    .ToArray
            Next

            For Each globalField As DeclareGlobal In symbolTable.GetAllGlobals
                fieldList += New DeclareGlobal With {
                    .init = globalField.init,
                    .[module] = className,
                    .name = globalField.name,
                    .type = globalField.type
                }
            Next

            ' 目前暂时还不支持实例对象的method和property
            For Each method In type.Members.OfType(Of MethodBlockSyntax)
                functions += method.ParseFunction(className, symbolTable)
                symbolTable.currentModuleLabel = className
                symbolTable.ClearLocals()
            Next

            Dim meta As New ClassMeta With {
                .Methods = functions,
                .ClassName = className,
                .[module] = [namespace],
                .Fields = fieldList
            }

            Return meta
        End Function

        <Extension>
        Public Iterator Function EnumerateTypes(type As NamespaceBlockSyntax, symbols As SymbolTable) As IEnumerable(Of ClassMeta)
            Dim name$ = type.NamespaceStatement.Name.ToString

            For Each [class] As ClassBlockSyntax In type.Members.OfType(Of ClassBlockSyntax)
                Yield [class].Parse(symbols, [namespace]:=name)
            Next
        End Function

    End Module
End Namespace
