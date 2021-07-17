Namespace Compiler

    Public Class ObjectManager

        Public HeapSize As Integer

        Public Function GetMemorySize() As Integer
            ' 0000: nop                                Performs an operation without behavior.
            ' 0001: ldarg.0                            Loads the argument at index 0 onto the evaluation stack.
            ' 0002: ldfld Int ObjectManager::HeapSize  Pushes the value of a field in a specified object onto the stack.
            ' 0007: stloc.0                            Pops a value from the stack into local variable 0.
            ' 0008: br.s 10                            Branches to a target instruction at the specified offset, short form.
            ' 0010: ldloc.0                            Loads the local variable at index 0 onto the evaluation stack.
            ' 0011: ret                                Returns from method, possibly returning a value.
            Return HeapSize
        End Function

        Friend Function AllocateObject(sizeof As Integer, class_id As Integer) As Integer
            ' 0000: nop
            ' 0001: ldarg.0
            ' 0002: ldfld int ObjectManager::HeapSize
            ' 0007: stloc0.1                          Pops a value from the stack into local variable 1.
            ' 0008: ldarg.0
            ' 0009: ldloc0.1
            ' 0010: ldarg0.1
            ' 0011: add.ovf
            ' 0012: stfld int ObjectManager::HeapSize
            ' 0017: ldloc0.1
            ' 0018: stloc.0
            ' 0019: br.s 21
            ' 0021: ldloc.0
            ' 0022: ret
            Dim offset As Integer = HeapSize
            HeapSize = offset + sizeof
            Return offset
        End Function

        Friend Function AllocateArray(sizeof As Integer, class_id As Integer, length As Integer) As Integer
            Return HeapSize
        End Function

    End Class
End Namespace