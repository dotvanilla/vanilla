' Wat2WasmResult：wat2wasm.exe 一次运行的封装结果。
' 调用方依据 Success 与 OutputFile 自行读取生成的 .wasm 文件。
Public Class Wat2WasmResult

    Private ReadOnly _exitCode As Integer
    Private ReadOnly _standardOutput As String
    Private ReadOnly _standardError As String
    Private ReadOnly _outputFile As String
    Private ReadOnly _success As Boolean

    Public ReadOnly Property ExitCode As Integer
        Get
            Return _exitCode
        End Get
    End Property

    Public ReadOnly Property StandardOutput As String
        Get
            Return _standardOutput
        End Get
    End Property

    Public ReadOnly Property StandardError As String
        Get
            Return _standardError
        End Get
    End Property

    Public ReadOnly Property OutputFile As String
        Get
            Return _outputFile
        End Get
    End Property

    Public ReadOnly Property Success As Boolean
        Get
            Return _success
        End Get
    End Property

    Public Sub New(exitCode As Integer, standardOutput As String, standardError As String, outputFile As String)
        _exitCode = exitCode
        _standardOutput = If(standardOutput, "")
        _standardError = If(standardError, "")
        _outputFile = If(outputFile, "")
        ' 成功判定：退出码为 0 且标准错误为空（wat2wasm 仅在出错时写 stderr 并返回非 0）。
        _success = (exitCode = 0) AndAlso String.IsNullOrEmpty(_standardError)
    End Sub

End Class
