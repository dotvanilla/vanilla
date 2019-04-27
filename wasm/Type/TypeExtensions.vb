#Region "Microsoft.VisualBasic::21ded58422b72d6017751548f57befc0, Type\TypeExtensions.vb"

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

    ' Class TypeExtensions
    ' 
    '     Properties: Comparison, Convert2Wasm, NumberOrders, wasmOpName
    ' 
    '     Function: ArrayElement, Compares, IsArray, TypeCharWasm
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage

' how it works
' 
' vb source => codeDOM => wast model => wast => wasm

Public Class TypeExtensions

    ''' <summary>
    ''' 在进行类型转换的是否，会需要使用这个索引来判断类型的优先度，同时，也可以使用这个索引来判断类型是否为基础类型
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property NumberOrders As Index(Of TypeAlias) = {
        TypeAlias.i32,
        TypeAlias.f32,
        TypeAlias.i64,
        TypeAlias.f64
    }

    ''' <summary>
    ''' Webassembly之中，逻辑值是一个32位整型数
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property Convert2Wasm As New Dictionary(Of Type, String) From {
        {GetType(Boolean), Types.booleanType},
        {GetType(Integer), "i32"},
        {GetType(Long), "i64"},
        {GetType(Single), "f32"},
        {GetType(Double), "f64"},
        {GetType(String), "string"}, ' 实际上这是一个integer类型
        {GetType(Char), "string"},
        {GetType(System.Void), "void"},
        {GetType(Object), "any"}
    }

    ''' <summary>
    ''' VisualBasic.NET operator to webassembly operator name
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property wasmOpName As New Dictionary(Of String, String) From {
        {"+", "add"},
        {"-", "sub"},
        {"*", "mul"},
        {"/", "div"},
        {"^", "$pow"},
        {"=", "eq"},
        {"<>", "ne"}
    }

    Friend Shared ReadOnly unaryOp As Index(Of String) = {
        wasmOpName("+"),
        wasmOpName("-")
    }
    Friend Shared ReadOnly integerType As Index(Of String) = {"i32", "i64"}
    Friend Shared ReadOnly floatType As Index(Of String) = {"f32", "f64"}

    Public Shared ReadOnly Property Comparison As Index(Of String) = {"f32", "f64", "i32", "i64"} _
        .Select(Function(type)
                    Return {">", ">=", "<", "<=", "="}.Select(Function(op) Compares(type, op))
                End Function) _
        .IteratesALL _
        .ToArray

    ''' <summary>
    ''' 值比较函数返回的是一个整型数
    ''' </summary>
    ''' <param name="type"></param>
    ''' <param name="op"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' 
    ''' </remarks>
    Public Shared Function Compares(type$, op$) As String
        Select Case op
            Case ">"
                If type Like integerType Then
                    Return $"{type}.gt_s"
                Else
                    Return $"{type}.gt"
                End If
            Case ">="
                If type Like integerType Then
                    Return $"{type}.ge_s"
                Else
                    Return $"{type}.ge"
                End If
            Case "<"
                If type Like integerType Then
                    Return $"{type}.lt_s"
                Else
                    Return $"{type}.lt"
                End If
            Case "<="
                If type Like integerType Then
                    Return $"{type}.le_s"
                Else
                    Return $"{type}.le"
                End If
            Case "="
                If type Like integerType OrElse type Like floatType Then
                    Return $"{type}.eq"
                Else
                    Throw New NotImplementedException
                End If
            Case Else
                If type = "i32" Then
                    ' 有一些位相关的操作只能够执行在i32上面
                    Select Case op
                        Case "<<" : Return "i32.shl"
                        Case ">>" : Return "i32.shr_s"
                        Case "And" : Return "i32.and"
                        Case "Or" : Return "i32.or"
                        Case Else
                            Throw New NotImplementedException
                    End Select
                Else
                    Throw New NotImplementedException
                End If
        End Select
    End Function

    Public Shared Function IsArray(type As String) As Boolean
        ' instr是从1开始的
        Dim p = InStr(type, "[]") - 1
        Dim lastIndex = (type.Length - 2)

        Return p = lastIndex
    End Function

    ''' <summary>
    ''' get array element type name
    ''' </summary>
    ''' <param name="fullName"></param>
    ''' <returns></returns>
    ''' 
    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Shared Function ArrayElement(fullName As String) As String
        Return fullName.Substring(Scan0, fullName.Length - 2)
    End Function

    <MethodImpl(MethodImplOptions.AggressiveInlining)>
    Public Shared Function TypeCharWasm(c As Char) As String
        Return Convert2Wasm(Scripting.GetType(Patterns.TypeCharName(c)))
    End Function
End Class
