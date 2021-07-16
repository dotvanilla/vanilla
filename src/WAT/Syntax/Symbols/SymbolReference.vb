Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class SymbolReference : Inherits WATSymbol

        Public Overrides ReadOnly Property Type As WATType

        Sub New()
        End Sub

        ''' <summary>
        ''' name应该是不包含有``$``符号的
        ''' </summary>
        ''' <param name="name"></param>
        Sub New(name As String, Optional type As WATType = Nothing)
            Me.Name = name
            Me.Type = type
        End Sub

        Sub New(type As WATType)
            Me.Type = type
        End Sub

        ''' <summary>
        ''' 这个函数会自动给<see cref="Name"/>添加一个``$``符号前缀
        ''' </summary>
        ''' <param name="env"></param>
        ''' <param name="indent"></param>
        ''' <returns></returns>
        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"${Name}"
        End Function

        Public Overrides Function ToString() As String
            Return Name
        End Function

        Public Shared Operator =(symbol As SymbolReference, name As String) As Boolean
            Return symbol.Name = name
        End Operator

        Public Shared Operator <>(symbol As SymbolReference, name As String) As Boolean
            Return Not symbol = name
        End Operator
    End Class
End Namespace