Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace VBLanguageParser

    Module ParseFunction

        <Extension>
        Public Function RunParser(func As MethodBlockSyntax, context As Environment) As FunctionDeclare
            Dim returnValue As WATType

            If func.SubOrFunctionStatement.SubOrFunctionKeyword.ValueText = "Sub" Then
                returnValue = WATType.void
            Else
                returnValue = func.SubOrFunctionStatement.AsClause.ParseAsType(env:=context)
            End If

            Return New FunctionDeclare(returnValue) With {
                .Name = func.SubOrFunctionStatement.Identifier.ValueText,
                .[namespace] = context.FullName
            }
        End Function
    End Module
End Namespace