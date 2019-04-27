﻿#Region "Microsoft.VisualBasic::0dd94f23ecd242379050a598204eb9c5, Symbols\DeclaredObject\JavaScriptImports\Dictionary.vb"

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
    ' 
    ' /********************************************************************************/

#End Region

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
            .result = TypeAbstract.void,
            .parameters = {
                "table".param(TypeAlias.table),
                "key".param(TypeAlias.any),
                "value".param(TypeAlias.any)
            }
        }

        Public ReadOnly Property GetValue As New ImportSymbol With {
            .ImportObject = "get",
            .[Module] = "table",
            .Name = "table_get",
            .Package = NameOf(Dictionary),
            .result = New TypeAbstract(TypeAlias.any),
            .parameters = {
                "table".param(TypeAlias.table),
                "key".param(TypeAlias.any)
            }
        }

        Public ReadOnly Property RemoveValue As New ImportSymbol With {
            .ImportObject = "delete",
            .[Module] = "table",
            .Name = "table_delete",
            .Package = NameOf(Dictionary),
            .result = TypeAbstract.void,
            .parameters = {
                 "table".param(TypeAlias.table),
                 "key".param(TypeAlias.any)
            }
        }

        Public ReadOnly Property Create As New ImportSymbol With {
            .ImportObject = "create",
            .[Module] = "table",
            .Name = "table_new",
            .Package = NameOf(Dictionary),
            .result = New TypeAbstract(TypeAlias.table),
            .parameters = {}
        }
    End Module
End Namespace
