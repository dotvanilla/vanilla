#Region "Microsoft.VisualBasic::0f1b18cc08ae7768433b59a03185994f, Compiler\SExpression\Extensions.vb"

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

'     Module Extensions
' 
'         Function: arrayInitialize, InitializeObjectManager, objectInitialize, ObjectMetaData, StringData
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Microsoft.VisualBasic.Text
Imports Wasm.Symbols
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Compiler.SExpression

    ''' <summary>
    ''' Helper for some special type when generate S-Expression
    ''' </summary>
    Module Extensions

        <Extension>
        Public Function StringData(memory As Memory) As String()
            Return memory _
                .Where(Function(oftype) TypeOf oftype Is StringSymbol) _
                .Select(Function(s) s.ToSExpression) _
                .ToArray
        End Function

        <Extension>
        Public Function ObjectMetaData(memory As Memory) As String()
            Return memory _
                .Where(Function(oftype) TypeOf oftype Is MetaJSON) _
                .Select(Function(s) s.ToSExpression) _
                .ToArray
        End Function

        <Extension>
        Public Iterator Function arrayInitialize(array As ArrayBlock) As IEnumerable(Of String)
            Yield ""
            Yield New CommentText($"Save {array.length} array element data to memory:")
            Yield New CommentText($"Array memory block begin at location: {array.memoryPtr}")

            For Each element As Expression In array
                If TypeOf element Is FuncInvoke Then
                    Dim func As FuncInvoke = element

                    If func.refer.Type = SymbolType.Operator AndAlso func.refer.Symbol = "i32.store" Then
                        ' 写入的是一个对象数据
                        ' i32.store intptr value
                        Dim value As Expression = func.parameters(1)

                        If TypeOf value Is UserObject Then
                            For Each init As Expression In DirectCast(value, UserObject)
                                Yield init.ToSExpression
                            Next
                        End If

                        Yield element.ToSExpression
                    Else
                        Yield element.ToSExpression
                    End If
                Else
                    Yield element.ToSExpression
                End If
            Next

            Yield New CommentText("Assign array memory data to another expression")
        End Function

        <Extension>
        Public Iterator Function objectInitialize(obj As UserObject) As IEnumerable(Of String)
            Yield ""
            Yield New CommentText($"Initialize a object instance of [{obj.UnderlyingType.raw}]")
            Yield New CommentText($"Object memory block begin at location: {obj.memoryPtr}")

            For Each element As Expression In obj
                Yield element.ToSExpression
            Next

            Yield New CommentText($"Initialize an object memory block with {obj.width} bytes data")
            Yield ""
        End Function

        ''' <summary>
        ''' 需要跳过最开始的字符串的位置
        ''' </summary>
        ''' <param name="memory"></param>
        ''' <returns></returns>
        <Extension>
        Public Function InitializeObjectManager(memory As Memory) As DeclareGlobal
            Dim offset As Integer = memory.TotalSize
            Dim init As New DeclareGlobal(IMemoryObject.ObjectManager) With {
                .init = Literal.i32(offset)
            }

            Return init
        End Function
    End Module
End Namespace
