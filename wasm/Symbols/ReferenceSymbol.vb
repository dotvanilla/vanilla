Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository

Namespace Symbols

    Public Class ReferenceSymbol : Implements IDeclaredObject

        Public Property [Module] As String Implements IDeclaredObject.Module
        Public Property Symbol As String Implements IKeyedEntity(Of String).Key
        Public Property IsOperator As Boolean

        Public Overrides Function ToString() As String
            If IsOperator Then
                Return Symbol
            Else
                Return [Module] & "." & Symbol
            End If
        End Function

        Public Shared Widening Operator CType(func As FuncSignature) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .IsOperator = False,
                .[Module] = func.Module,
                .Symbol = func.Name
            }
        End Operator
    End Class
End Namespace