#Region "Microsoft.VisualBasic::7f07c17344f5510086a02cd2211abe90, Symbols\Memory\ArrayBlockOperator.vb"

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
'         Function: GetArrayElement, GetArrayMember, SetArrayElement, writeArray
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols.Parser
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public Module ArrayBlockOperator

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="array"></param>
        ''' <param name="symbols"></param>
        ''' <param name="arrayType"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 在这里只是生成数组元素的内容，byte marks内容则在<see cref="ArrayBlock"/>的表达式迭代器部分完成
        ''' </remarks>
        <Extension>
        Friend Function writeArray(array As ArraySymbol, symbols As SymbolTable, arrayType As TypeAbstract) As Expression
            Dim ofElement As TypeAbstract = arrayType.generic(Scan0)
            Dim arrayBlock As ArrayBlock = symbols.memory.AllocateArrayBlock(ofElement, array.Initialize.Length)
            Dim save As New List(Of Expression)
            Dim size As Integer = sizeOf(ofElement)
            Dim byteType$ = ofElement.typefit
            ' 在这里需要跳过数组前面的8个字节
            Dim intptr As Integer = arrayBlock.memoryPtr + 4 + 4

            For Each element In array.Initialize
                element = CTypeHandle.CType(ofElement, element, symbols)
                save += BitConverter.save(byteType, intptr, element)
                intptr += size
            Next

            arrayBlock.elements = save

            Return arrayBlock
        End Function

        <Extension>
        Public Function GetArrayMember(array As GetLocalVariable, memberName$, symbols As SymbolTable) As Expression
            Select Case memberName
                Case "Length"
                    ' 读取第二个i32数据块即可得到长度
                    Dim intptr As Expression = BinaryStack(array, Literal.i32(4), "+", symbols)
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
            Dim intptr As Expression = BinaryStack(target, Literal.i32(4 + 4), "+", symbols)
            ' 然后位置的偏移量则是index索引，乘上元素的大小
            Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement)), "*", symbols)
            Dim read As Expression

            ' 然后得到实际的内存中的位置
            intptr = ArrayBlock.IndexOffset(intptr, offset)
            ' 最后使用load读取内存数据
            read = BitConverter.load(ofElement, intptr)

            Return read
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
            Dim intptr As Expression = BinaryStack(arraySymbol, Literal.i32(4 + 4), "+", symbols)
            ' 然后位置的偏移量则是index索引，乘上元素的大小
            Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement)), "*", symbols)
            Dim save As Expression

            ' 然后得到实际的内存中的位置
            intptr = ArrayBlock.IndexOffset(intptr, offset)
            ' 最后使用load读取内存数据
            save = BitConverter.save(ofElement, intptr, CTypeHandle.CType(ofElement, right, symbols))

            Return save
        End Function
    End Module
End Namespace
