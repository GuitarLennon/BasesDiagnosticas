Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Endocrinología
Imports Diagnósticos.Medicina.MedicinaInterna
Imports Diagnósticos.Medicina.Psiquiatría
Imports Diagnósticos.Medicina.Síntomas
Imports Diagnósticos.Programación

Namespace Medicina.Oncología

    Public Class Neoplasia
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
