Imports Wasm.Symbols

''' <summary>
''' Helpers for create literal expression
''' </summary>
Public NotInheritable Class Literal

    Private Sub New()
    End Sub

    Public Shared Function i32(i As Integer) As LiteralExpression
        Return New LiteralExpression With {
            .type = New TypeAbstract("i32"),
            .value = i
        }
    End Function

    Public Shared Function i64(i As Long) As LiteralExpression
        Return New LiteralExpression With {
            .type = New TypeAbstract("i64"),
            .value = i
        }
    End Function

    Public Shared Function f32(s As Single) As LiteralExpression
        Return New LiteralExpression With {
            .type = New TypeAbstract("f32"),
            .value = s
        }
    End Function

    Public Shared Function f64(f As Double) As LiteralExpression
        Return New LiteralExpression With {
            .type = New TypeAbstract("f64"),
            .value = f
        }
    End Function

    Public Shared Function bool(b As Boolean) As LiteralExpression
        Return New LiteralExpression With {
            .type = New TypeAbstract(GetType(Boolean)),
            .value = If(b, 1, 0)
        }
    End Function
End Class
