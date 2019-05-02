#Region "Microsoft.VisualBasic::1adc7ce6dcfa4ffb1731b180520f74f4, Symbols\Parser\AsTypeHandler.vb"

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

    '     Module AsTypeHandler
    ' 
    '         Function: [GetType], AsType, GetAsType, GetGenericType, listOf
    '                   TypeName
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.Parser

    ''' <summary>
    ''' 因为VB的变量可以使用TypeChar和As这两种形式的申明
    ''' 所以在这里对变量类型的申明解析会比较复杂一些
    ''' </summary>
    Module AsTypeHandler

        ''' <summary>
        ''' 这个函数返回WASM之中的基本数据类型
        ''' </summary>
        ''' <param name="name"></param>
        ''' <param name="asClause"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 当类型申明是空的时候，应该是从其初始化值得类型来推断申明的
        ''' </remarks>
        <Extension>
        Public Function AsType(ByRef name$, [asClause] As AsClauseSyntax, symbols As SymbolTable, Optional initType As TypeAbstract = Nothing) As TypeAbstract
            Dim type As TypeAbstract

            If Not asClause Is Nothing Then
                If TypeOf asClause Is SimpleAsClauseSyntax Then
                    type = New TypeAbstract(GetAsType(asClause, symbols))
                ElseIf TypeOf asClause Is AsNewClauseSyntax Then
                    Dim [new] As ObjectCreationExpressionSyntax = DirectCast(asClause, AsNewClauseSyntax).NewExpression
                    Dim objType As Type = AsTypeHandler.GetType([new].Type, symbols)

                    type = New TypeAbstract(objType)
                Else
                    Throw New NotImplementedException
                End If
            ElseIf name.Last Like Patterns.TypeChar Then
                type = New TypeAbstract(TypeExtensions.TypeCharWasm(name.Last))
                name = name.Substring(0, name.Length - 1)
            Else
                type = initType
            End If

            Return type
        End Function

        ''' <summary>
        ''' 如果目标存在于<see cref="TypeExtensions.Convert2Wasm"/>，则返回对应的类型别名
        ''' 否则则返回类型的全名称<see cref="System.Type.FullName"/>
        ''' </summary>
        ''' <param name="type"></param>
        ''' <returns></returns>
        <Extension> Public Function TypeName(type As Type) As String
            With type
                If TypeExtensions.Convert2Wasm.ContainsKey(.ByRef) Then
                    Return TypeExtensions.Convert2Wasm(.ByRef)
                Else
                    Return .FullName
                End If
            End With
        End Function

        ''' <summary>
        ''' Parse type define from [``As Type``] expression.
        ''' </summary>
        ''' <param name="[as]">``As Type``</param>
        ''' <param name="symbols"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetAsType([as] As SimpleAsClauseSyntax, symbols As SymbolTable) As Type
            If [as] Is Nothing Then
                Return GetType(System.Void)
            Else
                Return [GetType]([as].Type, symbols)
            End If
        End Function

        <Extension>
        Public Function GetGenericType(generic As GenericNameSyntax, symbols As SymbolTable) As NamedValue(Of Type())
            Dim typeName = generic.objectName
            Dim types = generic.TypeArgumentList.Arguments
            Dim elementType As Type() = types _
                .Select(Function(T) AsTypeHandler.GetType(T, symbols)) _
                .ToArray

            Return New NamedValue(Of Type()) With {
                .Name = typeName,
                .Value = elementType
            }
        End Function

        ''' <summary>
        ''' 创建一个泛型列表类型
        ''' </summary>
        ''' <param name="element"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function listOf(element As Type) As Type
            Return GetType(System.Collections.Generic.List(Of )).MakeGenericType(element)
        End Function

        <Extension>
        Public Function [GetType](asType As TypeSyntax, symbols As SymbolTable) As RawType
            If TypeOf asType Is PredefinedTypeSyntax Then
                Dim type = DirectCast(asType, PredefinedTypeSyntax)
                Dim token$ = type.Keyword.objectName

                Return Scripting.GetType(token)
            ElseIf TypeOf asType Is ArrayTypeSyntax Then
                Dim type = DirectCast(asType, ArrayTypeSyntax)
                Dim tokenType As Type = [GetType](type.ElementType, symbols)

                Return tokenType.MakeArrayType
            ElseIf TypeOf asType Is GenericNameSyntax Then
                Dim generic = DirectCast(asType, GenericNameSyntax)
                Dim define = generic.GetGenericType(symbols)
                Dim tokenType = define.Value

                ' 在javascript之中 array 和 list是一样的
                If define.Name = "List" Then
                    Return tokenType(Scan0).listOf
                ElseIf define.Name = "Dictionary" Then
                    ' 字典对象在javascript之中则是一个任意的object
                    Return GetType(DictionaryBase)
                Else
                    Throw New NotImplementedException
                End If
            Else
                Dim type = DirectCast(asType, IdentifierNameSyntax)
                Dim token$ = type.Identifier.objectName

                If symbols.HaveEnumType(token) Then
                    Dim [const] As EnumSymbol = symbols.GetEnumType(token)
                    Return [const].UnderlyingType
                ElseIf token = "Array" Then
                    Return GetType(System.Array)
                ElseIf token = "IList" Then
                    Return GetType(System.Collections.IList)
                Else
                    ' 用户的自定义类型
                    Throw New TypeAccessException($"Target type '{token}' is not defined!")
                End If
            End If
        End Function

    End Module
End Namespace
