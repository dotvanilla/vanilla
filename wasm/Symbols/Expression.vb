﻿#Region "Microsoft.VisualBasic::0e8e2a4907f20556757cd033c42fe86b, Symbols\Expression.vb"

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

    '     Class Expression
    ' 
    '         Properties: IsNumberLiteral
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Namespace Symbols

    ''' <summary>
    ''' The abstract ``S-Expression`` model
    ''' </summary>
    Public MustInherit Class Expression

        Public ReadOnly Property IsNumberLiteral As Boolean
            Get
                Return TypeOf Me Is LiteralExpression AndAlso TypeInfer(Nothing).type Like TypeExtensions.NumberOrders
            End Get
        End Property

        Public Overridable ReadOnly Property IsLiteralNothing As Boolean
            Get
                If Not TypeOf Me Is LiteralExpression Then
                    Return False
                Else
                    Return DirectCast(Me, LiteralExpression).IsLiteralNothing
                End If
            End Get
        End Property

        ''' <summary>
        ''' Get the webassembly data type of this expression that will be generated.
        ''' </summary>
        ''' <param name="symbolTable">local/global/function calls</param>
        ''' <returns></returns>
        Public MustOverride Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
        Public MustOverride Function ToSExpression() As String

        ''' <summary>
        ''' S-Expression debug previews
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function ToString() As String
            Return ToSExpression()
        End Function

    End Class
End Namespace
