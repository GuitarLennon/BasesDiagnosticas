Option Strict On
Option Explicit On
Option Compare Text
Option Infer Off

Imports System.IO
Imports System.Web.UI
Imports Diagnósticos.BasesDiagnósticas

Namespace Programación.myHtml

    Public Class HtmlDocument

#Region "Instance"
        ''' <summary>
        ''' líneas del texto presente
        ''' </summary>
        ''' <remarks></remarks>
        Private _líneas As New List(Of String)

        Private _texto As String

        Dim innerDocument As HtmlTextWriter

        Const EtiquetaApertura As String = "<T>"

        Const EtiquetaCierre As String = "</T>"
#End Region

#Region "property"
        Public Property CaracteresSeparadores As Char() = {CChar("."), CChar(";"), CChar(vbCrLf)}

        Public Property texto As String
            Get
                Return _texto
            End Get
            Set(value As String)
                If value Is Nothing Then Stop
                _líneas = value.Split(CaracteresSeparadores).ToList
                _texto = value
            End Set
        End Property

        Default Public Property línea(índice As Integer) As String
            Get
                Return _líneas(índice)
            End Get
            Set(value As String)
                _líneas(índice) = value
                Me._texto = String.Join("", value)
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
#End Region

        Sub New(texto As String)

            'crear un escritor de documento HTML
            Dim sw As New StringWriter()
            innerDocument = New HtmlTextWriter(sw)

            'Asignar el texto, generar líneas
            Me.texto = texto

            'Para cada línea, encontrar y marcar un término
            Me._líneas.ForEach(
                Sub(línea As String)

                    'Por cada término
                    For Each termino As String In Término.Lista

                        'Buscar el término en la línea
                        Dim i As Integer = InStr(línea, termino, CompareMethod.Text)

                        'Si se encuentra el término, marcarlo
                        If i > 0 Then

                            'inserta el final de la etiqueta
                            línea = línea.Insert(i + termino.Length - 1, EtiquetaCierre)

                            'inserta el inicio de la etiqueta
                            línea = línea.Insert(i, EtiquetaApertura)

                        End If
                    Next

                    'escribir la línea HTML

                End Sub)

        End Sub

    End Class

End Namespace