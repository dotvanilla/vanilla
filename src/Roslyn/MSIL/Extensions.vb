Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.IL
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

<Assembly: InternalsVisibleTo("vanilla")>

Namespace MSIL

    <HideModuleName>
    Public Module Extensions

        Public Function ParseMSIL(parameters As ParameterInfo(), methodBody As IEnumerable(Of ILInstruction), workspace As Workspace) As IEnumerable(Of WATSyntax)
            Dim translator As New MSILTranslator(parameters, methodBody, workspace)
            Dim result As WATSyntax() = translator.Interpret.ToArray

            Return result
        End Function
    End Module
End Namespace