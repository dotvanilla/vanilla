Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject

Namespace Symbols.Parser

    ''' <summary>
    ''' Parser for class/structure
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