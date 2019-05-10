#Region "Microsoft.VisualBasic::8dc91740fa51e8aa460bb8033e3ee020, Symbols\ModuleSymbol.vb"

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

'     Class ModuleSymbol
' 
'         Properties: [Imports], Exports, Globals, InternalFunctions, LabelName
'                     Memory, Start
' 
'         Constructor: (+1 Overloads) Sub New
'         Function: CreateModule, GenericEnumerator, GetEnumerator, Join, ToSExpression
'                   TypeInfer
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Compiler
Imports Wasm.Compiler.SExpression
Imports Wasm.SyntaxAnalysis
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' A WebAssembly module
    ''' </summary>
    Public Class ModuleSymbol : Inherits Expression
        Implements Enumeration(Of Expression)

        Public Property InternalFunctions As FuncSymbol()
        Public Property Exports As ExportSymbolExpression()
        Public Property [Imports] As ImportSymbol()
        Public Property Globals As DeclareGlobal()
        Public Property memory As Memory

        ''' <summary>
        ''' 模块的``Sub New``构造函数
        ''' </summary>
        ''' <returns></returns>
        Public Property start As Start
        Public Property globalStarter As FuncSymbol

        ''' <summary>
        ''' The module name label
        ''' </summary>
        ''' <returns></returns>
        Public Property LabelName As String

        Sub New()
        End Sub

        Friend Function Join(part As ModuleSymbol) As ModuleSymbol
            InternalFunctions = InternalFunctions.Join(part.InternalFunctions).ToArray
            Exports = Exports.Join(part.Exports).ToArray

            If Not part.Imports.IsNullOrEmpty Then
                [Imports] = part.Imports
            End If
            If Not part.Globals.IsNullOrEmpty Then
                Globals = part.Globals
            End If
            If Not part.memory Is Nothing Then
                memory = part.memory
            End If
            If start Is Nothing Then
                start = part.start
            Else
                start.constructors.AddRange(part.start.constructors)
            End If

            Return Me
        End Function

        Public Iterator Function GenericEnumerator() As IEnumerator(Of Expression) Implements Enumeration(Of Expression).GenericEnumerator
            For Each func As FuncSymbol In InternalFunctions
                Yield func
            Next
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator Implements Enumeration(Of Expression).GetEnumerator
            Yield GenericEnumerator()
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function ToSExpression() As String
            Return New ModuleBuilder(Me).ToSExpression()
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.void)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Function CreateModule(vbcode As String) As ModuleSymbol
            Return ModuleParser.CreateModule(vbcode)
        End Function
    End Class
End Namespace
