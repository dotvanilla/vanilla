Imports System.Reflection
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.IL
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Public Delegate Function IMSILTranslation(parameters As ParameterInfo(), methodBody As IEnumerable(Of ILInstruction), workspace As Workspace) As IEnumerable(Of WATSyntax)

End Namespace