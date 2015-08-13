Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.Gastroenterología

    Public Class Gastropatía
        Inherits Gastroenteropatía

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class

    Public Class Gastritis_Atrófica
        Inherits Gastropatía

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class

End Namespace