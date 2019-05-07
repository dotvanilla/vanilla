Imports System.Runtime.CompilerServices
Imports Microsoft.VisualBasic.Linq
Imports Microsoft.VisualBasic.Text
Imports Wasm.Compiler
Imports Wasm.TypeInfo

Namespace Symbols.Blocks

    Public MustInherit Class AbstractBlock : Inherits Expression

        ''' <summary>
        ''' The label of this block
        ''' </summary>
        ''' <returns></returns>
        Public Property guid As String

        ''' <summary>
        ''' By default no declares, returns an empty array 
        ''' </summary>
        ''' <returns></returns>
        Friend Overridable Iterator Function GetDeclareLocals() As IEnumerable(Of DeclareLocal)
            ' empty
        End Function
    End Class

    Public MustInherit Class Block : Inherits AbstractBlock

        Public Property internal As Expression()

        <MethodImpl(MethodImplOptions.AggressiveInlining)>
        Public Shared Function InternalBlock(block As IEnumerable(Of Expression), indent As String) As String
            Return block _
                .SafeQuery _
                .Select(Function(line)
                            Return indent & line.ToSExpression
                        End Function) _
                .JoinBy(ASCII.LF)
        End Function
    End Class

    ''' <summary>
    ''' 这个和<see cref="Block"/>类型的语法结构不一样，只是单纯的将表达式分组
    ''' </summary>
    Public Class ExpressionGroup : Inherits Expression

        Public Property group As Expression()

        Public Overrides Function TypeInfer(symbolTable As SymbolTable) As TypeAbstract
            Return TypeAbstract.void
        End Function

        Public Overrides Function ToSExpression() As String
            Return Block.InternalBlock(group, "    ")
        End Function
    End Class
End Namespace