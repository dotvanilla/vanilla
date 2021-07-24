Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax.Literal

Namespace VBLanguageParser

    Public Module AsTypeHandler

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
        Public Function AsType(ByRef name$,
                               [asClause] As AsClauseSyntax,
                               context As Environment,
                               Optional initType As WATType = Nothing) As WATType

            Dim type As WATType

            If Not asClause Is Nothing Then
                If TypeOf asClause Is SimpleAsClauseSyntax Then
                    type = WATType.GetUnderlyingType(GetAsType(asClause, context), context)
                ElseIf TypeOf asClause Is AsNewClauseSyntax Then
                    Dim [new] As ObjectCreationExpressionSyntax = DirectCast(asClause, AsNewClauseSyntax).NewExpression
                    Dim objType As VBType = AsTypeHandler.GetType([new].Type, context)

                    type = WATType.GetUnderlyingType(objType, context)
                Else
                    Throw New NotImplementedException
                End If
            ElseIf name.Last Like Patterns.TypeChar Then
                type = WATType.FromTypeChar(name.Last)
                name = name.Substring(0, name.Length - 1)
            Else
                type = initType
            End If

            Return type
        End Function

        ''' <summary>
        ''' Parse type define from [``As Type``] expression.
        ''' </summary>
        ''' <param name="[as]">``As Type``</param>
        ''' <param name="context"></param>
        ''' <returns></returns>
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetAsType([as] As SimpleAsClauseSyntax, context As Environment) As VBType
            If [as] Is Nothing Then
                Return GetType(Void)
            Else
                Return [GetType]([as].Type, context)
            End If
        End Function

        <Extension>
        Public Function PredefinedType(asType As PredefinedTypeSyntax) As Type
            Dim token$ = asType.Keyword.objectName
            ' parse from the token name
            Return Scripting.GetType(token)
        End Function

        <Extension>
        Public Function [GetType](asType As TypeSyntax, context As Environment) As VBType
            If TypeOf asType Is PredefinedTypeSyntax Then
                Return DirectCast(asType, PredefinedTypeSyntax).PredefinedType
            ElseIf TypeOf asType Is ArrayTypeSyntax Then
                Return DirectCast(asType, ArrayTypeSyntax).arrayType(context)
            ElseIf TypeOf asType Is GenericNameSyntax Then
                Dim generic = DirectCast(asType, GenericNameSyntax)
                Dim define = generic.GetGenericType(context)
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
                Return DirectCast(asType, IdentifierNameSyntax).DefinedType(context)
            End If
        End Function

        <Extension>
        Private Function DefinedType(type As IdentifierNameSyntax, context As Environment) As VBType
            Dim token$ = type.Identifier.objectName

            If context.HaveEnumType(token) Then
                Dim [const] As EnumSymbol = context.GetEnumType(token)
                Return [const].UnderlyingType
            ElseIf token = "Array" Then
                Return GetType(Array)
            ElseIf token = "IList" Then
                Return GetType(IList)
            Else
                ' 用户的自定义类型
                Return New VBType(token)
            End If
        End Function

        ''' <summary>
        ''' 创建一个泛型列表类型
        ''' </summary>
        ''' <param name="element"></param>
        ''' <returns></returns>
        ''' 
        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function listOf(element As VBType) As VBType
            Dim list As Type = GetType(System.Collections.Generic.List(Of ))
            Dim raw As VBType = element.AsGeneric(container:=list)

            Return raw
        End Function

        <Extension>
        Public Function GetGenericType(generic As GenericNameSyntax, context As Environment) As NamedValue(Of VBType())
            Dim typeName = generic.objectName
            Dim types = generic.TypeArgumentList.Arguments
            Dim elementType As VBType() = types _
                .Select(Function(T) AsTypeHandler.GetType(T, context)) _
                .ToArray

            Return New NamedValue(Of VBType()) With {
                .Name = typeName,
                .Value = elementType
            }
        End Function

        <Extension>
        Private Function arrayType(type As ArrayTypeSyntax, context As Environment) As VBType
            Dim tokenType As VBType = [GetType](type.ElementType, context)
            Dim array As VBType = tokenType.MakeArrayType

            For i As Integer = 1 To type.RankSpecifiers.Count - 1
                array = array.MakeArrayType
            Next

            Return array
        End Function

    End Module
End Namespace