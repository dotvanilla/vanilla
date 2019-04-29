#Region "Microsoft.VisualBasic::1838334ad16c0031890e9cbcce846202, Compiler\SymbolTable.vb"

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

'     Class SymbolTable
' 
'         Properties: currentFuncSymbol, currentModuleSymbol, memory, ModuleNames, NextGuid
'                     requires
' 
'         Constructor: (+2 Overloads) Sub New
' 
'         Function: AddFunctionDeclares, GetAllGlobals, GetAllImports, GetAllLocals, getArrayListInternal
'                   GetEnumType, GetFunctionSymbol, GetGlobal, GetObjectReference, GetObjectSymbol
'                   getStringInternal, GetUnderlyingType, HaveEnumType, IsAnyObject, IsLocal
'                   IsModuleFunction, stringContext
' 
'         Sub: AddEnumType, AddGlobal, AddImports, (+2 Overloads) AddLocal, ClearLocals
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.Collection
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Language.Default
Imports Microsoft.VisualBasic.Linq
Imports Wasm.Symbols
Imports Wasm.Symbols.Parser

Namespace Compiler

    ''' <summary>
    ''' A symbol table for type infer
    ''' </summary>
    Public Class SymbolTable

        ''' <summary>
        ''' [name => type]
        ''' </summary>
        Friend ReadOnly globals As New Dictionary(Of String, ModuleOf)
        ''' <summary>
        ''' 包含模块成员函数以及所导入的外部函数
        ''' </summary>
        Friend ReadOnly functionList As New Dictionary(Of String, ModuleOf)
        Friend ReadOnly enumConstants As New Dictionary(Of String, EnumSymbol)

        Dim locals As New Dictionary(Of String, DeclareLocal)
        Dim uid As VBInteger = 666

        ''' <summary>
        ''' 这个内存对象是全局范围内的
        ''' </summary>
        ''' <returns></returns>
        Public Property memory As New Memory

        ''' <summary>
        ''' 当前所进行解析的函数的名称
        ''' </summary>
        ''' <returns></returns>
        Public Property currentFuncSymbol As String
        ''' <summary>
        ''' 当前的VisualBasic模块的名称
        ''' </summary>
        ''' <returns></returns>
        Public Property currentModuleLabel() As DefaultValue(Of String)

        ''' <summary>
        ''' 为了满足基本的变成需求而自动添加的引用符号列表
        ''' </summary>
        ''' <returns></returns>
        Public Property requires As New Index(Of String)

        ''' <summary>
        ''' Generate a guid for loop controls
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NextGuid As String
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return (++uid).ToHexString
            End Get
        End Property

        ''' <summary>
        ''' 获取所有的静态模块名称
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ModuleNames As Index(Of String)
            Get
                Dim globals = Me.globals.Values _
                    .Select(Function(g) g.ModuleLabels) _
                    .IteratesALL _
                    .AsList
                Dim funcs As IEnumerable(Of String) =
                    functionList.Values _
                    .Select(Function(f) f.ModuleLabels) _
                    .IteratesALL

                Return globals + funcs
            End Get
        End Property

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Sub New(module$, methods As IEnumerable(Of MethodBlockSyntax), enums As EnumSymbol())
            For Each constant In enums
                Call AddEnumType(constant)
            Next

            Call AddFunctionDeclares(methods, [module])
        End Sub

        Friend Sub New(ParamArray locals As DeclareLocal())
            For Each var As DeclareLocal In locals
                Call AddLocal(var)
            Next
        End Sub

        Public Sub AddEnumType(type As EnumSymbol)
            Call enumConstants.Add(type.Name, type)
        End Sub

        Public Function HaveEnumType(type As String) As Boolean
            Return enumConstants.ContainsKey(type)
        End Function

        Public Function GetEnumType(type As String) As EnumSymbol
            Return enumConstants(type)
        End Function

        Public Function AddFunctionDeclares(methods As IEnumerable(Of MethodBlockSyntax), module$) As SymbolTable
            For Each method In methods
                With method.FuncVariable(Me)
                    Dim func As New FuncSignature(.ByRef) With {
                        .parameters = method.ParseParameters(Me),
                        .[Module] = [module]
                    }

                    If functionList.ContainsKey(.Name) Then
                        functionList(.Name).Add(func)
                    Else
                        functionList(.Name) = func
                    End If
                End With
            Next

            Return Me
        End Function

        ''' <summary>
        ''' 目标名称引用是否是局部变量或者全局变量所代表的对象实例？
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        Public Function IsAnyObject(name As String) As Boolean
            With name.Trim("$"c, "["c, "]"c)
                If locals.ContainsKey(.ByRef) Then
                    Return True
                ElseIf globals.ContainsKey(.ByRef) Then
                    Return True
                Else
                    Return False
                End If
            End With
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetAllImports() As IEnumerable(Of ImportSymbol)
            Return functionList.Values.OfType(Of ImportSymbol)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetAllGlobals() As IEnumerable(Of DeclareGlobal)
            Return globals.Values.IteratesALL.OfType(Of DeclareGlobal)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetAllLocals() As IEnumerable(Of DeclareLocal)
            Return locals.Values
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub AddImports(api As FuncSignature)
            functionList.Add(api.Name, api)
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub AddLocal([declare] As DeclareLocal)
            Call locals.Add([declare].name, [declare])
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function IsLocal(var As String) As Boolean
            Return locals.ContainsKey(var)
        End Function

        ''' <summary>
        ''' Get global variable type
        ''' </summary>
        ''' <param name="var"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetGlobal(var$, module$) As DeclareGlobal
            Return globals(var).FindSymbol([module])
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub AddGlobal(var$, type As TypeAbstract, moduleName$, init As LiteralExpression)
            Call globals.Add(var, New DeclareGlobal With {.name = var, .type = type, .init = init, .[Module] = moduleName})
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub AddLocal([declare] As NamedValue(Of TypeAbstract))
            Call locals.Add([declare].Name, New DeclareLocal With {.name = [declare].Name, .type = [declare].Value})
        End Sub

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Sub ClearLocals()
            Call locals.Clear()
        End Sub

        Private Function stringContext(context As String) As Boolean
            If context = Types.stringType Then
                Return True
            ElseIf functionList.ContainsKey(context) AndAlso functionList(context).OfType(Of FuncSignature).First.result = TypeAlias.string Then
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' 目标是否是模块内用户定义的一个函数？
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        Public Function IsModuleFunction(name As String) As Boolean
            Return functionList.ContainsKey(name)
        End Function

        ''' <summary>
        ''' 因为VB.NET之中，数组的元素获取和函数调用的语法是一样的，所以假若没有找到目标函数
        ''' 但是在local之中找到了一个数组，则会返回数组元素获取的语法
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="context">
        ''' 当这个上下文值为空值的时候，表示为静态方法
        ''' 这个参数可能是一个对象实例或者类型
        ''' </param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetFunctionSymbol(context$, name$) As FuncSignature
            Dim contextObj As DeclareLocal = Nothing

            If Not context.StringEmpty Then
                contextObj = locals.TryGetValue(context)

                If contextObj Is Nothing Then
                    With globals.TryGetValue(context)
                        If Not .IsNothing Then
                            contextObj = DirectCast(.First, DeclareGlobal).AsLocal
                        End If
                    End With
                End If
            End If

            name = name.Trim("$"c, "["c, "]"c)

            If contextObj Is Nothing Then
                If functionList.ContainsKey(name) Then
                    If context Like ModuleNames Then
                        Return functionList(name).FindSymbol(context)
                    Else
                        Return functionList(name).FindSymbol(currentModuleLabel)
                    End If
                Else
                    If locals.ContainsKey(name) AndAlso locals(name).type.iscollection Then
                        Return JavaScriptImports.GetArrayElement
                    ElseIf Not context.StringEmpty Then
                        ' 可能是类型之中所定义的静态方法
                        If True = stringContext(context) Then
                            Return getStringInternal(name)
                        ElseIf context Like ModuleNames Then
                            ' 模块变量或者方法的引用

                            If globals.ContainsKey(name) AndAlso Not globals(name).FindSymbol(context) Is Nothing Then
                                ' 因为需要查找的是函数对象
                                ' 但是在这里却找到了一个全局变量，所以需要返回空值
                                Return Nothing
                            End If

                            ' 接下来则尝试查找模块方法
                            If functionList.ContainsKey(name) Then
                                Dim moduleFunc As FuncSignature = functionList(name).FindSymbol(context)

                                If moduleFunc Is Nothing Then
                                    Return Nothing
                                Else
                                    Return moduleFunc
                                End If
                            End If
                        Else
                            Throw New NotImplementedException
                        End If
                    Else
                        Throw New MissingPrimaryKeyException(name)
                    End If
                End If
            Else
                Dim func As FuncSignature = functionList.TryGetValue(name).FindSymbol(currentFuncSymbol)

                If Not func Is Nothing AndAlso CTypeHandle.EqualOfType(func.parameters.First, contextObj.type) Then
                    Return func
                Else
                    If contextObj.type = TypeAlias.list OrElse contextObj.type = TypeAlias.array Then
                        Call JavaScriptImports.doArrayImports(Me)
                        ' 可能是是一个List
                        ' 将List的实例方法映射到javascript的array相关的api上面
                        Return getArrayListInternal(name)
                    ElseIf contextObj.type = TypeAlias.string Then
                        Return getStringInternal(name)
                    ElseIf contextObj.type = TypeAlias.table Then
                        Return getTableInternal(name)
                    Else
                        Throw New NotImplementedException
                    End If
                End If
            End If
        End Function

        Private Function getTableInternal(name As String) As FuncSignature
            Dim Api As ImportSymbol = JavaScriptImports.Dictionary.Method(name)
            Call addRequired(Api)
            Return Api
        End Function

        Private Function getArrayListInternal(name As String) As FuncSignature
            Dim Api As ImportSymbol = JavaScriptImports.Array.Method(name)
            Call addRequired(Api)
            Return Api
        End Function

        Private Function getStringInternal(name As String) As FuncSignature
            Dim Api As ImportSymbol = JavaScriptImports.String.Method(name)
            Call addRequired(Api)
            Return Api
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetObjectSymbol(name As String) As DeclareLocal
            Return locals(name.Trim("$"c))
        End Function

        ''' <summary>
        ''' Get type of the variable
        ''' </summary>
        ''' <param name="name"></param>
        ''' <returns></returns>
        Public Function GetUnderlyingType(name As String) As TypeAbstract
            If IsLocal(name) Then
                Return GetObjectSymbol(name).type
            Else
                Return GetGlobal(name, currentModuleLabel).type
            End If
        End Function

        ''' <summary>
        ''' A unify method for get local or get global variable
        ''' </summary>
        ''' <param name="name">优先选取local</param>
        ''' <returns></returns>
        Public Function GetObjectReference(name As String) As GetLocalVariable
            If IsLocal(name) Then
                Return New GetLocalVariable(name)
            ElseIf globals.ContainsKey(name) Then
                Return New GetGlobalVariable(name)
            Else
                Return Nothing
            End If
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Narrowing Operator CType(symbols As SymbolTable) As Memory
            Return symbols.memory
        End Operator
    End Class
End Namespace
