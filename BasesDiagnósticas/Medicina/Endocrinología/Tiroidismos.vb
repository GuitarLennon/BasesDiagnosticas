Option Strict On
Option Explicit On

Imports Diagnósticos.Medicina.Síntomas

Namespace Medicina.Endocrinología

    Public Class Hipotiroidismo
        Inherits Endocrinopatía

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class

    Public Class Hipertiroidismo
        Inherits Endocrinopatía

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

        Public Overrides Property Sinónimos As String() =
            {"Insuficiencia Tiroidea"}

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Adelgazamiento
            }
        End Sub

    End Class
End Namespace