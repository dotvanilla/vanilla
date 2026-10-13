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

        ''' <summary>
        ''' Fields of the ``My Project/AssemblyInfo.vb`` file as a plain string dictionary.
        ''' </summary>
        ''' <remarks>
        ''' 这里的实现必须要绕开 ``DataContractJsonSerializer``：``AssemblyInfo.BuiltTime`` 在单文件编译
        ''' 模式下没有被初始化，会保持为 ``DateTime.MinValue``，而 JSON 序列化该值会直接抛出
        ''' ``ArgumentOutOfRangeException``（值为空的时间在转换为 UTC 的时候会溢出）。
        ''' </remarks>
        Private Function AssemblyInfoFields(data As AssemblyInfo) As Dictionary(Of String, String)
            If data Is Nothing Then
                data = New AssemblyInfo
            End If

            Return New Dictionary(Of String, String) From {
                {"AssemblyTitle", If(data.AssemblyTitle, "")},
                {"AssemblyDescription", If(data.AssemblyDescription, "")},
                {"AssemblyCompany", If(data.AssemblyCompany, "")},
                {"AssemblyProduct", If(data.AssemblyProduct, "")},
                {"AssemblyCopyright", If(data.AssemblyCopyright, "")},
                {"AssemblyTrademark", If(data.AssemblyTrademark, "")},
                {"AssemblyInformationalVersion", If(data.AssemblyInformationalVersion, "")},
                {"TargetFramework", If(data.TargetFramework, "")},
                {"ComVisible", data.ComVisible.ToString},
                {"Name", If(data.Name, "")},
                {"Guid", If(data.Guid, "")},
                {"AssemblyVersion", If(data.AssemblyVersion, "")},
                {"AssemblyFileVersion", If(data.AssemblyFileVersion, "")},
                {"AssemblyFullName", If(data.AssemblyFullName, "")},
                {"BuiltTime", AssemblyBuiltTimeValue(data)}
            }
        End Function

        Private Function AssemblyBuiltTimeValue(data As AssemblyInfo) As String
            If data.BuiltTime <= Date.MinValue OrElse data.BuiltTime >= Date.MaxValue Then
                Return ""
            Else
                Return data.BuiltTime.ToString
            End If
        End Function

        <Extension>
        Private Function EncodeAssemblyInfo(project As Workspace) As StringLiteral
            Dim obj As Dictionary(Of String, String) = AssemblyInfoFields(project.AssemblyInfo)
            Dim scan0 As Integer = project.Memory.AddString(obj.ToBEncodeString)
            Dim memory As StringLiteral = project.Memory(scan0)

            Return memory
        End Function

        ''' <summary>
        ''' 生成 ``AssemblyInfo.&lt;Field&gt;`` 形式的导出函数，供 javascript 运行时读取程序集信息。
        ''' </summary>
        <Extension>
        Private Iterator Function AssemblyInfoExports(project As Workspace) As IEnumerable(Of String)
            Dim fields As Dictionary(Of String, String) = AssemblyInfoFields(project.AssemblyInfo)
            Dim order As String() = {
                "AssemblyTitle", "AssemblyDescription", "AssemblyCompany", "AssemblyProduct",
                "AssemblyCopyright", "AssemblyTrademark", "AssemblyInformationalVersion",
                "TargetFramework", "ComVisible", "Name", "Guid", "AssemblyVersion",
                "AssemblyFileVersion", "AssemblyFullName", "BuiltTime"
            }

            For Each name As String In order
                Dim text$ = fields(name)
                Dim scan0 As Integer = project.Memory.AddString(text)
                Dim memory As StringLiteral = project.Memory(scan0)
                Dim ptr As Integer = DirectCast(memory.MemoryPtr, StaticPtr).Scan0

                Yield memory.ToSExpression
                Yield $"(func $AssemblyInfo.{name} (result i32)
        (return (i32.const {ptr}))
    )"
                Yield $"(export ""AssemblyInfo.{name}"" (func $AssemblyInfo.{name}))"
            Next
        End Function

        Private Function WriteJavascriptImports(project As Workspace) As String
            Dim [imports] As New List(Of String)

            For Each library In project.Imports.Values.GroupBy(Function(i) i.ModuleName)
                [imports] += $";; JAVASCRIPT [{library.Key}]"

                For Each api As ImportsFunction In library
                    [imports] += api.ToSExpression(Nothing, "")
                Next
            Next

            Return $"
    ;; Javascript function imports into current WebAssembly Module
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
            Dim assemblyInfo As StringLiteral = project.EncodeAssemblyInfo
            Dim assemblyExports As String() = project.AssemblyInfoExports.ToArray
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
    {assemblyInfo.ToSExpression}

    (func $AssemblyInfo (result i32)
        (return (i32.const {DirectCast(assemblyInfo.MemoryPtr, StaticPtr).Scan0}))    
    )

    (export ""AssemblyInfo"" (func $AssemblyInfo)) 

    ;; AssemblyInfo fields (consumed by the javascript runtime)
    {assemblyExports.JoinBy(Indent)}

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