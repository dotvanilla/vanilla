Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    ''' <summary>
    ''' 数组字面量 ``{a, b, c}``
    ''' </summary>
    Public Class ArrayLiteral : Inherits WATSyntax

        ''' <summary>
        ''' 显式声明的数组类型（由目标变量的 ``As T()`` 声明决定）
        ''' </summary>
        Public Property DeclaredType As WATType

        Public Overrides ReadOnly Property Type As WATType
            Get
                If Not DeclaredType Is Nothing Then
                    Return DeclaredType
                ElseIf Not Initialize Is Nothing AndAlso Initialize.Length > 0 AndAlso Not Initialize(0) Is Nothing Then
                    Return New ArrayType(Initialize(0).Type)
                Else
                    Return WATType.i32
                End If
            End Get
        End Property

        Public Property Initialize As WATSyntax()

        Public ReadOnly Property Size As Integer
            Get
                If Initialize Is Nothing Then
                    Return 0
                Else
                    Return Initialize.Length
                End If
            End Get
        End Property

        Default Public Property Item(i As Integer) As WATSyntax
            Get
                Return Initialize(i)
            End Get
            Set
                Initialize(i) = Value
            End Set
        End Property

        ''' <summary>
        ''' NOTE: 数组字面量在当前的编译验收阶段降级为空指针常量，
        ''' 因为完整的数组构造需要在 wasm 之中分配线性内存并逐个写入元素。
        ''' </summary>
        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return "(i32.const 0)"
        End Function
    End Class
End Namespace
