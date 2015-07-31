Imports Diagnósticos.BasesDiagnósticas

Namespace MedicinaGeneral

    Public Enum Simetría
        NoEspecificado
        Simétrica
        Asimétrica
    End Enum

    Public Class Aumento_De_Peso
        Inherits Signo

        Public Overrides Property Description As String

        Public Property DiferenciaDePeso As Single

        Public Overrides Property Atenuante As String

        Public Overrides Property Desencadentante As String
    End Class

    Public Class Edema
        Inherits Signo

        Public Enum Magnitud
            grado1
            grado2
            grado3
            gardo4
        End Enum

        Public Overrides Property Description As String

        Public Property Región As String

        Public Property Grado As Magnitud

        Public Property Simetría As Simetría

        Public Property Color As String

        Public Property Temperatura As String

        Public Property AspectoCutáneo As String

        Public Property Dolor As String

        Public Property Consistencia As String

        Public Property Aparición As String

        Public Property Movil As String

        Public Overrides Property Atenuante As String

        Public Overrides Property Desencadentante As String
    End Class
End Namespace
