#Region "Microsoft.VisualBasic::a41995ac4f5e8aff5e301c02798329ef, Symbols\Memory\ArraySymbol.vb"

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

    '     Class ArraySymbol
    ' 
    '         Properties: Initialize
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    '     Class AbstractArray
    ' 
    '         Properties: type
    ' 
    '     Class Array
    ' 
    '         Properties: size
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    '     Class ArrayTable
    ' 
    '         Properties: initialVal, key
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Symbols

    ''' <summary>
    ''' Expression for create an new javascript array with initialize values
    ''' </summary>
    Public Class ArraySymbol : Inherits AbstractArray

        Public Property Initialize As Expression()

        Public Overrides Function ToSExpression() As String
            ' create array object in javascript runtime
            Dim newArray As New FuncInvoke(JavaScriptImports.Array.NewArray) With {
                .parameters = {Literal.i32(-1)}
            }

            If Initialize.IsNullOrEmpty Then
                ' 空数组
                Return newArray.ToSExpression
            End If

            Dim arrayPush$ = JavaScriptImports.PushArray.Name
            Dim array As Expression = New FuncInvoke(arrayPush) With {
                .parameters = {newArray, Initialize(Scan0)}
            }

            ' and then push elements into that new array
            For Each value As Expression In Initialize.Skip(1)
                array = New FuncInvoke(arrayPush) With {
                    .parameters = {array, value}
                }
            Next

            ' at last return new array intptr
            Return array.ToSExpression
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function
    End Class

    Public MustInherit Class AbstractArray : Inherits Expression

        ''' <summary>
        ''' Element type name or Data type of array table value
        ''' </summary>
        ''' <returns></returns>
        Public Property type As TypeAbstract

    End Class

    ''' <summary>
    ''' Expression for create a new javascript array with given length
    ''' </summary>
    Public Class Array : Inherits AbstractArray

        Public Property size As Expression

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        Public Overrides Function ToSExpression() As String
            Dim newArray As New FuncInvoke(JavaScriptImports.NewArray) With {
                .parameters = {size}
            }

            Return newArray.ToSExpression
        End Function
    End Class

    ''' <summary>
    ''' Javascript object
    ''' </summary>
    Public Class ArrayTable : Inherits AbstractArray

        ''' <summary>
        ''' Data type of array table key
        ''' </summary>
        ''' <returns></returns>
        Public Property key As TypeAbstract

        Public Property initialVal As (key As Expression, value As Expression)()

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        Public Overrides Function ToSExpression() As String
            ' create new table
            Dim newTable As New FuncInvoke(JavaScriptImports.Dictionary.Create) With {
                .parameters = {}
            }

            If initialVal.IsNullOrEmpty Then
                Return newTable.ToSExpression
            End If

            Dim tableAppend = JavaScriptImports.Dictionary.SetValue.Name
            Dim table As Expression = New FuncInvoke(tableAppend) With {
                .parameters = {
                    newTable,
                    initialVal(0).key,
                    initialVal(0).value
                }
            }

            For Each value In initialVal.Skip(1)
                table = New FuncInvoke(tableAppend) With {
                    .parameters = {table, value.key, value.value}
                }
            Next

            Return table.ToSExpression
        End Function
    End Class
End Namespace
