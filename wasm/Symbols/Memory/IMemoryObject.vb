#Region "Microsoft.VisualBasic::70535483df824a673a83338f88026c39, Symbols\Memory\IMemoryObject.vb"

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

'     Class IMemoryObject
' 
'         Properties: memoryPtr, ObjectManager
' 
'         Function: [AddressOf], GetSymbolReference, (+2 Overloads) IndexOffset
' 
' 
' /********************************************************************************/

#End Region

Imports System.Web.Script.Serialization
Imports System.Xml.Serialization
Imports Microsoft.VisualBasic.Language
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public MustInherit Class IMemoryObject : Inherits Expression

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>
        ''' 如果是一个表达式，则是一个动态资源，反之为字符串之类的静态资源
        ''' </remarks>
        ''' 
        <ScriptIgnore, XmlIgnore>
        Public Property memoryPtr As [Variant](Of Integer, Expression)

        Public Shared ReadOnly Property ObjectManager As New DeclareGlobal With {
            .init = Literal.i32(1),
            .[module] = "global",
            .name = NameOf(ObjectManager),
            .type = TypeAbstract.i32
        }

        Public Shared ReadOnly Property AddGCobject As New ImportSymbol With {
            .name = "GC.addObject",
            .[module] = "GC",
            .definedInModule = False,
            .importAlias = "addObject",
            .package = "GC",
            .parameters = {
                "offset".param("i32"),
                "class_id".param("i32")
            },
            .result = TypeAbstract.void
        }

        ''' <summary>
        ''' 进行新的用户对象的内存分配的函数
        ''' 
        ''' ```
        ''' func(size, class_id) as intptr
        ''' ```
        ''' </summary>
        ''' <returns></returns>
        Public Shared ReadOnly Property Allocate As New FuncSymbol With {
            .comment = "Add object information into javascript runtime",
            .locals = {
                New DeclareLocal With {.name = "offset", .init = ObjectManager.GetReference, .type = TypeAbstract.i32}
            },
            .parameters = {
                "sizeof".param("i32"),
                "class_id".param("i32")
            },
            .name = $"{NameOf(ObjectManager)}.{NameOf(Allocate)}",
            .result = TypeAbstract.i32,
            .[module] = "global",
            .body = allocateSteps(
                .locals(Scan0).GetReference,
                New GetLocalVariable(.parameters(Scan0).Name),
                New GetLocalVariable(.parameters(1).Name)
            ).ToArray
        }

        Private Shared Iterator Function allocateSteps(local As GetLocalVariable,
                                                       sizeof As GetLocalVariable,
                                                       class_id As GetLocalVariable) As IEnumerable(Of Expression)
            Yield New SetLocalVariable With {
                .var = local.var,
                .value = ObjectManager.GetReference
            }
            ' 将全局指针位移目标内存区域大小完成分配操作
            Yield New SetGlobalVariable(ObjectManager, IndexOffset(local, sizeof))
            ' 将对象写入javascript环境之中的内存回收模块
            Yield New FuncInvoke(AddGCobject) With {
                .[operator] = False,
                .parameters = {local, class_id}
            }
            ' 返回新内存区域的起始位置
            Yield New ReturnValue(local)
        End Function

        ''' <summary>
        ''' 这个对象在内存之中的起始位置
        ''' </summary>
        ''' <returns></returns>
        Public Overridable Function [AddressOf]() As Expression
            If memoryPtr Like GetType(Integer) Then
                Return Literal.i32(memoryPtr.TryCast(Of Integer))
            Else
                Return memoryPtr.TryCast(Of Expression)
            End If
        End Function

        Public Overrides Function GetSymbolReference() As IEnumerable(Of ReferenceSymbol)
            Return Me.AddressOf.GetSymbolReference
        End Function

        Public Shared Function IndexOffset(array As Expression, offset As Integer) As Expression
            Return IndexOffset(array, Literal.i32(offset))
        End Function

        ''' <summary>
        ''' 返回读写数组元素的内存的位置表达式
        ''' </summary>
        ''' <param name="array"></param>
        ''' <param name="offset"></param>
        ''' <returns></returns>
        ''' <remarks>
        ''' 因为array对象和i32对象之间不方便直接相加，所以在这里单独使用这个函数来计算实际的内存位置
        ''' </remarks>
        Public Shared Function IndexOffset(array As Expression, offset As Expression) As Expression
            Return New FuncInvoke() With {
                .[operator] = True,
                .parameters = {array, offset},
                .refer = i32Add
            }
        End Function
    End Class
End Namespace
