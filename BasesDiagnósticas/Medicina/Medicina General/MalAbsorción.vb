﻿Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.MedicinaGeneral
    Public Class Malabsorción_Intestinal
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() =
            {"Síndrome de malabsorción"}

        Sub New()
            MyBase.ManifestacionesObligatorias =
                {
                New Adelgazamiento
            }
        End Sub

    End Class
End Namespace


