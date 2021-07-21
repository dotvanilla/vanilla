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

            If TypeOf target Is SymbolReference AndAlso DirectCast(target, SymbolReference) = NameOf(Console) Then
                Return member.TranslateConsoleApi
            Else
                Throw New NotImplementedException
            End If
        End Function

        <Extension>
        Private Function TranslateConsoleApi(member As WATSyntax) As JavaScriptTranslation
            Static Console As New SymbolReference With {.Name = NameOf(Console)}

            If Not TypeOf member Is SymbolReference Then
                Throw New NotImplementedException
            End If

            Dim dotnet As New MemberAccess With {.target = Console, .member = member, .accessor = AccessorType.Method}

            Select Case DirectCast(member, SymbolReference).Name
                Case "WriteLine", "Write"
                    Return New JavaScriptTranslation(WATType.void) With {
                        .DotNetFramework = dotnet,
                        .JavaScript = "Console.WriteLine"
                    }
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function
    End Module
End Namespace