#Region "Microsoft.VisualBasic::60ed0db79868732d2654826687d2938d, Symbols\Parser\Expressions\ObjectCreatorParser.vb"

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

    '     Module ObjectCreatorParser
    ' 
    '         Function: AsNewObject, CreateCollection, CreateCollectionObject, CreateObject, GetInitializeValue
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Compiler
Imports Wasm.Symbols.JavaScriptImports
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Symbols.Parser

    Module ObjectCreatorParser

        <Extension>
        Public Function AsNewObject(newObj As NewExpressionSyntax, ByRef type As TypeAbstract, symbols As SymbolTable) As Expression
            Dim objNew = DirectCast(newObj, ObjectCreationExpressionSyntax).Initializer
            Dim rawType As RawType = DirectCast(newObj, ObjectCreationExpressionSyntax).Type.GetType(symbols)

            If TypeOf objNew Is ObjectCollectionInitializerSyntax Then
                Return DirectCast(objNew, ObjectCollectionInitializerSyntax).CreateCollection(type, symbols)
            Else
                type = rawType.WebAssembly(symbols)
                Return type.createUserObject(objNew, symbols)
            End If
        End Function

        <Extension>
        Private Function createUserObject(type As TypeAbstract, objNew As ObjectMemberInitializerSyntax, symbols As SymbolTable) As Expression
            Dim objType As ClassMeta = symbols.GetClassType(type.raw)
            Dim hashcode As Expression = New GetGlobalVariable(IMemoryObject.ObjectManager)
            ' 创建用户自定义类型的对象实例
            Dim obj As New UserObject With {
                .memoryPtr = hashcode,
                .UnderlyingType = type,
                .width = objType.sizeOf,
                .Meta = objType
            }

            ' 初始化字段值
            Dim initializer As New List(Of Expression)
            Dim fieldName$
            Dim fieldType As TypeAbstract
            Dim initValue As Expression
            Dim fieldOffset As Expression

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
        Public Function CreateCollection(objNew As ObjectCollectionInitializerSyntax, objType As TypeAbstract, symbols As SymbolTable) As Expression
            Dim collection As ArraySymbol = objNew.Initializer.CreateArray(symbols)

            If objType = TypeAlias.table Then
                'Dim genericTypes As Type() = DirectCast(objType, GenericNameSyntax) _
                '    .TypeArgumentList _
                '    .Arguments _
                '    .Select(Function(T)
                '                Return AsTypeHandler.GetType(T, symbols)
                '            End Function) _
                '    .ToArray

                'With New ArrayTable
                '    .initialVal = collection _
                '        .Initialize _
                '        .Select(Function(i)
                '                    With DirectCast(i, ArraySymbol)
                '                        Return (.Initialize(0), .Initialize(1))
                '                    End With
                '                End Function) _
                '        .ToArray
                '    .key = New TypeAbstract(genericTypes(0))
                '    .type = New TypeAbstract(genericTypes(1))

                '    Return .ByRef
                'End With
                Throw New NotImplementedException
            ElseIf objType = TypeAlias.list Then
                collection.type = objType
                Return collection
            Else
                Throw New NotImplementedException
            End If
        End Function

        <Extension>
        Public Function GetInitializeValue(objNew As ObjectCreationInitializerSyntax, objType As TypeAbstract, symbols As SymbolTable) As Expression
            If objNew Is Nothing Then
                Return Nothing
            ElseIf TypeOf objNew Is ObjectCollectionInitializerSyntax Then
                Return DirectCast(objNew, ObjectCollectionInitializerSyntax).CreateCollection(objType, symbols)
            End If

            Throw New NotImplementedException(objNew.GetType.FullName)
        End Function

        <Extension>
        Public Function CreateObject(create As ObjectCreationExpressionSyntax, symbols As SymbolTable) As Expression
            Dim type As TypeSyntax = create.Type

            If TypeOf type Is GenericNameSyntax Then
                Return create.CreateCollectionObject(type, symbols)
            ElseIf TypeOf type Is IdentifierNameSyntax Then
                Dim rawType As RawType = type.GetType(symbols)
                Dim objType As TypeAbstract = rawType.WebAssembly(symbols)

                Return objType.createUserObject(create.Initializer, symbols)
            Else
                Throw New NotImplementedException
            End If
        End Function

        <Extension>
        Private Function CreateCollectionObject(create As ObjectCreationExpressionSyntax, type As TypeSyntax, symbols As SymbolTable) As Expression
            Dim elementType As RawType()
            Dim typeName$

            With DirectCast(type, GenericNameSyntax).GetGenericType(symbols)
                typeName = .Name
                elementType = .Value
            End With

            If typeName = "List" Then
                ' array和list在javascript之中都是一样的
                Dim listType As TypeAbstract = elementType(Scan0).WebAssembly(symbols).MakeListType
                Dim listValues As ArraySymbol = create _
                    .Initializer _
                    .GetInitializeValue(listType, symbols)

                If listValues Is Nothing Then
                    Call symbols.doArrayImports(listType.generic(Scan0))

                    ' list没有集合元素的初始化语句
                    ' 则只能够创建一个新的空数组
                    Return New Array With {.type = listType, .size = Literal.i32(-1)}
                Else
                    Return listValues
                End If
            ElseIf typeName = "Dictionary" Then
                Return New ArrayTable With {
                    .initialVal = {},
                    .key = elementType(Scan0).WebAssembly(symbols),
                    .type = elementType(1).WebAssembly(symbols)
                }
            Else
                Throw New NotImplementedException
            End If
        End Function
    End Module
End Namespace
