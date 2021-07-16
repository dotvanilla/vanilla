Namespace Compiler

    Public Class ObjectManager

        Public HeapSize As Integer

        Public Function GetMemorySize() As Integer
            Return HeapSize
        End Function

        Public Function AllocateObject(sizeof As Integer, class_id As Integer) As Integer
            Dim offset As Integer = HeapSize
            HeapSize = offset + sizeof
            Return offset
        End Function

        Public Function AllocateArray(sizeof As Integer, class_id As Integer, length As Integer) As Integer

        End Function

    End Class
End Namespace