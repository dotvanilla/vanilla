#Region "Microsoft.VisualBasic::138debdf469563f7a36a69031f64df15, Symbols\DeclaredObject\JavaScriptImports\Array.vb"

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
Imports Wasm.Compiler
Imports Wasm.Symbols.Parser

Namespace Symbols.JavaScriptImports

    ''' <summary>
    ''' The javascript array list api
    ''' </summary>
    Public Module Array

        ''' <summary>
        ''' Push element value into a given array and then returns the array intptr
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Push(ofElement As TypeAbstract) As ImportSymbol
            Get
                Return New ImportSymbol With {
                    .ImportObject = "push",
                    .DefinedInModule = False,
                    .Name = $"{ofElement.type}_array.push",
                    .[Module] = "array",
                    .Package = NameOf(Array),
                    .result = New TypeAbstract(TypeAlias.list),
                    .parameters = {
                        "array".param(TypeAlias.list),
                        "element".param(New TypeAbstract(ofElement))
                    }
                }
            End Get
        End Property

        Public ReadOnly Property Pop(ofElement As TypeAbstract) As ImportSymbol
            Get
                Return New ImportSymbol With {
                    .ImportObject = "pop",
                    .DefinedInModule = False,
                    .Name = $"{ofElement.type}_array.pop",
                    .[Module] = "array",
                    .Package = NameOf(Array),
                    .result = New TypeAbstract(ofElement.type),
                    .parameters = {
                        "array".param(TypeAlias.list)
                    }
                }
            End Get
        End Property

        ''' <summary>
        ''' Create an new array and then returns the array intptr
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NewArray As New ImportSymbol With {
            .ImportObject = "create",
            .DefinedInModule = False,
            .[Module] = "array",
            .Name = "array.new",
            .Package = NameOf(Array),
            .result = New TypeAbstract(TypeAlias.list),
            .parameters = {
                "size".param("i32")
            }
        }

        Public ReadOnly Property GetArrayElement(ofElement As TypeAbstract) As ImportSymbol
            Get
                Return New ImportSymbol With {
                    .ImportObject = "get",
                    .[Module] = "array",
                    .DefinedInModule = False,
                    .Name = $"{ofElement.type}_array.get",
                    .Package = NameOf(Array),
                    .result = New TypeAbstract(ofElement),
                    .parameters = {
                        "array".param(TypeAlias.list),
                        "index".param("i32")
                    }
                }
            End Get
        End Property

        Public ReadOnly Property SetElement(ofElement As TypeAbstract) As ImportSymbol
            Get
                Return New ImportSymbol With {
                    .ImportObject = "set",
                    .[Module] = "array",
                    .DefinedInModule = False,
                    .Name = $"{ofElement.type}_array.set",
                    .Package = NameOf(Array),
                    .result = New TypeAbstract("void"),
                    .parameters = {
                        "array".param(TypeAlias.list),
                        "index".param("i32"),
                        "value".param(New TypeAbstract(ofElement))
                    }
                }
            End Get
        End Property

        Public ReadOnly Property Length As New ImportSymbol With {
            .ImportObject = "length",
            .[Module] = "array",
            .DefinedInModule = False,
            .Name = "array.length",
            .Package = NameOf(Array),
            .result = TypeAbstract.i32,
            .parameters = {
                 "array".param(TypeAlias.list)
            }
        }

        ' ReadOnly arrayOp As Index(Of String) = {GetArrayElement.Name, SetArrayElement.Name}

        Public Function IsArrayOperation(func As FuncSignature) As Boolean
            If func.Module <> NameOf(Array) OrElse Not TypeOf func Is ImportSymbol Then
                Return False
            End If

            ' Return TypeOf func Is ImportSymbol AndAlso func.Name Like arrayOp
            Throw New NotImplementedException
        End Function

        Public Function Method(name As String, ofElement As TypeAbstract) As ImportSymbol
            Select Case name
                Case "Add" : Return Array.Push(ofElement)
                Case "Remove"
                    Throw New NotImplementedException
                Case "Length", "Count"
                    Return Array.Length
                Case Else
                    Throw New NotImplementedException
            End Select
        End Function

        <Extension>
        Public Sub doArrayImports(symbols As SymbolTable, ofElement As TypeAbstract)
            Call symbols.addRequired(JavaScriptImports.NewArray)
            Call symbols.addRequired(JavaScriptImports.Push(ofElement))
            Call symbols.addRequired(JavaScriptImports.GetArrayElement(ofElement))
            Call symbols.addRequired(JavaScriptImports.SetElement(ofElement))
            Call symbols.addRequired(Array.Length)
        End Sub
    End Module
End Namespace
