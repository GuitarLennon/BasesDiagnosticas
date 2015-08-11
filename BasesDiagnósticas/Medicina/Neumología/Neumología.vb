Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Endocrinología
Imports Diagnósticos.Medicina.MedicinaInterna
Imports Diagnósticos.Medicina.Síntomas
Imports Diagnósticos.Programación

Namespace Medicina.Neumología

    Public Class Enfermedad_Pulmonar_Obstructiva_Crónica
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() = {"EPOC"}

    End Class


    Public Class Enfisema_Pulmonar
        Inherits Enfermedad_Pulmonar_Obstructiva_Crónica

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() = {"Enfisema"}

        Sub New()
            MyBase.ManifestacionesOpcionales = {
                New Adelgazamiento
            }
        End Sub

    End Class

End Namespace