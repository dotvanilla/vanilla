Imports System.Reflection
Imports System.Runtime.InteropServices
Imports VanillaBasic.WebAssembly.CodeAnalysis
Imports VanillaBasic.WebAssembly.Syntax

Namespace JavaScript

    Public Class Library

        Const ApiImports As MethodAttributes = MethodAttributes.Public Or MethodAttributes.Static Or MethodAttributes.PinvokeImpl

        Public Shared Iterator Function GetJavaScriptApi(Of T As Class)(workspace As Workspace) As IEnumerable(Of ImportsFunction)
            Dim methodList As MethodInfo() = GetType(T).GetMethods
            Dim javascriptModule As String = GetType(T).Name

            For Each api As MethodInfo In methodList
                If Not api.Attributes = ApiImports Then
                    Continue For
                End If

                Dim dllImports As DllImportAttribute = api.GetCustomAttribute(Of DllImportAttribute)
                Dim type As WATType = WATType.GetUnderlyingType(api.ReturnType, workspace)
                Dim parameters As DeclareLocal() = api _
                    .GetParameters _
                    .Select(Function(a)
                                Dim par As WATType = WATType.GetUnderlyingType(a.ParameterType, workspace)
                                Dim [default] As LiteralValue = Nothing

                                If a.IsOptional Then
                                    [default] = New LiteralValue(a.DefaultValue)
                                End If

                                Return New DeclareLocal(par) With {
                                    .Name = a.Name,
                                    .DefaultValue = [default]
                                }
                            End Function) _
                    .ToArray

                Yield New ImportsFunction(type) With {
                    .ImportsName = api.Name,
                    .ModuleName = javascriptModule,
                    .Name = dllImports.EntryPoint,
                    .Parameters = parameters
                }
            Next
        End Function

        Public Shared Sub [Imports](Of T As Class)(workspace As Workspace)
            For Each api As ImportsFunction In GetJavaScriptApi(Of T)(workspace)
                Call workspace.Imports.Add(api.Name, api)
            Next
        End Sub
    End Class
End Namespace