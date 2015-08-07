Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Programación

Namespace Evaluación

    Public Class Evaluación
#Region "Instance"
        Protected ElementoEvaluado As String
#End Region

#Region "Property"
        Public Property Errores As New List(Of String)
        Public Property Advertencias As New List(Of String)
        Public Property Mensajes As New List(Of String)
#End Region

#Region "Constructor"
        Sub New(Evaluando As String)
            Me.ElementoEvaluado = Evaluando
        End Sub
#End Region
    End Class

    Public Class EvaluaciónDeDiagnóstico
        Inherits Evaluación

#Region "Function"
        Public Overrides Function ToString() As String
            Dim t As New Text.StringBuilder
            Errores.ForEach(Sub(e As String) t.AppendLine(String.Format("Error : {0}", e)))
            Advertencias.ForEach(Sub(e As String) t.AppendLine(String.Format("Advertencia : {0}", e)))
            Mensajes.ForEach(Sub(e As String) t.AppendLine(String.Format("Mensaje : {0}", e)))
            If Errores.Count = 0 And Advertencias.Count = 0 And Mensajes.Count = 0 Then Return "Diagnóstico de '" & Me.ElementoEvaluado & " ' realizado correctamente"
            Return t.ToString
        End Function

#End Region

#Region "Shared functions"
        Public Shared Function Evaluar(texto As String) As Evaluación()
            Return Evaluar(New TextoMédico(texto))
        End Function

        Public Shared Function Evaluar(texto As TextoMédico) As Evaluación()
            Dim l As New List(Of Evaluación)

            texto.TérminosMédicos.ToList.ForEach(
                Sub(t As Término)
                    Dim d As Diagnóstico = TryCast(t, Diagnóstico)
                    If Not d Is Nothing Then l.Add(d.Evaluar(texto))
                    Dim m As Manifestación = TryCast(t, Manifestación)
                    If Not m Is Nothing Then
                        t.ObtenerUbicaciones(texto).ToList.ForEach(Sub(UbicaciónEnTexto As UbicaciónEnTexto)
                                                                       l.Add(m.EsCorrecto(UbicaciónEnTexto))
                                                                   End Sub)
                    End If
                End Sub)

            Return l.ToArray
        End Function
#End Region

#Region "Constructor"
        Sub New(Evaluando As String)
            MyBase.New(Evaluando)
        End Sub
#End Region
    End Class

    Public Class EvaluaciónDeManifestación
        Inherits Evaluación

#Region "Instance"

#End Region

#Region "Property"
        Property ubicaciónEnTexto As UbicaciónEnTexto
#End Region

#Region "Functions"
        Public Overrides Function ToString() As String
            Dim t As New Text.StringBuilder
            Errores.ForEach(Sub(e As String) t.AppendLine(String.Format("Error : {0}", e)))
            Advertencias.ForEach(Sub(e As String) t.AppendLine(String.Format("Advertencia : {0}", e)))
            Mensajes.ForEach(Sub(e As String) t.AppendLine(String.Format("Mensaje : {0}", e)))
            If Errores.Count = 0 And Advertencias.Count = 0 And Mensajes.Count = 0 Then Return "Manifestación de '" & Me.ElementoEvaluado & " ' realizado correctamente"
            Return t.ToString
        End Function
#End Region

#Region "Shared functions"
        Public Shared Function Evaluar(texto As Texto) As EvaluaciónDeManifestación()
            Dim l As New List(Of EvaluaciónDeManifestación)
            Diagnóstico.DiagnósticosDerivados.ToList.ForEach(Sub(d As Type)
                                                                 Dim dx As Manifestación = CType(Activator.CreateInstance(d), Manifestación)
                                                                 If Not dx.Existe(texto) Then Exit Sub
                                                                 dx.ObtenerUbicaciones(texto).ToList.ForEach(
                                                                     Sub(x As UbicaciónEnTexto)
                                                                         l.Add(dx.EsCorrecto(x))
                                                                     End Sub)
                                                             End Sub)
            Return l.ToArray
        End Function
#End Region

#Region "Constructor"
        Sub New(Evaluando As String)
            MyBase.New(Evaluando)
        End Sub
#End Region
    End Class

End Namespace