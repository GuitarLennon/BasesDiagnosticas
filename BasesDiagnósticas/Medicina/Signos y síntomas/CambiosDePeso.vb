Imports Diagnósticos.BasesDiagnósticas

Namespace Medicina.Síntomas
    Public Class Adelgazamiento
        Inherits Síntoma

        Public Overrides Property Description As String = "Pérdida de peso"

        Public Overrides Property Sinónimos As String() = {"Pérdida de peso"}

    End Class

    Public Class Aumento_de_peso
        Inherits Síntoma

        Public Overrides Property Description As String = "Incremento de peso"

        Public Overrides Property Sinónimos As String() = {"Incremento de peso"}

    End Class
End Namespace