Module dictionarytest

    Sub addValues()
        Dim table As New Dictionary(Of String, Double()) From {
            {"A", {56, 7, 56, 7}},
            {"GGGGGG", {353, 53, 48593, 465, 46}}
        }

        table = New Dictionary(Of String, Double())

        table("GG") = {44, 44}
        table.Add("AAAAAAA", {345, 654, 6, 5465445})

        table!XYZ = {89898}

        Dim z As Integer = table!ABC(999)
        Dim zz As Long = table("UI*")(100)

        table.Remove("QQQQQQ")

        Dim v As Double() = table("5678")

        Dim index As String() = table.Keys.ToArray
        Dim matrix As Double()() = table.Values.ToArray

    End Sub
End Module
