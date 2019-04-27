﻿#Region "Microsoft.VisualBasic::855b8b516df7bc8d226572d8b15dfb56, Symbols\DeclaredObject\JavaScriptImports\Array.vb"

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

    '     Module Array
    ' 
    '         Properties: GetArrayElement, Length, NewArray, Pop, Push
    '                     SetArrayElement
    ' 
    '         Function: IsArrayOperation, Method
    ' 
    '         Sub: doArrayImports
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Wasm.Compiler
Imports Wasm.Symbols.Parser

Namespace Symbols.JavaScriptImports

    ''' <summary>
    ''' The javascript array api
    ''' </summary>
    Public Module Array

        ''' <summary>
        ''' Push element value into a given array and then returns the array intptr
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Push As New ImportSymbol With {
            .ImportObject = "push",
            .Name = "array_push",
            .[Module] = "array",
            .Package = NameOf(Array),
            .result = New TypeAbstract(TypeAlias.array),
            .parameters = {
                "array".param(TypeAlias.array),
                "element".param(TypeAlias.any)
            }
        }

        Public ReadOnly Property Pop As New ImportSymbol With {
            .ImportObject = "pop",
            .Name = "array_pop",
            .[Module] = "array",
            .Package = NameOf(Array),
            .result = New TypeAbstract(TypeAlias.any),
            .parameters = {
                "array".param(TypeAlias.array)
            }
        }

        ''' <summary>
        ''' Create an new array and then returns the array intptr
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NewArray As New ImportSymbol With {
            .ImportObject = "create",
            .[Module] = "array",
            .Name = "array_new",
            .Package = NameOf(Array),
            .result = New TypeAbstract(TypeAlias.array),
            .parameters = {
                "size".param("i32")
            }
        }

        Public ReadOnly Property GetArrayElement As New ImportSymbol With {
            .ImportObject = "get",
            .[Module] = "array",
            .Name = "array_get",
            .Package = NameOf(Array),
            .result = New TypeAbstract(TypeAlias.any),
            .parameters = {
                "array".param(TypeAlias.array),
                "index".param("i32")
            }
        }

        Public ReadOnly Property SetArrayElement As New ImportSymbol With {
            .ImportObject = "set",
            .[Module] = "array",
            .Name = "array_set",
            .Package = NameOf(Array),
            .result = New TypeAbstract("void"),
            .parameters = {
                "array".param(TypeAlias.array),
                "index".param("i32"),
                "value".param("any")
            }
        }

        Public ReadOnly Property Length As New ImportSymbol With {
            .ImportObject = "length",
            .[Module] = "array",
            .Name = "array_length",
            .Package = NameOf(Array),
            .result = TypeAbstract.i32,
            .parameters = {
                 "array".param(TypeAlias.array)
            }
        }

        ReadOnly arrayOp As Index(Of String) = {GetArrayElement.Name, SetArrayElement.Name}

        Public Function IsArrayOperation(func As FuncSignature) As Boolean
            Return TypeOf func Is ImportSymbol AndAlso func.Name Like arrayOp
        End Function

        Public Function Method(name As String) As ImportSymbol
            Select Case name
                Case "Add" : Return Array.Push
                Case "Remove"
                    Throw New NotImplementedException
                Case "Length" : Return Array.Length
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        <Extension>
        Public Sub doArrayImports(symbols As SymbolTable)
            Call symbols.addRequired(JavaScriptImports.NewArray)
            Call symbols.addRequired(JavaScriptImports.Push)
            Call symbols.addRequired(JavaScriptImports.GetArrayElement)
            Call symbols.addRequired(Array.Length)
        End Sub
    End Module
End Namespace
