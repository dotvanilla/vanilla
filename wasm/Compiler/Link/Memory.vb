#Region "Microsoft.VisualBasic::83a09ea7ccaa607bf4580ac84c0ce96d, Compiler\Link\Memory.vb"

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
    '         Constructor: (+1 Overloads) Sub New
    '         Function: AddClassMeta, AddString, AllocateArrayBlock, GetEnumerator, IEnumerable_GetEnumerator
    '                   ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
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
        ''' <summary>
        ''' 因为<see cref="TypeAlias"/>之中基础类型都是小于10的
        ''' 所以在这里offset从10开始，从而能够避免class_id小于10
        ''' 导致用户自定义类型被误判为基础类型
        ''' </summary>
        Dim offset As Integer = 13
        Dim symbols As SymbolTable

        ''' <summary>
        ''' 获取得到在WebAssembly之中的初始化大小
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property TotalSize As Integer
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                ' 这个偏移量就是静态资源的总大小
                Return offset
            End Get
        End Property

        Sub New(symbols As SymbolTable)
            Me.symbols = symbols
        End Sub

        ''' <summary>
        ''' Moves the position of the <seealso cref="offset"/> to the next position aligned on
        ''' 8 bytes. If the buffer position is already a multiple of 8 the position will
        ''' not be changed.
        ''' </summary>
        Private Sub seekBufferToNextMultipleOfEight()
            Dim pos As Integer = offset

            If pos Mod 8 = 0 Then
                ' Already on a 8 byte multiple
                Return
            Else
                offset += (8 - (pos Mod 8))
            End If
        End Sub

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

            Call seekBufferToNextMultipleOfEight()

            Return buffer.memoryPtr
        End Function

        ''' <summary>
        ''' 为数组分配内存位置，然后返回数组在内存之中的起始位置
        ''' </summary>
        ''' <param name="ofElement"></param>
        ''' <param name="count">数组之中的元素的数量</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 和字符串数据不同，数组对象的内存不是静态分配的
        ''' </remarks>
        Public Function AllocateArrayBlock(ofElement As TypeAbstract, count As Expression) As ArrayBlock
            Dim array As New ArrayBlock(symbols) With {
                .length = count,
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
            Dim fieldTable As Dictionary(Of String, TypeAbstract) = meta _
                .fields _
                .ToDictionary(Function(field) field.name,
                              Function(field) field.type)
            Dim methodTable As Dictionary(Of String, FuncMetaJSON) = meta _
                .methods _
                .ToDictionary(Function(func) func.name,
                              Function(func)
                                  Return New FuncMetaJSON With {
                                      .isPublic = True,
                                      .result = func.result,
                                      .parameters = func _
                                          .parameters _
                                          .ToDictionary(Function(a) a.Name,
                                                        Function(a)
                                                            Return a.Value
                                                        End Function)
                                  }
                              End Function)

            Dim json As New MetaJSON With {
                .memoryPtr = class_id,
                .[class] = meta.className,
                .[namespace] = meta.module,
                .fields = fieldTable,
                .methods = methodTable,
                .class_id = class_id,
                .isStruct = meta.isStruct
            }

            ' 20190602 因为可能会存在后续再字段类型之中补进
            ' class_id的情况，造成额外的字符串偏移
            ' 所以在这里根据类型为intptr自定义类型的字段数量每一个字段添加5个字符数量的padding
            Dim padding = Aggregate field As TypeAbstract
                          In fieldTable.Values
                          Where field.type = TypeAlias.intptr
                          Into Sum(32)

            Me.buffer += json
            Me.offset += json.meta.Length + 1 + padding

            Call seekBufferToNextMultipleOfEight()

            Return class_id
        End Function

        Public Overrides Function ToString() As String
            Return $"Static memory: {TotalSize} bytes."
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
