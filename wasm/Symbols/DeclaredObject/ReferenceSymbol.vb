#Region "Microsoft.VisualBasic::463f6d4051f18d4b88c550d46345c509, Symbols\DeclaredObject\ReferenceSymbol.vb"

    ' Author:
    ' 
    '       xieguigang (I@xieguigang.me)
    '       asuka (evia@lilithaf.me)
    '       wasm project (developer@vanillavb.app)
    ' 
    ' Copyright (c) 2019 developer@vanillavb.app, VanillaBasic(https://vanillavb.app)
    ' 
    ' 
    ' MIT License
    ' 
    ' 
    ' Permission is hereby granted, free of charge, to any person obtaining a copy
    ' of this software and associated documentation files (the "Software"), to deal
    ' in the Software without restriction, including without limitation the rights
    ' to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    ' copies of the Software, and to permit persons to whom the Software is
    ' furnished to do so, subject to the following conditions:
    ' 
    ' The above copyright notice and this permission notice shall be included in all
    ' copies or substantial portions of the Software.
    ' 
    ' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    ' IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    ' FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    ' AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    ' LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    ' OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    ' SOFTWARE.



    ' /********************************************************************************/

    ' Summaries:

    '     Class ReferenceSymbol
    ' 
    '         Properties: [module], symbol, type
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository

Namespace Symbols

    Public Class ReferenceSymbol : Implements IDeclaredObject

        Public Property [module] As String Implements IDeclaredObject.module
        Public Property symbol As String Implements IKeyedEntity(Of String).Key
        Public Property type As SymbolType

        Public Overrides Function ToString() As String
            If type = SymbolType.Operator OrElse type = SymbolType.LogicalOperator Then
                Return symbol
            ElseIf type = SymbolType.Api Or type = SymbolType.Type Then
                If [module].StringEmpty Then
                    Return symbol
                Else
                    Return [module] & "." & symbol
                End If
            Else
                Return [module] & "." & symbol
            End If
        End Function

        Public Shared Widening Operator CType(func As FuncSignature) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .type = SymbolType.Func,
                .[module] = func.module,
                .symbol = func.name
            }
        End Operator

        Public Shared Widening Operator CType(func As ImportSymbol) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .type = SymbolType.Api,
                .[module] = If(func.definedInModule, func.module, Nothing),
                .symbol = func.name
            }
        End Operator

        Public Shared Widening Operator CType(g As DeclareGlobal) As ReferenceSymbol
            Return New ReferenceSymbol With {
                .type = SymbolType.GlobalVariable,
                .[module] = g.module,
                .symbol = g.name
            }
        End Operator
    End Class
End Namespace
