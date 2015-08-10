
Imports Diagnósticos.BasesDiagnósticas

Namespace Medicina.Mediciones

    Public Class Presión_Arterial
        Inherits Medición

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() = {"PA", "TA"}

    End Class

    Public Class Peso
        Inherits Medición

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String()

    End Class

    Public Class Índice_de_Masa_Corporal
        Inherits Medición

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() = {"IMC"}

    End Class

End Namespace