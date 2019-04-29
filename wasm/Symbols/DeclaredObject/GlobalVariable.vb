#Region "Microsoft.VisualBasic::07fd84ea5eda17975eceadba80d0533e, Symbols\DeclaredObject\GlobalVariable.vb"

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

'     Class DeclareGlobal
' 
'         Properties: [Module]
' 
'         Function: AsLocal, ToSExpression
' 
' 
' /********************************************************************************/

#End Region

Namespace Symbols

    ''' <summary>
    ''' 全局变量的初始值，只能够是常数或者其他的全局变量的值，也就是说<see cref="DeclareGlobal.init"/>的值只能够是常数
    ''' </summary>
    Public Class DeclareGlobal : Inherits DeclareVariable
        Implements IDeclaredObject

        ''' <summary>
        ''' The VB module name
        ''' </summary>
        ''' <returns></returns>
        Public Property [Module] As String Implements IDeclaredObject.Module

        Public Function GetReference() As GetGlobalVariable
            Return New GetGlobalVariable(name)
        End Function

        Public Function SetReference(value As Expression) As SetGlobalVariable
            Return New SetGlobalVariable With {
                .var = name,
                .value = value
            }
        End Function

        Public Function AsLocal() As DeclareLocal
            Return New DeclareLocal With {
                .name = name,
                .init = init,
                .type = type
            }
        End Function

        Public Overrides Function ToSExpression() As String
            Return $"(global ${name} (mut {type.typefit}) {init.ToSExpression})"
        End Function
    End Class
End Namespace
