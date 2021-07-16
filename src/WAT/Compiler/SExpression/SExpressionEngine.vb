Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization.Bencoding
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.Memory
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Public Module SExpressionEngine

        <Extension>
        Private Function EncodeAssemblyInfo(project As Workspace) As String
            Dim data As AssemblyInfo = project.AssemblyInfo
            Dim obj As Dictionary(Of String, String) = data.GetJson.LoadJSON(Of Dictionary(Of String, String))
            Dim scan0 As Integer = project.Memory.AddString(obj.ToBEncodeString)
            Dim memory As StringLiteral = project.Memory(scan0)

            Return memory.ToSExpression
        End Function

        Private Function WriteJavascriptImports(project As Workspace) As String
            Dim [imports] As New List(Of String)

            For Each library In project.Imports.Values.GroupBy(Function(i) i.ModuleName)
                [imports] += $";; JAVASCRIPT [{library.Key}]"

                For Each api As ImportsFunction In library
                    [imports] += api.ToSExpression(Nothing, "")
                Next
            Next

            Return $";; Javascript function imports into current WebAssembly Module
;;
;; ----- NOTE: imports must occur before all non-import definitions ------

    {[imports].JoinBy(ASCII.LF)}
    
;; ----- END OF JAVASCRIPT IMPORTS -----"
        End Function

        <Extension>
        Public Function ToSExpression(project As Workspace) As String
            Dim exportGroup As ExportSymbol() = project.GetPublicApi.ToArray
            Dim internal As String() = project.Methods.Values.ToSExpression(project).ToArray
            Dim stringsData As String() = StringWriter.StringExpressions(project.Memory)
            Dim assemblyInfo As String = project.EncodeAssemblyInfo
            Dim imports$ = WriteJavascriptImports(project)

            Return project.WriteProjectModule($"{[imports]}    

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
    
    ;; AssemblyInfo.vb
    {assemblyInfo}

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