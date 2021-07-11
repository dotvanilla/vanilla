Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis.Memory

Namespace CodeAnalysis

    ''' <summary>
    ''' The WebAssembly memory buffer device
    ''' </summary>
    Public Class MemoryBuffer : Implements IEnumerable(Of MemoryObject)

        Dim buffer As New List(Of MemoryObject)

        ''' <summary>
        ''' 因为<see cref="TypeAlias"/>之中基础类型都是小于10的
        ''' 所以在这里offset从10开始，从而能够避免class_id小于10
        ''' 导致用户自定义类型被误判为基础类型
        ''' </summary>
        Dim offset As Integer = 13

        ''' <summary>
        ''' 获取得到在WebAssembly之中的初始化大小
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property TotalSize As Integer
            <MethodImpl(MethodImplOptions.AggressiveInlining)>
            Get
                ' 这个偏移量就是静态资源的总大小
                Return offset
            End Get
        End Property

        ''' <summary>
        ''' Moves the position of the <seealso cref="offset"/> to the next position aligned on
        ''' 8 bytes. If the buffer position is already a multiple of 8 the position will
        ''' not be changed.
        ''' </summary>
        Private Sub seekBufferToNextMultipleOfEight()
            Dim pos As Integer = offset

            If pos Mod 8 = 0 Then
                ' Already on a 8 byte multiple
                Return
            Else
                offset += (8 - (pos Mod 8))
            End If
        End Sub

        ''' <summary>
        ''' 函数返回的是数据的内存位置
        ''' </summary>
        ''' <param name="str"></param>
        ''' <returns></returns>
        Public Function AddString(str As String) As Integer
            Dim buffer As New StringLiteral With {
                .Value = str,
                .MemoryPtr = New StaticPtr(offset)
            }

            Me.buffer += buffer
            ' 因为字符串末尾会添加一个零，来表示字符串的结束
            ' 所以在长度这里会需要添加1
            Me.offset += buffer.nchars + 1

            Call seekBufferToNextMultipleOfEight()

            Return DirectCast(buffer.MemoryPtr, StaticPtr).Scan0
        End Function

        Public Iterator Function GetEnumerator() As IEnumerator(Of MemoryObject) Implements IEnumerable(Of MemoryObject).GetEnumerator
            For Each item In buffer
                Yield item
            Next
        End Function

        Private Iterator Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function
    End Class
End Namespace