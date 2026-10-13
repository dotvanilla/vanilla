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
                    .LineIterators _
                    .SaveTo(tempfile, encoding:=Encodings.UTF8WithoutBOM.CodePage, newLine:=ASCII.LF)
            Else
                Call CType([module], String) _
                    .SolveStream _
                    .LineIterators _
                    .SaveTo(tempfile, encoding:=Encodings.UTF8WithoutBOM.CodePage, newLine:=ASCII.LF)
            End If

            Return tempfile
        End Function

        Public Shared Sub WastDump([module] As Workspace, file As String)
            Call [module] _
                .Copy _
                .ToSExpression _
                .SaveTo(file, encoding:=Encodings.UTF8WithoutBOM.CodePage)
        End Sub

        ''' <summary>
        ''' Dump the wasm module as a hex/text representation into <paramref name="file"/>.
        ''' Uses the new <see cref="Wat2WasmCompiler"/> wrapper (replaces the old
        ''' sciBASIC# CommandLine.Call + Wat2wasm parameter model).
        ''' </summary>
        Public Shared Function HexDump([module] As Workspace, file As String, Optional verbose As Boolean = False) As String
            Dim wast As String = tempfile_WAST([module])
            Dim compiler As New Wat2WasmCompiler()
            compiler.ExecutablePath = wat2wasm
            compiler.DumpModuleEnabled = True
            compiler.DebugParser = True
            compiler.VerboseCount = If(verbose, 1, 0)
            compiler.OutputFile = TempFileSystem.GetAppSysTempFile(
                ext:=".wasm",
                sessionID:=App.PID,
                prefix:="wat2wasm_hexdump_"
            )

            Dim result As Wat2WasmResult = compiler.DumpModule(wast, compiler.OutputFile)
            Dim text As String = If(result.Success, result.StandardOutput, result.StandardError)

            Call text.SaveTo(file)

            If Not result.Success Then
                Call Console.WriteLine(text)
            End If

            Return text
        End Function

        ''' <summary>
        ''' Compile VB.NET module parse result to webAssembly binary.
        ''' </summary>
        ''' <param name="[module]"></param>
        ''' <returns>
        ''' This function returns the compiler standard output (or standard error on failure).
        ''' </returns>
        Public Shared Function Compile([module] As Workspace, compiler As Wat2WasmCompiler) As String
            Dim wast As String = tempfile_WAST([module])
            compiler.ExecutablePath = wat2wasm

            Dim result As Wat2WasmResult = compiler.Compile(wast, compiler.OutputFile)
            Return If(result.Success, result.StandardOutput, result.StandardError)
        End Function

        ''' <summary>
        ''' Compile wast file to wasm binary and then returns the compiler log.
        ''' </summary>
        ''' <param name="wast">The file text</param>
        ''' <returns></returns>
        Public Shared Function CompileWast(wast As String, compiler As Wat2WasmCompiler) As String
            compiler.ExecutablePath = wat2wasm

            Dim result As Wat2WasmResult = compiler.Compile(tempfile_WAST(wast), compiler.OutputFile)
            Return If(result.Success, result.StandardOutput, result.StandardError)
        End Function
    End Class
End Namespace
