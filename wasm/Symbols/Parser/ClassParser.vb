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
        Public Function Parse(type As ClassBlockSyntax, symbols As SymbolTable) As ClassMeta
            Dim name$ = type.ClassStatement.Identifier.objectName

        End Function
    End Module
End Namespace