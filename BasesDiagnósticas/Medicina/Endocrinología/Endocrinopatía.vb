Option Strict On
Option Explicit On

Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.Endocrinología

    Public Class Endocrinopatía
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            Me.ManifestacionesObligatorias = {
                New Apetito
            }
        End Sub

    End Class

End Namespace