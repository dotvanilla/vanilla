Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Public Module ModuleWriter

        <Extension>
        Private Function writeStarter(project As Workspace) As String
            Dim calls As String() = project.Types.Values _
                .Select(Function(type)
                            Return New FunctionInvoke(type.Initializer).ToSExpression(Nothing, Nothing)
                        End Function) _
                .ToArray
            Dim subNews As String() = project.Types.Values _
                .Select(Function(type) type.Initializer) _
                .ToSExpression(project) _
                .ToArray

            Return $"
    ;; #region ""VisualBasic Application Initialize Of Each Modules""
        {subNews.JoinBy(ASCII.LF)}
    ;; #endregion

    ;; --------------------------------------------------
    ;; Microsoft.VisualBasic.My.Application_Startup Event
    (func $MyApplication_Startup
        {calls.JoinBy(ASCII.LF)}
    )
    ;; --------------------------------------------------

    (start $MyApplication_Startup)"
        End Function

        <Extension>
        Public Function WriteProjectModule(project As Workspace, content As String) As String
            Dim buildTime As String = Now.ToString
            Dim wasmSummary As AssemblyInfo = project.AssemblyInfo
            Dim app_start As String = project.writeStarter

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

    {app_start}
)
"
        End Function
    End Module
End Namespace