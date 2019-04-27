#Region "Microsoft.VisualBasic::8cc60b761dd07f63afd6c927c3ccb922, Extensions.vb"

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

    ' Module Extensions
    ' 
    '     Function: assmInfoModule, CreateModule, (+2 Overloads) CreateModuleFromProject, getModules, getString
    '               SolveStream
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio
Imports Microsoft.VisualBasic.ComponentModel
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Symbols
Imports Wasm.Symbols.Parser
Imports Vbproj = Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.Project

Public Module Extensions

    ''' <summary>
    ''' Create a WebAssembly module from vb project. 
    ''' </summary>
    ''' <param name="vbproj"></param>
    ''' <returns></returns>
    ''' 
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function CreateModuleFromProject(vbproj As String) As ModuleSymbol
        Return vbproj.LoadXml(Of Vbproj).CreateModuleFromProject
    End Function

    <Extension>
    Public Function CreateModuleFromProject(vbproj As Vbproj) As ModuleSymbol
        Dim sourcefiles = vbproj _
            .EnumerateSourceFiles(skipAssmInfo:=True) _
            .ToArray
        Dim assemblyInfo As AssemblyInfo = vbproj.AssemblyInfo
        Dim dir As String = DirectCast(vbproj, IFileReference) _
            .FilePath _
            .ParentPath
        Dim symbols As SymbolTable = Nothing
        Dim vbcodes As ModuleBlockSyntax()

        With sourcefiles _
            .Select(Function(file) $"{dir}/{file}") _
            .getModules _
            .ToArray

            vbcodes = .OfType(Of ModuleBlockSyntax()) _
                      .IteratesALL _
                      .ToArray

            ' 在刚开始的时候应该将函数的申明全部进行解析
            ' 然后再解析函数体的时候才不会出现没有找到符号的问题
            For Each modulePart As ModuleBlockSyntax In vbcodes
                symbols = modulePart.ParseDeclares(symbols, {})
            Next

            For Each [const] As EnumSymbol In .OfType(Of EnumSymbol()).IteratesALL
                Call symbols.AddEnumType([const])
            Next
        End With

        Dim project = vbcodes.CreateModule(symbols, vbproj.RootNamespace)
        Dim info = assemblyInfo.assmInfoModule(project.Memory)

        Return project.Join(info)
    End Function

    <Extension>
    Private Function assmInfoModule(AssemblyInfo As AssemblyInfo, memory As Memory) As ModuleSymbol
        Dim schema As PropertyInfo() = DataFramework _
            .Schema(Of AssemblyInfo)(PropertyAccess.Readable, True, True) _
            .Values _
            .Where(Function(p) p.PropertyType Is GetType(String)) _
            .ToArray
        Dim symbols As New SymbolTable With {.memory = memory}
        Dim getStrings As FuncSymbol() = schema _
            .Select(Function(val)
                        Dim name = val.Name
                        Dim string$ = val.GetValue(AssemblyInfo)

                        ' readonly function() as string
                        Return symbols.getString(name, [string])
                    End Function) _
            .ToArray

        Return New ModuleSymbol With {
            .Memory = memory,
            .InternalFunctions = getStrings,
            .Exports = getStrings _
                .Select(Function(func)
                            Return New ExportSymbolExpression With {
                                .[Module] = func.Module,
                                .Name = func.Name,
                                .target = func.Name,
                                .type = "func"
                            }
                        End Function) _
                .ToArray
        }
    End Function

    <Extension>
    Private Function getString(memory As SymbolTable, name$, string$) As FuncSymbol
        Return New FuncSymbol() With {
            .Name = name,
            .parameters = {},
            .[Module] = NameOf(AssemblyInfo),
            .result = Types.string,
            .Body = {
                New ReturnValue With {
                    .Internal = memory.StringConstant([string])
                }
            }
        }
    End Function

    <Extension>
    Private Iterator Function getModules(files As IEnumerable(Of String)) As IEnumerable(Of [Variant](Of EnumSymbol(), ModuleBlockSyntax()))
        Dim vbcode As CompilationUnitSyntax
        Dim modules As ModuleBlockSyntax()
        Dim enums As EnumSymbol()

        For Each file As String In files
            vbcode = VisualBasicSyntaxTree.ParseText(file.SolveStream).GetRoot
            modules = vbcode.Members.OfType(Of ModuleBlockSyntax).ToArray
            enums = vbcode.ParseEnums

            If modules.Length > 0 Then
                Yield modules
            End If
            If enums.Length > 0 Then
                Yield enums
            End If
        Next
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Function CreateModule(vbcode As [Variant](Of FileInfo, String)) As ModuleSymbol
        Return ModuleParser.CreateModule(vbcode)
    End Function

    <Extension>
    Friend Function SolveStream(vbcode As [Variant](Of FileInfo, String)) As String
        If vbcode Like GetType(String) Then
            Return CType(vbcode, String).SolveStream
        Else
            Return CType(vbcode, FileInfo).FullName.SolveStream
        End If
    End Function
End Module
