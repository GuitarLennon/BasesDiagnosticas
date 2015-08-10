Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.MedicinaInterna

Namespace Medicina.Ginecología

    Public Class Embarazo
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String = "Z33"

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() = {"Gestación"}

    End Class

    Public Class Hipertensión_Gestacional
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String = "O10"

        Public Overrides Property Description As String

    End Class

    Public Class Preeclampsia
        Inherits Hipertensión_Gestacional


    End Class

End Namespace