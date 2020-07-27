Imports Wasm.TypeInfo

Namespace Symbols.JavaScriptImports

    Module Text

        Public Property parseFloat As New ImportSymbol With {
            .importAlias = "parseFloat",
            .definedInModule = False,
            .[module] = "string",
            .name = "parseFloat",
            .package = "string",
            .parameters = {"input".param(TypeAlias.string)},
            .result = New TypeAbstract(TypeAlias.f64)
        }

        Public Property parseInt As New ImportSymbol With {
            .importAlias = "parseInt",
            .definedInModule = False,
            .[module] = "string",
            .name = "parseInt",
            .package = "string",
            .parameters = {"input".param(TypeAlias.string)},
            .result = New TypeAbstract(TypeAlias.i32)
        }

    End Module
End Namespace