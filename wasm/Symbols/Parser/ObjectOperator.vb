#Region "Microsoft.VisualBasic::324d06e398dbe816729e191301618a64, Symbols\Parser\ObjectOperator.vb"

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

    '     Module ObjectOperator
    ' 
    '         Function: GetMemberField, SetMemberField
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Symbols.Parser

    Public Module ObjectOperator

        <Extension>
        Public Function SetMemberField(objName$, memberName$, right As Expression, symbols As SymbolTable) As Expression
            Dim type As TypeAbstract = symbols.GetUnderlyingType(objName)
            Dim objType As ClassMeta = symbols.GetClassType(type.raw)
            Dim offset As Integer = objType.GetFieldOffset(memberName)
            Dim fieldType As TypeAbstract = objType(memberName).type
            Dim intptr As Expression = symbols.GetObjectReference(objName)

            intptr = ArrayBlock.IndexOffset(intptr, offset)
            right = CTypeHandle.CType(fieldType, right, symbols)

            ' 只需要将数据写入指定的内存位置即可完成实例对象的字段的赋值操作
            Return BitConverter.save(fieldType, intptr, right)
        End Function

        <Extension>
        Public Function GetMemberField(obj As GetLocalVariable, memberName$, symbols As SymbolTable) As Expression
            Dim type As ClassMeta = obj.GetUserType(symbols)
            Dim fieldType As TypeAbstract = type(memberName).type
            Dim fieldOffset As Expression = Literal.i32(type.GetFieldOffset(memberName))
            Dim getValue As Expression

            fieldOffset = ArrayBlock.IndexOffset(obj, fieldOffset)
            getValue = BitConverter.load(fieldType, fieldOffset)
            getValue = New FieldValue With {
                .type = fieldType,
                .value = getValue
            }

            Return getValue
        End Function
    End Module
End Namespace
