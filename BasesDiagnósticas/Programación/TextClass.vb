Option Explicit On
Option Infer Off
Option Strict On
Option Compare Text

Imports Diagnósticos.BasesDiagnósticas

Namespace Programación

    ''' <summary>
    ''' Superclase para String Permite trabajar con el texto y el XML y eso.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Texto

#Region "Instance"

        ''' <summary>
        ''' líneas del texto presente
        ''' </summary>
        ''' <remarks></remarks>
        Protected _líneas As New List(Of String)

        ''''' <summary>
        '''' Cadena de texto buffer
        '''' </summary>
        '''' <remarks></remarks>
        'Private _texto As String

#End Region

#Region "Property"

        Public Property CaracteresSeparadores As Char() = {CChar("."), CChar(";"), CChar(vbCrLf)}

        Public Property Texto As String
            Get

                Return String.Join("", _líneas)

            End Get
            Set(value As String)

                If value Is Nothing Then Stop
                _líneas = value.Split(CaracteresSeparadores).ToList
                '_texto = value

            End Set
        End Property

        Public Property Líneas As String()
            Get

                Return _líneas.ToArray

            End Get
            Protected Set(value As String())

                'Me._texto = String.Join("", value)
                '_líneas.Clear()
                _líneas = value.ToList

            End Set
        End Property

#End Region

#Region "Constructor"

        Sub New(Texto As String)

            Me.texto = Texto

        End Sub

        Sub New()

        End Sub

#End Region

#Region "Function"

        Public Function Exists(word As String) As Boolean

            Return texto.Contains(word)

        End Function

        Public Function GetUbicationOnText(word As String) As UbicaciónEnTexto()

            Dim result As New List(Of UbicaciónEnTexto)

            Me._líneas.ForEach(Sub(s As String)
                                   If s.ToLower.Contains(word.ToLower) Then
                                       result.Add(
                                           New UbicaciónEnTexto With {
                                               .Oración = s,
                                               .NúmeroOración = Me._líneas.IndexOf(s),
                                               .CaracterInicioOración = Strings.InStr(s, word, CompareMethod.Text),
                                               .Longitud = word.Length})
                                   End If
                               End Sub)

            Return result.ToArray

        End Function

        Public Function GetUbicationOnLine(línea As Integer, word As String) As UbicaciónEnTexto

            Dim result As UbicaciónEnTexto = Nothing 'New List(Of UbicaciónEnTexto)

            Dim s As String = _líneas(línea)

            If s.Contains(word) Then
                result = New UbicaciónEnTexto With {
                        .Oración = s,
                        .NúmeroOración = Me._líneas.IndexOf(s),
                        .CaracterInicioOración = Strings.InStr(s, word, CompareMethod.Text),
                        .Longitud = word.Length}
            End If

            Return result '.ToArray

        End Function

#End Region

    End Class

    ''' <summary>
    ''' Define la ubicación de una región de texto en la clase texto correspondiente
    ''' </summary>
    ''' <remarks></remarks>
    Public Class UbicaciónEnTexto

#Region "Property"

        Property CaracterInicioOración As Long
        ReadOnly Property CaracterFinalOración As Long
            Get

                Return CaracterInicioOración + Longitud

            End Get
        End Property
        Property Longitud As Long
        Property NúmeroOración As Integer
        Property Oración As String

#End Region

    End Class

End Namespace