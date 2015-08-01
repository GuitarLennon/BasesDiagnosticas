Imports Diagnósticos.BasesDiagnósticas

Namespace Evaluación

    Public Class Evaluación
        Private DiagnósticoEvaluado As String
        Sub New(diagnóstico As String)
            Me.DiagnósticoEvaluado = diagnóstico
        End Sub
        Public Property Errores As New List(Of String)
        Public Property Advertencias As New List(Of String)
        Public Property Mensajes As New List(Of String)
        Public Overrides Function ToString() As String
            Dim t As New Text.StringBuilder
            Errores.ForEach(Sub(e As String) t.AppendLine(String.Format("Error : {0}", e)))
            Advertencias.ForEach(Sub(e As String) t.AppendLine(String.Format("Advertencia : {0}", e)))
            Mensajes.ForEach(Sub(e As String) t.AppendLine(String.Format("Mensaje : {0}", e)))
            If Errores.Count = 0 And Advertencias.Count = 0 And Mensajes.Count = 0 Then Return "Diagnóstico de '" & Me.DiagnósticoEvaluado & " ' realizado correctamente"
            Return t.ToString
        End Function

        Public Shared Function Evaluar(texto As String) As Evaluación()
            Dim l As New List(Of Evaluación)
            Diagnóstico.DiagnósticosDerivados.ToList.ForEach(Sub(d As Type)
                                                                 Dim dx As Diagnóstico = CType(Activator.CreateInstance(d), Diagnóstico)
                                                                 'If dx.ObtenerUbicación(texto) Then l.Add(dx.EvaluarDiagnóstico(texto))
                                                             End Sub)
            Return l.ToArray
        End Function
    End Class

End Namespace