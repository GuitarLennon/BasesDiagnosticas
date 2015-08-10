Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Endocrinología
Imports Diagnósticos.Medicina.MedicinaInterna
Imports Diagnósticos.Medicina.Síntomas
Imports Diagnósticos.Programación

Namespace Medicina.MedicinaGeneral

    Public MustInherit Class Trastorno_Del_Peso
        Inherits Diagnóstico

        Sub New()
            MyBase.DiagnósticosComplementarios = {
                New Diabetes_Mellitus,
                New Hipertiroidismo,
                New Malabsorción_Intestinal,
                New Feocromocitoma,
                New Síndrome_Carcinoide
            }
            MyBase.ManifestacionesObligatorias = {
                New Orexia
            }
        End Sub

    End Class

    Public Class Delgadez
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String = "Peso inferior al promedio, establecido y aceptado como normal en muchos individuos."

    End Class

    Public Class Caquexia
        Inherits Delgadez

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String =
         "Índice demasa corporal inferior a 20 kg/m² con anemia y astenia lo cual implica la pérdida de masa muscular, además de tejido adiposo."

        Sub New()
            MyBase.DiagnósticosNecesarios = {New Anemia}
            MyBase.ManifestacionesObligatorias = {New Astenia}
        End Sub

    End Class

    Public Class Sobrepeso
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String =
            "aumento de peso a expensas de tejido adiposo, con un índice de masa corporal de 25 a 29."

    End Class

    Public Class Obesidad
        Inherits Diagnóstico

        Public Overrides Property Cie10 As String

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String =
            "aumento de peso a expensas de tejido adiposo con un índice de masa corporal de 30 a 39."

    End Class

End Namespace