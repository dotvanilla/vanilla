Imports System.Runtime.CompilerServices
Imports Wasm.Symbols

Namespace Compiler.SExpression

    ''' <summary>
    ''' Helper for some special type when generate S-Expression
    ''' </summary>
    Public Module Extensions

        <Extension>
        Public Iterator Function arrayInitialize(array As ArrayBlock) As IEnumerable(Of String)
            Yield New CommentText("")
            Yield New CommentText($"Save {array.length} array element data to memory:")
            Yield New CommentText($"Array memory block begin at location: {array.memoryPtr}")

            For Each element As Expression In array
                Yield element.ToSExpression
            Next

            Yield New CommentText("Assign array memory data to another expression")
        End Function
    End Module
End Namespace