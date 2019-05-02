Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject

Namespace Symbols.Parser

    ''' <summary>
    ''' Parser for class/structure
    ''' </summary>
    Module ClassParser

        <Extension>
        Public Function Parse(type As ClassBlockSyntax, symbols As SymbolTable, Optional namespace$ = Nothing) As ClassMeta
            Dim name$ = type.ClassStatement.Identifier.objectName

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