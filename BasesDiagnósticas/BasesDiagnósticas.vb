Option Strict On
Option Explicit On

Module BasesDiagnósticas

    MustInherit Class Término
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

    MustInherit Class Manifestación
        Inherits Término
        Public MustOverride Property Desencadentante As String
        Public MustOverride Property Atenuante As String

    End Class

    MustInherit Class Signo
        Inherits Manifestación


    End Class

    MustInherit Class Síntoma
        Inherits Manifestación

    End Class

    MustInherit Class Diagnóstico
        Inherits Término

        Friend Shared ReadOnly Property Diagnósticos As Type()
            Get
                Return AppDomain.CurrentDomain.GetAssemblies().SelectMany(Function(a As System.Reflection.Assembly) a.GetTypes()).Where(Function(t As Type) t.IsSubclassOf(GetType(Diagnóstico))).ToArray
            End Get
        End Property

        Public MustOverride Property DiagnósticosNecesarios As Diagnóstico()
        Public MustOverride Property DiagnósticosComplementarios As Diagnóstico()
        Public MustOverride Property ManifestaciónObligatoria As Manifestación()
        Public MustOverride Property ManifestaciónOpcionales As Manifestación()
        Public MustOverride Property ManifestaciónComplementarios As Manifestación()

        Public Overridable Function Evaluar(texto As String) As Evaluación
            Dim n As New Evaluación(Me.Nombre)
            If Not DiagnósticosNecesarios Is Nothing Then
                DiagnósticosNecesarios.ToList.ForEach(Sub(m As Diagnóstico)
                                                          If Not m.EsEvocado(texto) Then
                                                              n.Errores.Add("Para poder diagnósticar" & Me.Nombre & " es necesario el diagnóstico de : '" & m.Nombre & "'")
                                                          End If
                                                      End Sub)
            End If

            If Not DiagnósticosComplementarios Is Nothing Then
                DiagnósticosComplementarios.ToList.ForEach(Sub(m As Diagnóstico)
                                                               If Not m.EsEvocado(texto) Then
                                                                   n.Errores.Add("Para poder diagnósticar" & Me.Nombre & " es necesario el diagnóstico de : '" & m.Nombre & "'")
                                                               End If
                                                           End Sub)
            End If

            If Not ManifestaciónObligatoria Is Nothing Then
                ManifestaciónObligatoria.ToList.ForEach(Sub(m As Manifestación)
                                                            If Not m.EsEvocado(texto) Then
                                                                n.Errores.Add("No se mencionan datos acerca de: '" & m.Nombre & "', necesarios para sostener el diagnóstico")
                                                            End If
                                                        End Sub)
            End If

            If Not ManifestaciónOpcionales Is Nothing Then
                ManifestaciónOpcionales.ToList.ForEach(Sub(m As Manifestación)
                                                           If Not m.EsEvocado(texto) Then
                                                               n.Advertencias.Add("Deberían mencionarse datos acerca de: '" & m.Nombre & "' para apoyar el diagnóstico")
                                                           End If
                                                       End Sub)
            End If


            If Not ManifestaciónComplementarios Is Nothing Then
                ManifestaciónComplementarios.ToList.ForEach(Sub(m As Manifestación)
                                                                If Not m.EsEvocado(texto) Then
                                                                    n.Mensajes.Add("Puede complementarse agregando datos de: '" & m.Nombre & "' ")
                                                                End If
                                                            End Sub)
            End If

            Return n
        End Function


    End Class

    Public Function Evaluar(texto As String) As Evaluación()
        Dim l As New List(Of Evaluación)
        Diagnóstico.Diagnósticos.ToList.ForEach(Sub(d As Type)
                                                    Dim dx As Diagnóstico = CType(Activator.CreateInstance(d), Diagnóstico)
                                                    If dx.EsEvocado(texto) Then l.Add(dx.Evaluar(texto))
                                                End Sub)
        Return l.ToArray
    End Function

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
    End Class

End Module
