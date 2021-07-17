Namespace CodeAnalysis.TypeInfo.Operator

    Module SymbolMap

        ReadOnly SymbolMaps As New Dictionary(Of String, String) From {
            {"+", "add"},
            {"-", "sub"},
            {"*", "mul"},
            {"/", "div"}
        }

        Public Function GetOperator(type As WATType, [operator] As String) As String
            If SymbolMaps.ContainsKey([operator]) Then
                Return $"{type.UnderlyingWATType.Description}.{SymbolMaps([operator])}"
            Else
                Throw New NotImplementedException
            End If
        End Function

    End Module
End Namespace