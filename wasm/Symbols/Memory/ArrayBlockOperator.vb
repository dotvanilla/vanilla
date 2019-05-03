Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Language
Imports Wasm.Compiler
Imports Wasm.Symbols.Parser
Imports Wasm.TypeInfo

Namespace Symbols.MemoryObject

    Public Module ArrayBlockOperator


        <Extension>
        Friend Function writeArray(array As ArraySymbol, symbols As SymbolTable, arrayType As TypeAbstract) As Expression
            Dim ofElement As TypeAbstract = arrayType.generic(Scan0)
            Dim arrayBlock As ArrayBlock = symbols.memory.AllocateArrayBlock(ofElement, array.Initialize.Length)
            Dim save As New List(Of Expression)
            Dim size As Integer = sizeOf(ofElement)
            Dim byteType$ = ofElement.typefit
            Dim intptr As Integer = arrayBlock.memoryPtr

            For Each element In array.Initialize
                element = CTypeHandle.CType(ofElement, element, symbols)
                save += BitConverter.save(byteType, intptr, element)
                intptr += size
            Next

            arrayBlock.elements = save

            Return arrayBlock
        End Function

        <Extension>
        Public Function GetArrayMember(array As GetLocalVariable, memberName$, symbols As SymbolTable) As Expression

        End Function

        <Extension>
        Public Function GetArrayElement(target As Expression, index As Expression, ofElement As TypeAbstract, symbols As SymbolTable) As Expression
            ' 从webassembly内存之中读取数据
            ' 对于数组对象而言，其值是一个内存区块的起始位置来的
            Dim intptr As Expression = target
            ' 然后位置的偏移量则是index索引，乘上元素的大小
            Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement)), "*", symbols)
            Dim read As Expression

            ' 然后得到实际的内存中的位置
            intptr = ArrayBlock.IndexOffset(intptr, offset)
            ' 最后使用load读取内存数据
            read = BitConverter.load(ofElement, intptr)

            Return read
        End Function

        <Extension>
        Public Function SetArrayElement(arraySymbol As GetLocalVariable, index As Expression, ofElement As TypeAbstract, right As Expression, symbols As SymbolTable) As Expression
            ' 从webassembly内存之中读取数据
            ' 对于数组对象而言，其值是一个内存区块的起始位置来的
            Dim intptr As Expression = arraySymbol
            ' 然后位置的偏移量则是index索引，乘上元素的大小
            Dim offset As Expression = BinaryStack(index, Literal.i32(sizeOf(ofElement)), "*", symbols)
            Dim save As Expression

            ' 然后得到实际的内存中的位置
            intptr = ArrayBlock.IndexOffset(intptr, offset)
            ' 最后使用load读取内存数据
            save = BitConverter.save(ofElement, intptr, CTypeHandle.CType(ofElement, right, symbols))

            Return save
        End Function
    End Module
End Namespace