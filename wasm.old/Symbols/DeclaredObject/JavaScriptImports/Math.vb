#Region "Microsoft.VisualBasic::ef4fd9e77661f4a66f59dbd4e7c6478d, Symbols\DeclaredObject\JavaScriptImports\Math.vb"

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

'     Module Math
' 
'         Properties: Ceiling, Cos, Exp, Floor, Pow
'                     Rnd, Sqrt
' 
'         Function: Method
' 
'         Sub: DoImports
' 
' 
' /********************************************************************************/

#End Region

Imports Wasm.TypeInfo

Namespace Symbols.JavaScriptImports

    ''' <summary>
    ''' The javascript ``Math.xxx`` function imports
    ''' </summary>
    Public Module Math

        ''' <summary>
        ''' <see cref="System.Math.Pow"/>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Pow As New ImportSymbol With {
            .importAlias = "pow",
            .name = "Math.pow",
            .package = "Math",
            .definedInModule = False,
            .[module] = "Math",
            .result = TypeAbstract.f64,
            .parameters = {
                "a".param(TypeAlias.f64),
                "b".param(TypeAlias.f64)
            }
        }

        ''' <summary>
        ''' <see cref="System.Math.Sqrt(Double)"/>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Sqrt As New ImportSymbol With {
            .importAlias = "sqrt",
            .name = "Math.sqrt",
            .package = "Math",
            .definedInModule = False,
            .[module] = "Math",
            .result = TypeAbstract.f64,
            .parameters = {
                "a".param(TypeAlias.f64)
            }
        }

        ''' <summary>
        ''' <see cref="System.Math.Exp(Double)"/>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Exp As New ImportSymbol With {
            .importAlias = "exp",
            .name = "Math.exp",
            .package = "Math",
            .definedInModule = False,
            .[module] = "Math",
            .result = TypeAbstract.f64,
            .parameters = {
                "x".param(TypeAlias.f64)
            }
        }

        ''' <summary>
        ''' <see cref="System.Math.Cos(Double)"/>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Cos As New ImportSymbol With {
            .importAlias = "cos",
            .name = "Math.cos",
            .package = "Math",
            .definedInModule = False,
            .[module] = "Math",
            .result = TypeAbstract.f64,
            .parameters = {
                "x".param(TypeAlias.f64)
            }
        }

        ''' <summary>
        ''' <see cref="VBMath.Rnd()"/>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Rnd As New ImportSymbol With {
            .importAlias = "random",
            .name = "Math.random",
            .package = "Math",
            .definedInModule = False,
            .isExtensionMethod = False,
            .[module] = "Math",
            .parameters = {},
            .result = TypeAbstract.f64
        }

        ''' <summary>
        ''' <see cref="System.Math.Ceiling(Double)"/>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Ceiling As New ImportSymbol With {
            .importAlias = "ceil",
            .name = "Math.ceil",
            .package = "Math",
            .definedInModule = False,
            .isExtensionMethod = False,
            .[module] = "Math",
            .parameters = {"x".param(TypeAlias.f64)},
            .result = TypeAbstract.f64
        }

        ''' <summary>
        ''' <see cref="System.Math.Floor(Double)"/>
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property Floor As New ImportSymbol With {
            .importAlias = "floor",
            .name = "Math.floor",
            .package = "Math",
            .definedInModule = False,
            .isExtensionMethod = False,
            .[module] = "Math",
            .parameters = {"x".param(TypeAlias.f64)},
            .result = TypeAbstract.f64
        }

        Public Function Method(name As String) As ImportSymbol
            Static index As Dictionary(Of String, ImportSymbol) = InternalIndexer.HandleVisualBasicSymbols(GetType(Math))

            If index.ContainsKey(name) Then
                Return index(name)
            Else
                Throw New MissingMethodException($"Math.{name}")
            End If
        End Function
    End Module
End Namespace
