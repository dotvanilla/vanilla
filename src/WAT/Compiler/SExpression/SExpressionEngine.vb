Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Public Module SExpressionEngine

        <Extension>
        Public Function ToSExpression(project As Workspace) As String
            Dim buildTime As String = Now.ToString
            Dim wasmSummary As AssemblyInfo = project.AssemblyInfo
            Dim exportGroup As FunctionDeclare() = project.GetPublicApi.ToArray
            Dim exportApiSText As String = exportGroup.ToSExpression(project)

            Return $"(module ;; Microsoft VisualBasic Project {project.DefaultNamespace}

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: {wasmSummary.AssemblyVersion}
    ;; build: {buildTime}
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    {{[imports].JoinBy(ASCII.LF)}}
    
    ;; Only allows one memory block in each module
    (memory (import ""env"" ""bytechunks"") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    {{objectManager}}

    ;; memory allocate in javascript runtime
    {{IMemoryObject.Allocate.ToSExpression}}
    {{IMemoryObject.GetMemorySize.ToSExpression}}

    ;; Memory data for string constant
    {{stringsData}}
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    {{objectMeta}}

    ;; Pre-defined constant values
    {{predefinedGlobals.JoinBy(ASCII.LF)}}

    ;; Global variables in this module
    {{Globals.JoinBy(ASCII.LF)}}

    ;; Export methods of this module
    {{New ExportSymbolExpression(IMemoryObject.GetMemorySize).ToSExpression}}

    {exportApiSText} 

    {{internal.JoinBy(ASCII.LF)}}

    {{[module].starter}}
)"
        End Function
    End Module
End Namespace