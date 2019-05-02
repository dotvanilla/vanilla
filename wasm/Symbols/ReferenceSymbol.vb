#Region "Microsoft.VisualBasic::ae4eb878e44776e53f79c238427a358f, Symbols\ReferenceSymbol.vb"

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

    '     Class ReferenceSymbol
    ' 
    '         Properties: [Module], Symbol, Type
    ' 
    '         Function: ToString
    ' 
    '     Enum SymbolType
    ' 
    '         [Operator], Api, GlobalVariable, Type
    ' 
    '  
    ' 
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository

Namespace Symbols

    Public Class ReferenceSymbol : Implements IDeclaredObject

        Public Property [Module] As String Implements IDeclaredObject.Module
        Public Property Symbol As String Implements IKeyedEntity(Of String).Key
        Public Property Type As SymbolType

        Public Overrides Function ToString() As String
            If Type = SymbolType.Operator Then
                Return Symbol
            ElseIf Type = SymbolType.Api Then
                If [Module].StringEmpty Then
                    Return Symbol
                Else
                    Return [Module] & "." & Symbol
                End If
            Else
                Return [Module] & "." & Symbol
            End If
        End Function

        Public Shared Widening Operator CType(func As FuncSignature) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .Type = SymbolType.Func,
                .[Module] = func.Module,
                .Symbol = func.Name
            }
        End Operator

        Public Shared Widening Operator CType(func As ImportSymbol) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .Type = SymbolType.Api,
                .[Module] = If(func.DefinedInModule, func.Module, Nothing),
                .Symbol = func.Name
            }
        End Operator

        Public Shared Widening Operator CType(g As DeclareGlobal) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .Type = SymbolType.GlobalVariable,
                .[Module] = g.Module,
                .Symbol = g.name
            }
        End Operator
    End Class

    Public Enum SymbolType As Byte
        ''' <summary>
        ''' 用户在源代码之中所定义的一个用户函数
        ''' </summary>
        Func = 0
        ''' <summary>
        ''' 从JavaScript外部导入的一个Api函数引用
        ''' </summary>
        Api
        ''' <summary>
        ''' WebAssembly内部的运算符函数
        ''' </summary>
        [Operator]
        ''' <summary>
        ''' 是一个全局变量
        ''' </summary>
        GlobalVariable
        ''' <summary>
        ''' 引用的是一个用户自定义的类型
        ''' </summary>
        Type
    End Enum
End Namespace
