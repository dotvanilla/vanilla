Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.IL
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.JavaScript
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Module WATWriter

        Private Iterator Function GetWASM(IL As MethodBodyReader) As IEnumerable(Of WATSyntax)

        End Function

        <Extension>
        Private Iterator Function GetMethods(heapMgr As Type, project As Workspace) As IEnumerable(Of FunctionDeclare)
            For Each method As MethodInfo In heapMgr.GetMethods
                Dim IL As New MethodBodyReader(method)
                Dim body As WATSyntax() = GetWASM(IL).ToArray
                Dim parameters As DeclareLocal() = method _
                    .GetParameters _
                    .Select(Function(a)
                                Return Library.ToParameter(a, project)
                            End Function) _
                    .ToArray
                Dim func As New FunctionDeclare(WATType.GetUnderlyingType(method.ReturnType, project)) With {
                    .parameters = parameters,
                    .[namespace] = "WASM",
                    .Name = method.Name,
                    .body = body.ToArray
                }

                Yield func
            Next
        End Function

        Public Function WriteWAT(project As Workspace) As String
            Dim heapMgr As Type = GetType(ObjectManager)
            Dim methods As FunctionDeclare() = heapMgr.GetMethods(project).ToArray
            Dim wast As String() = methods.ToSExpression(project).ToArray

            Return $"    
    ;; memory allocate in javascript runtime
    {wast.JoinBy(ASCII.LF)}"
        End Function

        Public Function GetHeapSize0(memory As MemoryBuffer) As String
            Return $"
    ;; A global object manager for create user object in WebAssembly
    ;; Its initialize value is the total size of the string data
    ;; of this webassembly module
    (global $WATMemoryBufferSize (mut i32) (i32.const {memory.TotalSize}))"
        End Function
    End Module
End Namespace