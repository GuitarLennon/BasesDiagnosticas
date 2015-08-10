Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Programación
'Imports Diagnósticos.

Namespace Medicina.Signos

    <Autor(Autor:=Autores.Arturo)>
    Public Class Edema
        Inherits Signo

        Public Overrides Property Description As String

        Public Property Región As String

        Public Property Grado As String

        Public Property Simetría As String

        Public Property Color As String

        Public Property Temperatura As String

        Public Property AspectoCutáneo As String

        Public Property Dolor As String

        Public Property Consistencia As String

        Public Property Aparición As String

        Public Property Movil As String

    End Class

End Namespace
