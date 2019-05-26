#Region "Microsoft.VisualBasic::ce4c523243414f7b9f5659bff5906806, SyntaxAnalysis\ObjectOperator.vb"

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
    '         Function: allocateNew, Clone, CopyTo, (+2 Overloads) createUserObject, GetAnonymousField
    '                   getFieldValues, (+2 Overloads) GetMemberField, initializeField, SetMemberField
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace SyntaxAnalysis

    Public Module ObjectOperator

        <Extension>
        Public Function GetAnonymousField(ref As GetGlobalVariable, symbols As SymbolTable) As Expression
            Dim fieldName As String = ref.var
            Dim type As ClassMeta = symbols.context.object.Meta
            Dim fieldValue As Expression = symbols.context _
                .object _
                .AddressOf _
                .GetMemberField(type, fieldName)

            Return fieldValue
        End Function

        ''' <summary>
        ''' Create an new object and then copy field values to the new object.
        ''' (应用于结构体的内存赋值操作)
        ''' </summary>
        ''' <param name="type"></param>
        ''' <param name="intptr">The memory address of target user object, can be a class or structure.</param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function Clone(type As TypeAbstract, intptr As Expression, symbols As SymbolTable) As Expression
            Dim objType As ClassMeta = symbols.GetClassType(type.raw)
            Dim [new] = type.allocateNew(symbols)
            Dim hashcode As DeclareLocal = [new].hashcode
            Dim obj As UserObject = [new].object
            Dim initialize As NamedValue(Of Expression)() = objType.getFieldValues(intptr)

            Return type.createUserObject(hashcode, obj, initialize, symbols, False, Nothing)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="objType"></param>
        ''' <param name="intptr">目标用户对象在内存之中的位置</param>
        ''' <returns></returns>
        <Extension>
        Private Function getFieldValues(objType As ClassMeta, intptr As Expression) As NamedValue(Of Expression)()
            Return objType.fields _
                .Select(Function(field)
                            Dim fieldName = field.name
                            Dim initValue = intptr.GetMemberField(objType, fieldName)

                            Return New NamedValue(Of Expression) With {
                                .Name = fieldName,
                                .Value = initValue
                            }
                        End Function) _
                .ToArray
        End Function

        ''' <summary>
        ''' 将class或者structure对象的值从<paramref name="from"/>内存之中的位置拷贝到<paramref name="to"/>位置
        ''' </summary>
        ''' <param name="type"></param>
        ''' <param name="from"></param>
        ''' <param name="[to]"></param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <Extension>
        Public Function CopyTo(type As TypeAbstract,
                               from As Expression,
                               [to] As DeclareLocal,
                               symbols As SymbolTable,
                               funCalls As Expression) As Expression

            Dim objType As ClassMeta = symbols.GetClassType(type.raw)
            Dim obj As New UserObject With {
                .memoryPtr = [to].GetReference,
                .UnderlyingType = type,
                .Meta = objType,
                .width = objType.sizeOf
            }

            symbols.context.object = obj

            Dim initialize As NamedValue(Of Expression)() = objType.getFieldValues(from)
            Dim copy = type.createUserObject(
                hashcode:=[to],
                obj:=obj,
                initialize:=initialize,
                symbols:=symbols,
                isCopy:=True,
                funCalls:=funCalls
            )

            Return copy
        End Function

        <Extension>
        Friend Function createUserObject(type As TypeAbstract, objNew As ObjectMemberInitializerSyntax, symbols As SymbolTable) As Expression
            Dim [new] = type.allocateNew(symbols)
            Dim hashcode As DeclareLocal = [new].hashcode
            Dim obj As UserObject = [new].object
            Dim initialize As NamedValue(Of Expression)()

            If objNew Is Nothing Then
                ' Dim a As New type
                ' 创建新对象的时候没有指定初始化
                ' 则在这里留一个空集合，字段的初始化全部都使用默认值
                initialize = {}
            Else
                initialize = objNew.Initializers _
                    .Select(Function(init)
                                Dim fieldName = DirectCast(init, NamedFieldInitializerSyntax).Name.objectName
                                Dim initValue = DirectCast(init, NamedFieldInitializerSyntax).Expression.ValueExpression(symbols)

                                Return New NamedValue(Of Expression)(fieldName, initValue)
                            End Function) _
                    .ToArray
            End If

            Return type.createUserObject(hashcode, obj, initialize, symbols, False, Nothing)
        End Function

        <Extension>
        Private Function allocateNew(type As TypeAbstract, symbols As SymbolTable) As ([object] As UserObject, hashcode As DeclareLocal)
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
            symbols.context.object = obj
            symbols.AddLocal(hashcode)

            Return (obj, hashcode)
        End Function

        <Extension>
        Friend Function createUserObject(type As TypeAbstract,
                                         hashcode As DeclareLocal,
                                         obj As UserObject,
                                         initialize As NamedValue(Of Expression)(),
                                         symbols As SymbolTable,
                                         isCopy As Boolean,
                                         funCalls As Expression) As Expression

            Dim objType As ClassMeta = symbols.GetClassType(type.raw)
            ' 初始化字段值
            Dim initializer As New List(Of Expression)
            Dim fieldName$
            Dim initValue As Expression
            Dim optionalFields As Index(Of String) = objType.fields _
                .Select(Function(g) g.name) _
                .ToArray

            If Not isCopy Then
                initializer += New SetLocalVariable(hashcode, IMemoryObject.ObjectManager.GetReference)
            End If

            initializer += New CommentText($"Offset object manager with {obj.width} bytes.")
            initializer += New SetGlobalVariable(IMemoryObject.ObjectManager) With {
                .value = ArrayBlock.IndexOffset(hashcode.GetReference, obj.width)
            }

            If Not funCalls Is Nothing Then
                initializer += funCalls
            End If

            For Each init As NamedValue(Of Expression) In initialize
                fieldName = init.Name
                initValue = init.Value
                initializer += hashcode.initializeField(objType, fieldName, initValue, symbols)

                ' 为了了解有多少个字段是需要使用默认值进行初始化的
                Call optionalFields.Delete(fieldName)
            Next

            ' optional value for the fields that not initialized
            For Each name As String In optionalFields.Objects
                initValue = objType(name).init

                If Not initValue Is Nothing Then
                    ' it have default value, then we use this default for initialize this field
                    initializer += hashcode.initializeField(objType, name, initValue, symbols)
                End If
            Next

            Return obj.With(Sub(o) o.Initialize = initializer)
        End Function

        <Extension>
        Private Iterator Function initializeField(hashcode As DeclareLocal,
                                                  objType As ClassMeta,
                                                  fieldName$,
                                                  initValue As Expression,
                                                  symbols As SymbolTable) As IEnumerable(Of Expression)

            Dim offsetSize As Integer = objType.GetFieldOffset(fieldName)
            Dim fieldOffset As Expression = ArrayBlock.IndexOffset(hashcode.GetReference, offsetSize)
            Dim fieldType As TypeAbstract = objType(fieldName).type

            ' 因为在VB代码之中，字段的初始化可能不是按照类型之中的定义顺序来的
            ' 所以下面的保存的位置值intptr不能够是累加的结果
            ' 而每次必须是从hashcode的位置处进行位移，才能够正常的读取结果值
            Yield New CommentText($"set field [{objType.reference.ToString}::{fieldName}]")
            Yield BitConverter.save(
                type:=fieldType,
                intptr:=fieldOffset,
                value:=CTypeHandle.CType(fieldType, initValue, symbols)
            )
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
                .Internal = getValue
            }

            Return getValue
        End Function
    End Module
End Namespace
