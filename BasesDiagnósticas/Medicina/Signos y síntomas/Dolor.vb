Imports Diagnósticos.BasesDiagnósticas

Namespace Medicina.Síntomas

    Public Enum Dolor_Carácter
        Lacinante
        Urente
        Opresivo
        Transfictivo
        Sordo
        Exquisito
        Fulgurante
        Desgarrante
        Taladrante
        Pulsatil
        Cólico
        Gravitativo
    End Enum

    Public Class Dolor
        Inherits Síntoma

        Public Overrides Property Description As String =
            "Sensación molestay aflictiva de una parte del cuerpo por causa interior o exterior."

        Public Property Antiguedad As String

        Public Property Localización As String

        Public Property Irradiación As String

        Public Property Carácter As String

        Public Property Intensidad As String

        Public Property Atenuante As String

        Public Property Agravante As String

        Public Property Tipo As String
    End Class


    Public Class Alodinia
        Inherits Síntoma

        Public Overrides Property Description As String =
            "Dolor procovado por un estímulo mecánico o t´rmico que en condiciones habituales es inocuo."

    End Class

    Public Class Hiperalgesia
        Inherits Síntoma

        Public Overrides Property Description As String =
            "Descenso del umbral perceptivo para estímulos dolorosos que procova una facilitación en la producción del dolor."

    End Class

    Public Class Hiperpatía
        Inherits Síntoma

        Public Overrides Property Description As String =
            "Umbral elevado para el dolor."
    End Class

End Namespace