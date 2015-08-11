Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.Gastroenterología

    Public Class Hepatopatía
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Adelgazamiento
            }
        End Sub

    End Class
End Namespace