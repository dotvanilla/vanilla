Namespace JavaScript

    ''' <summary>
    ''' Math is a built-in object that has properties and methods for 
    ''' mathematical constants and functions.
    ''' 
    ''' https://developer.mozilla.org/zh-CN/docs/Web/JavaScript/Reference/Global_Objects/Math
    ''' </summary>
    Public Class Math

        ''' <summary>
        ''' The Math.pow() function returns the base to the exponent power, as in base^exponent.
        ''' </summary>
        Public Declare Function pow Lib "Math" Alias "Math.Pow" (base As Double, exponent As Double) As Double

        Public Declare Function sqrt Lib "Math" Alias "Math.Sqrt" (x As Double) As Double

        Public Declare Function exp Lib "Math" Alias "Math.Exp" (x As Double) As Double

        Public Declare Function log Lib "Math" Alias "Math.Log" (x As Double) As Double

        Public Declare Function log10 Lib "Math" Alias "Math.Log10" (x As Double) As Double

        Public Declare Function cos Lib "Math" Alias "Math.Cos" (x As Double) As Double

        Public Declare Function sin Lib "Math" Alias "Math.Sin" (x As Double) As Double

        Public Declare Function tan Lib "Math" Alias "Math.Tan" (x As Double) As Double

        Public Declare Function floor Lib "Math" Alias "Math.Floor" (x As Double) As Double

        Public Declare Function ceil Lib "Math" Alias "Math.Ceiling" (x As Double) As Double

        Public Declare Function round Lib "Math" Alias "Math.Round" (x As Double) As Double

        Public Declare Function abs Lib "Math" Alias "Math.Abs" (x As Double) As Double

        Public Declare Function min Lib "Math" Alias "Math.Min" (a As Double, b As Double) As Double

        Public Declare Function max Lib "Math" Alias "Math.Max" (a As Double, b As Double) As Double

        Public Declare Function random Lib "Math" Alias "Math.Random" () As Double

    End Class
End Namespace
