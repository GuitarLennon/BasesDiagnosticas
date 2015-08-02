Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.MedicinaInterna

Namespace Ginecología

    Public Class Embarazo
        Inherits Diagnóstico

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String = "Gestación"

    End Class

    Public Class Hipertensión_Gestacional
        Inherits Diagnóstico


    End Class

    Public Class Preeclampsia
        Inherits Hipertensión_Gestacional

    End Class

End Namespace