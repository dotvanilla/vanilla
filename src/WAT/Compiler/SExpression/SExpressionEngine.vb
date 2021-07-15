Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.Memory
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Public Module SExpressionEngine

        <Extension>
        Public Function ToSExpression(project As Workspace) As String
            Dim exportGroup As ExportSymbol() = project.GetPublicApi.ToArray
            Dim internal As String() = project.Methods.Values.ToSExpression(project).ToArray
            Dim stringsData As String() = project.Memory _
                .Where(Function(m) TypeOf m Is StringLiteral) _
                .Select(Function(str) DirectCast(str, StringLiteral).ToSExpression) _
                .ToArray

            Return project.WriteProjectModule($";; imports must occur before all non-import definitions

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
    {stringsData.JoinBy(ASCII.LF)}
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    {{objectMeta}}

    ;; Math constant values in .NET Framework
    {MathConstant.GetVBMathConstants.JoinBy(ASCII.LF)}

    ;; Global variables in this module
    {{Globals.JoinBy(ASCII.LF)}}

    ;; Export methods of this module
    {{New ExportSymbolExpression(IMemoryObject.GetMemorySize).ToSExpression}}

    {exportGroup.Select(Function(api) api.ToSExpression).JoinBy(ASCII.LF)} 

    {internal.JoinBy(ASCII.LF)}
")
        End Function
    End Module
End Namespace