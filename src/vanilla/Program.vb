#Region "Microsoft.VisualBasic::57c49ca1ba1146c6bba97b0d3d22a680, Program.vb"

' Author:
' 
'       xieguigang (I@xieguigang.me)
'       asuka (evia@lilithaf.me)
'       wasm project (developer@vanillavb.app)
' 
' Copyright (c) 2019 developer@vanillavb.app, VanillaBasic(https://vanillavb.app)
' 
' 
' MIT License
' 
' 
' Permission is hereby granted, free of charge, to any person obtaining a copy
' of this software and associated documentation files (the "Software"), to deal
' in the Software without restriction, including without limitation the rights
' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
' copies of the Software, and to permit persons to whom the Software is
' furnished to do so, subject to the following conditions:
' 
' The above copyright notice and this permission notice shall be included in all
' copies or substantial portions of the Software.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
' SOFTWARE.



' /********************************************************************************/

' Summaries:

' Module Program
' 
'     Function: AutoSearchRoutine, CompileTargetFileRoutine, GetOutputWasm, Main
' 
'     Sub: CreateWasm
' 
' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.vbproj
Imports Microsoft.VisualBasic.CommandLine
Imports VanillaBasic.Roslyn
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Compiler

''' <summary>
''' 在进行编译的时候会遵循下面的搜索规则：
''' 
''' 1. 会首先在当前的文件夹内搜索vbproj文件，不会搜索子文件夹
''' 2. 如果搜索成功，则会将vbproj文件进行编译
''' 3. 如果搜索不成功，则会将当前文件夹内的所有vb源代码文件进行编译，包括子文件夹
''' 
''' 如果在命令行之中明确的提供了文件路径，则只会对所传递进来的文件进行编译
''' </summary>
Module Program

    Public Function Main() As Integer
        Return GetType(Program).RunCLI(App.CommandLine, AddressOf CompileTargetFileRoutine, AddressOf AutoSearchRoutine)
    End Function

    ''' <summary>
    ''' ``vanilla &lt;file> [...args]``
    ''' </summary>
    ''' <param name="file"></param>
    ''' <param name="args"></param>
    ''' <returns></returns>
    Private Function CompileTargetFileRoutine(file As String, args As CommandLine) As Integer
        Dim project As Scanner
        Dim out$
        Dim debug As Boolean = args("/debug")

        If file.ExtensionSuffix.TextEquals("vb") Then
            project = New Scanner
            project.AddModules(file)
            out = args("/out") Or file.ChangeSuffix("wasm")
        Else
            Dim profile$ = args("/profile") Or "Release|AnyCPU"
            Dim vbproj As VBProject = Nothing

            Try
                vbproj = VBProject.Load(file, parseDoc:=False)
            Catch ex As Exception
                ' GCModeller 的 VBProject 类型无法被 XmlSerializer 反序列化
                ' （其 VBDocument.Types 是 Dictionary，XmlSerializer 不支持这种成员），
                ' 在这里回退到一个轻量级的 vbproj XML 解析流程。
                Call System.Console.Error.WriteLine($"Load vbproj '{file}' failure, fallback to lightweight parser: {ex.Message}")

                Return CompileVbprojFallback(file, args)
            End Try

            project = New Scanner(vbproj)
            vbproj.EnumerateSourceFiles(fullName:=True).DoEach(AddressOf project.AddModules)
            out = args("/out") Or vbproj.GetOutputWasm(profile)

            If profile.Split("|"c).First = "Debug" Then
                debug = True
            End If
        End If

        Call project.Workspace.CreateWasm(debug, out)

        Return 0
    End Function

    <Extension>
    Private Sub CreateWasm(VisualBasic As Workspace, debug As Boolean, output$)
        Dim config As New Wat2WasmCompiler With {.OutputFile = output}

        If debug Then
            config.DebugNames = True
            config.DebugParser = True
            config.VerboseCount = 1
        End If

        Call VanillaBuild.WastDump(VisualBasic, output.ChangeSuffix("*.wast"))
        Call VanillaBuild.HexDump(VisualBasic, output.ChangeSuffix("*.dmp"), verbose:=True)
        Call VanillaBuild.Compile(VisualBasic, config)
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Private Function GetOutputWasm(vbproj As VBProject, Optional profile$ = "Release|AnyCPU") As String
        Return $"{vbproj.GetOutputDirectory(profile)}/{vbproj.GetOutputName}.wasm"
    End Function

    Private Function AutoSearchRoutine() As Integer
        Dim vbprojs = App.CurrentDirectory _
            .EnumerateFiles("*.vbproj") _
            .Select(AddressOf LoadXml(Of VBProject)) _
            .ToArray
        Dim out$
        Dim moduleSymbol As Scanner

        If vbprojs.IsNullOrEmpty Then
            ' Compile each file as single WebAssembly module

            Throw New NotImplementedException
        Else
            For Each proj As VBProject In vbprojs
                out = proj.GetOutputWasm()
                moduleSymbol = New Scanner(proj)

                Call moduleSymbol.Workspace.CreateWasm(False, out)
            Next
        End If

        Return 0
    End Function

    ''' <summary>
    ''' 轻量级的 vbproj 编译流程：直接在 XML 之中读取根命名空间以及编译文件列表，
    ''' 从而绕开 GCModeller 的 ``VBProject`` 的 ``XmlSerializer`` 反序列化缺陷。
    ''' </summary>
    Private Function CompileVbprojFallback(file As String, args As CommandLine) As Integer
        Dim doc As XDocument = XDocument.Load(file)
        Dim ns As XNamespace = doc.Root.Name.Namespace
        Dim rootNamespace$ = doc.Descendants(ns + "RootNamespace") _
            .Select(Function(x) x.Value) _
            .FirstOrDefault()
        Dim baseDir$ = file.ParentPath
        Dim sourceFiles As New List(Of String)

        For Each node As XElement In doc.Descendants(ns + "Compile")
            Dim include As String = CStr(node.Attribute("Include"))

            If String.IsNullOrEmpty(include) Then
                Continue For
            End If

            For Each path$ In ExpandVbInclude(baseDir, include)
                sourceFiles.Add(path)
            Next
        Next

        If sourceFiles.Count = 0 Then
            ' SDK 风格的项目文件（例如 test/base64/base64.vbproj）没有显式的 <Compile> 项，
            ' 此时默认递归 glob 项目目录下的所有 .vb 源文件。
            For Each path As String In Directory.GetFiles(baseDir, "*.vb", SearchOption.AllDirectories)
                If Not IsBuildOutputPath(path) Then
                    sourceFiles.Add(path)
                End If
            Next
        End If

        Dim project As New Scanner(rootNamespace, GetFallbackAssemblyInfo(file))

        For Each path$ In sourceFiles.Distinct
            Call project.AddModules(path)
        Next

        Dim out$ = args("/out") Or $"{baseDir}/{System.IO.Path.GetFileNameWithoutExtension(file)}.wasm"

        Call project.Workspace.CreateWasm(args("/debug"), out)

        Return 0
    End Function

    Private Function GetFallbackAssemblyInfo(vbproj As String) As AssemblyInfo
        Try
            Return vbproj.GetAssemblyInfo
        Catch ex As Exception
            Return New AssemblyInfo
        End Try
    End Function

    ''' <summary>
    ''' 展开 ``&lt;Compile Include="..." /&gt;`` 之中可能包含通配符的路径。
    ''' </summary>
    Private Iterator Function ExpandVbInclude(baseDir As String, include As String) As IEnumerable(Of String)
        Dim pattern$ = include.Replace("\"c, "/"c)
        Dim wildcard As Integer = pattern.IndexOfAny({"*"c, "?"c})

        If wildcard < 0 Then
            Dim full$ = ResolveIncludePath(baseDir, pattern)

            If File.Exists(full) Then
                Yield full
            End If
        Else
            ' 形如 **\*.vb 或者 *.vb 的通配符
            Dim slash As Integer = pattern.LastIndexOf("/"c)
            Dim dir$ = If(slash < 0, "", pattern.Substring(0, slash)) _
                .Replace("**", "") _
                .Trim("/"c)
            Dim file$ = pattern.Substring(slash + 1)
            Dim root$ = ResolveIncludePath(baseDir, dir)

            If Directory.Exists(root) Then
                For Each path$ In Directory.GetFiles(root, file$, SearchOption.AllDirectories)
                    Yield path
                Next
            End If
        End If
    End Function

    ''' <summary>
    ''' 判断路径是否位于编译输出目录（bin / obj）之中
    ''' </summary>
    Private Function IsBuildOutputPath(path As String) As Boolean
        Dim normalized As String = path.Replace("/"c, "\"c)

        Return normalized.IndexOf("\bin\", StringComparison.OrdinalIgnoreCase) >= 0 OrElse
               normalized.IndexOf("\obj\", StringComparison.OrdinalIgnoreCase) >= 0
    End Function

    Private Function ResolveIncludePath(baseDir As String, path As String) As String
        If String.IsNullOrEmpty(path) Then
            Return baseDir
        ElseIf System.IO.Path.IsPathRooted(path) Then
            Return path
        Else
            Return $"{baseDir}/{path}"
        End If
    End Function
End Module
