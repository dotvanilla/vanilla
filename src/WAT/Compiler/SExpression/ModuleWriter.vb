Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports VanillaBasic.WebAssembly.CodeAnalysis

Public Module ModuleWriter

    <Extension>
    Private Function writeStarter(funcs As IEnumerable(Of String), calls As String, globals As Object) As String
        Return $"
    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        {globals.Call.ToSExpression}

        {calls}
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    {globals.ToSExpression}

    {funcs.JoinBy(vbCrLf & vbCrLf)}

    (start $Application_SubNew)"
    End Function

    <Extension>
    Public Function WriteProjectModule(project As Workspace, content As String) As String
        Dim buildTime As String = Now.ToString
        Dim wasmSummary As AssemblyInfo = project.AssemblyInfo

        Return $"(module ;; Microsoft VisualBasic Project {project.DefaultNamespace}

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: {wasmSummary.AssemblyVersion}
    ;; build: {buildTime}
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    {content}

    {{[module].starter}}
)
"
    End Function
End Module
