Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Endocrinología
Imports Diagnósticos.Medicina.Laboratorios
Imports Diagnósticos.Medicina.MedicinaInterna
Imports Diagnósticos.Medicina.Signos
Imports Diagnósticos.Medicina.Síntomas
Imports Diagnósticos.Programación

Namespace Medicina.Nefrología

    Public Class Síndrome_Nefrótico
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Proteinuria,
                New Edema,
                New Hipercolesterolemia
            }
        End Sub

    End Class

End Namespace