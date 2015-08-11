Option Strict On
Option Explicit On
Option Compare Text
Option Infer On

Imports Diagnósticos.BasesDiagnósticas

Namespace Diagnósticos.BasesDiagnósticas

    Public MustInherit Class Laboratorio
        Inherits Manifestación

        Public Overridable Property valor As String

    End Class

End Namespace