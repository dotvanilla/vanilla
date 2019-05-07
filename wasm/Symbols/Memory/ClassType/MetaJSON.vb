#Region "Microsoft.VisualBasic::44514e9e56a24517d91b7f814cd9f45b, Symbols\Memory\ClassType\MetaJSON.vb"

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

    '     Class MetaJSON
    ' 
    '         Properties: [class], [namespace], class_id, fields, isStruct
    '                     meta, methods
    ' 
    '         Function: ToSExpression, TypeInfer
    ' 
    '     Class FuncMetaJSON
    ' 
    '         Properties: isPublic, parameters, result
    ' 
    '         Function: ToString
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Microsoft.VisualBasic.ComponentModel.DataSourceModel.Repository
Imports Microsoft.VisualBasic.Net.Http
Imports Microsoft.VisualBasic.Serialization.JSON
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    ''' <summary>
    ''' Json data model of <see cref="ClassMeta"/> for javascript runtime
    ''' </summary>
    ''' <remarks>
    ''' 这个对象是写入到WebAssembly的内存之中的实际类型
    ''' </remarks>
    Public Class MetaJSON : Inherits IMemoryObject
        Implements IDeclaredObject

        Public Property [namespace] As String Implements IDeclaredObject.module
        Public Property [class] As String Implements IKeyedEntity(Of String).Key
        Public Property class_id As Integer
        Public Property isStruct As Boolean
        ''' <summary>
        ''' A mapping table of [Field name => field type]
        ''' </summary>
        ''' <returns></returns>
        Public Property fields As Dictionary(Of String, TypeAbstract)
        Public Property methods As Dictionary(Of String, FuncMetaJSON)

        Public ReadOnly Property reference As ReferenceSymbol
            Get
                Return New ReferenceSymbol With {
                    .[module] = [namespace],
                    .symbol = $"[{class_id}] {[class]}",
                    .type = SymbolType.Type
                }
            End Get
        End Property

        ''' <summary>
        ''' 先json序列化，然后base64编码
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property meta As StringSymbol
            Get
                Dim text As String = GetJson.Base64String
                Dim [string] As New StringSymbol With {
                    .[string] = text,
                    .memoryPtr = memoryPtr,
                    .comment = $"{If(isStruct, "structure", "class")} {reference}"
                }

                Return [string]
            End Get
        End Property

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.void
        End Function

        Public Overrides Function ToSExpression() As String
            Return meta.ToSExpression
        End Function
    End Class

    ''' <summary>
    ''' Function interface JSON data model for <see cref="MetaJSON.Methods"/>
    ''' </summary>
    Public Class FuncMetaJSON

        Public Property isPublic As Boolean
        Public Property parameters As Dictionary(Of String, TypeAbstract)
        Public Property result As TypeAbstract

        Public Overrides Function ToString() As String
            Return Me.GetJson
        End Function

    End Class
End Namespace
