#Region "Microsoft.VisualBasic::8ab848d73f6d7aabd09470c9c6d4b998, Symbols\Memory\Array\ArrayBlockOperator.vb"

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

'     Module ArrayBlockOperator
' 
'         Function: GetArrayElement, GetArrayMember, (+2 Overloads) SetArrayElement, (+2 Overloads) writeArray, writeEmptyArray
'                   writeStructArray
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.SyntaxAnalysis
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public Module ArrayBlockOperator

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="array"></param>
        ''' <param name="symbols"></param>
        ''' <param name="arrayType">是一个完整的数组类型定义，而非元素的类型定义</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 在这里只是生成数组元素的内容，byte marks内容则在<see cref="ArrayBlock"/>的表达式迭代器部分完成
        ''' </remarks>
        <Extension>
        Friend Function writeArray(array As ArraySymbol, symbols As SymbolTable, arrayType As TypeAbstract) As Expression
            Return array.Initialize.writeArray(symbols, arrayType)
        End Function

        <Extension>
        Public Function writeEmptyArray(symbols As SymbolTable, ofElement As TypeAbstract, length As Expression) As ArrayBlock
            Dim intPtr As DeclareLocal = symbols.AddLocal("arrayOffset_" & symbols.NextGuid, TypeAbstract.i32)
            Dim arrayBlock As New ArrayBlock(symbols) With {
                .length = length,
                .type = ofElement.MakeArrayType,
                .memoryPtr = intPtr.GetReference
            }

            Return arrayBlock
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="arrayInitialize"></param>
        ''' <param name="symbols"></param>
        ''' <param name="arrayType">是一个完整的数组类型定义，而非元素的类型定义</param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 在这里只是生成数组元素的内容，byte marks内容则在<see cref="ArrayBlock"/>的表达式迭代器部分完成
        ''' </remarks>
        <Extension>
        Friend Function writeArray(arrayInitialize As Expression(), symbols As SymbolTable, arrayType As TypeAbstract) As Expression
            Dim ofElement As TypeAbstract = arrayType.generic(Scan0)
            Dim arrayBlock As ArrayBlock = symbols.writeEmptyArray(ofElement, Literal.i32(arrayInitialize.Length))
            Dim save As New List(Of Expression)
            Dim size As Integer = sizeOf(ofElement, symbols)
            ' 在这里需要跳过数组前面的8个字节
            Dim offset As DeclareLocal = symbols.AddLocal("itemOffset_" & symbols.NextGuid, TypeAbstract.i32)

            save += New SetLocalVariable(offset, IMemoryObject.IndexOffset(arrayBlock.AddressOf, 8))

            If ofElement = TypeAlias.intptr AndAlso symbols.FindByClassId(ofElement.class_id).isStruct Then
                save += symbols.writeStructArray(offset.GetReference, size, arrayInitialize, ofElement)
            Else
                Dim byteType As String = ofElement.typefit
                Dim i As VBInteger = Scan0
                Dim location As Expression

                For Each element As Expression In arrayInitialize
                    element = CTypeHandle.CType(ofElement, element, symbols)
                    location = IMemoryObject.IndexOffset(offset.GetReference, ++i * size)
                    save += BitConverter.save(byteType, location, element)
                Next
            End If

            arrayBlock.elements = save

            Return arrayBlock
        End Function

        <Extension>
        Private Function writeStructArray(symbols As SymbolTable,
                                          offset As GetLocalVariable,
                                          size%,
                                          arrayInitialize As Expression(),
                                          ofElement As TypeAbstract) As IEnumerable(Of Expression)
            Dim copy As DeclareLocal
            Dim i As VBInteger = Scan0
            Dim location As Expression
            Dim save As New List(Of Expression)
            Dim class_id As Expression = Literal.i32(ofElement.class_id)

            ' 结构体类型比较特殊
            ' 会需要与引用类型的class区分开来
            For Each element As Expression In arrayInitialize
                location = IMemoryObject.IndexOffset(offset, ++i * size)
                copy = New DeclareLocal With {
                    .name = "structCopyOf_" & symbols.NextGuid,
                    .type = TypeAbstract.i32
                }
                symbols.AddLocal(copy.name, "i32")
                save += New SetLocalVariable(copy, location)
                save += IMemoryObject.AddGCobject.Call(copy.GetReference, class_id)

                If TypeOf element Is UserObject Then
                    ' 如果是创建的新对象的话，则修改指针位置后直接赋值
                    With DirectCast(element, UserObject)
                        Dim intptrName$ = .memoryPtr _
                                          .TryCast(Of GetLocalVariable) _
                                          .var

                        ' modify the memory location of 
                        ' this New Object
                        save += New SetLocalVariable With {
                            .var = intptrName,
                            .value = copy.GetReference
                        }
                        ' Add statements for initialize new object
                        save += .AsEnumerable.Skip(1)
                    End With
                ElseIf TypeOf element Is FuncInvoke Then
                    ' 如果是通过函数产生的话，则需要将函数得到的结果进行临时本地变量的赋值，
                    ' 然后再从这个本地变量进行复制
                    Dim temp As New DeclareLocal With {
                        .name = "tempOfStructFunc_" & symbols.NextGuid,
                        .type = TypeAbstract.i32
                    }

                    symbols.AddLocal(temp.name, "i32")
                    save += ofElement.CopyTo(
                        from:=temp.GetReference,
                        [to]:=copy,
                        symbols:=symbols,
                        funCalls:=New SetLocalVariable(temp, element)
                    )
                Else
                    ' 可能是其他的变量或者函数调用产生的值
                    ' 则需要按照地址进行复制
                    save += ofElement.CopyTo(element, copy, symbols, Nothing)
                End If
            Next

            Return save
        End Function

        <Extension>
        Public Function GetArrayMember(array As GetLocalVariable, memberName$, symbols As SymbolTable) As Expression
            Select Case memberName
                Case "Length"
                    ' 读取第二个i32数据块即可得到长度
                    Dim intptr As Expression = ArrayBlock.IndexOffset(array, Literal.i32(4))
                    Dim counts As Expression = BitConverter.Loadi32(intptr)

                    Return counts
                Case Else
                    Throw New NotImplementedException(memberName)
            End Select
        End Function

        <Extension>
        Public Function GetArrayElement(target As Expression,
                                        index As Expression,
                                        ofElement As TypeAbstract,
                                        symbols As SymbolTable) As Expression

            ' 从webassembly内存之中读取数据
            ' 对于数组对象而言，其值是一个内存区块的起始位置来的
            ' 因为前面还有8个字节的元数据信息，所以需要从target跳过8个字节才能够到真正的数据区
            Dim intptr As Expression = ArrayBlock.IndexOffset(target, Literal.i32(4 + 4))
            ' 然后位置的偏移量则是index索引，乘上元素的大小
            Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement, symbols)), "*", symbols)
            Dim read As Expression

            ' 然后得到实际的内存中的位置
            intptr = ArrayBlock.IndexOffset(intptr, offset)
            ' 最后使用load读取内存数据
            read = BitConverter.load(ofElement, intptr)

            Return New FieldValue With {
                .type = ofElement,
                .Internal = read
            }
        End Function

        <Extension>
        Public Function SetArrayElement(arraySymbol As GetLocalVariable,
                                        index As Expression,
                                        ofElement As TypeAbstract,
                                        right As Expression,
                                        symbols As SymbolTable) As Expression

            ' 从webassembly内存之中读取数据
            ' 对于数组对象而言，其值是一个内存区块的起始位置来的
            ' 因为前面还有8个字节的元数据信息，所以需要从target跳过8个字节才能够到真正的数据区
            Dim intptr As Expression = ArrayBlock.IndexOffset(arraySymbol, Literal.i32(4 + 4))
            ' 然后位置的偏移量则是index索引，乘上元素的大小
            Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement, symbols)), "*", symbols)
            Dim save As Expression

            ' 然后得到实际的内存中的位置
            intptr = ArrayBlock.IndexOffset(intptr, offset)
            ' 最后使用load读取内存数据
            save = BitConverter.save(ofElement, intptr, CTypeHandle.CType(ofElement, right, symbols))

            Return save
        End Function

        <Extension>
        Public Function SetArrayElement(type As TypeAbstract, memberName$, func$, index As Expression, right As Expression, symbols As SymbolTable) As Expression
            ' 是引用的数组之中的某一个元素
            ' 因为只有class或者structure类型才可能有成员
            ' 所以在这里就直接取class类型了
            Dim ofElement As TypeAbstract = type.generic(Scan0)
            Dim meta As ClassMeta = symbols.FindByClassId(ofElement.class_id)
            Dim fieldType As TypeAbstract = meta(memberName).type

            ' 如果是结构体，则内存的位置是数组之中的位置
            ' 反之，如果是class引用，则从数组之中取出intptr指针之后得到内存地址
            Dim array As GetLocalVariable = symbols.GetObjectReference(func)
            ' 数组之中的元素
            Dim element As Expression = IMemoryObject.IndexOffset(array, 8)

            right = CTypeHandle.CType(fieldType, right, symbols)

            If meta.isStruct Then
                ' 直接在内存里面写
                ' 结构体是实际上存储在数组中的
                element = IMemoryObject.IndexOffset(element, BinaryStack(index, Literal.i32(meta.sizeOf), "*", symbols))
            Else
                ' intptr实际上为i32，只有4个字节
                element = IMemoryObject.IndexOffset(element, BinaryStack(index, Literal.i32(4), "*", symbols))
                ' 然后读取即可得到对象的位置
                element = BitConverter.Loadi32(element)
            End If

            ' 然后计算出field offset， 然后存储数据即可
            Dim location As Expression = IMemoryObject.IndexOffset(element, meta.GetFieldOffset(memberName))
            Dim save As Expression = BitConverter.save(fieldType, location, right)

            Return save
        End Function
    End Module
End Namespace
