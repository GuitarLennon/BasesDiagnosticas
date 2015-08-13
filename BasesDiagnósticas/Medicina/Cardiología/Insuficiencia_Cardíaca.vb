Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Endocrinología
Imports Diagnósticos.Medicina.MedicinaInterna
Imports Diagnósticos.Medicina.Síntomas
Imports Diagnósticos.Programación

Namespace Medicina.Cardiologia

    Public Class Insuficiencia_Cardíaca
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class

    Public Class Insuficiencia_Cardíaca_Congestiva
        Inherits Insuficiencia_Cardíaca


        Sub New()
            MyBase.ManifestacionesOpcionales = {
                New Aumento_de_peso
            }
        End Sub

    End Class

End Namespace
