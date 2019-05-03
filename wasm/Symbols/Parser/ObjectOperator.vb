Imports System.Runtime.CompilerServices
Imports Wasm.Compiler
Imports Wasm.Symbols.MemoryObject
Imports Wasm.TypeInfo

Namespace Symbols.Parser

    Public Module ObjectOperator

        <Extension>
        Public Function SetMemberField(objName$, memberName$, right As Expression, symbols As SymbolTable) As Expression
            Dim type As TypeAbstract = symbols.GetUnderlyingType(objName)
            Dim objType As ClassMeta = symbols.GetClassType(type.raw)
            Dim offset As Integer = objType.GetFieldOffset(memberName)
            Dim fieldType As TypeAbstract = objType(memberName).type
            Dim intptr As Expression = symbols.GetObjectReference(objName)

            intptr = ArrayBlock.IndexOffset(intptr, offset)
            right = CTypeHandle.CType(fieldType, right, symbols)

            ' 只需要将数据写入指定的内存位置即可完成实例对象的字段的赋值操作
            Return BitConverter.save(fieldType, intptr, right)
        End Function

        <Extension>
        Public Function GetMemberField(obj As GetLocalVariable, memberName$, symbols As SymbolTable) As Expression
            Dim type As ClassMeta = obj.GetUserType(symbols)
            Dim fieldOffset As Expression = Literal.i32(type.GetFieldOffset(memberName))
            Dim getValue As Expression

            fieldOffset = ArrayBlock.IndexOffset(obj, fieldOffset)
            getValue = BitConverter.load(type(memberName).type, fieldOffset)

            Return getValue
        End Function
    End Module
End Namespace