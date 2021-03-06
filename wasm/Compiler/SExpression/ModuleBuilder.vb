﻿#Region "Microsoft.VisualBasic::56737909193ecde4a12b53a4dd1759fa, Compiler\SExpression\ModuleBuilder.vb"

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

'     Class ModuleBuilder
' 
'         Properties: [imports], globals, internal, objectMetaData, predefinedGlobals
'                     stringData
' 
'         Constructor: (+1 Overloads) Sub New
'         Function: ToSExpression
' 
' 
' /********************************************************************************/

#End Region

Imports System.IO
Imports Microsoft.VisualBasic.ApplicationServices.Development
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Text
Imports Wasm.Symbols
Imports Wasm.Symbols.MemoryObject

Namespace Compiler.SExpression

    Public Class ModuleBuilder

        ReadOnly [module] As ModuleSymbol

        Public ReadOnly Property [imports] As String()
            Get
                Return [module].[Imports] _
                    .SafeQuery _
                    .Select(Function(i) i.ToSExpression) _
                    .ToArray
            End Get
        End Property

        Public ReadOnly Property globals As String()
            Get
                Return [module].Globals _
                    .SafeQuery _
                    .Where(Function(g) Not g.fullName Like [module].PredefinedConst) _
                    .Select(Function(g) g.ToSExpression) _
                    .ToArray
            End Get
        End Property

        Public ReadOnly Property predefinedGlobals As String()
            Get
                Return [module].Globals _
                    .SafeQuery _
                    .Where(Function(g) g.fullName Like [module].PredefinedConst) _
                    .Select(Function(g) g.ToSExpression) _
                    .ToArray
            End Get
        End Property

        Public ReadOnly Property internal As String()
            Get
                Return [module].InternalFunctions _
                    .funcGroup _
                    .JoinBy(vbCrLf) _
                    .LineTokens _
                    .Select(Function(line) "    " & line) _
                    .ToArray
            End Get
        End Property

        Public ReadOnly Property stringData As String()
            Get
                Return [module].memory.StringData
            End Get
        End Property

        Public ReadOnly Property objectMetaData As String()
            Get
                Return [module].memory.ObjectMetaData
            End Get
        End Property

        Sub New([module] As ModuleSymbol)
            Me.module = [module]
        End Sub

        Public Function ToSExpression() As String
            Dim wasmSummary As AssemblyInfo = GetType(ModuleSymbol).GetAssemblyDetails
            Dim buildTime$ = File.GetLastWriteTime(GetType(ModuleSymbol).Assembly.Location)
            Dim stringsData$ = stringData.JoinBy(ASCII.LF)
            Dim objectMeta$ = objectMetaData.JoinBy(ASCII.LF)
            Dim objectManager As DeclareGlobal = [module].memory.InitializeObjectManager

            Return $"(module ;; Module {[module].LabelName}

    ;; Auto-Generated VisualBasic.NET WebAssembly Code
    ;;
    ;; WASM for VisualBasic.NET
    ;; 
    ;; version: {wasmSummary.AssemblyVersion}
    ;; build: {buildTime}
    ;; 
    ;; Want to know how it works? please visit https://vanillavb.app/#compiler_design_notes

    ;; imports must occur before all non-import definitions

    {[imports].JoinBy(ASCII.LF)}
    
    ;; Only allows one memory block in each module
    (memory (import ""env"" ""bytechunks"") 1)

    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    {objectManager}

    ;; memory allocate in javascript runtime
    {IMemoryObject.Allocate.ToSExpression}
    {IMemoryObject.GetMemorySize.ToSExpression}

    ;; Memory data for string constant
    {stringsData}
    
    ;; Memory data for user defined class object its meta data
    ;; all of these string is base64 encoded json object
    {objectMeta}

    ;; Pre-defined constant values
    {predefinedGlobals.JoinBy(ASCII.LF)}

    ;; Global variables in this module
    {globals.JoinBy(ASCII.LF)}

    ;; Export methods of this module
    {New ExportSymbolExpression(IMemoryObject.GetMemorySize).ToSExpression}

    {[module].Exports.exportGroup.JoinBy(ASCII.LF & "    ")} 

{internal.JoinBy(ASCII.LF)}

{[module].starter}
)"
        End Function
    End Class
End Namespace
