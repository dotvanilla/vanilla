Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Wasm.Compiler
Imports Wasm.SyntaxAnalysis

Namespace Symbols.JavaScriptImports

    Module InternalIndexer

        ''' <summary>
        ''' Do imports for webassembly application by default
        ''' </summary>
        ''' <param name="symbols"></param>
        <Extension>
        Public Sub DoImports(symbols As SymbolTable, [module] As Type)
            For Each api As PropertyInfo In [module].GetProperties()
                Call symbols.addRequired(api.GetValue(Nothing))
            Next
        End Sub

        Public Function HandleVisualBasicSymbols([module] As Type) As Dictionary(Of String, ImportSymbol)
            Dim apiList = [module].GetProperties(BindingFlags.Public Or BindingFlags.Static)
            Dim handler As New Dictionary(Of String, ImportSymbol)
            Dim symbol As ImportSymbol
            Dim symbolKey As String

            For Each apiSymbol As PropertyInfo In apiList
                symbol = DirectCast(apiSymbol.GetValue(Nothing, Nothing), ImportSymbol)
                symbolKey = apiSymbol.Name

                Call handler.Add(symbolKey, symbol)
            Next

            Return handler
        End Function
    End Module
End Namespace