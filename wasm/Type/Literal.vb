#Region "Microsoft.VisualBasic::d2fce9ddd9b9c20b2a271e623712dafa, Type\Literal.vb"

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

    '     Class Literal
    ' 
    '         Properties: f32Max, f32Min, f64Max, f64Min
    ' 
    '         Constructor: (+1 Overloads) Sub New
    '         Function: bool, f32, f64, i32, i64
    '                   IntPtr
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Symbols
Imports Wasm.Symbols.MemoryObject

Namespace TypeInfo

    ''' <summary>
    ''' Helpers for create literal expression
    ''' </summary>
    Public NotInheritable Class Literal

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' https://github.com/WebAssembly/spec/blame/5f240d1c3e6d1f16ee8328c9cc6f3bb2e352aae9/test/core/const.wast#L128
        ''' </remarks>
        Public Shared ReadOnly Property f64Max As LiteralExpression
            Get
                Return f64(Double.MaxValue)
            End Get
        End Property

        Public Shared ReadOnly Property f64Min As LiteralExpression
            Get
                Return f64(Double.MinValue)
            End Get
        End Property

        Public Shared ReadOnly Property f32Max As LiteralExpression
            Get
                Return f32(Single.MaxValue)
            End Get
        End Property

        Public Shared ReadOnly Property f32Min As LiteralExpression
            Get
                Return f32(Single.MinValue)
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Shared Function [Nothing](type As TypeAbstract) As LiteralExpression
            Return New LiteralExpression With {
                .type = type,
                .value = "0",
                .comment = $"Nothing(Of {type.ToString})"
            }
        End Function

        ''' <summary>
        ''' Memory pointer type
        ''' </summary>
        ''' <param name="p"></param>
        ''' <returns></returns>
        Public Shared Function IntPtr(p As Integer) As IntPtr
            Return New IntPtr With {
                .expression = Literal.i32(p)
            }
        End Function

        Public Shared Function i32(i As Integer) As LiteralExpression
            Return New LiteralExpression With {
                .type = New TypeAbstract("i32"),
                .value = i
            }
        End Function

        Public Shared Function i64(i As Long) As LiteralExpression
            Return New LiteralExpression With {
                .type = New TypeAbstract("i64"),
                .value = i
            }
        End Function

        Public Shared Function f32(s As Single) As LiteralExpression
            Return New LiteralExpression With {
                .type = New TypeAbstract("f32"),
                .value = s.ToString("G17").ToLower
            }
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="f"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' https://github.com/WebAssembly/wabt/issues/1078
        ''' </remarks>
        Public Shared Function f64(f As Double) As LiteralExpression
            Return New LiteralExpression With {
                .type = New TypeAbstract("f64"),
                .value = f.ToString("G17").ToLower
            }
        End Function

        Public Shared Function bool(b As Boolean) As LiteralExpression
            Return New LiteralExpression With {
                .type = New TypeAbstract(GetType(Boolean)),
                .value = If(b, 1, 0)
            }
        End Function
    End Class
End Namespace
