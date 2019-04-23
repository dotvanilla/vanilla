Public Module console

    Public Declare Sub log Lib "console" Alias "log" (obj As Object)
    Public Declare Sub warn Lib "console" Alias "warn" (obj As Object)
    Public Declare Sub info Lib "console" Alias "info" (obj As Object)
    Public Declare Sub [error] Lib "console" Alias "error" (obj As Object)
    Public Declare Sub table Lib "console" Alias "table" (obj As Object)
    Public Declare Sub trace Lib "console" Alias "trace" (obj As Object)
    Public Declare Sub debug Lib "console" Alias "debug" (obj As Object)

End Module
