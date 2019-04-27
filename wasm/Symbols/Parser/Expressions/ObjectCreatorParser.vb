#Region "Microsoft.VisualBasic::f126d083d88354d7daede6c48ad5a532, Symbols\Parser\Expressions\ObjectCreatorParser.vb"

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
    '         Function: AsNewObject, CreateObject, GetInitializeValue
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language

Namespace Symbols.Parser

    Module ObjectCreatorParser

        <Extension>
        Public Function AsNewObject(newObj As NewExpressionSyntax, ByRef type As TypeAbstract, symbols As SymbolTable) As Expression
            Dim objNew = DirectCast(newObj, ObjectCreationExpressionSyntax).Initializer
            Dim objType = DirectCast(newObj, ObjectCreationExpressionSyntax).Type

            If TypeOf objNew Is ObjectCollectionInitializerSyntax Then
                With DirectCast(objNew, ObjectCollectionInitializerSyntax)
                    Dim collection As ArraySymbol = .Initializer.CreateArray(symbols)

                    If type = GetType(DictionaryBase).FullName Then
                        Dim genericTypes As Type() = DirectCast(objType, GenericNameSyntax) _
                            .TypeArgumentList _
                            .Arguments _
                            .Select(Function(T)
                                        Return AsTypeHandler.GetType(T, symbols)
                                    End Function) _
                            .ToArray

                        With New ArrayTable
                            .initialVal = collection _
                                .Initialize _
                                .Select(Function(i)
                                            With DirectCast(i, ArraySymbol)
                                                Return (.Initialize(0), .Initialize(1))
                                            End With
                                        End Function) _
                                .ToArray
                            .key = New TypeAbstract(genericTypes(0))
                            .type = New TypeAbstract(genericTypes(1))

                            Return .ByRef
                        End With
                    Else
                        Throw New NotImplementedException
                    End If
                End With
            Else
                Throw New NotImplementedException
            End If
        End Function

        <Extension>
        Public Function GetInitializeValue(objNew As ObjectCreationInitializerSyntax, objType As TypeAbstract, symbols As SymbolTable) As IEnumerable(Of Expression)

            Throw New NotImplementedException
        End Function

        <Extension>
        Public Function CreateObject(create As ObjectCreationExpressionSyntax, symbols As SymbolTable) As Expression
            Dim type = create.Type
            Dim typeName$

            If TypeOf type Is GenericNameSyntax Then
                Dim elementType As Type()

                With DirectCast(type, GenericNameSyntax).GetGenericType(symbols)
                    typeName = .Name
                    elementType = .Value
                End With

                If typeName = "List" Then
                    ' array和list在javascript之中都是一样的
                    Return New ArraySymbol With {
                        .type = New TypeAbstract(elementType(Scan0)).MakeArrayType,
                        .Initialize = create.Initializer _
                            .GetInitializeValue(.type, symbols) _
                            .ToArray
                    }
                ElseIf typeName = "Dictionary" Then
                    Return New ArrayTable With {
                        .initialVal = {},
                        .key = New TypeAbstract(elementType(Scan0)),
                        .type = New TypeAbstract(elementType(1))
                    }
                Else
                    Throw New NotImplementedException
                End If
            Else
                Throw New NotImplementedException
            End If
        End Function
    End Module
End Namespace
