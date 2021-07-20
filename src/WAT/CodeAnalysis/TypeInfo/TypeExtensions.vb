
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

    End Module
End Namespace