Imports VanillaBasic.WebAssembly.CodeAnalysis.TypeInfo.Operator
Imports VanillaBasic.WebAssembly.CodeAnalysis

Namespace Syntax

    Public Class [For] : Inherits WASMLoop

        Public Property control As DeclareLocal
        Public Property stepvalue As WATSyntax
        Public Property initFrom As WATSyntax

        ''' <summary>
        ''' set for loop variable init value.
        '''  
        ''' (VB中的for循环的循环变量在WASM之中为一个本地变量)
        ''' </summary>
        ''' <returns></returns>
        Public Function SetLocalVariable(context As Environment) As WATSyntax
            Return New SymbolSetValue With {
                .isGlobal = False,
                .Target = New SymbolReference(control.Name),
                .Value = CTypeHandle.CType(control.Type, initFrom, context)
            }
        End Function

    End Class
End Namespace