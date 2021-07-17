Imports System.Reflection
Imports System.Reflection.Emit
Imports Microsoft.VisualBasic.ApplicationServices.Development.VisualStudio.IL
Imports Microsoft.VisualBasic.Language
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace MSIL

    ''' <summary>
    ''' Translate IL instruction code as Webassembly S-expression
    ''' </summary>
    Friend Class MSILTranslator

        ReadOnly instructions As ILInstruction()
        ReadOnly arguments As Object()
        ReadOnly stack As New Stack(Of Object)
        ReadOnly locals As Object() = New Object(255 - 1) {}
        ReadOnly workspace As Workspace

        Sub New(parameters As ParameterInfo(), methodBody As IEnumerable(Of ILInstruction), workspace As Workspace)
            Me.instructions = methodBody.ToArray
            Me.workspace = workspace
            Me.arguments = New List(Of Object) From {Me} + ParseArguments(parameters)
        End Sub

        Private Iterator Function ParseArguments(parameters As ParameterInfo()) As IEnumerable(Of Object)
            For Each par As ParameterInfo In parameters
                Dim type As WATType = WATType.GetUnderlyingType(par.ParameterType, workspace)

                Yield New DeclareLocal(type) With {
                    .Name = par.Name,
                    .DefaultValue = Nothing
                }
            Next
        End Function

        Public Iterator Function Interpret() As IEnumerable(Of WATSyntax)
            Dim offsetToIndexMapping = instructions _
                .Select(Function(instruction, index) (instruction.Offset, index)) _
                .ToDictionary(Function(x) x.Offset,
                              Function(x)
                                  Return x.index
                              End Function)
            Dim flowIndexer As Integer = 0

            While flowIndexer < instructions.Count
                Dim currentInstruction = instructions(flowIndexer)

                Select Case currentInstruction.Code.FlowControl
                    Case FlowControl.Branch, FlowControl.Cond_Branch
                        Dim jumpTo = InterpretBranchInstruction(currentInstruction)

                        If jumpTo = -1 Then
                            flowIndexer += 1
                            Continue While
                        End If

                        flowIndexer = offsetToIndexMapping(jumpTo)
                    Case FlowControl.Break
                        Throw New NotSupportedException(currentInstruction.ToString())
                    Case FlowControl.Call
                        InterpretCallInstruction(currentInstruction)
                        flowIndexer += 1
                    Case FlowControl.Meta
                        Throw New NotSupportedException(currentInstruction.ToString())
                    Case FlowControl.Next
                        InterpretNextInstruction(currentInstruction)
                        flowIndexer += 1
                    Case FlowControl.Return
                        If currentInstruction.Code.Name = "ret" Then
                            If stack.Count > 0 Then
                                Yield New ReturnValue(stack.Pop)
                            End If

                            flowIndexer += 1
                        Else
                            Throw New NotSupportedException(currentInstruction.ToString())
                        End If
                    Case FlowControl.Throw
                        Throw New NotSupportedException(currentInstruction.ToString())
                End Select
            End While
        End Function

        Private Function CastToSymbolReference(obj As WATSyntax) As WATSyntax

        End Function

        Private Sub InterpretNextInstruction(ByVal instruction As ILInstruction)
            Select Case instruction.Code.Name
                Case "add", "add.ovf"
                    Dim op2 As WATSyntax = DirectCast(PopFromStack(), WATSyntax)
                    Dim op1 As WATSyntax = DirectCast(PopFromStack(), WATSyntax)
                    Dim add As New BinaryOperator With {
                        .[operator] = "+",
                        .left = CastToSymbolReference(op1),
                        .right = CastToSymbolReference(op2),
                        .Annotation = instruction.ToString
                    }

                    PushToStack(add)

                Case "ceq"
                    Dim op2 As Object = PopFromStack()
                    Dim op1 As Object = PopFromStack()
                    PushToStack(If(op1 Is op2, 1, 0))

                Case "cgt"
                    Dim op2 As Object = PopFromStack()
                    Dim op1 As Object = PopFromStack()
                    Me.PushToStack(If(op1 > op2, 1, 0))

                Case "clt"
                    Dim value2 As Object = PopFromStack()
                    Dim value1 As Object = PopFromStack()
                    Me.PushToStack(If(value1 < value2, 1, 0))

                Case "conv.i4"
                    Dim value = PopFromStack()
                    PushToStack(Convert.ToInt32(value))

                Case "div"
                    Dim op2 As Object = PopFromStack()
                    Dim op1 As Object = PopFromStack()
                    PushToStack(op1 / op2)

                Case "dup"
                    Dim value = PopFromStack()
                    Dim duplicate = value
                    PushToStack(value)
                    PushToStack(duplicate)

                Case "isinst"
                    'Dim objReference = PopFromStack()
                    '' If the object reference itself is a null reference, then isinst likewise returns a null reference.
                    'If objReference Is Nothing Then PushToStack(Nothing)
                    'Dim objType = GetFromHeap(objReference).TypeHandler
                    'Dim possibleConversions = New List(Of DotType) From {
                    '    objType
                    '} ' TODO: test also against its base classes and interfaces
                    'Dim testAgainstType = LookUpType(TryCast(instruction.Operand, Type))
                    'Dim result = If(possibleConversions.Contains(testAgainstType), objReference, Nothing) ' TODO: return casted reference
                    'PushToStack(result)

                Case "ldarg.0", "ldarg.1", "ldarg.2", "ldarg.3", "ldarg.s"
                    Dim index = instruction.Code.Name.Split("."c)(1)
                    Dim argPosition As Integer

                    If Equals(index, "s") Then
                        argPosition = CByte(instruction.Operand)
                    Else
                        argPosition = Convert.ToInt32(index)
                    End If

                    PushToStack(arguments(argPosition))

                Case "ldc.i4.0"
                    PushToStack(0)
                Case "ldc.i4.1"
                    PushToStack(1)
                Case "ldc.i4.2"
                    PushToStack(2)
                Case "ldc.i4.3"
                    PushToStack(3)
                Case "ldc.i4.4"
                    PushToStack(4)
                Case "ldc.i4.5"
                    PushToStack(5)
                Case "ldc.i4.6"
                    PushToStack(6)
                Case "ldc.i4.7"
                    PushToStack(7)
                Case "ldc.i4.8"
                    PushToStack(8)
                Case "ldc.i4.m1"
                    PushToStack(-1)
                Case "ldc.i4.s"
                    PushToStack(Convert.ToInt32(instruction.Operand))
                Case "ldelem.i1", "ldelem.i2", "ldelem.i4"
                    'Dim index As Integer = CInt(PopFromStack())
                    'Dim arrayReference = CType(PopFromStack(), Guid)
                    'Dim array = TryCast(GetFromHeap(arrayReference)("Values"), Integer())
                    'PushToStack(array(index))

                Case "ldelem.ref", "ldelema"
                    'Dim index As Integer = CInt(PopFromStack())
                    'Dim arrayInstance = GetFromHeap(CType(PopFromStack(), Guid))
                    'Dim arrayValues = TryCast(arrayInstance("Values"), Guid())

                    'If arrayValues(index) = Guid.Empty Then
                    '    Dim instance As Object
                    '    Dim elementType = TryCast(instruction.Operand, Type)

                    '    If elementType IsNot Nothing Then
                    '        arrayValues(index) = CreateObjectInstance(LookUpType(elementType), instance)
                    '    Else
                    '        arrayValues(index) = CreateObjectInstance(TryCast(arrayInstance("ElementType"), DotType), instance)
                    '    End If
                    'End If

                    'PushToStack(arrayValues(index))

                Case "ldfld"
                    Dim instanceRef = PopFromStack()
                    Dim field As FieldInfo = TryCast(instruction.Operand, FieldInfo)
                    Dim fieldName = field.Name
                    Dim typeName As String = field.DeclaringType.Name
                    Dim type As WATType = WATType.GetUnderlyingType(field.FieldType, workspace)

                    PushToStack(New SymbolGetValue(type) With {.isGlobal = True, .Name = $"{typeName}.{fieldName}", .Annotation = instruction.ToString})

                Case "ldind.ref"
                    ' address is already on the stack

                Case "ldlen"
                    'Dim arrayRef = PopFromStack()
                    'Dim arrayInstance = GetFromHeap(arrayRef)
                    'PushToStack(arrayInstance("Length"))

                Case "ldloc.0"
                    PushLocalToStack(0)
                Case "ldloc.1"
                    PushLocalToStack(1)
                Case "ldloc.2"
                    PushLocalToStack(2)
                Case "ldloc.3"
                    PushLocalToStack(3)
                Case "ldloc.s"
                    PushLocalToStack(instruction.Operand)
                Case "ldnull"
                    PushToStack(Nothing)
                Case "ldstr"
                    'Dim stringInstance As Object
                    'Dim reference = CreateObjectInstance(LookUpType(GetType(String)), stringInstance)
                    'stringInstance("Value") = instruction.Operand
                    'PushToStack(reference)

                Case "newarr"
                    'Dim arrayInstance As Object
                    'Dim reference = CreateObjectInstance(LookUpType(GetType(Array)), arrayInstance)
                    'Dim elementType = CType(instruction.Operand, Type)
                    'Dim size As Integer = CInt(PopFromStack())
                    'arrayInstance("Length") = size

                    'If elementType.IsValueType Then
                    '    Dim array = System.Array.CreateInstance(elementType, size)
                    '    arrayInstance("Values") = array
                    'Else
                    '    Dim array = New Guid(size - 1) {}
                    '    arrayInstance("Values") = array
                    '    arrayInstance("ElementType") = LookUpType(elementType)
                    'End If

                    'PushToStack(reference)

                Case "nop"
                Case "pop"
                    PopFromStack()
                Case "stelem.i1", "stelem.i2", "stelem.i4"
                    'Dim value As Integer = CInt(PopFromStack())
                    'Dim index As Integer = CInt(PopFromStack())
                    'Dim arrayReference = CType(PopFromStack(), Guid)
                    'Dim array = TryCast(GetFromHeap(arrayReference)("Values"), Integer())
                    'array(index) = value

                Case "stelem.ref"
                    'Dim value As Object = PopFromStack()
                    'Dim index As Integer = CInt(PopFromStack())
                    'Dim arrayRef = CType(PopFromStack(), Guid)
                    'Dim arrayValues As Object = GetFromHeap(arrayRef)("Values")
                    'arrayValues(index) = value

                Case "stfld"
                    'Dim newFieldValue = PopFromStack()
                    'Dim instanceRef = PopFromStack()
                    'Dim instance = GetFromHeap(instanceRef)
                    'Dim fieldName = TryCast(instruction.Operand, FieldInfo).Name
                    'instance(fieldName) = newFieldValue

                Case "stind.ref"
                    'Dim value = PopFromStack()
                    'Dim objectRef = CType(PopFromStack(), Guid)
                    'Dim instance = GetFromHeap(objectRef)
                    'instance("Value") = GetFromHeap(value)("Value")

                Case "stloc.0"
                    PopFromStackToLocal(0)
                Case "stloc.1"
                    PopFromStackToLocal(1)
                Case "stloc.2"
                    PopFromStackToLocal(2)
                Case "stloc.3"
                    PopFromStackToLocal(3)
                Case "stloc.s"
                    PopFromStackToLocal(instruction.Operand)
                Case "sub"
                    Dim op2 As Object = PopFromStack()
                    Dim op1 As Object = PopFromStack()
                    PushToStack(op1 - op2)

                Case Else
                    Throw New NotImplementedException(instruction.Code.Name & " is not implemented.")
            End Select
        End Sub

        Private Sub InterpretCallInstruction(ByVal instruction As ILInstruction)
            Select Case instruction.Code.Name
                Case "call", "callvirt"
                    'Dim callee = LookUpMethod(TryCast(instruction.Operand, MethodBase))
                    'Dim args = PopArgumentsFromStack(callee)

                    'If Not callee.IsStatic Then
                    '    Dim instanceRef = PopFromStack()
                    '    args.Insert(0, instanceRef)

                    '    If Equals(instruction.Code.Name, "callvirt") Then ' a virtual method can be called non-virtually with call (e.g. base.ToString()) and then it would cause StackOverflow without this check
                    '        Dim method = TryCast(callee, DotMethod)

                    '        If method IsNot Nothing AndAlso method.IsVirtual Then
                    '            Dim instance = GetFromHeap(instanceRef)
                    '            callee = LookUpVirtualMethod(instance.TypeHandler, method)
                    '        End If
                    '    End If
                    'End If

                    'PushToCallStack(callee, args)
                    'Dim nestedInterp = New Interpreter(Runtime)
                    'nestedInterp.Execute(callee)

                Case "newobj"
                    'Dim ctor = LookUpMethod(TryCast(instruction.Operand, ConstructorInfo))
                    'Dim newObjReference = TryCast(ctor, DotConstructor).Invoke(Me)
                    'Dim args = PopArgumentsFromStack(ctor)
                    'args.Insert(0, newObjReference)
                    'PushToCallStack(ctor, args)
                    'Dim nestedInterp = New Interpreter(Runtime)
                    'nestedInterp.Execute(ctor)
                    'PushToStack(newObjReference)

                Case Else
                    Throw New NotImplementedException(instruction.Code.Name & " is not implemented.")
            End Select
        End Sub

        Private Function InterpretBranchInstruction(ByVal instruction As ILInstruction) As Integer
            Select Case instruction.Code.Name
                Case "br", "br.s"
                    Return instruction.Operand
                Case "brfalse", "brfalse.s"
                    Return If(CInt(PopFromStack()) = 0, CInt(instruction.Operand), -1)
                Case "brtrue", "brtrue.s"
                    Return If(CInt(PopFromStack()) = 1, CInt(instruction.Operand), -1)
                Case Else
                    Throw New NotImplementedException(instruction.Code.Name & " is not implemented.")
            End Select
        End Function

#Region "Stack, heap and locals manipulation"


#Region "Current method call stack and locals"

        Private Function PopArgumentsFromStack(ParametersCount As Integer) As List(Of Object)
            Dim args = New Object(ParametersCount - 1) {}

            For i = ParametersCount - 1 To 0 Step -1
                args(i) = PopFromStack()
            Next

            Return New List(Of Object)(args)
        End Function

        Friend Sub PushToStack(ByVal value As Object)
            stack.Push(value)
        End Sub

        Friend Function PopFromStack() As Object
            Return stack.Pop()
        End Function

        Private Sub PushLocalToStack(ByVal index As Byte)
            PushToStack(locals(index))
        End Sub

        Private Sub PopFromStackToLocal(ByVal index As Byte)
            locals(index) = stack.Pop()
        End Sub

#End Region

#End Region

    End Class
End Namespace
