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

        ''' <summary>
        ''' Obtiene la ubicación del término en el texto
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function ObtenerUbicaciones(texto As Texto) As UbicationOnText()
            Dim result As UbicationOnText() 'New List(Of UbicationOnText)
            result = texto.GetUbicationOnText(Nombre)
            If result IsNot Nothing AndAlso result.Count > 0 Then Return result

            For Each s As String In Sinónimos
                result = texto.GetUbicationOnText(s)
                If result IsNot Nothing AndAlso result.Count > 0 Then Return result
            Next

            Return Nothing
        End Function
    End Class

    Public Class Texto

        Private _líneas As New List(Of String)
        Private _texto As String

        Public Property CaracteresSeparadores As Char() = {CChar("."), CChar(";"), CChar(vbCrLf)}

        Public Property texto As String
            Get
                Return _texto
            End Get
            Set(value As String)
                _líneas = texto.Split(CaracteresSeparadores).ToList
                _texto = value
            End Set
        End Property

        Public Property Líneas As String()
            Get
                Return _líneas.ToArray
            End Get
            Protected Set(value As String())
                Me._texto = String.Join("", value)
            End Set
        End Property

        Public Function GetUbicationOnText(word As String) As UbicationOnText()
            Dim result As New List(Of UbicationOnText)
            Me._líneas.ForEach(Sub(s As String)
                                   If s.Contains(word) Then
                                       result.Add(New UbicationOnText With {.Oración = s,
                                                                            .NúmeroOración = Me._líneas.IndexOf(s),
                                                                            .CaracterInicioOración = Strings.InStr(s, word, CompareMethod.Text),
                                                                            .Longitud = word.Length})
                                   End If
                               End Sub)
            Return result.ToArray
        End Function

        Sub New(Texto As String)
            Me.texto = Texto
        End Sub

        Sub New()

        End Sub

    End Class

    Public Class UbicationOnText
        Property CaracterInicioOración As Long
        ReadOnly Property CaracterFinalOración As Long
            Get
                Return CaracterInicioOración + Longitud
            End Get
        End Property
        Property Longitud As Long
        Property NúmeroOración As Integer
        Property Oración As String
    End Class
End Namespace