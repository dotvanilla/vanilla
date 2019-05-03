#Region "Microsoft.VisualBasic::9ee4f7dfef855bd06db119939b155684, Compiler\Link\Memory.vb"

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
    '         Properties: TotalSize
    ' 
    '         Function: AddClassMeta, AddString, AllocateArrayBlock, GetEnumerator, IEnumerable_GetEnumerator
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
        ''' 获取得到在WebAssembly之中的初始化大小
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property TotalSize As Integer
            Get
                ' 这个偏移量就是静态资源的总大小
                Return offset
            End Get
        End Property

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
        ''' <remarks>
        ''' 和字符串数据不同，数组对象的内存不是静态分配的
        ''' </remarks>
        Public Function AllocateArrayBlock(ofElement As TypeAbstract, size As Integer) As ArrayBlock
            Dim array As New ArrayBlock With {
                .length = size,
                .type = ofElement.MakeArrayType,
                .memoryPtr = IMemoryObject.ObjectManager.GetReference
            }

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
                .[Namespace] = meta.module,
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
