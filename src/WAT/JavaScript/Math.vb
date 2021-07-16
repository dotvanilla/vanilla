Namespace JavaScript

    ''' <summary>
    ''' Math is a built-in object that has properties and methods for 
    ''' mathematical constants and functions. It’s not a function 
    ''' object.
    ''' 
    ''' https://developer.mozilla.org/zh-CN/docs/Web/JavaScript/Reference/Global_Objects/Math
    ''' </summary>
    Public Class Math

        ''' <summary>
        ''' The Math.pow() function returns the base to the exponent power, as in base^exponent.
        ''' </summary>
        ''' <param name="base">The base number.</param>
        ''' <param name="exponent">The exponent used to raise the base.</param>
        ''' <returns>A number representing the given base taken to the power of the given exponent.</returns>
        Public Declare Function pow Lib "Math" Alias "Math.Pow" (base As Double, exponent As Double) As Double


    End Class
End Namespace