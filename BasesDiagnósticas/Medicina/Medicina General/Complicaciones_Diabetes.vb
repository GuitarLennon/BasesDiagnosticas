Option Strict On
Option Explicit On

Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.MedicinaGeneral

    Public Class Complicaciones_propias_de_la_diabetes
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class


    Public Class Cetoacidosis_diabética
        Inherits Complicaciones_propias_de_la_diabetes

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class

End Namespace