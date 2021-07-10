Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis.Memory

    Public MustInherit Class MemoryObject

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 如果是一个表达式，则是一个动态资源，反之为字符串之类的静态资源
        ''' </remarks>
        Public Property MemoryPtr As MemoryPtr
        Public Property Annotation As String

        Public MustOverride ReadOnly Property sizeOf As WATSyntax

        Public Function [AddressOf]() As WATSyntax
            If TypeOf MemoryPtr Is StaticPtr Then
                Return New LiteralValue(DirectCast(MemoryPtr, StaticPtr).Scan0)
            Else
                Return DirectCast(MemoryPtr, InstancePtr).Scan0
            End If
        End Function

    End Class
End Namespace