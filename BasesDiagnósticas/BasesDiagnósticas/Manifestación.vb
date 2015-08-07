Option Strict On    'Dice que tu programación debe ser estricta (sin errores)
Option Explicit On  'Dice que tu programación debe ser explícita

Imports Diagnósticos.Programación
Imports Métodos_Juárez.Métodos_Juárez.Propiedades

Namespace BasesDiagnósticas 'Le da el nombre a la 'carpeta' donde guardaremos los siguientes objetos

    ''' <summary>
    ''' Define una manifestación y sus métodos comunes para sus derivados, los signos y los síntomas
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Manifestación
        Inherits Término

        Public Overridable Function EsCorrecto(UbicaciónEnTexto As UbicaciónEnTexto) As Evaluación.EvaluaciónDeManifestación
            Dim e As New Evaluación.EvaluaciónDeManifestación(Me.Nombre) With {.ubicaciónEnTexto = UbicaciónEnTexto}

            Propiedades(Me).ToList.ForEach(Sub(x As Propiedad)
                                               Dim t As New Texto(UbicaciónEnTexto.Oración)
                                               t.Exists(x.Nombre)
                                           End Sub)
            'todo: fix this
            Return e
        End Function


    End Class

    Public MustInherit Class Signo
        Inherits Manifestación

    End Class

    Public MustInherit Class Síntoma
        Inherits Manifestación

    End Class

End Namespace
