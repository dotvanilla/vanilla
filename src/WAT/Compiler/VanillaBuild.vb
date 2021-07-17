Imports Microsoft.VisualBasic.ApplicationServices
Imports Microsoft.VisualBasic.Language
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
        Private Shared Function tempfile_WAST([module] As [Variant](Of Workspace, String)) As String
            Dim tempfile As String = TempFileSystem.GetAppSysTempFile(
                ext:=$"{RandomASCIIString(10, skipSymbols:=True)}.wast",
                sessionID:=App.PID,
                prefix:="wat2wasm_"
            )

            If [module] Like GetType(Workspace) Then
                Call [module].TryCast(Of Workspace) _
                    .Copy _
                    .ToSExpression _
                    .SaveTo(tempfile, encoding:=Encodings.UTF8WithoutBOM.CodePage)
            Else
                Call CType([module], String) _
                    .SolveStream _
                    .SaveTo(tempfile, encoding:=Encodings.UTF8WithoutBOM.CodePage)
            End If

            Return tempfile
        End Function

        Public Shared Sub WastDump([module] As Workspace, file As String)
            Call [module] _
                .Copy _
                .ToSExpression _
                .SaveTo(file, encoding:=Encodings.UTF8WithoutBOM.CodePage)
        End Sub

        Public Shared Function HexDump([module] As Workspace, file As String, Optional verbose As Boolean = False) As String
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

            Call stdOut.SaveTo(file)

            Return stdOut
        End Function

        ''' <summary>
        ''' Compile VB.NET module parse result to webAssembly binary
        ''' </summary>
        ''' <param name="[module]"></param>
        ''' <returns>
        ''' This function returns the compiler standard output
        ''' </returns>
        Public Shared Function Compile([module] As Workspace, config As Wat2wasm) As String
            Dim stdOut As String = CommandLine.Call(
                app:=wat2wasm,
                args:=$"{tempfile_WAST([module]).CLIPath} {config}",
                debug:=False
            )

            Return stdOut
        End Function

        ''' <summary>
        ''' Compile wast file to wasm binary and then returns the compiler log.
        ''' </summary>
        ''' <param name="wast">The file text</param>
        ''' <returns></returns>
        Public Shared Function CompileWast(wast As String, config As Wat2wasm) As String
            Dim stdOut As String = CommandLine.Call(
                app:=wat2wasm,
                args:=$"{tempfile_WAST(wast).CLIPath} {config}",
                debug:=False
            )

            Return stdOut
        End Function
    End Class
End Namespace