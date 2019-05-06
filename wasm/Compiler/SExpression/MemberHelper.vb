#Region "Microsoft.VisualBasic::c33785a08699060ba08e89ddbb577bdc, Compiler\SExpression\MemberHelper.vb"

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

    '     Module MemberHelper
    ' 
    '         Function: exportGroup, funcGroup, starter
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Wasm.Symbols

Namespace Compiler.SExpression

    Module MemberHelper

        <Extension>
        Friend Iterator Function exportGroup(exports As ExportSymbolExpression()) As IEnumerable(Of String)
            Dim moduleGroup = exports.GroupBy(Function(api) api.module).ToArray

            For Each [module] In moduleGroup
                Yield $";; export from VB.NET module: [{[module].Key}]"
                Yield ""

                For Each func As ExportSymbolExpression In [module]
                    Yield func.ToSExpression
                Next

                Yield ""
                Yield ""
            Next
        End Function

        <Extension>
        Friend Iterator Function funcGroup(internal As FuncSymbol()) As IEnumerable(Of String)
            Dim moduleGroups = internal.GroupBy(Function(f) f.module).ToArray

            For Each [module] In moduleGroups
                Yield $";; functions in [{[module].Key}]"
                Yield ""

                For Each func As FuncSymbol In [module]
                    Yield func.ToSExpression
                Next

                Yield ""
                Yield ""
            Next
        End Function
    End Module
End Namespace
