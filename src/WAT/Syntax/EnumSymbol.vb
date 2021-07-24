Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    ''' <summary>
    ''' The enum type object model
    ''' </summary>
    Public Class EnumSymbol : Inherits WATSyntax

        ''' <summary>
        ''' WebAssembly Type: i32 or i64
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property Type As WATType
        ''' <summary>
        ''' The enum type name
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Name As String
        ''' <summary>
        ''' [member name => value]
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Members As New Dictionary(Of String, LiteralValue)

        Public ReadOnly Property UnderlyingType As VBType
            Get
                Return Type.UnderlyingVBType
            End Get
        End Property

        Sub New(name As String, type As Type)
            Me.Name = name
            Throw New NotImplementedException
        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace