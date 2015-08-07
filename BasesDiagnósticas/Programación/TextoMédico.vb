Option Explicit On
Option Infer Off
Option Strict On
Option Compare Text

Imports Diagnósticos.Programación
Imports System.Xml

Namespace BasesDiagnósticas

    Public Class TextoMédico
        Inherits Texto

        Protected _TérminosMédicos As New List(Of Término)

        Public ReadOnly Property TérminosMédicos As Término()
            Get

                Return _TérminosMédicos.ToArray()

            End Get
        End Property

        Public Sub New(texto As String)
            MyBase.New(texto)

            BuscarTérminos()

        End Sub

        Protected Overridable Sub BuscarTérminos()

            Dim list As New List(Of String)

            For i As Integer = 0 To _líneas.Count - 1

                'Sub(línea As String)
                If Not String.IsNullOrEmpty(_líneas(i)) Then

                    'Excluir términos ya identificados
                    _líneas(i) = deleteTerminos(_líneas(i))

                    For Each s As String In Término.Lista

                        's = s.ToLower

                        'revisar si ya existe dicho término
                        If list.Contains(s) Then

                            _líneas(i) = etiquetarTérminos(_líneas(i), s)

                        Else

                            'Buscar términos
                            If _líneas(i).ToLower.Contains(s.ToLower) Then

                                'Agrégalo a la lista
                                list.Add(s.ToLower)

                                'etiquetar término
                                _líneas(i) = etiquetarTérminos(_líneas(i), s)

                                'agregar término
                                Dim t As Término = CType(Activator.CreateInstance(Término.Diccionario.Item(s)), Término)

                                t.ubicaciónEnTexto = GetUbicationOnText(s)

                                _TérminosMédicos.Add(t)

                            End If

                        End If

                    Next

                End If

            Next

        End Sub

#Region "Shared Component"

        Public Shared EtiquetaDeTérminos As String = "T"

        Shared Function deleteTerminos(línea As String) As String

            Dim etiquetaApertura As String = "<" & EtiquetaDeTérminos & ">"
            Dim etiquetaDeCierre As String = "</" & EtiquetaDeTérminos & ">"

            Dim inicio As Integer = Strings.InStr(línea, etiquetaApertura, CompareMethod.Text)

            If inicio > 0 Then

                Dim final As Integer = Strings.InStr(línea, etiquetaDeCierre, CompareMethod.Text) + etiquetaDeCierre.Length

                línea = línea.Remove(inicio, final - inicio)

            Else

                Return línea

            End If

            Return deleteTerminos(línea)

        End Function

        Shared Function etiquetarTérminos(línea As String, término As String) As String
            Dim etiquetaApertura As String = "<" & EtiquetaDeTérminos & ">"
            Dim etiquetaDeCierre As String = "</" & EtiquetaDeTérminos & ">"

            Dim etiquetado As String = etiquetaApertura + término + etiquetaDeCierre

            Return Strings.Replace(línea, término, etiquetado)

        End Function
    End Class

#End Region

End Namespace