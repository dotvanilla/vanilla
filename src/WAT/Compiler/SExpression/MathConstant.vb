Imports VanillaBasic.WebAssembly.CodeAnalysis

Friend Structure MathConstant

    Dim [module] As String
    Dim name As String
    Dim value As Object

    Public Shared Iterator Function GetVBMathConstants() As IEnumerable(Of MathConstant)
        ' Math
        Yield New MathConstant With {.[module] = NameOf(Math), .name = NameOf(Math.PI), .value = Math.PI}
        Yield New MathConstant With {.[module] = NameOf(Math), .name = NameOf(Math.E), .value = Math.E}
        Yield New MathConstant With {.[module] = NameOf(Math), .name = NameOf(Math.Tau), .value = Math.Tau}

        Yield New MathConstant With {.[module] = "Integer", .name = NameOf(Integer.MinValue), .value = Integer.MinValue}
        Yield New MathConstant With {.[module] = "Integer", .name = NameOf(Integer.MaxValue), .value = Integer.MaxValue}

        Yield New MathConstant With {.[module] = "Long", .name = NameOf(Long.MinValue), .value = Long.MinValue}
        Yield New MathConstant With {.[module] = "Long", .name = NameOf(Long.MaxValue), .value = Long.MaxValue}

        Yield New MathConstant With {.[module] = "Single", .name = NameOf(Single.MinValue), .value = Single.MinValue}
        Yield New MathConstant With {.[module] = "Single", .name = NameOf(Single.MaxValue), .value = Single.MaxValue}

        Yield New MathConstant With {.[module] = "Double", .name = NameOf(Double.MinValue), .value = Double.MinValue}
        Yield New MathConstant With {.[module] = "Double", .name = NameOf(Double.MaxValue), .value = Double.MaxValue}
    End Function

    Private Function GetWATType() As WATElements
        Return WATType.GetElementType(value.GetType).UnderlyingWATType
    End Function

    Public Overrides Function ToString() As String
        Return $"(global ${[module]}.{name} (mut {GetWATType.ToString}) ({GetWATType.ToString}.const {value.ToString}))"
    End Function

End Structure
