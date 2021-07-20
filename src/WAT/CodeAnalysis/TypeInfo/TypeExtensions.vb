
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ComponentModel.Collection

Namespace CodeAnalysis

    Public Module TypeExtensions

        ''' <summary>
        ''' 在进行类型转换的是否，会需要使用这个索引来判断类型的优先度，同时，也可以使用这个索引来判断类型是否为基础类型
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property NumberOrders As Index(Of WATElements) = {
            WATElements.i32,
            WATElements.f32,
            WATElements.i64,
            WATElements.f64
        }

        Friend ReadOnly integerType As Index(Of String) = {"i32", "i64"}
        Friend ReadOnly floatType As Index(Of String) = {"f32", "f64"}

        <Extension>
        Public Function MakeArrayType(type As WATType) As WATType
            Throw New NotImplementedException
        End Function

        Public Function IsDirectCastCapable(left As WATElements, right As WATElements) As Boolean
            If left.ToString Like integerType Then
                Return right.ToString Like integerType
            ElseIf left.ToString Like floatType Then
                Return right.ToString Like floatType
            End If

            Return False
        End Function
    End Module
End Namespace