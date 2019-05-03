Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' Json data model of <see cref="ClassMeta"/>
    ''' </summary>
    Public Class MetaJSON : Inherits IMemoryObject
        Implements IDeclaredObject

        Public Property [Namespace] As String Implements IDeclaredObject.Module
        Public Property ClassName As String Implements IKeyedEntity(Of String).Key
        ''' <summary>
        ''' A mapping table of [Field name => field type]
        ''' </summary>
        ''' <returns></returns>
        Public Property Fields As Dictionary(Of String, String)
        Public Property Methods As FuncSignature()

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Throw New NotImplementedException()
        End Function

        Public Overrides Function ToSExpression() As String
            Throw New NotImplementedException()
        End Function
    End Class

    Public Class FuncMetaJSON

    End Class
End Namespace