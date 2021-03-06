﻿#Region "Microsoft.VisualBasic::3835b4dcbd4b270ef42e7722f6e46588, test\globalStarter.vb"

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

    ' Module treeTest
    ' 
    '     Sub: documentApitest, FileTest, Main
    ' 
    ' Module Test
    ' 
    '     Sub: filetest, globalTest, projectTest, testDemo
    ' 
    '  
    ' 
    '     Sub: IfTest
    ' 
    '  
    ' 
    ' 
    ' 
    ' /********************************************************************************/

#End Region

Imports Wasm.Compiler
Imports Wasm.Symbols
Imports Wasm.TypeInfo

Module treeTest

    Sub documentApitest()
        Dim moduletest = Wasm.CreateModule("..\Demo\vbscript\HelloWorld\App.vb")

        Console.WriteLine(moduletest.ToSExpression)

        moduletest.Compile("..\Demo\vbscript\HelloWorld.wasm")
        moduletest.ToSExpression.SaveTo("..\Demo\vbscript\HelloWorld.wast")
        moduletest.HexDump(True).SaveTo("..\Demo\vbscript\HelloWorld.dmp")

        Pause()
    End Sub

    Sub FileTest()
        For Each file As String In {
                "objectArrayInitlizeTest.vb",
                "nestedTest.vb",
                "objectArrayTest.vb",
                "GCtest.vb",
                "DeclareTest2.vb",
                "ForLoopTest2.vb",
                "rectangleArrayTest2.vb",
                "starterTest.vb",
                "rectangleArrayTest.vb",
                "functionVariableTest.vb",
                "logicalTest.vb",
                "functionReferenceTest.vb",
                "multipleLevelLoops.vb",
                 "loopTest4.vb",
                 "loopTest3.vb",
                "loopTest2.vb",
                "loopTest1.vb",
                "arrayDeclareTest2.vb",
                 "structurearrayTest.vb",
                "structuretest.vb",
                   "DeclareTest.vb",
                "differenceTest.vb",
                 "ClassTest3.vb",
                "multipleModuleContructortest.vb",
                "ClassTest.vb",
                 "arrayTest2.vb",
                 "arrayTest.vb",
                 "Modulemethod_test.vb",
                   "EnumTest.vb",
                  "Stringstest.vb",
                    "functionTest.vb",
                  "boolTest.vb",
                 "nullreferenceTest.vb",
                "ForLoopTest.vb",
                "incrementTest.vb",
                 "dictionarytest.vb"}

            Call filetest("..\test\" & file)

            Pause()
        Next
    End Sub

    Sub Main()

        'Dim type As RawType = New RawType("A")

        'For i As Integer = 0 To 5
        '    Call Console.WriteLine(type.IsArray)
        '    type = type.MakeArrayType
        'Next


        ' Call projectTest("D:\repo\home\vbscripts\WebGL-Demo\WebGL-Demo.vbproj")
        ' Call projectTest("D:\repo\home\vbscripts\base64\base64.vbproj")
        ' Call projectTest("D:\vanilla\test\demo_proj\HelloWorld.vbproj")

        ' Call documentApitest()

        '  Call Wasm.CompileWast("..\Demo\string.wast", "..\Demo\string.wasm")
        ' Call Wasm.CompileWast("..\Demo\new_test.wast", "..\Demo\new_test.wast")

        '  Pause()

        Call FileTest()

        Return

        Pause()

        Dim code = "Module Main

Public Function Main(x As Integer, Optional y& = 99) As Long

Dim z% = (1+1) / x 
Dim a& = 88888

x = z * y * 2

return x / 99 + a
End Function

Public Function Test1(a As Double) As Double

return a + Main(a, -1)

End Function

End Module"

        Dim moduleMain As ModuleSymbol = Wasm.CreateModule(code)

        Console.WriteLine(moduleMain.ToSExpression)

        ' compile vbcode to webassembly
        Dim log = moduleMain.Compile("X:\test.wasm")

        Dim hex = moduleMain.HexDump(verbose:=True)

        Call hex.SaveTo("X:\test.dmp")

        Pause()
    End Sub

    Sub projectTest(proj As String)
        Dim [moduletest] As ModuleSymbol = Wasm.CreateModuleFromProject(proj)

        Console.WriteLine(moduletest.ToSExpression)
        moduletest.Compile(proj.ChangeSuffix("wasm"))
        moduletest.ToSExpression.SaveTo(proj.ChangeSuffix("wast"))
        moduletest.HexDump(True).SaveTo(proj.ChangeSuffix("dmp"))

        Pause()
    End Sub

    Sub filetest(file As String)
        Dim moduletest As ModuleSymbol = Wasm.CreateModule(file)

        Console.WriteLine(moduletest.ToSExpression)
        moduletest.Compile(file.ChangeSuffix("wasm"))
        moduletest.ToSExpression.SaveTo(file.ChangeSuffix("wast"))
        moduletest.HexDump(True).SaveTo(file.ChangeSuffix("dmp"))
    End Sub

    Sub testDemo()
        Dim moduletest = Wasm.CreateModule("..\Demo\PoissonPDF\Math.vb")

        Console.WriteLine(moduletest.ToSExpression)

        moduletest.Compile("..\Demo\PoissonPDF.wasm")
        moduletest.ToSExpression.SaveTo("..\Demo\PoissonPDF.wast")
        moduletest.HexDump(True).SaveTo("..\Demo\PoissonPDF.dmp")

        Pause()
    End Sub

    Sub globalTest()
        Dim code = "
Module Test

Dim x As Double
Dim y&, z As Single

Public Function AddAndSet(x As Double) As Integer

Test.x = Test.x + x
Return Test.x

End Function

End Module
"
        Dim moduletest = Wasm.CreateModule(code)

        Console.WriteLine(moduletest.ToSExpression)

        moduletest.Compile("X:\global.wasm")
        moduletest.ToSExpression.SaveTo("X:\global.wast")
        moduletest.HexDump(True).SaveTo("X:\global.dmp")

        Pause()
    End Sub

    Sub IfTest()

        Dim code = "Module Test

Public Function Abs(x as Double) As Long

If x > 0 Then
Return 1
Else 
Return -10
End If

End Function

End Module"


        Dim moduletest = Wasm.CreateModule(code)

        Console.WriteLine(moduletest.ToSExpression)

        moduletest.Compile("X:\iF.wasm")
        moduletest.ToSExpression.SaveTo("X:\iF.wast")
        moduletest.HexDump(True).SaveTo("X:\iF.dmp")

        Pause()
    End Sub

End Module
