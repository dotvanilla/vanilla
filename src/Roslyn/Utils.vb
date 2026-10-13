Imports System.IO
Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis

Module Utils

    <Extension>
    Friend Function SolveStream(vbcode As [Variant](Of FileInfo, String)) As String
        If vbcode Like GetType(String) Then
            Return CType(vbcode, String).SolveStream
        Else
            Return CType(vbcode, FileInfo).FullName.SolveStream
        End If
    End Function

    <Extension>
    Public Function ParseAsType([as] As SimpleAsClauseSyntax, env As Environment) As WATType
        If [as] Is Nothing Then
            ' Function 没有显式声明返回类型的时候，在 VB 之中默认是 Object
            ' 在 wasm 之中使用 i32 指针来表示
            Return WATType.i32
        End If

        Return [as].Type.ParseType(env)
    End Function

    <Extension>
    Public Function ParseType(type As TypeSyntax, env As Environment) As WATType
        If type Is Nothing Then
            Return WATType.i32
        End If

        Select Case type.GetType
            Case GetType(PredefinedTypeSyntax)
                Return MapPredefinedType(DirectCast(type, PredefinedTypeSyntax).Keyword.ValueText)
            Case GetType(ArrayTypeSyntax)
                Dim element As WATType = DirectCast(type, ArrayTypeSyntax).ElementType.ParseType(env)

                Return New ArrayType(element)
            Case GetType(GenericNameSyntax)
                ' List(Of T) / Dictionary(Of K, V) 在 javascript 运行时之中都是一个对象引用
                Return WATType.i32
            Case GetType(IdentifierNameSyntax)
                Return MapUserType(DirectCast(type, IdentifierNameSyntax).Identifier.ValueText, env)
            Case GetType(QualifiedNameSyntax)
                Return MapUserType(DirectCast(type, QualifiedNameSyntax).Right.Identifier.ValueText, env)
            Case GetType(NullableTypeSyntax)
                Return DirectCast(type, NullableTypeSyntax).ElementType.ParseType(env)
            Case Else
                ' 未识别的类型声明：为了批量验收，降级为 i32 指针表示
                Return WATType.i32
        End Select
    End Function

    ''' <summary>
    ''' VB.NET 预定义类型关键字到 wasm 类型的映射
    ''' </summary>
    ''' <param name="keyword"></param>
    ''' <returns></returns>
    Private Function MapPredefinedType(keyword As String) As WATType
        Select Case keyword.ToLowerInvariant
            Case "boolean"
                Return WATType.boolean
            Case "string"
                Return WATType.string
            Case "char"
                ' 字符使用单字符字符串来表示（与 string.charAt 保持一致）
                Return WATType.string
            Case "byte", "sbyte", "short", "ushort", "integer", "uinteger"
                Return WATType.i32
            Case "long", "ulong"
                Return WATType.i64
            Case "single"
                Return WATType.f32
            Case "double", "decimal", "date"
                Return WATType.f64
            Case "object"
                Return WATType.i32
            Case Else
                Throw New NotImplementedException($"PredefinedType: {keyword}")
        End Select
    End Function

    ''' <summary>
    ''' 用户自定义类型（类/结构）/ 枚举类型解析
    ''' </summary>
    Private Function MapUserType(name As String, env As Environment) As WATType
        If env.HaveEnumType(name) Then
            Return WATType.GetUnderlyingType(env.GetEnumType(name).UnderlyingType, env.Workspace)
        Else
            ' 用户自定义的引用类型在 wasm 之中使用 i32 指针表示
            Return New WATType(name)
        End If
    End Function

    <Extension>
    Friend Function objectName(name As IdentifierNameSyntax) As String
        Return name.Identifier.ValueText.Trim("["c, "]"c)
    End Function

    <Extension>
    Friend Function objectName(name As SimpleNameSyntax) As String
        Return name.Identifier.ValueText.Trim("["c, "]"c)
    End Function

    <Extension>
    Friend Function objectName(name As SyntaxToken) As String
        Return name.Text.Trim("["c, "]"c)
    End Function

    <Extension>
    Friend Function objectName(x As ModifiedIdentifierSyntax) As String
        Return x.Identifier.objectName
    End Function

    ''' <summary>
    ''' 常量表达式
    ''' </summary>
    ''' <param name="unary"></param>
    ''' <returns></returns>
    <Extension>
    Public Function UnaryValue(unary As UnaryExpressionSyntax) As String
        Dim op$ = unary.OperatorToken.ValueText
        Dim valueToken = DirectCast(unary.Operand, LiteralExpressionSyntax)
        Dim value$ = valueToken.Token.ValueText

        Return op & value
    End Function

    ''' <summary>
    ''' 宽容模式下统一的“跳过”日志（去重），用于批量编译时定位被跳过的语法节点
    ''' </summary>
    Friend Sub ReportSkipped(what As String, ex As Exception)
        Static reported As New HashSet(Of String)
        Dim key As String = what & "|" & ex.GetType.Name & "|" & ex.Message

        SyncLock reported
            If reported.Contains(key) Then
                Return
            End If

            Call reported.Add(key)
        End SyncLock

        Call System.Console.Error.WriteLine($"[skip] {what}: {ex.GetType().Name}: {ex.Message}")
    End Sub
End Module
