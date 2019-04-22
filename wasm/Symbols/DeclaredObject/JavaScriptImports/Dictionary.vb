Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel

Namespace Symbols.JavaScriptImports

    ''' <summary>
    ''' The javascript any object api
    ''' </summary>
    Public Module Dictionary

        ''' <summary>
        ''' Set key-value and then returns the table hash code
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property SetValue As New ImportSymbol With {
            .ImportObject = "set",
            .[Module] = "table",
            .Name = "table_set",
            .Package = NameOf(Dictionary),
            .Result = "i32",
            .Parameters = {
                New NamedValue(Of String)("table", "i32"),
                New NamedValue(Of String)("key", "i32"),
                New NamedValue(Of String)("value", "any")
            }
        }

        Public ReadOnly Property GetValue As New ImportSymbol With {
            .ImportObject = "get",
            .[Module] = "table",
            .Name = "table_get",
            .Package = NameOf(Dictionary),
            .Result = "i32",
            .Parameters = {
                New NamedValue(Of String)("table", "i32"),
                New NamedValue(Of String)("key", "i32")
            }
        }

        Public ReadOnly Property RemoveValue As New ImportSymbol With {
            .ImportObject = "delete",
            .[Module] = "table",
            .Name = "table_delete",
            .Package = NameOf(Dictionary),
            .Result = "void",
            .Parameters = {
                New NamedValue(Of String)("table", "i32"),
                New NamedValue(Of String)("key", "i32")
            }
        }

        Public ReadOnly Property Create As New ImportSymbol With {
            .ImportObject = "create",
            .[Module] = "table",
            .Name = "table_new",
            .Package = NameOf(Dictionary),
            .Result = "i32",
            .Parameters = {}
        }
    End Module
End Namespace