#Region "Microsoft.VisualBasic::a77bb5f284a76ec1cd235ef82f0c5cc8, Symbols\DeclaredObject\ImportSymbol.vb"

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

    '     Class ImportSymbol
    ' 
    '         Properties: definedInModule, importAlias, package, VBDeclare
    ' 
    '         Constructor: (+2 Overloads) Sub New
    '         Function: ToSExpression, ToString, TypeInfer
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' Imports object can be parse from the VB.NET ``Declare`` statement
    ''' </summary>
    Public Class ImportSymbol : Inherits FuncSignature
        Implements IDeclaredObject

        ''' <summary>
        ''' 外部的模块对象引用名称
        ''' 
        ''' 请注意，这个是外部模块的名称，对于在VB之中申明的这个API，
        ''' 其还存在一个<see cref="[module]"/>标记其在VB工程项目之中的模块名称
        ''' </summary>
        ''' <returns></returns>
        Public Property package As String
        ''' <summary>
        ''' 这个函数对象在外部模块之中的名称字符串
        ''' </summary>
        ''' <returns></returns>
        Public Property importAlias As String

        ''' <summary>
        ''' 这个Api是在用户代码之中定义的
        ''' </summary>
        ''' <returns></returns>
        Public Property definedInModule As Boolean = True

        Public ReadOnly Property VBDeclare As String
            Get
                With parameters _
                    .Select(Function(a) $"{a.Name} As {a.Value}") _
                    .JoinBy(", ")

                    Return $"Declare Function {Name} Lib ""{package}"" Alias ""{importAlias}"" ({ .ByRef}) As {result}"
                End With
            End Get
        End Property

        Sub New()
        End Sub

        Sub New(ParamArray args As NamedValue(Of TypeAbstract)())
            parameters = args
        End Sub

        Public Overrides Function [Call](ParamArray params() As Expression) As Expression
            Return New FuncInvoke(api:=Me) With {
                .[operator] = False,
                .parameters = params
            }
        End Function

        Public Overrides Function ToSExpression() As String
            Dim params$ = parameters _
                .Select(Function(a) a.param) _
                .JoinBy(" ")
            Dim returnType$ = result.typefit
            Dim ref As New ReferenceSymbol With {
                .[module] = If(definedInModule, [module], Nothing),
                .Symbol = Name,
                .Type = SymbolType.Api
            }

            If returnType = "void" Then
                returnType = ""
            Else
                returnType = $"(result {returnType})"
            End If

            Return $";; {VBDeclare}
    (func ${ref} (import ""{package}"" ""{importAlias}"") {params} {returnType})"
        End Function

        Public Overrides Function ToString() As String
            Return ToSExpression()
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return result
        End Function
    End Class
End Namespace
