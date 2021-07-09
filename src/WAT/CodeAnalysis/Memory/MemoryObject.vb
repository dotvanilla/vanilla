Imports VanillaBasic.WebAssembly.Syntax

Public Class MemoryObject

    ''' <summary>
    ''' 这个对象在内存之中的起始位置
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' 如果是一个表达式，则是一个动态资源，反之为字符串之类的静态资源
    ''' </remarks>
    Public Property MemoryPtr As MemoryPtr

    Public Function [AddressOf]() As WATSyntax
        If TypeOf MemoryPtr Is StaticPtr Then
            Return New LiteralValue(DirectCast(MemoryPtr, StaticPtr).Scan0)
        Else
            Return DirectCast(MemoryPtr, InstancePtr).Scan0
        End If
    End Function

End Class


Public MustInherit Class MemoryPtr

End Class

Public Class StaticPtr : Inherits MemoryPtr

    Public Property Scan0 As Integer

End Class

Public Class InstancePtr : Inherits MemoryPtr

    Public Property Scan0 As WATSyntax

End Class