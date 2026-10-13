Namespace CodeAnalysis

    ''' <summary>
    ''' 数组类型：在 wasm 之中数组使用线性内存指针（i32）来表示，
    ''' 元素类型保存在 <see cref="ElementType"/> 之中。
    ''' </summary>
    Public Class ArrayType : Inherits WATType

        ''' <summary>
        ''' 数组元素的类型
        ''' </summary>
        Public ReadOnly Property ElementType As WATType

        Sub New(predefinedType As WATElements)
            Call MyBase.New(predefinedType)

            ElementType = New WATType(predefinedType)
        End Sub

        Sub New(elementType As WATType)
            ' 数组在 wasm 之中统一使用 i32 指针表示
            Call MyBase.New(WATElements.array)

            Me.ElementType = If(elementType, WATType.i32)
        End Sub

        Public Overrides Function ToString() As String
            Return $"{ElementType.ToString}[]"
        End Function

    End Class
End Namespace
