﻿#Region "Microsoft.VisualBasic::4d24ac42461a76a6d39f296ddfdcee78, Symbols\EnumSymbol.vb"

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

    '     Class EnumSymbol
    ' 
    '         Properties: [module], Members, Name, type, UnderlyingType
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: ToString, wasmUnderlying
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Wasm.SyntaxAnalysis
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' The enum type object model
    ''' </summary>
    Public Class EnumSymbol : Implements IDeclaredObject

        ''' <summary>
        ''' The enum type name
        ''' </summary>
        ''' <returns></returns>
        Public Property Name As String Implements IKeyedEntity(Of String).Key
        ''' <summary>
        ''' WebAssembly Type: i32 or i64
        ''' </summary>
        ''' <returns></returns>
        Public Property type As String

        ''' <summary>
        ''' [member name => value]
        ''' </summary>
        ''' <returns></returns>
        Public Property Members As New Dictionary(Of String, String)

        Public ReadOnly Property UnderlyingType As Type
            Get
                If type = "i32" Then
                    Return GetType(Integer)
                Else
                    Return GetType(Long)
                End If
            End Get
        End Property

        Public Property [module] As String Implements IDeclaredObject.module

        Sub New(constants As EnumBlockSyntax)
            With constants.EnumStatement
                Name = .Identifier.objectName

                If .UnderlyingType Is Nothing Then
                    type = "i32"
                Else
                    type = wasmUnderlying(.UnderlyingType)
                End If
            End With

            Dim last As Long = 0
            Dim memberName$
            Dim value As String

            For Each member As EnumMemberDeclarationSyntax In constants.Members
                memberName = member.Identifier.objectName

                If member.Initializer Is Nothing Then
                    value = last
                    last += 1
                Else
                    value = member.Initializer.Value.ToString
                    last = value + 1
                End If

                Members.Add(memberName, value)
            Next
        End Sub

        ''' <summary>
        ''' 枚举类型肯定不是用户自定义类型
        ''' </summary>
        ''' <param name="[declare]"></param>
        ''' <returns></returns>
        Private Function wasmUnderlying([declare] As AsClauseSyntax) As String
            Dim raw As RawType = AsTypeHandler.GetAsType([declare], Nothing)
            Dim wasm As String = raw.WebAssembly(Nothing).typefit

            Return wasm
        End Function

        Public Overrides Function ToString() As String
            Return $"Dim {Name} As {type}"
        End Function
    End Class
End Namespace
