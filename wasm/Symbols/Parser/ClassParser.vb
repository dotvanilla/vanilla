#Region "Microsoft.VisualBasic::63d7da79801bd5b8ec80343b0c033ea0, Symbols\Parser\ClassParser.vb"

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
'         Function: EnumerateTypes, (+3 Overloads) Parse
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
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
            Return type.Members.Parse(type.ClassStatement.Identifier.objectName, True, symbolTable, [namespace])
        End Function

        <Extension>
        Public Function Parse(type As StructureBlockSyntax, symbolTable As SymbolTable, Optional namespace$ = Nothing) As ClassMeta
            Return type.Members.Parse(type.StructureStatement.Identifier.objectName, False, symbolTable, [namespace])
        End Function

        ''' <summary>
        ''' 解析一个结构体
        ''' </summary>
        ''' <param name="body"></param>
        ''' <param name="className$"></param>
        ''' <param name="isClass"></param>
        ''' <param name="symbolTable"></param>
        ''' <param name="namespace$"></param>
        ''' <returns></returns>
        <Extension>
        Public Function Parse(body As SyntaxList(Of StatementSyntax),
                              className$,
                              isClass As Boolean,
                              symbolTable As SymbolTable,
                              Optional namespace$ = Nothing) As ClassMeta

            Dim functions As New List(Of FuncSymbol)
            Dim fieldList As New List(Of DeclareGlobal)
            Dim fieldInitialize As New List(Of Expression)

            For Each field As FieldDeclarationSyntax In body.OfType(Of FieldDeclarationSyntax)
                ' 如果是结构体内的一个常数，则将这个常数加入到全局变量之中
                fieldInitialize += field.Declarators _
                    .ParseDeclarator(symbolTable, className, field.isConst) _
                    .ToArray
            Next

            For Each globalField As DeclareGlobal In symbolTable.GetAllGlobals
                ' init值则是在初始化的时候对于没有赋值的字段进行初始值得赋值所需要的
                fieldList += New DeclareGlobal With {
                    .init = globalField.init,
                    .[module] = className,
                    .name = globalField.name,
                    .type = globalField.type
                }
            Next

            ' 目前暂时还不支持实例对象的method和property
            For Each method As MethodBlockSyntax In body.OfType(Of MethodBlockSyntax)
                functions += method.ParseFunction(className, symbolTable)
                symbolTable.currentModuleLabel = className
                symbolTable.ClearLocals()
            Next

            Dim meta As New ClassMeta(symbols:=symbolTable) With {
                .methods = functions,
                .className = className,
                .[module] = [namespace],
                .fields = fieldList,
                .isStruct = Not isClass
            }

            Return meta
        End Function

        <Extension>
        Public Iterator Function EnumerateTypes(container As SyntaxList(Of StatementSyntax),
                                                containerName$,
                                                symbols As SymbolTable) As IEnumerable(Of ClassMeta)

            For Each [class] As ClassBlockSyntax In container.OfType(Of ClassBlockSyntax)
                Yield [class].Parse(symbols, [namespace]:=containerName)

                ' 因为class的模块变量是放在global里面的
                ' 所以每解析完一个class都要清空一次全局变量
                Call symbols.ClearGlobals(includeConst:=False)
            Next

            For Each struct As StructureBlockSyntax In container.OfType(Of StructureBlockSyntax)
                Yield struct.Parse(symbols, [namespace]:=containerName)

                ' 因为class的模块变量是放在global里面的
                ' 所以每解析完一个class都要清空一次全局变量
                Call symbols.ClearGlobals(includeConst:=False)
            Next
        End Function

    End Module
End Namespace
