Option Strict On    'Dice que tu programación debe ser estricta (sin errores)
Option Explicit On  'Dice que tu programación debe ser explícita

Imports Diagnósticos.Programación 'Importa los recursos de Diagnósticos.Programación

Namespace BasesDiagnósticas 'Le da el nombre a la 'carpeta' donde guardaremos los siguientes objetos

    Public MustInherit Class Manifestación
        Inherits Término
        Public MustOverride Property Desencadentante As String
        Public MustOverride Property Atenuante As String

    End Class

    Public MustInherit Class Signo
        Inherits Manifestación


    End Class

    Public MustInherit Class Síntoma
        Inherits Manifestación

    End Class

    Public MustInherit Class Diagnóstico
        Inherits Término

        Friend Shared ReadOnly Property DiagnósticosDerivados As Type()
            Get
                Return AppDomain.CurrentDomain.GetAssemblies().SelectMany(Function(a As System.Reflection.Assembly) a.GetTypes()).Where(Function(t As Type) t.IsSubclassOf(GetType(Diagnóstico))).ToArray
            End Get
        End Property

        Public MustOverride Property DiagnósticosNecesarios As Diagnóstico()
        Public MustOverride Property DiagnósticosComplementarios As Diagnóstico()
        Public MustOverride Property ManifestaciónObligatoria As Manifestación()
        Public MustOverride Property ManifestaciónOpcionales As Manifestación()
        Public MustOverride Property ManifestaciónComplementarios As Manifestación()

        Public Overridable Function EvaluarDiagnóstico(texto As String) As Evaluación
            Dim n As New Evaluación(Me.Nombre)
            If Not DiagnósticosNecesarios Is Nothing Then
                DiagnósticosNecesarios.ToList.ForEach(Sub(m As Diagnóstico)
                                                          If Not m.ObtenerUbicación(texto) Then
                                                              n.Errores.Add("Para poder diagnósticar" & Me.Nombre & " es necesario el diagnóstico de : '" & m.Nombre & "'")
                                                          End If
                                                      End Sub)
            End If

            If Not DiagnósticosComplementarios Is Nothing Then
                DiagnósticosComplementarios.ToList.ForEach(Sub(m As Diagnóstico)
                                                               If Not m.ObtenerUbicación(texto) Then
                                                                   n.Errores.Add("Para poder diagnósticar" & Me.Nombre & " es necesario el diagnóstico de : '" & m.Nombre & "'")
                                                               End If
                                                           End Sub)
            End If

            If Not ManifestaciónObligatoria Is Nothing Then
                ManifestaciónObligatoria.ToList.ForEach(Sub(m As Manifestación)
                                                            If Not m.ObtenerUbicación(texto) Then
                                                                n.Errores.Add("No se mencionan datos acerca de: '" & m.Nombre & "', necesarios para sostener el diagnóstico")
                                                            End If
                                                        End Sub)
            End If

            If Not ManifestaciónOpcionales Is Nothing Then
                ManifestaciónOpcionales.ToList.ForEach(Sub(m As Manifestación)
                                                           If Not m.ObtenerUbicación(texto) Then
                                                               n.Advertencias.Add("Deberían mencionarse datos acerca de: '" & m.Nombre & "' para apoyar el diagnóstico")
                                                           End If
                                                       End Sub)
            End If


            If Not ManifestaciónComplementarios Is Nothing Then
                ManifestaciónComplementarios.ToList.ForEach(Sub(m As Manifestación)
                                                                If Not m.ObtenerUbicación(texto) Then
                                                                    n.Mensajes.Add("Puede complementarse agregando datos de: '" & m.Nombre & "' ")
                                                                End If
                                                            End Sub)
            End If

            Return n
        End Function

    End Class

End Namespace
