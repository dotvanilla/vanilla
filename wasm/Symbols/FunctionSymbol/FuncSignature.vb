﻿#Region "Microsoft.VisualBasic::ff1e4335fee283aa53fe6d43de0c747c, Symbols\FunctionSymbol\FuncSignature.vb"

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

    '     Class FuncSignature
    ' 
    '         Properties: [module], isExtensionMethod, name, parameters, result
    ' 
    '         Constructor: (+2 Overloads) Sub New
    '         Function: ToSExpression, ToString, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' The abstract of the function object, only have function name, parameter and result type definition.
    ''' </summary>
    Public Class FuncSignature : Inherits NoReferenceExpression
        Implements INamedValue
        Implements IDeclaredObject

        ''' <summary>
        ''' 函数在WebAssembly模块内部的引用名称字符串
        ''' </summary>
        ''' <returns></returns>
        Public Property name As String Implements IKeyedEntity(Of String).Key

        ''' <summary>
        ''' The function parameter name and parameter type
        ''' </summary>
        ''' <returns></returns>
        Public Property parameters As NamedValue(Of TypeAbstract)()

        ''' <summary>
        ''' 函数的返回值类型
        ''' </summary>
        ''' <returns></returns>
        Public Property result As TypeAbstract

        ''' <summary>
        ''' 当前的这个方法是否是一个被<see cref="ExtensionAttribute"/>所标记的拓展函数
        ''' </summary>
        ''' <returns></returns>
        Public Property isExtensionMethod As Boolean

        ''' <summary>
        ''' VB module name
        ''' </summary>
        ''' <returns></returns>
        Public Property [module] As String Implements IDeclaredObject.module

        Friend Sub New()
        End Sub

        Friend Sub New(var As NamedValue(Of TypeAbstract))
            name = var.Name
            result = var.Value
        End Sub

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return result
        End Function

        Public Overridable Function [Call](ParamArray params As Expression()) As Expression
            Return New FuncInvoke(Me) With {
                .[operator] = False,
                .parameters = params
            }
        End Function

        Public Overrides Function ToSExpression() As String
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ToString() As String
            With parameters _
                    .Select(Function(a)
                                If a.Description Is Nothing Then
                                    Return $"{a.Name} As {a.Value}"
                                Else
                                    ' 默认参数
                                    Return $"{a.Name} As {a.Value} [ = {a.Description}]"
                                End If
                            End Function) _
                    .JoinBy(", ")

                Return $"Public Function {name}({ .ByRef}) As {result}"
            End With
        End Function
    End Class
End Namespace
