#Region "Microsoft.VisualBasic::221b037bb6c5b86dc3f68eb3460d59b7, Type\Models\TypeAbstract.vb"

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

    '     Class TypeAbstract
    ' 
    '         Properties: class_id, f32, f64, generic, i32
    '                     i64, iscollection, isprimitive, raw, type
    '                     typefit, void
    ' 
    '         Constructor: (+8 Overloads) Sub New
    ' 
    '         Function: buildRaw, MakeArrayType, MakeListType, ToString
    ' 
    '         Sub: fromFullName
    ' 
    '         Operators: (+3 Overloads) <>, (+3 Overloads) =
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.SyntaxAnalysis
Imports r = System.Text.RegularExpressions.Regex

Namespace TypeInfo

    ''' <summary>
    ''' Type model in WebAssembly compiler
    ''' </summary>
    Public Class TypeAbstract

        Public Property type As TypeAlias
        ''' <summary>
        ''' Generic type arguments in VisualBasic.NET language.
        ''' </summary>
        ''' <returns></returns>
        Public Property generic As TypeAbstract()
        ''' <summary>
        ''' The raw definition: <see cref="System.Type.FullName"/>
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 如果是用户的自定义类型的话，则在字符串的前面还存在一个前缀用来保存class_id
        ''' 格式，例如：
        ''' 
        ''' ```
        ''' [123]class_name
        ''' ```
        ''' </remarks>
        Public Property raw As String

        ''' <summary>
        ''' 注意，因为存在类型申明的meta信息的缘故，class_id永远都不会小于10，
        ''' 所以从这个只读属性得到的class_id是小于10的话，则说明是基础类型或者object类型
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property class_id As Integer
            Get
                If type <> TypeAlias.intptr Then
                    Return CInt(type)
                Else
                    Return r.Match(raw, "\[\d+\]", RegexICSng) _
                        .Value _
                        .GetStackValue("[", "]") _
                        .ParseInteger
                End If
            End Get
        End Property

        ''' <summary>
        ''' Type symbol for generate S-Expression.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property typefit As String
            Get
                Return CTypeHandle.typefit(type)
            End Get
        End Property

        ''' <summary>
        ''' 当前的类型是否是WebAssembly之中的4个基础类型
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property isprimitive As Boolean
            Get
                Return type Like TypeExtensions.NumberOrders
            End Get
        End Property

        Public ReadOnly Property iscollection As Boolean
            Get
                Return type = TypeAlias.list OrElse type = TypeAlias.table
            End Get
        End Property

#Region "WebAssembly Primitive Types"
        Public Shared ReadOnly Property i32 As New TypeAbstract("i32")
        Public Shared ReadOnly Property i64 As New TypeAbstract("i64")
        Public Shared ReadOnly Property f32 As New TypeAbstract("f32")
        Public Shared ReadOnly Property f64 As New TypeAbstract("f64")

        ''' <summary>
        ''' Expression returns no value
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property void As New TypeAbstract("void")
#End Region

        ''' <summary>
        ''' For json serialization
        ''' </summary>
        Sub New()
        End Sub

        Sub New(raw As RawType, symbols As SymbolTable)
            Call Me.New(raw.WebAssembly(symbols))
        End Sub

        Sub New(type As Type)
            Dim isList As Boolean = type.IsInheritsFrom(GetType(List(Of )), strict:=False)

            If isList Then
                Me.type = TypeAlias.list
                Me.generic = {New TypeAbstract(type.GenericTypeArguments(Scan0))}
                Me.raw = buildRaw(TypeAlias.list, generic)
            ElseIf type.IsArray Then
                Me.type = TypeAlias.array
                Me.generic = {New TypeAbstract(type.GetElementType)}
                Me.raw = buildRaw(TypeAlias.array, generic)
            Else
                Call fromFullName(type.TypeName)
            End If
        End Sub

        ''' <summary>
        ''' Object value copy
        ''' </summary>
        ''' <param name="type"></param>
        Sub New(type As TypeAbstract)
            Me.generic = type.generic _
                .SafeQuery _
                .Select(Function(gr) New TypeAbstract(gr)) _
                .ToArray
            Me.type = type.type
            Me.raw = type.raw
        End Sub

        Sub New([alias] As TypeAlias, ref As ReferenceSymbol)
            Me.generic = {}
            Me.type = [alias]
            Me.raw = ref.ToString
        End Sub

        Private Sub fromFullName(fullName As String)
            If TypeExtensions.IsArray(fullName) Then
                _type = TypeAlias.array
                _generic = {Types.ArrayElement(fullName)}
            Else
                _type = Types.ParseAliasName(fullName)
            End If

            _raw = fullName
        End Sub

        Sub New(fullName As String)
            Call fromFullName(fullName)
        End Sub

        Sub New([alias] As TypeAlias, Optional generic$() = Nothing)
            Me.type = [alias]
            Me.generic = generic _
                .SafeQuery _
                .Select(Function(type) New TypeAbstract(type)) _
                .ToArray
            Me.raw = buildRaw(type, Me.generic)
        End Sub

        ''' <summary>
        ''' array or generic list
        ''' </summary>
        ''' <param name="type"></param>
        ''' <param name="element"></param>
        Private Sub New(type As TypeAlias, element As TypeAbstract)
            Me.type = type
            Me.generic = {element}
            Me.raw = buildRaw(type, generic)
        End Sub

        Private Shared Function buildRaw(type As TypeAlias, generic As TypeAbstract()) As String
            Static otherSingles As TypeAlias() = {
                TypeAlias.boolean,
                TypeAlias.any,
                TypeAlias.intptr,
                TypeAlias.string,
                TypeAlias.void
            }
            Static singleElements As Index(Of TypeAlias) = TypeExtensions.NumberOrders _
                .Objects _
                .Join(otherSingles) _
                .ToArray

            If type Like singleElements Then
                Return type.Description
            ElseIf type = TypeAlias.array OrElse type = TypeAlias.list Then
                If generic.IsNullOrEmpty Then
                    Return "any[]"
                Else
                    Return generic(Scan0).raw & "[]"
                End If
            ElseIf type = TypeAlias.table Then
                If generic.IsNullOrEmpty Then
                    Return "[any]"
                Else
                    Return $"[{generic(Scan0).raw}]"
                End If
            Else
                Throw New NotImplementedException
            End If
        End Function

        Public Function MakeArrayType() As TypeAbstract
            Return New TypeAbstract(TypeAlias.array, Me)
        End Function

        Public Function MakeListType() As TypeAbstract
            Return New TypeAbstract(TypeAlias.list, Me)
        End Function

        Public Overrides Function ToString() As String
            If generic.IsNullOrEmpty Then
                Return type.Description
            Else
                Return $"{type.Description}(Of {generic.JoinBy(", ")})"
            End If
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Operator <>(type As TypeAbstract, name As TypeAlias) As Boolean
            Return type.type <> name
        End Operator

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Operator =(type As TypeAbstract, name As TypeAlias) As Boolean
            Return type.type = name
        End Operator

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Operator <>(type As TypeAbstract, name$) As Boolean
            Return type.type.ToString <> name
        End Operator

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Operator =(type As TypeAbstract, name$) As Boolean
            Return type.type.ToString = name
        End Operator

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Operator <>(type As TypeAbstract, another As TypeAbstract) As Boolean
            Return Not type = another
        End Operator

        Public Shared Operator =(type As TypeAbstract, another As TypeAbstract) As Boolean
            If type.type <> another.type Then
                Return False
            End If

            Return True
        End Operator
    End Class
End Namespace
