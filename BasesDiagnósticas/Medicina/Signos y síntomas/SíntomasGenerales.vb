Imports Diagnósticos.BasesDiagnósticas

Namespace Medicina.Síntomas

    Public Class Astenia
        Inherits Síntoma

        Public Overrides Property Description As String

    End Class

    Public Class Orexia
        Inherits Síntoma

        Public Overrides Property Description As String

    End Class

    Public Class Anorexia
        Inherits Orexia

    End Class

    Public Class Hiporexia
        Inherits Orexia

    End Class

    Public Class Hiperorexia
        Inherits Orexia

    End Class

    Public Class Normorexia
        Inherits Orexia

    End Class

End Namespace