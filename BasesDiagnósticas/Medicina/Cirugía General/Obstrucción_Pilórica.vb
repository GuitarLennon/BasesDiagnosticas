Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Endocrinología
Imports Diagnósticos.Medicina.MedicinaInterna
Imports Diagnósticos.Medicina.Síntomas
Imports Diagnósticos.Programación

Namespace Medicina.Cirugía

    Public Class Obstrucción_Pilórica
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