Imports System.Runtime.CompilerServices
Imports Wasm.Symbols

Namespace Compiler

    ''' <summary>
    ''' Find symbol helper
    ''' </summary>
    Module FindSymbols

        <Extension>
        Public Function FindModuleMemberFunction(symbols As SymbolTable, context$, name$) As FuncSignature
            Dim funcs = symbols.functionList.TryGetValue(name)

            If funcs Is Nothing Then
                Return Nothing
            Else
                Return funcs.FindSymbol(context Or symbols.currentModuleLabel)
            End If
        End Function

        <Extension>
        Public Function FindModuleGlobal(symbols As SymbolTable, context$, name$) As DeclareGlobal
            Dim ref = symbols.globals.TryGetValue(name)

            If ref Is Nothing Then
                Return Nothing
            Else
                Return ref.FindSymbol(context Or symbols.currentModuleLabel)
            End If
        End Function

        Public Function FindEnumValue(symbols As SymbolTable, context$, name$) As EnumSymbol
            Return symbols.enumConstants.TryGetValue(context)
        End Function
    End Module
End Namespace