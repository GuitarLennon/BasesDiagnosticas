Option Explicit On
Option Infer Off
Option Strict On
Option Compare Text

Namespace Programación

    ''' <summary>
    ''' Define a un término médico o no médico cualquiera que se puede buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Término
        Public Overridable ReadOnly Property Nombre As String
            Get
                Return MyClass.GetType.Name.Replace("_", " ")
            End Get
        End Property

        Public Overridable Property Sinónimos As String

        Public MustOverride Property Description As String

        Public Overridable Function EsEvocado(texto As String) As Boolean
            If Strings.InStr(texto, Me.Nombre, CompareMethod.Text) > 0 Then
                Return True
            Else
                If Sinónimos Is Nothing Then Return False
                For Each sinonimo As String In Sinónimos
                    If Strings.InStr(texto, sinonimo, CompareMethod.Text) > 0 Then Return True
                Next
            End If
            Return False
        End Function
    End Class

    Public Class UbicationOnText
        Property 
    End Class

End Namespace