Module multipleLevelLoops

    Sub Main()

        Dim i As Integer

        Do

            Do While i < 999

                Do

                    i += 1

                    If i Mod 3 = 0 Then
                        Exit Do
                    End If

                Loop Until i > 50

            Loop

            If i > 10 Then
                Exit Do
            End If
        Loop

    End Sub
End Module
