Imports System.IO
Imports System.Text

Module Program

    Sub Main()
        ' 使用提供的两个测试文件：
        '   demo.wast        —— 正确的 wast 源文件，可正常编译
        '   globalStarter.wast —— 包含错误的 wast 源文件，无法编译
        Dim validFile As String = "G:\vanilla\test\demo_v2\demo.wast"
        Dim invalidFile As String = "G:\vanilla\test\test\globalStarter.wast"

        ' ExecutablePath 默认为 "wat2wasm.exe"，Demo 已将其复制到输出目录。
        Dim compiler As New Wat2WasmLib.Wat2WasmCompiler()

        Console.WriteLine("===== 1) 正常编译 (Compile) =====")
        Dim validOut As String = Path.ChangeExtension(validFile, ".wasm")
        Dim r1 As Wat2WasmLib.Wat2WasmResult = compiler.Compile(validFile, validOut)
        PrintResult(r1)
        Console.WriteLine("产物文件存在: " & File.Exists(r1.OutputFile))

        Console.WriteLine(vbCrLf & "===== 2) 错误源文件编译 (应当失败) =====")
        Dim r2 As Wat2WasmLib.Wat2WasmResult = compiler.Compile(invalidFile)
        PrintResult(r2)
        Console.WriteLine("成功? " & r2.Success)

        Console.WriteLine(vbCrLf & "===== 3) VerboseCompile (verbose=2) =====")
        Dim r3 As Wat2WasmLib.Wat2WasmResult =
            compiler.VerboseCompile(validFile, 2, Path.ChangeExtension(validFile, ".v2.wasm"))
        PrintResult(r3, maxLines:=20)

        Console.WriteLine(vbCrLf & "===== 4) DumpModule (十六进制转储到 stdout) =====")
        Dim r4 As Wat2WasmLib.Wat2WasmResult =
            compiler.DumpModule(validFile, Path.ChangeExtension(validFile, ".dump.wasm"))
        PrintResult(r4, maxLines:=12)

        Console.WriteLine(vbCrLf & "===== 5) GetHelp =====")
        Dim r5 As Wat2WasmLib.Wat2WasmResult = compiler.GetHelp()
        PrintResult(r5, maxLines:=8)

        Console.WriteLine(vbCrLf & "===== 6) 特性开关示例 (EnableSimd=True) =====")
        Dim simdCompiler As New Wat2WasmLib.Wat2WasmCompiler()
        simdCompiler.EnableSimd = True
        Dim r6 As Wat2WasmLib.Wat2WasmResult =
            simdCompiler.Compile(validFile, Path.ChangeExtension(validFile, ".simd.wasm"))
        PrintResult(r6)

        Console.WriteLine(vbCrLf & "演示完成。")
        If Not Console.IsInputRedirected Then Console.ReadKey()
    End Sub

    Private Sub PrintResult(r As Wat2WasmLib.Wat2WasmResult, Optional maxLines As Integer = 30)
        Console.WriteLine("ExitCode   : " & r.ExitCode)
        Console.WriteLine("Success    : " & r.Success)
        Console.WriteLine("OutputFile : " & r.OutputFile)
        Console.WriteLine("--- StandardOutput (" & CountLines(r.StandardOutput) & " 行) ---")
        Console.WriteLine(Truncate(r.StandardOutput, maxLines))
        Console.WriteLine("--- StandardError ---")
        Console.WriteLine(Truncate(r.StandardError, maxLines))
    End Sub

    Private Function CountLines(s As String) As Integer
        If String.IsNullOrEmpty(s) Then Return 0
        Return s.Split(New String() {vbCrLf, vbLf, vbCr}, StringSplitOptions.None).Length
    End Function

    Private Function Truncate(s As String, maxLines As Integer) As String
        If String.IsNullOrEmpty(s) Then Return "<空>"
        Dim lines As String() = s.Split(New String() {vbCrLf, vbLf, vbCr}, StringSplitOptions.None)
        Dim n As Integer = System.Math.Min(maxLines, lines.Length)
        Dim sb As New StringBuilder()
        For i As Integer = 0 To n - 1
            sb.AppendLine(lines(i))
        Next
        If lines.Length > maxLines Then sb.AppendLine("... (省略 " & (lines.Length - maxLines) & " 行)")
        Return sb.ToString()
    End Function

End Module
