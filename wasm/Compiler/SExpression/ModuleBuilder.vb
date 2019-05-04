#Region "Microsoft.VisualBasic::a1a8bea4205b372dd698e6d7d4e90d58, Compiler\SExpression\ModuleBuilder.vb"

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

    '     Module ModuleBuilder
    ' 
    '         Function: exportGroup, funcGroup, starter, ToSExpression
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Text
Imports Wasm.Symbols

Namespace Compiler.SExpression

    Module ModuleBuilder

        Public Function ToSExpression(m As ModuleSymbol) As String
            Dim import$ = ""
            Dim globals$ = ""
            Dim internal$ = m.InternalFunctions _
                .funcGroup _
                .JoinBy(vbCrLf) _
                .LineTokens _
                .Select(Function(line) "    " & line) _
                .JoinBy(ASCII.LF)

            If Not m.[Imports].IsNullOrEmpty Then
                import = m.[Imports] _
                    .SafeQuery _
                    .Select(Function(i) i.ToSExpression) _
                    .JoinBy(ASCII.LF & "    ")
            End If
            If Not m.Globals.IsNullOrEmpty Then
                globals = m.Globals _
                    .Select(Function(g) g.ToSExpression) _
                    .JoinBy(ASCII.LF & ASCII.LF)
            End If

            Dim wasmSummary As AssemblyInfo = GetType(ModuleSymbol).GetAssemblyDetails
            Dim buildTime$ = File.GetLastWriteTime(GetType(ModuleSymbol).Assembly.Location)
            Dim stringsData$ = m.Memory.StringData
            Dim objectMeta$ = m.Memory.ObjectMetaData
            Dim objectManager As DeclareGlobal = m.Memory.InitializeObjectManager

            Return $"(module ;; Module {m.LabelName}

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: {wasmSummary.AssemblyVersion}
    ;; build: {buildTime}
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    {import}
    
    ;; Only allows one memory block in each module
    (memory (import ""env"" ""bytechunks"") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    {objectManager}

    ;; Memory data for string constant
    {stringsData}
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    {objectMeta}

    ;; Global variables in this module
    {globals}

    ;; Export methods of this module
    {m.Exports.exportGroup.JoinBy(ASCII.LF & "    ")} 

{internal}

;; Application Initialize
;; 
;; Sub New
(func $Application_SubNew
{m.starter}
)

(start $Application_SubNew)

)"
        End Function
    End Module
End Namespace
