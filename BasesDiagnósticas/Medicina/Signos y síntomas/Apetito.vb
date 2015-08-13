Imports Diagnósticos.BasesDiagnósticas

Namespace Medicina.Síntomas

    Public Class Apetito
        Inherits Síntoma

        Public Overrides Property Description As String =
            "Deseo de alimentarse; el impulso de comer."

    End Class

    Public Class Anorexia
        Inherits Apetito

    End Class

    Public Class Hiporexia
        Inherits Apetito

        Public Overrides Property Sinónimos As String() = {"Inapetencia"}
        Public Overrides Property Description As String =
            "Apetito auscente."

    End Class

    Public Class Hiperorexia
        Inherits Apetito

        Public Overrides Property Description As String =
            "Apetito exagerado."

    End Class

    Public Class Normorexia
        Inherits Apetito

    End Class

    Public Class Pica
        Inherits Síntoma

        Public Overrides Property Description As String =
            "Perversión de apetito."

    End Class

    Public Class Hambre
        Inherits Síntoma

        Public Overrides Property Description As String =
            "Complejo de sensaciones evocadas por la depleción de las reservas nutritivas del cuerpo."

    End Class

End Namespace