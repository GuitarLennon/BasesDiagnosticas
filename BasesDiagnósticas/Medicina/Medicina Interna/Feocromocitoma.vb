Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.MedicinaInterna

    Public Class Feocromocitoma
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesOpcionales = {
                New Adelgazamiento
            }
        End Sub

    End Class

    Public Class Síndrome_Carcinoide
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