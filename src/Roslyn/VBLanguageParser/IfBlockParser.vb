Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace VBLanguageParser

    Module IfBlockParser

        <Extension>
        Public Function IfBlock(doif As MultiLineIfBlockSyntax, context As Environment) As [If]
            Dim test As WATSyntax = doif.IfStatement.Condition.ParseValue(context)

        End Function
    End Module
End Namespace