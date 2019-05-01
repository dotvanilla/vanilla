Module Modulemethod_test

    Public Function arraytypeInferTest() As Long()
        Return {2342, 34, 322, 343}
    End Function

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
        Call ThisIsAInternalFunction()
    End Sub

    Private Function ThisIsAInternalFunction() As Object
        Return "This is a internal function"
    End Function

End Module

Module module2

    Private Function ThisIsAInternalFunction() As Object
        Return "This is a internal function too"
    End Function

    Public Sub Runapp()
        Call Modulemethod_test.calls()
        Call ThisIsAInternalFunction()
    End Sub

    Public Function test(gg As String) As String()
        Return {gg, gg & "ddddd"}
    End Function
End Module