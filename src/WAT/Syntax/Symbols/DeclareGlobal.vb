Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class DeclareGlobal : Inherits WATSymbol

        Private _declaredType As WATType

        Public Overrides ReadOnly Property Type As WATType
            Get
                If _declaredType Is Nothing Then
                    Return Value.Type
                Else
                    Return _declaredType
                End If
            End Get
        End Property

        ''' <summary>
        ''' 该全局量声明的类型（用于字符串/数组等引用类型，其常量初值为 i32 零值）
        ''' </summary>
        Public Property DeclaredType As WATType
            Get
                Return _declaredType
            End Get
            Set(value As WATType)
                _declaredType = value
            End Set
        End Property

        Public Property Value As WATSyntax

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return $"(global ${Name} (mut {Type.UnderlyingWATType.Description}) {Value.ToSExpression(env, indent)})"
        End Function
    End Class
End Namespace
