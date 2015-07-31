Imports Diagnósticos.BasesDiagnósticas

Module Ginecología

    Public Class Embarazo
        Inherits Diagnóstico

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String = "Gestación"

        Public Overrides Property ManifestaciónComplementarios As Manifestación()

        Public Overrides Property ManifestaciónObligatoria As Manifestación()

        Public Overrides Property ManifestaciónOpcionales As Manifestación()

        Public Overrides Property DiagnósticosComplementarios As Diagnóstico()

        Public Overrides Property DiagnósticosNecesarios As Diagnóstico()
    End Class

    Public Class HipertensiónGestacional
        Inherits Diagnóstico

        Public Overrides Property Description As String

        Public Overrides Property DiagnósticosComplementarios As Diagnóstico() = {New Embarazo}

        Public Overrides Property DiagnósticosNecesarios As Diagnóstico()

        Public Overrides Property ManifestaciónComplementarios As Manifestación()

        Public Overrides Property ManifestaciónObligatoria As Manifestación()

        Public Overrides Property ManifestaciónOpcionales As Manifestación()
    End Class

    Public Class Preeclampsia
        Inherits HipertensiónGestacional

        Public Overrides Property DiagnósticosComplementarios As Diagnóstico()

        Public Overrides Property DiagnósticosNecesarios As Diagnóstico()
    End Class

End Module

