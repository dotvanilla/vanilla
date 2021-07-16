Imports System.Reflection
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.IL
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace Compiler

    Public Class MSILAsm

        ReadOnly IL As MethodBodyReader
        ReadOnly project As Workspace

        ReadOnly evaluation_stack As New Stack(Of ILInstruction)
        ReadOnly local_variable As ILInstruction() = New ILInstruction(256 - 1) {}

        Sub New(IL As MethodBodyReader, project As Workspace)
            Me.IL = IL
            Me.project = project
        End Sub

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

        Public Iterator Function GetWASM() As IEnumerable(Of WATSyntax)
            For Each line As ILInstruction In IL.Where(Function(i) i.Code.Name <> "br.s" AndAlso i.Code.Name <> "nop")
                Dim codeName As String = line.Code.Name
                Dim index As Integer = -1

                If codeName = "add.ovf" Then
                    Dim bin As New BinaryOperator With {
                        .[operator] = "i32.add",
                        .left = GetValue(evaluation_stack.Pop, project),
                        .right = GetValue(evaluation_stack.Pop, project),
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
                    Dim setGlobal As New SymbolSetValue With {
                        .Target = New SymbolReference(type) With {
                            .Name = $"{target.DeclaringType.Name}.{target.Name}"
                        },
                        .Value = GetValue(evaluation_stack.Pop, project),
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

    End Class
End Namespace