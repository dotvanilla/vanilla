#Region "Microsoft.VisualBasic::19a23141a7704617a05bb13483040345, SyntaxAnalysis\AttributeParser.vb"

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

'     Module AttributeParser
' 
'         Function: IsExtensionMethod
' 
' 
' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports Microsoft.VisualBasic.ComponentModel.Collection

Namespace SyntaxAnalysis.CompilerServices

    Module AttributeParser

        ReadOnly extensionAttrNames As Index(Of String) = {"Extension", "ExtensionAttribute"}

        ''' <summary>
        ''' Target method is an Exetnsion attribute marked method? have <see cref="ExtensionAttribute"/> marked?
        ''' </summary>
        ''' <param name="method"></param>
        ''' <returns></returns>
        <Extension>
        Public Function IsExtensionMethod(method As MethodBaseSyntax) As Boolean
            Dim attrs = method.AttributeLists.ToArray
            Dim name As String

            For Each group As AttributeListSyntax In attrs
                For Each attr As AttributeSyntax In group.Attributes
                    name = DirectCast(attr.Name, IdentifierNameSyntax).objectName

                    If name Like extensionAttrNames Then
                        Return True
                    End If
                Next
            Next

            Return False
        End Function
    End Module
End Namespace
