#Region "Microsoft.VisualBasic::684e33d1e50cfaaac6e7dac20d80f940, Symbols\DeclaredObject\JavaScriptImports\String.vb"

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

'     Module [String]
' 
'         Properties: Append, IndexOf, Length, Replace, Trim
' 
'         Function: Method, ToString
' 
' 
' /********************************************************************************/

#End Region

Imports Wasm.TypeInfo

Namespace Symbols.JavaScriptImports

    Public Module [String]

        ''' <summary>
        ''' string append operator: a &amp; b
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Append As New ImportSymbol With {
            .importAlias = "add",
            .name = "string.add",
            .package = "string",
            .definedInModule = False,
            .[module] = "string",
            .result = New TypeAbstract(TypeAlias.string),
            .parameters = {
                "a".param(TypeAlias.string),
                "b".param(TypeAlias.string)
            }
        }

        Public ReadOnly Property Length As New ImportSymbol With {
            .importAlias = "length",
            .[module] = "string",
            .name = "string.length",
            .definedInModule = False,
            .package = "string",
            .result = TypeAbstract.i32,
            .parameters = {
                "text".param(TypeAlias.string)
            }
        }

        Public ReadOnly Property Trim As New ImportSymbol With {
            .importAlias = "trim",
            .[module] = "string",
            .name = "string.trim",
            .definedInModule = False,
            .package = "string",
            .result = New TypeAbstract(TypeAlias.string),
            .parameters = {
                "s".param("string")
            }
        }

        Public ReadOnly Property Replace As New ImportSymbol With {
            .importAlias = "replace",
            .[module] = "string",
            .name = "string.replace",
            .definedInModule = False,
            .package = "string",
            .result = New TypeAbstract(TypeAlias.string),
            .parameters = {
                "input".param(TypeAlias.string),
                "find".param(TypeAlias.intptr),
                "replacement".param(TypeAlias.string)
            }
        }

        Public ReadOnly Property IndexOf As New ImportSymbol With {
            .importAlias = "indexOf",
            .[module] = "string",
            .name = "string.indexOf",
            .definedInModule = False,
            .package = "string",
            .result = TypeAbstract.i32,
            .parameters = {
                "input".param(TypeAlias.string),
                "find".param(TypeAlias.string)
            }
        }

        Public ReadOnly Property SubString As New ImportSymbol With {
            .importAlias = "substr",
            .[module] = "string",
            .name = "string.substr",
            .definedInModule = False,
            .package = "string",
            .parameters = {
                "input".param(TypeAlias.string),
                "start".param(TypeAlias.i32),
                "length".param(TypeAlias.i32)
            },
            .result = New TypeAbstract(TypeAlias.string)
        }

        Public ReadOnly Property Split As New ImportSymbol With {
            .importAlias = "split",
            .[module] = "string",
            .name = "string.split",
            .definedInModule = False,
            .package = "string",
            .parameters = {
                "input".param(TypeAlias.string),
                "deli".param(TypeAlias.string)
            },
            .result = New TypeAbstract(TypeAlias.intptr)
        }

        Public Function Method(name As String) As ImportSymbol
            Static index As Dictionary(Of String, ImportSymbol) = InternalIndexer.HandleVisualBasicSymbols(GetType([String]))

            If index.ContainsKey(name) Then
                Return index(name)
            Else
                Throw New MissingMethodException($"String.{name}")
            End If
        End Function

        ''' <summary>
        ''' 因为WebAssembly没有自动类型转换，所以在这里会需要对每一种数据类型都imports一个相同的函数来完成
        ''' </summary>
        ''' <param name="type"></param>
        ''' <returns></returns>
        Public Function ToString(Optional type As String = "i32") As ImportSymbol
            Return New ImportSymbol With {
                .importAlias = "toString",
                .name = $"{type}.toString",
                .[module] = "string",
                .definedInModule = False,
                .package = "string",
                .result = New TypeAbstract(TypeAlias.string),
                .parameters = {
                    "x".param(type)
                }
            }
        End Function
    End Module
End Namespace
