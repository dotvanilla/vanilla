#Region "Microsoft.VisualBasic::10b9aba99f2d2df7eaf62c425b3ca8eb, Type\TypeAbstract.vb"

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

    ' Class TypeAbstract
    ' 
    '     Properties: f32, f64, generic, i32, i64
    '                 raw, type, typefit
    ' 
    '     Constructor: (+3 Overloads) Sub New
    '     Function: ToString
    '     Operators: <>, =
    ' 
    ' /********************************************************************************/

#End Region

''' <summary>
''' Type model in WebAssembly compiler
''' </summary>
Public Class TypeAbstract

    Public ReadOnly Property type As TypeAlias
    ''' <summary>
    ''' Generic type arguments in VisualBasic.NET language.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property generic As String()
    ''' <summary>
    ''' The raw definition: <see cref="System.Type.FullName"/>
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property raw As String

    ''' <summary>
    ''' Type symbol for generate S-Expression.
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property typefit As String
        Get
            Return CTypeHandle.typefit(type)
        End Get
    End Property

#Region "WebAssembly Primitive Types"
    Public Shared ReadOnly Property i32 As New TypeAbstract("i32")
    Public Shared ReadOnly Property i64 As New TypeAbstract("i64")
    Public Shared ReadOnly Property f32 As New TypeAbstract("f32")
    Public Shared ReadOnly Property f64 As New TypeAbstract("f64")
#End Region

    Sub New(type As Type)

    End Sub

    Sub New(fullName As String)
        type = Types.ParseAliasName(fullName)
        raw = fullName
    End Sub

    Sub New([alias] As TypeAlias, Optional generic$() = Nothing)
        Me.type = [alias]
        Me.generic = generic
    End Sub

    Public Overrides Function ToString() As String
        If generic.IsNullOrEmpty Then
            Return type.Description
        Else
            Return $"{type.Description}(Of {generic.JoinBy(", ")})"
        End If
    End Function

    Public Shared Operator <>(type As TypeAbstract, name$) As Boolean
        Return type.type.ToString <> name
    End Operator

    Public Shared Operator =(type As TypeAbstract, name$) As Boolean
        Return type.type.ToString = name
    End Operator
End Class
