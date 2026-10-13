Imports System.Diagnostics
Imports System.Text
Imports System.IO
Imports System.Collections.Generic

Namespace VanillaBasic.WebAssembly.Compiler

    ' Wat2WasmCompiler：wat2wasm.exe 的 VB.NET 命令行封装。
    ' 通过属性设置全部命令行开关，运行进程并捕获 stdout/stderr，
    ' 不同种类的功能由 Compile / DumpModule / VerboseCompile / GetHelp 提供。
    Public Class Wat2WasmCompiler

        ' ---- 可执行文件与运行环境 ----
        Public Property ExecutablePath As String = "wat2wasm.exe"
        Public Property WorkingDirectory As String = ""
        Public Property TimeoutMs As Integer = 30000

        ' ---- 输出（映射 -o / --output） ----
        Public Property OutputFile As String = ""

        ' ---- 通用开关 ----
        Public Property VerboseCount As Integer = 0               ' -v 重复次数
        Public Property DumpModuleEnabled As Boolean = False       ' -d / --dump-module
        Public Property DebugParser As Boolean = False            ' --debug-parser
        Public Property Relocatable As Boolean = False            ' -r / --relocatable
        Public Property NoCanonicalizeLeb128s As Boolean = False  ' --no-canonicalize-leb128s
        Public Property DebugNames As Boolean = False             ' --debug-names
        Public Property NoCheck As Boolean = False                ' --no-check

        ' ---- 特性开关（--enable-* / --disable-*） ----
        Public Property EnableExceptions As Boolean = False
        Public Property DisableMutableGlobals As Boolean = False
        Public Property EnableSaturatingFloatToInt As Boolean = False
        Public Property EnableSignExtension As Boolean = False
        Public Property EnableSimd As Boolean = False
        Public Property EnableThreads As Boolean = False
        Public Property EnableMultiValue As Boolean = False
        Public Property EnableTailCall As Boolean = False
        Public Property EnableBulkMemory As Boolean = False
        Public Property EnableReferenceTypes As Boolean = False
        Public Property EnableAnnotations As Boolean = False

        ' ================= 功能封装函数 =================

        ' 标准编译：将 wast/wat 文本编译为 .wasm 二进制（写入 outputFile）。
        Public Function Compile(inputFile As String, Optional outputFile As String = "") As Wat2WasmResult
            Return RunInternal(inputFile, outputFile, False, -1)
        End Function

        ' 模块转储：开启 -d，把模块的十六进制转储打印到标准输出并捕获。
        Public Function DumpModule(inputFile As String, Optional outputFile As String = "") As Wat2WasmResult
            Return RunInternal(inputFile, outputFile, True, -1)
        End Function

        ' 详细编译：以指定 verbose 级别（重复 -v 次数）编译。
        Public Function VerboseCompile(inputFile As String, level As Integer, Optional outputFile As String = "") As Wat2WasmResult
            Return RunInternal(inputFile, outputFile, False, level)
        End Function

        ' 帮助查询：运行 --help，返回帮助文本（位于 StandardOutput）。
        Public Function GetHelp() As Wat2WasmResult
            Return RunRaw(New List(Of String) From {"--help"})
        End Function

        ' ================= 内部实现 =================

        Private Function RunInternal(inputFile As String, outputFile As String, forceDump As Boolean, forceVerbose As Integer) As Wat2WasmResult
            Dim effectiveOutput As String = If(String.IsNullOrEmpty(outputFile), Me.OutputFile, outputFile)
            If String.IsNullOrEmpty(effectiveOutput) Then
                effectiveOutput = System.IO.Path.ChangeExtension(inputFile, ".wasm")
            End If
            Dim verbose As Integer = If(forceVerbose >= 0, forceVerbose, VerboseCount)
            Dim dump As Boolean = DumpModuleEnabled OrElse forceDump
            Dim args As List(Of String) = BuildArguments(inputFile, effectiveOutput, verbose, dump)
            Return RunRaw(args, effectiveOutput)
        End Function

        ' 根据当前属性（及传入的 verbose/dump 覆盖）拼出命令行参数列表。
        Private Function BuildArguments(inputFile As String, outputFile As String, verbose As Integer, dump As Boolean) As List(Of String)
            Dim args As New List(Of String)

            For i As Integer = 1 To verbose
                args.Add("-v")
            Next

            If dump Then args.Add("-d")
            If DebugParser Then args.Add("--debug-parser")
            If EnableExceptions Then args.Add("--enable-exceptions")
            If DisableMutableGlobals Then args.Add("--disable-mutable-globals")
            If EnableSaturatingFloatToInt Then args.Add("--enable-saturating-float-to-int")
            If EnableSignExtension Then args.Add("--enable-sign-extension")
            If EnableSimd Then args.Add("--enable-simd")
            If EnableThreads Then args.Add("--enable-threads")
            If EnableMultiValue Then args.Add("--enable-multi-value")
            If EnableTailCall Then args.Add("--enable-tail-call")
            If EnableBulkMemory Then args.Add("--enable-bulk-memory")
            If EnableReferenceTypes Then args.Add("--enable-reference-types")
            If EnableAnnotations Then args.Add("--enable-annotations")
            If Relocatable Then args.Add("-r")
            If NoCanonicalizeLeb128s Then args.Add("--no-canonicalize-leb128s")
            If DebugNames Then args.Add("--debug-names")
            If NoCheck Then args.Add("--no-check")

            args.Add("-o")
            args.Add(outputFile)
            args.Add(inputFile)
            Return args
        End Function

        ' 通用运行器：启动子进程，异步累积 stdout/stderr，支持超时。
        Private Function RunRaw(args As List(Of String), Optional outputFile As String = "") As Wat2WasmResult
            Dim psi As New ProcessStartInfo()
            psi.FileName = ExecutablePath
            psi.WorkingDirectory = WorkingDirectory
            psi.UseShellExecute = False
            psi.CreateNoWindow = True
            psi.RedirectStandardOutput = True
            psi.RedirectStandardError = True
            psi.ArgumentList.Clear()
            For Each a In args
                psi.ArgumentList.Add(a)
            Next

            Dim sbOut As New StringBuilder()
            Dim sbErr As New StringBuilder()

            Using proc As New Process()
                proc.StartInfo = psi
                AddHandler proc.OutputDataReceived,
                    Sub(sender As Object, e As DataReceivedEventArgs)
                        If e.Data IsNot Nothing Then sbOut.AppendLine(e.Data)
                    End Sub
                AddHandler proc.ErrorDataReceived,
                    Sub(sender As Object, e As DataReceivedEventArgs)
                        If e.Data IsNot Nothing Then sbErr.AppendLine(e.Data)
                    End Sub

                proc.Start()
                proc.BeginOutputReadLine()
                proc.BeginErrorReadLine()

                If Not proc.WaitForExit(TimeoutMs) Then
                    Try
                        proc.Kill()
                    Catch
                    End Try
                    sbErr.AppendLine("[Wat2Wasm wrapper] process timed out after " & TimeoutMs & " ms")
                End If
                proc.WaitForExit()

                Return New Wat2WasmResult(proc.ExitCode, sbOut.ToString(), sbErr.ToString(), outputFile)
            End Using
        End Function

    End Class

End Namespace
