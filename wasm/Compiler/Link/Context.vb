Imports Microsoft.VisualBasic.Language.Default
Imports Wasm.Symbols.MemoryObject

Namespace Compiler

    ''' <summary>
    ''' 当前的程序代码的上下文
    ''' </summary>
    Public Class Context

        ''' <summary>
        ''' 当前所进行解析的函数的名称
        ''' </summary>
        ''' <returns></returns>
        Public Property funcSymbol As String
        ''' <summary>
        ''' 当前的代码块的guid，这个是用来退出特定的block所需要的
        ''' </summary>
        ''' <returns></returns>
        Public Property blockGuid As String
        ''' <summary>
        ''' 当前的VisualBasic模块的名称
        ''' </summary>
        ''' <returns></returns>
        Public Property moduleLabel() As DefaultValue(Of String)
        ''' <summary>
        ''' 对象初始化或者匿名对象引用的时候使用
        ''' </summary>
        ''' <returns></returns>
        Public Property [object] As UserObject

    End Class
End Namespace