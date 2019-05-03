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

                For Each globalField As DeclareGlobal In symbolTable.GetAllGlobals
                    fieldList += New DeclareGlobal With {
                        .init = globalField.init,
                        .[Module] = className,
                        .name = globalField.name,
                        .type = globalField.type
                    }
                Next

                Call symbolTable.ClearLocals()
            Next

            For Each method In type.Members.OfType(Of MethodBlockSyntax)
                functions += method.ParseFunction(className, symbolTable)
                symbolTable.currentModuleLabel = className
                symbolTable.ClearLocals()
            Next

            Return New ClassMeta With {
                .Methods = functions,
                .ClassName = className,
                .[Module] = [namespace],
                .Fields = fieldList
            }
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