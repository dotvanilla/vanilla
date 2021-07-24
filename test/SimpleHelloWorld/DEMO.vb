Module DEMO

    Public Tag As Integer = Integer.MaxValue

    Public Function Main() As Integer
        Console.WriteLine("Hello World!")
        Return 0
    End Function

    Public Function fact(n As Integer) As Integer
        If n = 0 OrElse n = 1 Then
            Return 1
        Else
            Return n * fact(n - 1)
        End If
    End Function

    Public Function forLoop() As Integer
        For i As Integer = 0 To 10 Step 1
            Return i
        Next

        Return -1
    End Function

End Module

Public Class People

    Public Property FirstName As String
    Public Property MiddleName As String
    Public Property LastName As String

End Class