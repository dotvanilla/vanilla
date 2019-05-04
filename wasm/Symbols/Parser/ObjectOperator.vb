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
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Symbols.Parser

    Public Module ObjectOperator

        <Extension>
        Public Function GetAnonymousField(ref As GetGlobalVariable, symbols As SymbolTable) As Expression
            Dim fieldName$ = ref.var
            Dim fieldValue As Expression = symbols.currentObject.GetMemberField(symbols.currentObject.Meta, fieldName)

            Return fieldValue
        End Function

        <Extension>
        Friend Function createUserObject(type As TypeAbstract, objNew As ObjectMemberInitializerSyntax, symbols As SymbolTable) As Expression
            Dim objType As ClassMeta = symbols.GetClassType(type.raw)
            Dim hashcode As New DeclareLocal With {
                .name = "newObject_" & symbols.NextGuid,
                .type = type
            }
            ' 创建用户自定义类型的对象实例
            Dim obj As New UserObject With {
                .memoryPtr = New GetLocalVariable(hashcode),
                .UnderlyingType = type,
                .width = objType.sizeOf,
                .Meta = objType
            }

            ' 如果存在匿名对象的引用
            ' 会需要这个来完成引用
            symbols.currentObject = obj
            symbols.AddLocal(hashcode)

            ' 初始化字段值
            Dim initializer As New List(Of Expression)
            Dim fieldName$
            Dim fieldType As TypeAbstract
            Dim initValue As Expression
            Dim fieldOffset As Expression

            initializer += New SetLocalVariable(hashcode, IMemoryObject.ObjectManager.GetReference)

            For Each init As FieldInitializerSyntax In objNew.Initializers
                fieldName = DirectCast(init, NamedFieldInitializerSyntax).Name.objectName
                initValue = DirectCast(init, NamedFieldInitializerSyntax).Expression.ValueExpression(symbols)
                fieldType = objType(fieldName).type
                fieldOffset = ArrayBlock.IndexOffset(hashcode, objType.GetFieldOffset(fieldName))

                ' 因为在VB代码之中，字段的初始化可能不是按照类型之中的定义顺序来的
                ' 所以下面的保存的位置值intptr不能够是累加的结果
                ' 而每次必须是从hashcode的位置处进行位移，才能够正常的读取结果值
                initializer += New CommentText($"set field [{objType.Reference.ToString}::{fieldName}]")
                initializer += BitConverter.save(
                    type:=fieldType,
                    intptr:=fieldOffset,
                    value:=CTypeHandle.CType(fieldType, initValue, symbols)
                )
            Next

            initializer += New CommentText($"Offset object manager with {obj.width} bytes.")
            initializer += New SetGlobalVariable(IMemoryObject.ObjectManager) With {
                .value = ArrayBlock.IndexOffset(hashcode, obj.width)
            }

            Return obj.With(Sub(ByRef o)
                                o.Initialize = initializer
                            End Sub)
        End Function

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
            Return obj.GetMemberField(obj.GetUserType(symbols), memberName$)
        End Function

        <Extension>
        Public Function GetMemberField(obj As Expression, type As ClassMeta, memberName$) As Expression
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
