Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module IfBlockParser

        <Extension>
        Public Function IfBlock(doif As MultiLineIfBlockSyntax, context As Environment) As [If]
            Dim test As WATSyntax = doif.IfStatement.Condition.ParseValue(context)
            Dim thenBlock As Closure = doif.Statements.ParseClosure(context)

            If doif.ElseBlock Is Nothing Then
                Return New [If] With {
                    .condition = New BooleanLogical(test),
                    .[then] = thenBlock
                }
            Else
                Return New IfElse With {
                    .condition = New BooleanLogical(test),
                    .[then] = thenBlock,
                    .[else] = doif.ElseBlock.Statements.ParseClosure(context)
                }
            End If
        End Function
    End Module
End Namespace