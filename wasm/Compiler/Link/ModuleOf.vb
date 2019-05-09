#Region "Microsoft.VisualBasic::8c4eb5117b9f510ce605cc97ccb0bc40, Compiler\Link\ModuleOf.vb"

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

    '     Class ModuleOf
    ' 
    '         Properties: GetUniqueSymbol, IsUnique, ModuleLabels, SymbolName
    ' 
    '         Constructor: (+2 Overloads) Sub New
    ' 
    '         Function: FindSymbol, GetEnumerator, IEnumerable_GetEnumerator, ToString
    ' 
    '         Sub: Add
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Wasm.Symbols

Namespace Compiler

    Public Class ModuleOf : Implements IReadOnlyId
        Implements IEnumerable(Of IDeclaredObject)

        ''' <summary>
        ''' [moudle_label => object]
        ''' </summary>
        ReadOnly modules As New Dictionary(Of String, IDeclaredObject)

        Public ReadOnly Property SymbolName As String Implements IReadOnlyId.Identity

        Public ReadOnly Property IsUnique As Boolean
            Get
                Return modules.Count = 1
            End Get
        End Property

        ''' <summary>
        ''' 如果目标不是在模块之间唯一的话，则属性返回空值
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property GetUniqueSymbol As IDeclaredObject
            Get
                If Not IsUnique Then
                    Return Nothing
                Else
                    Return modules.Values.First
                End If
            End Get
        End Property

        Public ReadOnly Property ModuleLabels As IEnumerable(Of String)
            Get
                Return modules.Keys
            End Get
        End Property

        Sub New(symbolName As String)
            Me.SymbolName = symbolName
        End Sub

        Sub New(symbol As IDeclaredObject)
            Add(symbol)
            SymbolName = symbol.Key
        End Sub

        Public Sub Add(symbol As IDeclaredObject)
            Call modules.Add(symbol.module, symbol)
        End Sub

        Public Sub Delete(moduleLabel As String)
            Call modules.Remove(moduleLabel)
        End Sub

        Public Function OfType(Of T As IDeclaredObject)() As IEnumerable(Of T)
            Return Me.Where(Function(d)
                                Return d.GetType.IsInheritsFrom(GetType(T), strict:=False)
                            End Function) _
                     .Select(Function(d) DirectCast(d, T))
        End Function

        ''' <summary>
        ''' 查找失败则返回一个空值
        ''' </summary>
        ''' <param name="moduleLabel"></param>
        ''' <returns></returns>
        Public Function FindSymbol(moduleLabel As String) As IDeclaredObject
            Return modules.TryGetValue(moduleLabel)
        End Function

        Public Overrides Function ToString() As String
            Return SymbolName
        End Function

        Public Shared Widening Operator CType(func As FuncSignature) As ModuleOf
            Return New ModuleOf(func)
        End Operator

        Public Shared Widening Operator CType([global] As DeclareGlobal) As ModuleOf
            Return New ModuleOf([global])
        End Operator

        Public Iterator Function GetEnumerator() As IEnumerator(Of IDeclaredObject) Implements IEnumerable(Of IDeclaredObject).GetEnumerator
            For Each obj As IDeclaredObject In modules.Values.ToArray
                Yield obj
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace
