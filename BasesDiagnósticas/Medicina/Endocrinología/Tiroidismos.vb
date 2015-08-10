Option Strict On
Option Explicit On

Imports Diagnósticos.BasesDiagnósticas

Namespace Medicina.Endocrinología
    Public Class Hipotiroidismo
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class

    Public Class Hipertiroidismo
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class
End Namespace