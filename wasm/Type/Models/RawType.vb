﻿#Region "Microsoft.VisualBasic::b1665f4c776e2d4b4c0d2792e1422a57, Type\Models\RawType.vb"

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

    '     Class RawType
    ' 
    '         Properties: IsArray, IsUserDefined
    ' 
    '         Constructor: (+2 Overloads) Sub New
    '         Function: AsGeneric, asReference, castType, MakeArrayType, ToString
    '                   WebAssembly
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.Symbols.MemoryObject

Namespace TypeInfo

    ''' <summary>
    ''' Type in .NET Framework
    ''' </summary>
    Public Class RawType

        Dim raw As [Variant](Of String, Type)

        ''' <summary>
        ''' 是否是用户自定义的类型?
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsUserDefined As Boolean
            Get
                Return raw Like GetType(String)
            End Get
        End Property

        ''' <summary>
        ''' 是否是一个数组类型？
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property IsArray As Boolean
            Get
                If raw Like GetType(String) Then
                    Return TypeExtensions.IsArray(raw.TryCast(Of String))
                Else
                    Return raw.TryCast(Of Type).IsArray
                End If
            End Get
        End Property

        Sub New()
        End Sub

        Sub New(rawString As String)
            Me.raw = rawString
        End Sub

        ''' <summary>
        ''' 获取得到WebAssembly编译器的中间类型
        ''' </summary>
        ''' <param name="symbols">如果目标是用户自定义类型，则这个参数必须不为空</param>
        ''' <returns></returns>
        Public Function WebAssembly(symbols As SymbolTable) As TypeAbstract
            If IsUserDefined AndAlso symbols Is Nothing Then
                Throw New InvalidProgramException
            End If

            If IsUserDefined Then
                Return castType(symbols)
            Else
                Return New TypeAbstract(type:=raw.TryCast(Of Type))
            End If
        End Function

        Private Function castType(symbols As SymbolTable) As TypeAbstract
            If TypeExtensions.IsArray(raw) Then
                ' 可能是其他的数组
                Dim base As TypeAlias = TypeAlias.array
                Dim ofElement As RawType = TypeExtensions.ArrayElement(raw)

                If ofElement.IsArray Then
                    ' possibably it is a rectangle array
                    Return New TypeAbstract(base, ofElement.WebAssembly(symbols))
                Else
                    Dim className$ = ofElement.raw.TryCast(Of String)
                    Dim class_id As Integer = symbols _
                        .GetClassType(className) _
                        .memoryPtr

                    Return New TypeAbstract(base, {$"[{class_id}]{ofElement}"})
                End If
            Else
                Return New TypeAbstract(TypeAlias.intptr, asReference(symbols))
            End If
        End Function

        ''' <summary>
        ''' 在这里主要是从符号表之中获取得到模块以及名称的引用
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        Private Function asReference(symbols As SymbolTable) As ReferenceSymbol
            Dim typeName As String = raw
            Dim type As ClassMeta = symbols.GetClassType(typeName)
            Dim class_id As Integer = type.memoryPtr

            Return New ReferenceSymbol With {
                .symbol = $"[{class_id}]{typeName}",
                .type = SymbolType.Type
            }
        End Function

        Public Function MakeArrayType() As RawType
            If IsUserDefined Then
                Return raw.TryCast(Of String) & "[]"
            Else
                Return raw.TryCast(Of Type).MakeArrayType
            End If
        End Function

        Public Function AsGeneric(container As Type) As RawType
            If raw Like GetType(String) Then
                Return container.MakeGenericType(Type.GetType(raw.TryCast(Of String)))
            Else
                Return container.MakeGenericType(raw.TryCast(Of Type))
            End If
        End Function

        Public Overrides Function ToString() As String
            If raw Like GetType(String) Then
                Return raw
            Else
                Return raw.TryCast(Of Type).FullName
            End If
        End Function

        Public Shared Widening Operator CType(type As Type) As RawType
            Return New RawType With {.raw = type}
        End Operator

        Public Shared Widening Operator CType(name As String) As RawType
            Return New RawType With {.raw = name}
        End Operator

    End Class
End Namespace
