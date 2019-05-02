﻿#Region "Microsoft.VisualBasic::915d5365ba19a1311f1f1769db3343bd, Symbols\Parser\Expressions\ArrayExpressionParser.vb"

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

    '     Module ArrayExpressionParser
    ' 
    '         Function: arrayListIndexer, setArrayListElement
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Wasm.Compiler

Namespace Symbols.Parser

    Public Module ArrayExpressionParser

        ''' <summary>
        ''' array和list都是统一使用数字索引来获取元素值的
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="target"></param>
        ''' <param name="targetType"></param>
        ''' <returns></returns>
        <Extension>
        Friend Function arrayListIndexer(symbols As SymbolTable, target As Expression, args As ArgumentListSyntax, targetType As TypeAbstract) As Expression
            Dim ofElement As TypeAbstract = targetType.generic(Scan0)
            ' 数组或者列表的数字索引
            Dim index As Expression = args.FirstArgument(symbols, "index".param("i32"))

            If targetType = TypeAlias.array Then
                ' 从webassembly内存之中读取数据
                ' 对于数组对象而言，其值是一个内存区块的起始位置来的
                Dim intptr As Expression = target
                ' 然后位置的偏移量则是index索引，乘上元素的大小
                Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement)), "*", symbols)
                Dim read As Expression

                ' 然后得到实际的内存中的位置
                intptr = ArrayBlock.IndexOffset(intptr, offset)
                ' 最后使用load读取内存数据
                read = BitConverter.load(ofElement, intptr)

                Return read
            Else
                ' 从javascript内存之中读取数据

                ' 返回的是一个对象引用
                ' 在这里假设是一个数组
                Return JavaScriptImports.Array _
                    .GetArrayElement(ofElement) _
                    .FunctionInvoke({target, index})
            End If
        End Function

        ''' <summary>
        ''' 对列表或者数组的某一个元素进行赋值操作
        ''' </summary>
        ''' <param name="left"></param>
        ''' <param name="right"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Friend Function setArrayListElement(left As InvocationExpressionSyntax, right As Expression, symbols As SymbolTable) As Expression
            Dim arrayName = DirectCast(left.Expression, IdentifierNameSyntax).objectName
            Dim index As Expression = left.ArgumentList.FirstArgument(symbols, "a".param("i32"))
            Dim arraySymbol = symbols.GetObjectReference(arrayName)
            Dim arrayType As TypeAbstract = arraySymbol.TypeInfer(symbols)
            Dim ofElement As TypeAbstract = arrayType.generic(Scan0)

            If arrayType = TypeAlias.array Then
                ' 从webassembly内存之中读取数据
                ' 对于数组对象而言，其值是一个内存区块的起始位置来的
                Dim intptr As Expression = arraySymbol
                ' 然后位置的偏移量则是index索引，乘上元素的大小
                Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement)), "*", symbols)
                Dim save As Expression

                ' 然后得到实际的内存中的位置
                intptr = ArrayBlock.IndexOffset(intptr, offset)
                ' 最后使用load读取内存数据
                save = BitConverter.save(ofElement, intptr, CTypeHandle.CType(ofElement, right, symbols))

                Return save
            Else
                Return JavaScriptImports _
                    .SetElement(ofElement) _
                    .FunctionInvoke({arraySymbol, index, right})
            End If
        End Function
    End Module
End Namespace
