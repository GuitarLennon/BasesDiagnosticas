Imports Diagnósticos.BasesDiagnósticas
Imports Diagnósticos.Medicina.Cardiologia
Imports Diagnósticos.Medicina.Cirugía
Imports Diagnósticos.Medicina.Endocrinología
Imports Diagnósticos.Medicina.Infectología
Imports Diagnósticos.Medicina.MedicinaInterna
Imports Diagnósticos.Medicina.Neumología
Imports Diagnósticos.Medicina.Oncología
Imports Diagnósticos.Medicina.Psiquiatría
Imports Diagnósticos.Medicina.Síntomas
Imports Diagnósticos.Programación

Namespace Medicina.MedicinaGeneral

    Public Class Trastorno_Del_Peso
        Inherits Diagnóstico

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Apetito
            }
        End Sub

        Public Overrides Property Cie10 As String

        Public Overrides Property Description As String

    End Class

    'Public MustInherit Class Pérdida_de_peso
    '    Inherits Trastorno_Del_Peso

    '    Sub New()
    '        MyBase.DiagnósticosComplementarios = {
    '            New Diabetes_Mellitus,+
    '            New Hipertiroidismo,+
    '            New Malabsorción_Intestinal,+
    '            New Feocromocitoma,+
    '            New Síndrome_Carcinoide,+
    '            New Depresión,+
    '            New Hepatopatía,+
    '            New Neoplasia,+
    '            New Anorexia_Nerviosa,+
    '            New Infección,+
    '            New Enfisema_Pulmonar,+
    '            New Insuficiencia_Cardíaca,+
    '            New Obstrucción_Pilórica
    '        }
    '    End Sub
    'End Class

    'Public MustInherit Class Aumento_de_peso
    '    Inherits Trastorno_Del_Peso

    '    Sub New()
    '        MyBase.DiagnósticosComplementarios = {
    '            }
    '    End Sub

    'End Class

    Public Class Desnutrición
        Inherits Trastorno_Del_Peso

        Public Overrides Property Cie10 As String

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String = "Peso inferior al promedio, establecido y aceptado como normal en muchos individuos."

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Adelgazamiento
            }
        End Sub
    End Class

    Public Class Caquexia
        Inherits Desnutrición

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String =
         "Índice demasa corporal inferior a 20 kg/m² con anemia y astenia lo cual implica la pérdida de masa muscular, además de tejido adiposo."

        Sub New()
            MyBase.DiagnósticosNecesarios = {New Anemia}
            MyBase.ManifestacionesObligatorias = {New Astenia}
        End Sub

    End Class

    Public Class Sobrepeso
        Inherits Trastorno_Del_Peso

        Public Overrides Property Cie10 As String

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String =
            "aumento de peso a expensas de tejido adiposo, con un índice de masa corporal de 25 a 29."

        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Aumento_de_peso
            }
        End Sub
    End Class

    Public Class Obesidad
        Inherits Trastorno_Del_Peso

        Public Overrides Property Cie10 As String

        <Referencia(Referencia:=Referencias.Jinich)>
        Public Overrides Property Description As String =
            "aumento de peso a expensas de tejido adiposo con un índice de masa corporal de 30 a 39."
        Sub New()
            MyBase.ManifestacionesObligatorias = {
                New Aumento_de_peso
            }
        End Sub

    End Class

End Namespace