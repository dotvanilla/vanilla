Namespace JavaScript

    ''' <summary>
    ''' 线性内存之中的数组对象相关的运行时桥接。
    ''' 数组的内存布局为：``[class_id:i32][length:i32][element0][element1]...``。
    ''' </summary>
    Public Class ArrayApi

        ''' <summary>
        ''' ``Array.New(size)``，返回数组指针
        ''' </summary>
        Public Declare Function [New] Lib "Array" Alias "Array.New" (size As Integer) As Integer

        ''' <summary>
        ''' ``array.Length``
        ''' </summary>
        Public Declare Function Length Lib "Array" Alias "Array.length" (array As Integer) As Integer

        ''' <summary>
        ''' ``array(i)``，读取 i32 元素（Byte/Integer/Boolean 等）
        ''' </summary>
        Public Declare Function GetI32 Lib "Array" Alias "Array.get.i32" (array As Integer, index As Integer) As Integer

        ''' <summary>
        ''' ``array(i) = value``，写入 i32 元素
        ''' </summary>
        Public Declare Function SetI32 Lib "Array" Alias "Array.set.i32" (array As Integer, index As Integer, value As Integer) As Integer

        ''' <summary>
        ''' ``array(i)``，读取 f64 元素（Double）
        ''' </summary>
        Public Declare Function GetF64 Lib "Array" Alias "Array.get.f64" (array As Integer, index As Integer) As Double

        ''' <summary>
        ''' ``array(i) = value``，写入 f64 元素
        ''' </summary>
        Public Declare Function SetF64 Lib "Array" Alias "Array.set.f64" (array As Integer, index As Integer, value As Double) As Integer

    End Class
End Namespace
