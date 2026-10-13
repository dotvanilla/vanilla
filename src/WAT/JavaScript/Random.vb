Namespace JavaScript

    ''' <summary>
    ''' ``System.Random`` 的 BCL 桥接。
    ''' 说明：这里只需要提供一个在指定区间内确定性取值的随机数发生器，
    ''' 不要求与 .NET 的带种子算法逐值一致。
    ''' </summary>
    Public Class Random

        ''' <summary>
        ''' ``New Random(seed)``
        ''' </summary>
        Public Declare Function [New] Lib "Random" Alias "Random.New" (seed As Integer) As Integer

        ''' <summary>
        ''' ``rnd.Next(minValue, maxValue)``
        ''' </summary>
        Public Declare Function [Next] Lib "Random" Alias "Random.Next" (rnd As Integer, minValue As Integer, maxValue As Integer) As Integer

        ''' <summary>
        ''' ``rnd.NextDouble()``
        ''' </summary>
        Public Declare Function NextDouble Lib "Random" Alias "Random.NextDouble" (rnd As Integer) As Double

    End Class
End Namespace
