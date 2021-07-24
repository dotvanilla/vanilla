Imports System.Runtime.CompilerServices
Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis

    Public Enum SymbolTypes
        Project
        [Namespace]
        [Module]
        [Function]
    End Enum

    ''' <summary>
    ''' local environment/module environment for find symbol reference
    ''' </summary>
    Public Class Environment

        Public ReadOnly Property [global] As ProjectEnvironment
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                If Container Is Nothing Then
                    Return Me
                Else
                    Return Container.global
                End If
            End Get
        End Property

        Public Overridable ReadOnly Property Workspace As Workspace
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                Return [global].Workspace
            End Get
        End Property

        Public ReadOnly Property FullName As String
        Public ReadOnly Property Container As Environment
        ''' <summary>
        ''' local symbols or global symbols
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Symbols As New Dictionary(Of String, WATType)
        ''' <summary>
        ''' usualy is the function name 
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SymbolName As String
        Public ReadOnly Property Type As WATType
        Public Property Level As SymbolTypes = SymbolTypes.Module

        Sub New(name As String, Optional container As Environment = Nothing, Optional type As WATType = Nothing)
            Me.Container = container
            Me.FullName = If(container Is Nothing, name, $"{container.FullName}.{name}")
            Me.SymbolName = name
            Me.Type = If(type, WATType.void)
        End Sub

        Public Function GetSymbolType(target As WATSyntax) As WATType
            If TypeOf target Is JavaScriptTranslation OrElse TypeOf target Is SymbolReference Then
                Return target.Type
            Else
                Throw New NotImplementedException(target.GetType.FullName)
            End If
        End Function

        Public Function GetSymbolType(name As String) As WATType
            If Symbols.ContainsKey(name) Then
                Return Symbols(name)
            ElseIf Container IsNot Nothing Then
                Return Container.GetSymbolType(name)
            ElseIf Workspace.GlobalSymbols.ContainsKey(name) Then
                Return Workspace.GlobalSymbols(name).Type
            Else
                Return Nothing
            End If
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Overrides Function ToString() As String
            Return $"{Type} {FullName}"
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function GetEnumType(name As String) As EnumSymbol
            Return Workspace.EnumVals(name)
        End Function

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Function HaveEnumType(name As String) As Boolean
            Return Workspace.EnumVals.ContainsKey(name)
        End Function
    End Class
End Namespace