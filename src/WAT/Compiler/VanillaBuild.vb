Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Compiler

    Public NotInheritable Class VanillaBuild

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

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="[module]">The module symbol object or wast source file text content.</param>
        ''' <returns></returns>
        Private Shared Function tempfile_WAST([module] As Workspace) As String
            Dim tempfile As String = TempFileSystem.GetAppSysTempFile(
                ext:=$"{RandomASCIIString(10, skipSymbols:=True)}.wast",
                sessionID:=App.PID,
                prefix:="wat2wasm_"
            )

            Call [module] _
                .ToSExpression _
                .SaveTo(tempfile, encoding:=Encodings.UTF8WithoutBOM.CodePage)

            Return tempfile
        End Function

        Public Shared Function HexDump([module] As Workspace, Optional verbose As Boolean = False) As String
            Dim config As New Wat2wasm With {
                .verbose = verbose,
                .dumpModule = True,
                .debugParser = True
            }
            Dim stdOut As String = CommandLine.Call(
                app:=wat2wasm,
                args:=$"{tempfile_WAST([module]).CLIPath} {config}",
                debug:=False
            )

            Return stdOut
        End Function

    End Class
End Namespace