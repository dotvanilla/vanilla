Namespace MSIL

    ''' <summary>
    ''' Stack, heap and locals manipulation
    ''' </summary>
    Public Class EvaluationStack

        ReadOnly stack As New Stack(Of Object)
        ReadOnly locals As Object() = New Object(255 - 1) {}

        Public ReadOnly Property size As Integer
            Get
                Return stack.Count
            End Get
        End Property

        Public Function Pop() As Object
            Return stack.Pop
        End Function

        Public Function PopArgumentsFromStack(ParametersCount As Integer) As List(Of Object)
            Dim args = New Object(ParametersCount - 1) {}

            For i = ParametersCount - 1 To 0 Step -1
                args(i) = PopFromStack()
            Next

            Return New List(Of Object)(args)
        End Function

        Public Sub PushToStack(ByVal value As Object)
            Stack.Push(value)
        End Sub

        Public Function PopFromStack() As Object
            Return Stack.Pop()
        End Function

        Public Sub PushLocalToStack(ByVal index As Byte)
            PushToStack(locals(index))
        End Sub

        Public Sub PopFromStackToLocal(ByVal index As Byte)
            locals(index) = Stack.Pop()
        End Sub
    End Class
End Namespace