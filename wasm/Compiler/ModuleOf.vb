Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Wasm.Symbols

Namespace Compiler

    Public Class ModuleOf : Implements IReadOnlyId

        ''' <summary>
        ''' [moudle_label => object]
        ''' </summary>
        ReadOnly modules As New Dictionary(Of String, IDeclaredObject)

        Public ReadOnly Property SymbolName As String Implements IReadOnlyId.Identity

        Sub New(symbol As IDeclaredObject)
            Add(symbol)
            SymbolName = symbol.Key
        End Sub

        Public Sub Add(symbol As IDeclaredObject)
            Call modules.Add(symbol.Module, symbol)
        End Sub

        Public Function FindSymbol(moduleLabel As String) As IDeclaredObject
            Return modules.TryGetValue(moduleLabel)
        End Function

        Public Overrides Function ToString() As String
            Return SymbolName
        End Function
    End Class
End Namespace