Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.Infectología

    Public Class Infección
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesOpcionales = {
                New Adelgazamiento
            }
        End Sub

    End Class


End Namespace