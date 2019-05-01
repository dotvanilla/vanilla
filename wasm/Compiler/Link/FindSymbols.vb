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

        ' 查找的规则如下
        '
        ' 1. 假若目标名称是惟一的。则可以不依赖于模块名称
        ' 2. 假若目标名称在模块之间不唯一，则需要依赖于模块名称来查找，但是模块名称为空的话，则表示为当前模块

        <Extension>
        Public Function FindModuleMemberFunction(symbols As SymbolTable, context$, name$) As FuncSignature
            Dim funcs As ModuleOf = symbols.functionList.TryGetValue(name)

            ' 没有找到目标函数
            If funcs Is Nothing Then
                Return Nothing
            ElseIf funcs.IsUnique Then
                Return funcs.GetUniqueSymbol
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
            Dim ref As ModuleOf = symbols.globals.TryGetValue(name)

            If ref Is Nothing Then
                Return Nothing
            ElseIf ref.IsUnique Then
                Return ref.GetUniqueSymbol
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
                ' 函数调用时从一个存在的变量对象开始的
                Return symbols.findMethodByFirstSignature(typeContext, name, True)
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

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="symbols"></param>
        ''' <param name="typeContext">函数的第一个参数所要求的类型</param>
        ''' <param name="name">函数名</param>
        ''' <param name="callFromObj"> 
        ''' 这个函数调用是否是从一个存在的对象实例开始的，是的话则可能是拓展函数或者类型实例的成员函数
        ''' 
        ''' 如果是拓展函数，则优先从当前模块进行查找
        ''' </param>
        ''' <returns></returns>
        <Extension>
        Private Function findMethodByFirstSignature(symbols As SymbolTable, typeContext As TypeAbstract, name$, callFromObj As Boolean) As FuncSignature
            Dim test = Function(func As FuncSignature)
                           If func.parameters.IsNullOrEmpty Then
                               Return False
                           Else
                               Return TypeEquality.Test.Equals(typeContext, func.parameters(Scan0).Value)
                           End If
                       End Function
            Dim funcs As FuncSignature() = symbols.functionList(name) _
                .Select(Function(o) DirectCast(o, FuncSignature)) _
                .Where(test) _
                .ToArray

            If funcs.IsNullOrEmpty Then
                Return Nothing
            ElseIf funcs.Length = 1 Then
                Return funcs(Scan0)
            Else
                ' 有多个函数，则优先选取当前模块的拓展函数
                Dim currFuncs = funcs.Where(Function(f) f.Module = symbols.currentModuleLabel).ToArray

                If currFuncs.IsNullOrEmpty Then
                    Throw New NotImplementedException
                Else
                    Return currFuncs(Scan0)
                End If
            End If
        End Function

        <Extension>
        Private Function handleStringMethods(symbols As SymbolTable, name As String) As FuncSignature

        End Function
    End Module
End Namespace
