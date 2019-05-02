#Region "Microsoft.VisualBasic::7d3625b3470c1f7c4472d32d88dbe02b, Type\TypeEquality.vb"

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

    ' Class TypeEquality
    ' 
    '     Properties: Test
    ' 
    '     Constructor: (+1 Overloads) Sub New
    '     Function: Equals, GetHashCode, IsTargetType
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.Collection

Public NotInheritable Class TypeEquality : Implements IEqualityComparer(Of TypeAbstract)

    ReadOnly arrayList As Index(Of TypeAlias) = {TypeAlias.array, TypeAlias.list}

    Public Shared ReadOnly Property Test As New TypeEquality

    Private Sub New()
    End Sub

    Public Shared Function IsTargetType(target As TypeAbstract) As Func(Of TypeAbstract, Boolean)
        Return Function(other) Test.Equals(target, other)
    End Function

    Public Overloads Function Equals(x As TypeAbstract, y As TypeAbstract) As Boolean Implements IEqualityComparer(Of TypeAbstract).Equals
        If x.type <> y.type Then
            Return False
        End If

        If x.type Like arrayList AndAlso x.type = y.type Then
            If x.generic.IsNullOrEmpty Then
                ' 是一个通用的list列表
                Return True
            Else
                Return Equals(x.generic(Scan0), y.generic(Scan0))
            End If
        End If

        Return True
    End Function

    Public Overloads Function GetHashCode(obj As TypeAbstract) As Integer Implements IEqualityComparer(Of TypeAbstract).GetHashCode
        Return obj.GetHashCode
    End Function
End Class
