Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Serialization.Bencoding
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.CodeAnalysis.Memory
Imports VanillaBasic.WebAssembly.Syntax
Imports TypeSchema = VanillaBasic.WebAssembly.CodeAnalysis.TypeSchema

Namespace Compiler

    Public Module SExpressionEngine

        Public Const Indent As String = vbLf & "    "

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

    {[imports].JoinBy(Indent)}
    
;; ----- END OF JAVASCRIPT IMPORTS -----"
        End Function

        <Extension>
        Public Function ToSExpression(project As Workspace) As String
            Dim exportGroup As ExportSymbol() = project.GetPublicApi.ToArray
            Dim internal As String() = project.Methods.Values.ToSExpression(project).ToArray
            Dim stringsData As String() = StringWriter.StringExpressions(project.Memory)
            Dim assemblyInfo As String = project.EncodeAssemblyInfo
            Dim imports$ = WriteJavascriptImports(project)
            Dim typeMetas As String() = project.ObjectMetaData.ToArray

            ' 必须要放在最后一步添加
            ' 因为前面还有一些动态添加的字符串
            Dim heapMgr As String = WATWriter.WriteWAT(project)
            Dim globals As String() = project.GlobalSymbols.Values _
                .Select(Function(a)
                            Return a.ToSExpression(Nothing, Nothing)
                        End Function) _
                .ToArray

            Return project.WriteProjectModule($"{[imports]}    

    ;; Only allows one memory block in each module
    (memory (import ""env"" ""bytechunks"") 1)

    {WATWriter.GetHeapSize0(project.Memory)}

    {heapMgr}

    ;; Memory data for string constant
    {stringsData.JoinBy(Indent)}
    
    ;; AssemblyInfo.vb
    {assemblyInfo}

    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    {typeMetas.JoinBy(Indent)}

    ;; Math constant values in .NET Framework
    {MathConstant.GetVBMathConstants.JoinBy(Indent)}

    ;; Global variables in this module
    {globals.JoinBy(Indent)}

    ;; Export methods of this module
    {exportGroup.Select(Function(api) api.ToSExpression).JoinBy(Indent)} 

    {internal.JoinBy(Indent)}
")
        End Function

        <Extension>
        Private Iterator Function ObjectMetaData(project As Workspace) As IEnumerable(Of String)
            Dim i As Integer = 0

            For Each type As TypeSchema In project.Types.Values
                If type.IsStandardModule Then
                    Continue For
                Else
                    i += 1
                End If

                Dim meta As String = type.GetBEncodeMetaDataString
                Dim scan0 As Integer = project.Memory.AddString(meta)
                Dim memory As StringLiteral = project.Memory(scan0)

                Yield memory.ToSExpression
            Next

            If i = 0 Then
                Yield ""
                Yield ";; ------------------------------------"
                Yield ";; No user type meta data at here...."
                Yield ";; ------------------------------------"
            End If
        End Function
    End Module
End Namespace