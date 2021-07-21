#Region "Microsoft.VisualBasic::14756efc9ff98fa37c061c3c816cabd1, Symbols\DeclaredObject\JavaScriptImports\Dictionary.vb"

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

'     Module Dictionary
' 
'         Properties: Create, GetValue, RemoveValue, SetValue
' 
'         Function: Method
' 
' 
' /********************************************************************************/

#End Region

#If netcore5 = 1 Then
Imports System.Data
#End If
Imports Wasm.TypeInfo

Namespace Symbols.JavaScriptImports

    ''' <summary>
    ''' The javascript any object api
    ''' </summary>
    Public Module Dictionary

        ''' <summary>
        ''' Set key-value and then returns the table hash code
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Add As New ImportSymbol With {
            .importAlias = "set",
            .[module] = "table",
            .name = "table.set",
            .definedInModule = False,
            .package = NameOf(Dictionary),
            .result = TypeAbstract.void,
            .parameters = {
                "table".param(TypeAlias.table),
                "key".param(TypeAlias.any),
                "value".param(TypeAlias.any)
            }
        }

        Public ReadOnly Property GetValue As New ImportSymbol With {
            .importAlias = "get",
            .[module] = "table",
            .name = "table.get",
            .definedInModule = False,
            .package = NameOf(Dictionary),
            .result = New TypeAbstract(TypeAlias.any),
            .parameters = {
                "table".param(TypeAlias.table),
                "key".param(TypeAlias.any)
            }
        }

        Public ReadOnly Property Remove As New ImportSymbol With {
            .importAlias = "delete",
            .[module] = "table",
            .name = "table.delete",
            .definedInModule = False,
            .package = NameOf(Dictionary),
            .result = TypeAbstract.void,
            .parameters = {
                 "table".param(TypeAlias.table),
                 "key".param(TypeAlias.any)
            }
        }

        Public ReadOnly Property Create As New ImportSymbol With {
            .importAlias = "create",
            .[module] = "table",
            .name = "table.new",
            .definedInModule = False,
            .package = NameOf(Dictionary),
            .result = New TypeAbstract(TypeAlias.table),
            .parameters = {}
        }

        Public Function Method(name As String) As ImportSymbol
            Static index As Dictionary(Of String, ImportSymbol) = InternalIndexer.HandleVisualBasicSymbols(GetType(Dictionary))

            If index.ContainsKey(name) Then
                Return index(name)
            Else
                Throw New MissingPrimaryKeyException($"Dictionary(Of any, any).{name}")
            End If
        End Function
    End Module
End Namespace
