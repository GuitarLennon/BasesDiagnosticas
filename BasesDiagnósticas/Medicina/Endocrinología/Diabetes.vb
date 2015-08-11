Option Strict On
Option Explicit On

Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Signos
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.Endocrinología

    Public Class Diabetes_Mellitus
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesObligatorias = {
            New Adelgazamiento,
            New Poliuria,
            New Polidipsia,
            New Polifagia
            }
        End Sub

    End Class
End Namespace