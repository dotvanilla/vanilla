Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis

    Public Class TypeSchema

        Public Property Name As String
        Public Property [Namespace] As String
        Public Property ExportApi As String()
        Public Property IsStandardModule As Boolean

        Public ReadOnly Property FullName As String
            Get
                Return $"{[Namespace]}.{Name}"
            End Get
        End Property

        ''' <summary>
        ''' 这个一般是指``Shared Sub New``过程。因为这个构造函数过程仅仅会被(start)
        ''' 表达式所调用（也就是所其他的任何函数都不会调用这个构造函数）
        ''' 所以这个构造函数就不放在公共的工作区内了
        ''' </summary>
        ''' <returns></returns>
        Public Property Initializer As FunctionDeclare

        Public Function GetWATType() As WATType

        End Function

        Public Overrides Function ToString() As String
            Return FullName
        End Function

    End Class
End Namespace