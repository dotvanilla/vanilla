Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis.TypeInfo.Operator

    Public Module CTypeHandle

        Public Function [CDbl](exp As WATSyntax, context As Environment) As WATSyntax
            Dim type As WATType = exp.Type
            Dim operator$

            Select Case type.UnderlyingWATType
                Case WATElements.i32
                    [operator] = "f64.convert_s/i32"
                Case WATElements.i64
                    [operator] = "f64.convert_s/i64"
                Case WATElements.f32
                    [operator] = "f64.promote/f32"
                Case WATElements.f64
                    Return exp
                Case Else
                    Throw New NotImplementedException(type.UnderlyingWATType.Description)
            End Select

            Return New UnaryOperator([operator], exp)
        End Function
    End Module
End Namespace