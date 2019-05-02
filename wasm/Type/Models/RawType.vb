﻿#Region "Microsoft.VisualBasic::5b1ce0f6153712a020532876eaddfe09, Type\Models\RawType.vb"

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
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.Language

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

        Public ReadOnly Property WebAssemblyType As TypeAbstract
            Get
                Throw New NotImplementedException
            End Get
        End Property

        Public Function MakeArrayType() As RawType
            Throw New NotImplementedException
        End Function

        Public Function AsGeneric(container As Type) As RawType
            ' Return GetType(System.Collections.Generic.List(Of )).MakeGenericType(element)
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
