﻿Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports VanillaBasic.WebAssembly.Syntax
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports Microsoft.VisualBasic.Scripting.SymbolBuilder.VBLanguage
Imports any = Microsoft.VisualBasic.Scripting

Namespace VBLanguageParser

    Module ParseFunction

        <Extension>
        Public Function RunParser(func As MethodBlockSyntax, context As Environment) As FunctionDeclare
            Dim returnValue As WATType
            Dim pars As DeclareLocal() = func.SubOrFunctionStatement _
                .ParseParameters(context) _
                .ToArray

            If func.SubOrFunctionStatement.SubOrFunctionKeyword.ValueText = "Sub" Then
                returnValue = WATType.void
            Else
                returnValue = func.SubOrFunctionStatement.AsClause.ParseAsType(env:=context)
            End If

            Return New FunctionDeclare(returnValue) With {
                .Name = func.SubOrFunctionStatement.Identifier.ValueText,
                .[namespace] = context.FullName
            }
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        <Extension>
        Private Function ParseParameters(method As MethodBaseSyntax, context As Environment) As IEnumerable(Of DeclareLocal)
            Return method.ParameterList _
                .Parameters _
                .Select(Function(p) ParseParameter(p, context))
        End Function

        Public Function ParseParameter(parameter As ParameterSyntax, context As Environment) As DeclareLocal
            Dim name As String = parameter.Identifier.objectName
            Dim type As WATType
            Dim [default] As WATSyntax = Nothing

            If parameter.AsClause Is Nothing Then
                type = WATType.GetUnderlyingType(
                    value:=any.GetType(Patterns.TypeCharName(name.Last)),
                    wasm:=context.Workspace
                )
                name = name.Substring(0, name.Length - 1)
            Else
                type = parameter.AsClause.ParseAsType(env:=context)
            End If

            If Not parameter.Default Is Nothing Then
                [default] = parameter.Default.GetLiteralDefault(type)
            End If

            Return New DeclareLocal(type) With {
                .Name = name,
                .DefaultValue = [default]
            }
        End Function

        <Extension>
        Public Function GetLiteralDefault([default] As EqualsValueSyntax, type As WATType) As WATSyntax
            Dim defaultText As String

            Select Case [default].Value.GetType
                Case GetType(LiteralExpressionSyntax)
                    With DirectCast([default].Value, LiteralExpressionSyntax)
                        defaultText = .Token.Value
                    End With
                Case GetType(UnaryExpressionSyntax)
                    With DirectCast([default].Value, UnaryExpressionSyntax)
                        defaultText = .UnaryValue
                    End With
                Case Else
                    Throw New NotImplementedException
            End Select

            If type Is WATType.f32 Then
                Return New LiteralValue(Single.Parse(defaultText))
            ElseIf type Is WATType.f64 Then
                Return New LiteralValue(Double.Parse(defaultText))
            ElseIf type Is WATType.i32 Then
                Return New LiteralValue(Integer.Parse(defaultText))
            ElseIf type Is WATType.i64 Then
                Return New LiteralValue(Long.Parse(defaultText))
            Else
                Throw New NotImplementedException
            End If
        End Function
    End Module
End Namespace