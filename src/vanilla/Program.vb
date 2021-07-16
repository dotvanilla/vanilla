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

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio
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
            Dim vbproj As Project = file.LoadXml(Of Project)

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
    Private Sub CreateWasm(moduleSymbol As Workspace, debug As Boolean, out$)
        Call moduleSymbol.ToSExpression.SaveTo(out.ChangeSuffix("wast"))
        Call moduleSymbol.HexDump(verbose:=True).SaveTo(out.ChangeSuffix("dmp"))

        Dim config As New Wat2wasm With {.output = out}

        If debug Then
            config.debugNames = True
            config.debugParser = True
            config.verbose = True
        End If

        Call Wasm.Compiler _
            .Compile(moduleSymbol, config) _
            .SaveTo(out.ChangeSuffix("log"))
    End Sub

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    <Extension>
    Private Function GetOutputWasm(vbproj As Project, Optional profile$ = "Release|AnyCPU") As String
        Return $"{vbproj.GetOutputDirectory(profile)}/{vbproj.GetOutputName}.wasm"
    End Function

    Private Function AutoSearchRoutine() As Integer
        Dim vbprojs = App.CurrentDirectory _
            .EnumerateFiles("*.vbproj") _
            .Select(AddressOf LoadXml(Of Project)) _
            .ToArray
        Dim out$
        Dim moduleSymbol As ModuleSymbol

        If vbprojs.IsNullOrEmpty Then
            ' Compile each file as single WebAssembly module

            Throw New NotImplementedException
        Else
            For Each proj As Project In vbprojs
                out = proj.GetOutputWasm()
                moduleSymbol = Wasm.CreateModuleFromProject(proj)

                Call moduleSymbol.CreateWasm(False, out)
            Next
        End If

        Return 0
    End Function
End Module
