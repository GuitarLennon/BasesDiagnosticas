Option Strict On
Option Explicit On

Imports Diagnósticos.BasesDiagnósticas

Namespace Medicina.MedicinaGeneral

    Class Síndrome_Urémico
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() = {"Uremia"}

    End Class

End Namespace
