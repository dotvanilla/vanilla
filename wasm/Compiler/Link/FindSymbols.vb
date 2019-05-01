#Region "Microsoft.VisualBasic::b61bf24aad0c63748234c0bb0130a9f1, Compiler\Link\FindSymbols.vb"

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

    '     Module FindSymbols
    ' 
    '         Function: FindEnumValue, FindModuleGlobal, FindModuleMemberFunction, FindTypeMethod, handleStringMethods
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Wasm.Symbols

Namespace Compiler

    ''' <summary>
    ''' Find symbol helper
    ''' </summary>
    Module FindSymbols

        <Extension>
        Public Function FindModuleMemberFunction(symbols As SymbolTable, context$, name$) As FuncSignature
            Dim funcs = symbols.functionList.TryGetValue(name)

            If funcs Is Nothing Then
                Return Nothing
            Else
                Return funcs.FindSymbol(context Or symbols.currentModuleLabel)
            End If
        End Function

        ''' <summary>
        ''' Get global variable type
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="context$"></param>
        ''' <param name="name$"></param>
        ''' <returns></returns>
        <Extension>
        Public Function FindModuleGlobal(symbols As SymbolTable, context$, name$) As DeclareGlobal
            Dim ref = symbols.globals.TryGetValue(name)

            If ref Is Nothing Then
                Return Nothing
            Else
                Return ref.FindSymbol(context Or symbols.currentModuleLabel)
            End If
        End Function

        Public Function FindEnumValue(symbols As SymbolTable, context$, name$) As EnumSymbol
            Return symbols.enumConstants.TryGetValue(context)
        End Function

        ''' <summary>
        ''' 查找某一类型的方法，或者拓展函数，第一个参数必须是目标的类型
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="context">变量名称或者类型名称</param>
        ''' <param name="name">函数名称</param>
        ''' <returns></returns>
        <Extension>
        Public Function FindTypeMethod(symbols As SymbolTable, context$, name$) As FuncSignature
            ' 首先按照变量类型查找
            ' 假设context是一个变量
            Dim contextObj As DeclareLocal = symbols.GetObjectSymbol(context)

            If contextObj Is Nothing Then
                ' 局部变量查找失败，则查找全局变量
                With symbols.globals.TryGetValue(context)
                    If Not .IsNothing Then
                        Dim [global] As DeclareGlobal = .FindSymbol(symbols.currentModuleLabel)

                        If Not [global] Is Nothing Then
                            contextObj = [global].AsLocal
                        End If
                    End If
                End With
            End If

            Dim typeContext As TypeAbstract

            If Not contextObj Is Nothing Then
                ' 找到了变量，则转换为类型
                typeContext = contextObj.TypeInfer(symbols)
            ElseIf context Like symbols.ModuleNames Then
                ' 模块方法引用
                Return symbols.FindModuleMemberFunction(context, name)
            Else
                ' 没有找到，则可能是一个类型，或者模块方法的引用
                typeContext = New TypeAbstract(context)
            End If

            If typeContext.type = TypeAlias.string Then
                Return symbols.handleStringMethods(name)
            End If

            ' 接着按照类型查找函数
            Dim funcList = symbols.functionList.TryGetValue(name)

            ' 但是不存在目标名称的函数
            If funcList Is Nothing Then
                Return Nothing
            Else
                ' 查找所有第一个参数为目标类型的函数
                Dim funcs As FuncSignature() = funcList _
                    .OfType(Of FuncSignature) _
                    .Where(Function(f) Not f.parameters.IsNullOrEmpty) _
                    .Where(Function(f)
                               Return f.parameters(Scan0).Value.Equals(typeContext)
                           End Function) _
                    .ToArray
                Dim func As FuncSignature

                If funcs.Length > 1 Then
                    ' 默认是当前模块的优先？
                    func = funcs.FirstOrDefault(Function(f) f.Module = symbols.currentModuleLabel)

                    If func Is Nothing Then
                        func = funcs.First
                    End If
                Else
                    func = funcs.FirstOrDefault
                End If

                Return func
            End If
        End Function

        <Extension>
        Private Function handleStringMethods(symbols As SymbolTable, name As String) As FuncSignature

        End Function
    End Module
End Namespace
