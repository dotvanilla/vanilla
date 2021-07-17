Imports System.Runtime.CompilerServices
Imports VanillaBasic.WebAssembly.Syntax

Namespace CodeAnalysis

    <HideModuleName> Public Module Extensions

        <Extension>
        Public Function Copy(work As Workspace) As Workspace
            Return New Workspace With {
                .DefaultNamespace = work.DefaultNamespace,
                .AssemblyInfo = work.AssemblyInfo,
                .EnumVals = New Dictionary(Of String, EnumSymbol)(work.EnumVals),
                .Types = New Dictionary(Of String, TypeSchema)(work.Types),
                .GlobalSymbols = New Dictionary(Of String, DeclareGlobal)(work.GlobalSymbols),
                .[Imports] = New Dictionary(Of String, ImportsFunction)(work.Imports),
                .Methods = New Dictionary(Of String, FunctionDeclare)(work.Methods),
                .MSIL = work.MSIL,
                .Memory = work.Memory.CopyMemory
            }
        End Function

        <Extension>
        Private Function CopyMemory(memory As MemoryBuffer) As MemoryBuffer
            Return New MemoryBuffer(memory)
        End Function
    End Module
End Namespace