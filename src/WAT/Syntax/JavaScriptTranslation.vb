Namespace Syntax

    ''' <summary>
    ''' mapping the javascript api function into .NET framework api function.
    ''' </summary>
    ''' <remarks>
    ''' example as: ``console.log`` -> ``Console.WriteLine``
    ''' </remarks>
    Public Class JavaScriptTranslation

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

    End Class
End Namespace