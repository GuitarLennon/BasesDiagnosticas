Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.Psiquiatría

    Public Class Depresión
        Inherits Trastorno_Psiquiátrico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesOpcionales = {
                New Adelgazamiento
            }
        End Sub

    End Class

    Public Class Anorexia_Nerviosa
        Inherits Trastorno_Psiquiátrico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Adelgazamiento,
                New Apetito
            }
        End Sub

    End Class

    Public Class Bulimia_Nerviosa
        Inherits Trastorno_Psiquiátrico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Adelgazamiento,
                New Apetito
            }
        End Sub

    End Class

End Namespace