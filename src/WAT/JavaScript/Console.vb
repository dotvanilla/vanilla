Namespace JavaScript

    Public Class Console

        ''' <summary>
        ''' The console.log() method outputs a message to the web console. The message 
        ''' may be a single string (with optional substitution values), 
        ''' or it may be any one or more JavaScript objects.
        ''' </summary>
        ''' <param name="msg">A JavaScript string containing zero or more substitution strings.</param>
        Public Declare Sub log Lib "console" Alias "Console.WriteLine" (msg As String)

    End Class
End Namespace


