﻿Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    ''' <summary>
    ''' mapping the javascript api function into .NET framework api function.
    ''' </summary>
    ''' <remarks>
    ''' example as: ``console.log`` -> ``Console.WriteLine``
    ''' </remarks>
    Public Class JavaScriptTranslation : Inherits WATSyntax

        ''' <summary>
        ''' 所被映射的.NET Framework函数，例如<see cref="Console.WriteLine"/>
        ''' </summary>
        ''' <returns></returns>
        Public Property DotNetFramework As WATSyntax

        ''' <summary>
        ''' 所导入的JavaScript函数，例如``console.log``
        ''' </summary>
        ''' <returns></returns>
        Public Property JavaScript As String

        ''' <summary>
        ''' the result value type of this function returns
        ''' </summary>
        ''' <returns></returns>
        Public Overrides ReadOnly Property Type As WATType

        Sub New()
        End Sub

        Sub New(type As WATType)
            Me.Type = type
        End Sub

        Public Overrides Function ToSExpression(env As Environment, indent As String) As String
            Return "$" & JavaScript
        End Function
    End Class
End Namespace