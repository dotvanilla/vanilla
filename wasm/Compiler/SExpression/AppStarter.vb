﻿#Region "Microsoft.VisualBasic::88f9ded7e9efd5d3bc58e7497b8d382f, Compiler\SExpression\AppStarter.vb"

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

    '     Module AppStarter
    ' 
    '         Function: starter, writeStarter
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Wasm.Symbols

Namespace Compiler.SExpression

    Module AppStarter

        <Extension>
        Private Function writeStarter(funcs As IEnumerable(Of String), calls As String, globals As FuncSymbol) As String
            Return $"
    ;; Application Initialize
    ;; 
    ;; Sub New
    (func $Application_SubNew
        ;; call of the global variable initialize
        {globals.Call.ToSExpression}

        {calls}
    )

    ;; Initializer for global variables if it is not a primitive abstract type
    {globals.ToSExpression}

    {funcs.JoinBy(vbCrLf & vbCrLf)}

    (start $Application_SubNew)"
        End Function

        <Extension>
        Public Function starter([module] As ModuleSymbol) As String
            If [module].Start Is Nothing Then
                Return New String() {}.writeStarter("", [module].globalStarter)
            Else
                Return [module].start _
                    .constructors _
                    .Select(Function(f) f.ToSExpression) _
                    .writeStarter([module].start.ToSExpression, [module].globalStarter)
            End If
        End Function
    End Module
End Namespace
