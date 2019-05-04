Imports System.Runtime.CompilerServices
Imports Wasm.Symbols

Namespace Compiler.SExpression

    Module MemberHelper

        <Extension>
        Friend Function starter([module] As ModuleSymbol) As String
            If [module].Start Is Nothing Then
                Return ""
            Else
                Return [module].Start.ToSExpression
            End If
        End Function

        <Extension>
        Friend Iterator Function exportGroup(exports As ExportSymbolExpression()) As IEnumerable(Of String)
            Dim moduleGroup = exports.GroupBy(Function(api) api.module).ToArray

            For Each [module] In moduleGroup
                Yield $";; export from VB.NET module: [{[module].Key}]"
                Yield ""

                For Each func As ExportSymbolExpression In [module]
                    Yield func.ToSExpression
                Next

                Yield ""
                Yield ""
            Next
        End Function

        <Extension>
        Friend Iterator Function funcGroup(internal As FuncSymbol()) As IEnumerable(Of String)
            Dim moduleGroups = internal.GroupBy(Function(f) f.module).ToArray

            For Each [module] In moduleGroups
                Yield $";; functions in [{[module].Key}]"
                Yield ""

                For Each func As FuncSymbol In [module]
                    Yield func.ToSExpression
                Next

                Yield ""
                Yield ""
            Next
        End Function
    End Module
End Namespace