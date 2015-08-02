Option Explicit On
Option Infer Off
Option Strict On
Option Compare Text

Imports Diagnósticos.BasesDiagnósticas

Namespace Programación
    ''' <summary>
    ''' Define a un término médico o no médico cualquiera que se puede buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Término

#Region "Property"

        ''' <summary>
        ''' Nombre del término, que será el mismo que el de la clase sin guiones bajos y no se podrá cambiar
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable ReadOnly Property Nombre As String
            Get
                Return MyClass.GetType.Name.Replace("_", " ")
            End Get
        End Property

        ''' <summary>
        ''' Otros nombres reconocidos por el programa además del término principal, se podrá editar
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property Sinónimos As String

        ''' <summary>
        ''' La descripción del término descrito por esta clase
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Property Description As String
#End Region

#Region "Functions"
        ''' <summary>
        ''' Obtiene la ubicación del término en el texto
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function ObtenerUbicaciones(texto As Texto) As UbicaciónEnTexto()
            Dim result As UbicaciónEnTexto() 'New List(Of UbicationOnText)
            result = texto.GetUbicationOnText(Nombre)
            If result IsNot Nothing AndAlso result.Count > 0 Then Return result

            For Each s As String In Sinónimos
                result = texto.GetUbicationOnText(s)
                If result IsNot Nothing AndAlso result.Count > 0 Then Return result
            Next

            Return Nothing
        End Function

        ''' <summary>
        ''' Determina si existe este término en el texto
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function Existe(texto As Texto) As Boolean
            Return texto.Exists(Nombre)
        End Function
#End Region

    End Class
End Namespace