Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module MemberAccessParser

        <Extension>
        Public Function ParseReference(indexer As MemberAccessExpressionSyntax, context As Environment) As WATSyntax
            Dim target = indexer.Expression.ParseValue(context)
            Dim member = indexer.Name.ParseValue(context)


        End Function
    End Module
End Namespace