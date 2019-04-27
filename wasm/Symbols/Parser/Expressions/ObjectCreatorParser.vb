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