﻿#Region "Microsoft.VisualBasic::001ee10ac219f130c708ff8dbaac6ab0, Symbols\Symbols.vb"

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

'     Class NoReferenceExpression
' 
'         Function: GetSymbolReference
' 
'     Class CommentText
' 
'         Properties: text
' 
'         Constructor: (+2 Overloads) Sub New
'         Function: ToSExpression, TypeInfer
' 
'     Class LiteralExpression
' 
'         Properties: IsLiteralNothing, Sign, type, value
' 
'         Constructor: (+2 Overloads) Sub New
'         Function: ToSExpression, TypeInfer
' 
'     Class GetLocalVariable
' 
'         Properties: var
' 
'         Constructor: (+2 Overloads) Sub New
'         Function: GetSymbolReference, ToSExpression, TypeInfer
' 
'     Class SetLocalVariable
' 
'         Properties: value, var
' 
'         Constructor: (+2 Overloads) Sub New
'         Function: GetSymbolReference, ToSExpression, TypeInfer
' 
'     Class GetGlobalVariable
' 
'         Properties: [module]
' 
'         Constructor: (+3 Overloads) Sub New
'         Function: ToSExpression, TypeInfer
' 
'     Class SetGlobalVariable
' 
'         Properties: [module]
' 
'         Constructor: (+2 Overloads) Sub New
'         Function: ToSExpression, TypeInfer
' 
'     Class DeclareLocal
' 
'         Properties: GetReference, SetLocal
' 
'         Function: ToSExpression
' 
'     Class DeclareVariable
' 
'         Properties: init, isConst, name, type
' 
'         Function: GetSymbolReference, TypeInfer
' 
'     Class Parenthesized
' 
'         Properties: Internal
' 
'         Function: GetSymbolReference, ToSExpression, TypeInfer
' 
'     Class ReturnValue
' 
'         Function: ToSExpression
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection.Generic
Imports Microsoft.VisualBasic.Text
Imports Wasm.Compiler
Imports Wasm.Compiler.SExpression
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Symbols

    ''' <summary>
    ''' This expression tree have no symbol reference.
    ''' </summary>
    Public MustInherit Class NoReferenceExpression : Inherits Expression

        ''' <summary>
        ''' Reference no symbols
        ''' </summary>
        ''' <returns></returns>
        Public Overrides Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Return {}
        End Function
    End Class

    Public Class CommentText : Inherits NoReferenceExpression

        Public Property text As String

        Sub New()
        End Sub

        Sub New([rem] As String)
            text = [rem]
        End Sub

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.void)
        End Function

        Public Overrides Function ToSExpression() As String
            Return ";; " & text
        End Function

        Public Shared Narrowing Operator CType([rem] As CommentText) As String
            Return [rem].ToSExpression
        End Operator

    End Class

    ''' <summary>
    ''' 常量值表达式
    ''' </summary>
    Public Class LiteralExpression : Inherits NoReferenceExpression

        Public Property type As TypeAbstract
        Public Property value As String

        ''' <summary>
        ''' 返回常数的符号
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Sign As Integer
            Get
                If isNumberLiteral Then
                    Return Math.Sign(Double.Parse(value))
                Else
                    Throw New InvalidOperationException($"Constant value '{value}' is not numeric type!")
                End If
            End Get
        End Property

        ''' <summary>
        ''' Current literal expression is a literal token expression of 
        ''' ``Nothing`` or ``null`` in general programing language.
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property isLiteralNothing As Boolean
            Get
                Select Case type.type
                    Case TypeAlias.boolean, TypeAlias.f32, TypeAlias.f64, TypeAlias.i32, TypeAlias.i64, TypeAlias.void, TypeAlias.string
                        ' 基础类型总是有一个默认值零的
                        ' 所以不是Nothing
                        Return False
                    Case Else
                        ' 对于其他类型，只要是零就表示可能是Nothing
                        Return value = "0"
                End Select
            End Get
        End Property

        Sub New()
        End Sub

        Sub New(value$, type As TypeAbstract)
            Me.type = type
            Me.value = value
        End Sub

        Public Overrides Function ToSExpression() As String
            Return $"({type.typefit}.const {value})"
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function
    End Class

    ''' <summary>
    ''' Reference to a variable name <see cref="GetLocalVariable.var"/>
    ''' </summary>
    Public Class GetLocalVariable : Inherits Expression

        Public Property var As String

        Sub New(Optional ref As String = Nothing)
            var = ref
        End Sub

        Sub New(local As DeclareLocal)
            Call Me.New(local.name)
        End Sub

        Public Overrides Function ToSExpression() As String
            Return $"(get_local ${var})"
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            If symbolTable Is Nothing Then
                Return New TypeAbstract(TypeAlias.i32)
            Else
                Return symbolTable.GetObjectSymbol(var).type
            End If
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Yield New ReferenceSymbol With {.symbol = var, .type = SymbolType.LocalVariable}
        End Function
    End Class

    Public Class SetLocalVariable : Inherits Expression

        Public Property var As String
        Public Property value As Expression

        Sub New()
        End Sub

        Sub New(var As DeclareLocal, value As Expression)
            Me.var = var.name
            Me.value = value
        End Sub

        Sub New(var$, value As Expression)
            Me.var = var
            Me.value = value
        End Sub

        Public Overrides Function ToSExpression() As String
            If TypeOf value Is FuncInvoke Then
                Return $"(set_local ${var} {value})"
            Else
                Return $"(set_local ${var} {value})"
            End If
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.void)
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Yield New ReferenceSymbol With {.symbol = var, .type = SymbolType.LocalVariable}

            For Each symbol In value.GetSymbolReference
                Yield symbol
            Next
        End Function
    End Class

    Public Class GetGlobalVariable : Inherits GetLocalVariable

        Public Property [module] As String

        Sub New()
        End Sub

        Sub New([global] As DeclareGlobal)
            Call Me.New([global].module, [global].name)
        End Sub

        Sub New(module$, name As String)
            Me.var = name
            Me.module = [module]
        End Sub

        Public Overrides Function ToSExpression() As String
            Return $"(get_global ${[module]}.{var})"
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return symbolTable.FindModuleGlobal([module], var).type
        End Function
    End Class

    Public Class SetGlobalVariable : Inherits SetLocalVariable

        Public Property [module] As String

        Sub New()
        End Sub

        Sub New([global] As DeclareGlobal, Optional value As Expression = Nothing)
            Me.var = [global].name
            Me.module = [global].module
            Me.value = value
        End Sub

        Public Overrides Function ToSExpression() As String
            Return $"(set_global ${[module]}.{var} {value})"
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return New TypeAbstract(TypeAlias.void)
        End Function
    End Class

    Public Class DeclareLocal : Inherits DeclareVariable

        ''' <summary>
        ''' 对这个变量进行初始值设置
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SetLocal As SetLocalVariable
            Get
                Return New SetLocalVariable With {
                    .var = name,
                    .value = init
                }
            End Get
        End Property

        Public Overrides ReadOnly Property GetReference() As GetLocalVariable
            Get
                Return New GetLocalVariable With {.var = name}
            End Get
        End Property

        Sub New()
        End Sub

        Sub New(name$, type As TypeAbstract)
            Me.name = name
            Me.type = type
        End Sub

        Public Overrides Function ToSExpression() As String
            Return $"(local ${name} {type.typefit})"
        End Function

    End Class

    Public MustInherit Class DeclareVariable : Inherits Expression
        Implements INamedValue

        ''' <summary>
        ''' 变量名称
        ''' </summary>
        ''' <returns></returns>
        Public Property name As String Implements INamedValue.Key
        ''' <summary>
        ''' 变量的类型
        ''' </summary>
        ''' <returns></returns>
        Public Property type As TypeAbstract
        ''' <summary>
        ''' 是否是常量
        ''' </summary>
        ''' <returns></returns>
        Public Property isConst As Boolean = False

        ''' <summary>
        ''' 初始值，对于全局变量而言，则必须要有一个初始值，全局变量默认的初始值为零
        ''' </summary>
        ''' <returns></returns>
        Public Property init As Expression

        Public MustOverride ReadOnly Property GetReference() As GetLocalVariable

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return type
        End Function

        Public Overrides Iterator Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Yield GetReference.GetSymbolReference

            For Each symbol As ReferenceSymbol In init.GetSymbolReference
                Yield symbol
            Next
        End Function
    End Class

    ''' <summary>
    ''' 一个括号表达式
    ''' </summary>
    Public Class Parenthesized : Inherits Expression

        Public Property Internal As Expression

        Public Overrides Function ToSExpression() As String
            Return $"{Internal}"
        End Function

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return Internal.TypeInfer(symbolTable)
        End Function

        Public Overrides Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Return Internal.GetSymbolReference
        End Function
    End Class

    Public Class ReturnValue : Inherits Parenthesized

        Sub New()
        End Sub

        Sub New(value As Expression)
            Me.Internal = value
        End Sub

        Public Overrides Function ToSExpression() As String
            Dim sexp$

            If TypeOf Internal Is ArrayBlock Then
                sexp = DirectCast(Internal, ArrayBlock) _
                    .arrayInitialize _
                    .JoinBy(ASCII.LF)
                sexp = sexp & ASCII.LF & $"(return {Internal.ToSExpression})"
            ElseIf TypeOf Internal Is UserObject Then
                sexp = DirectCast(Internal, UserObject) _
                   .objectInitialize _
                   .JoinBy(ASCII.LF)
                sexp = sexp & ASCII.LF & $"(return {Internal.ToSExpression})"
            Else
                sexp = $"(return {Internal.ToSExpression})"
            End If

            Return sexp
        End Function
    End Class
End Namespace
