Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.IL
Imports Microsoft.VisualBasic.Text
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.JavaScript
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Module WATWriter

        <Extension>
        Private Function GetValue(IL As ILInstruction, project As Workspace) As WATSyntax
            If IL.Code.Name = "ldfld" Then
                ' load field
                ' global symbol reference
                Dim target As FieldInfo = IL.Operand
                Dim type As WATType = WATType.GetUnderlyingType(target.FieldType, project)
                Dim symbol As New SymbolReference(type) With {
                    .Name = $"{target.DeclaringType.Name}.{target.Name}",
                    .Annotation = IL.ToString
                }

                Return symbol
            ElseIf IL.Code.Name.StartsWith("ldarg") Then
                Dim symbol As New SymbolReference(WATType.i32) With {
                    .Name = IL.Code.Name,
                    .Annotation = IL.ToString
                }

                Return symbol
            Else
                Throw New NotImplementedException
            End If
        End Function

        Private Iterator Function GetWASM(IL As MethodBodyReader, project As Workspace) As IEnumerable(Of WATSyntax)
            Dim evaluation_stack As New Stack(Of ILInstruction)
            Dim local_variable As ILInstruction() = New ILInstruction(256 - 1) {}

            For Each line As ILInstruction In IL.Where(Function(i) i.Code.Name <> "br.s" AndAlso i.Code.Name <> "nop")
                Dim codeName As String = line.Code.Name
                Dim index As Integer = -1

                If codeName = "add.ovf" Then
                    Dim bin As New BinaryOperator With {
                        .[operator] = "i32.add",
                        .left = evaluation_stack.Pop.GetValue(project),
                        .right = evaluation_stack.Pop.GetValue(project),
                        .Annotation = line.ToString
                    }

                    Yield bin
                Else
                    With codeName.Split("."c)
                        If .Length > 1 Then
                            index = Integer.Parse(.Last)
                        End If
                    End With
                End If

                If codeName.StartsWith("ldarg") Then
                    If local_variable(index) Is Nothing Then
                        local_variable(index) = line

                        Yield New DeclareLocal(WATType.i32) With {
                            .Name = codeName,
                            .Annotation = line.ToString
                        }
                    End If

                    evaluation_stack.Push(local_variable(index))

                ElseIf codeName.StartsWith("ldfld") Then
                    evaluation_stack.Push(line)
                ElseIf codeName.StartsWith("stloc") Then
                    local_variable(index) = evaluation_stack.Pop
                ElseIf codeName.StartsWith("ldloc") Then
                    ' Loads the local variable at index index onto stack.
                    evaluation_stack.Push(local_variable(index))
                ElseIf codeName = "stfld" Then
                    Dim target As FieldInfo = line.Operand
                    Dim type As WATType = WATType.GetUnderlyingType(target.FieldType, project)
                    Dim setGlobal As New SetGlobal With {
                        .Target = New SymbolReference(type) With {
                            .Name = $"{target.DeclaringType.Name}.{target.Name}"
                        },
                        .Value = evaluation_stack.Pop.GetValue(project),
                        .Annotation = line.ToString
                    }

                    Yield setGlobal
                ElseIf codeName = "ret" Then
                    Dim data As ILInstruction = evaluation_stack.Pop
                    Dim rtvl As New ReturnValue With {
                        .Value = GetValue(data, project),
                        .Annotation = line.ToString
                    }

                    Yield rtvl
                End If
            Next
        End Function

        <Extension>
        Private Iterator Function GetMethods(heapMgr As TypeInfo, project As Workspace, exports As List(Of ExportSymbol)) As IEnumerable(Of FunctionDeclare)
            For Each method As MethodInfo In heapMgr.DeclaredMembers.OfType(Of MethodInfo)
                If method.MethodImplementationFlags = MethodImplAttributes.InternalCall Then
                    Continue For
                ElseIf method.Attributes.HasFlag(MethodAttributes.Virtual) Then
                    Continue For
                End If

                Dim IL As New MethodBodyReader(method)
                Dim body As WATSyntax() = GetWASM(IL, project).ToArray
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

                If method.IsPublic Then
                    Call exports.Add(New ExportSymbol(func))
                End If

                Yield func
            Next
        End Function

        Public Function WriteWAT(project As Workspace) As String
            Dim heapMgr As Type = GetType(ObjectManager)
            Dim exports As New List(Of ExportSymbol)
            Dim methods As FunctionDeclare() = CType(heapMgr, TypeInfo).GetMethods(project, exports).ToArray
            Dim wast As String() = methods.ToSExpression(project).ToArray

            Return $"    
    ;; memory allocate in javascript runtime
    {wast.JoinBy(ASCII.LF)}
    
    ;; Export Api to JavaScript runtime for
    ;; expose GC in WASM module.
    {exports.Select(Function(api) api.ToSExpression).JoinBy(ASCII.LF)}
    "
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