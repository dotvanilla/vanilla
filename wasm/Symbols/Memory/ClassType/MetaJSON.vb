Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Net.Http
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' Json data model of <see cref="ClassMeta"/>
    ''' </summary>
    ''' <remarks>
    ''' 这个对象是写入到WebAssembly的内存之中的实际类型
    ''' </remarks>
    Public Class MetaJSON : Inherits IMemoryObject
        Implements IDeclaredObject

        Public Property [Namespace] As String Implements IDeclaredObject.Module
        Public Property [Class] As String Implements IKeyedEntity(Of String).Key
        ''' <summary>
        ''' A mapping table of [Field name => field type]
        ''' </summary>
        ''' <returns></returns>
        Public Property Fields As Dictionary(Of String, String)
        Public Property Methods As Dictionary(Of String, FuncMetaJSON)
        Public Property intptr As Integer

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.void
        End Function

        Public Overrides Function ToSExpression() As String
            ' 先json序列化，然后base64编码
            Dim text As String = GetJson.Base64String
            Dim [string] As New StringSymbol With {
                .[string] = text,
                .memoryPtr = intptr
            }

            Return [string].ToSExpression
        End Function
    End Class

    ''' <summary>
    ''' Function interface JSON data model for <see cref="MetaJSON.Methods"/>
    ''' </summary>
    Public Class FuncMetaJSON

        Public Property IsPublic As Boolean
        Public Property Parameters As Dictionary(Of String, TypeAbstract)
        Public Property Result As TypeAbstract

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class
End Namespace