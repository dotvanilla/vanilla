Module Modulemethod_test

    ''' <summary>
    ''' this function overloads with <see cref="module2.test(String)"/>
    ''' </summary>
    ''' <returns></returns>
    Public Function test()
        Return -9999
    End Function

    Public Sub calls()
        Call test()
        Call module2.test($"34546734853{test()}8sdjkfsdhfsdfsdf")
    End Sub

End Module

Module module2
    Public Function test(gg As String) As String()
        Return {gg, gg & "ddddd"}
    End Function
End Module