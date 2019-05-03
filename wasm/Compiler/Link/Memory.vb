﻿#Region "Microsoft.VisualBasic::95b10d488dd49e0ce20cd5d0e67e789f, Compiler\Link\Memory.vb"

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

'     Class Memory
' 
'         Function: AddString, AllocateArrayBlock, GetEnumerator, IEnumerable_GetEnumerator
' 
' 
' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language
Imports Wasm.Symbols
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Compiler

    ''' <summary>
    ''' The WebAssembly memory buffer device
    ''' </summary>
    Public Class Memory : Implements IEnumerable(Of Expression)

        Dim buffer As New List(Of Expression)
        Dim offset As Integer = 1

        ''' <summary>
        ''' 函数返回的是数据的内存位置
        ''' </summary>
        ''' <param name="str"></param>
        ''' <returns></returns>
        Public Function AddString(str As String) As Integer
            Dim buffer As New StringSymbol With {
                .[string] = str,
                .memoryPtr = offset
            }

            Me.buffer += buffer
            ' 因为字符串末尾会添加一个零，来表示字符串的结束
            ' 所以在长度这里会需要添加1
            Me.offset += buffer.Length + 1

            Return buffer.memoryPtr
        End Function

        ''' <summary>
        ''' 为数组分配内存位置，然后返回数组在内存之中的起始位置
        ''' </summary>
        ''' <param name="ofElement"></param>
        ''' <param name="size"></param>
        ''' <returns></returns>
        Public Function AllocateArrayBlock(ofElement As TypeAbstract, size As Integer) As ArrayBlock
            Dim array As New ArrayBlock With {
                .length = size,
                .type = ofElement.MakeArrayType,
                .memoryPtr = offset
            }

            If ofElement.type = TypeAlias.f64 OrElse ofElement.type = TypeAlias.i64 Then
                ' 8 bytes
                ' zero terminated
                Me.offset += 8 * size + 1
            Else
                ' other elements(f32/i32) and intptr(i32) type, 4 bytes
                ' zero terminated
                Me.offset += 4 * size + 1
            End If

            Return array
        End Function

        ''' <summary>
        ''' 添加类型定义的meta信息，然后返回class_id，即该自定义类型的内存之中的位置
        ''' </summary>
        ''' <param name="meta"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' https://vanillavb.app/#class_impl
        ''' </remarks>
        Public Function AddClassMeta(meta As ClassMeta) As Integer
            Dim class_id As Integer = offset
            ' 生成json数据模型，然后对json字符串进行base64序列化
            Dim fieldTable = meta.Fields.ToDictionary(Function(field) field.name, Function(field) field.type)
            Dim methodTable As Dictionary(Of String, FuncMetaJSON) = meta _
                .Methods _
                .ToDictionary(Function(func) func.Name,
                              Function(func)
                                  Return New FuncMetaJSON With {
                                      .IsPublic = True,
                                      .Result = func.result,
                                      .Parameters = func _
                                          .parameters _
                                          .ToDictionary(Function(a) a.Name, Function(a) a.Value)
                                  }
                              End Function)

            Dim json As New MetaJSON With {
                .memoryPtr = class_id,
                .[Class] = meta.ClassName,
                .[Namespace] = meta.Module,
                .Fields = fieldTable,
                .Methods = methodTable
            }

            Me.buffer += json
            Me.offset += json.Meta.Length + 1

            Return class_id
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of Expression) Implements IEnumerable(Of Expression).GetEnumerator
            For Each data As Expression In buffer
                Yield data
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace