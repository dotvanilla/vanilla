Imports System.Runtime.CompilerServices
Imports Wasm.Symbols

Namespace Compiler.SExpression

    Module AppStarter

        <Extension>
        Private Function writeStarter(funcs As IEnumerable(Of String), calls As String) As String
            Return $"
;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
    {calls}
)

{funcs.JoinBy(vbCrLf & vbCrLf)}

(start $Application_SubNew)"
        End Function

        <Extension>
        Public Function starter([module] As ModuleSymbol) As String
            If [module].Start Is Nothing Then
                Return New String() {}.writeStarter("")
            Else
                Return [module].Start _
                    .constructors _
                    .Select(Function(f) f.ToSExpression) _
                    .writeStarter([module].Start.ToSExpression)
            End If
        End Function
    End Module
End Namespace