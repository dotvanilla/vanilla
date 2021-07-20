Namespace CodeAnalysis

    ''' <summary>
    ''' local environment/module environment for find symbol reference
    ''' </summary>
    Public Class Environment

        Public ReadOnly Property [global] As ProjectEnvironment
            Get
                If Container Is Nothing Then
                    Return Me
                Else
                    Return Container.global
                End If
            End Get
        End Property

        Public Overridable ReadOnly Property Workspace As Workspace
            Get
                Return [global].Workspace
            End Get
        End Property

        Public ReadOnly Property FullName As String
        Public ReadOnly Property Container As Environment
        Public ReadOnly Property Symbols As New Dictionary(Of String, WATType)

        Sub New(name As String, Optional container As Environment = Nothing)
            Me.Container = container
            Me.FullName = If(container Is Nothing, name, $"{container.FullName}.{name}")
        End Sub

        Public Function GetSymbolType(name As String) As WATType
            If Symbols.ContainsKey(name) Then
                Return Symbols(name)
            ElseIf Workspace.GlobalSymbols.ContainsKey(name) Then
                Return Workspace.GlobalSymbols(name).Type
            Else
                Return Nothing
            End If
        End Function

    End Class
End Namespace