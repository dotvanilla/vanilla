Namespace Compiler

    Public Class VanillaBuild

        ''' <summary>
        ''' read a file in the wasm text format, check it for errors, and
        ''' convert it To the wasm binary format.
        ''' </summary>
        Shared ReadOnly wat2wasm As String = $"{App.ProductSharedDIR}/wabt_bin/wat2wasm.exe"

        Shared Sub New()
            Call ReleaseExecFile()
            Call CheckWACompiler()
        End Sub

        Private Shared Sub ReleaseExecFile()
            If Not wat2wasm.FileExists Then
                Call My.Resources.wat2wasm.FlushStream(wat2wasm)
            End If
        End Sub

        Private Shared Sub CheckWACompiler()
            If Not wat2wasm.FileExists Then
                Throw New UnauthorizedAccessException($"Access Denied on filesystem location: {wat2wasm.ParentPath}")
            ElseIf wat2wasm.FileLength = 0 Then
                Throw New ApplicationException("Compiler is not accessable!")
            Else
                Dim help As String = CommandLine.Call(wat2wasm)

            End If
        End Sub

    End Class
End Namespace