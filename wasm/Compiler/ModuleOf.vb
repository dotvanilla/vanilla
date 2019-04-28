Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Wasm.Symbols

Namespace Compiler

    Public Class ModuleOf : Implements IReadOnlyId
        Implements IEnumerable(Of IDeclaredObject)

        ''' <summary>
        ''' [moudle_label => object]
        ''' </summary>
        ReadOnly modules As New Dictionary(Of String, IDeclaredObject)

        Public ReadOnly Property SymbolName As String Implements IReadOnlyId.Identity
        Public ReadOnly Property ModuleLabels As IEnumerable(Of String)
            Get
                Return modules.Keys
            End Get
        End Property

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

        Public Shared Widening Operator CType(func As FuncSignature) As ModuleOf
            Return New ModuleOf(func)
        End Operator

        Public Shared Widening Operator CType([global] As DeclareGlobal) As ModuleOf
            Return New ModuleOf([global])
        End Operator

        Public Iterator Function GetEnumerator() As IEnumerator(Of IDeclaredObject) Implements IEnumerable(Of IDeclaredObject).GetEnumerator
            For Each obj As IDeclaredObject In modules.Values
                Yield obj
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace