Option Strict On    'Dice que tu programación debe ser estricta (sin errores)
Option Explicit On  'Dice que tu programación debe ser explícita

Imports Diagnósticos.Programación 'Importa los recursos de Diagnósticos.Programación
Imports Diagnósticos.Evaluación
Imports Métodos_Juárez.Métodos_Juárez.Propiedades

Namespace BasesDiagnósticas
    Public MustInherit Class Diagnóstico
        Inherits Término
        'Implements iEvaluable

#Region "instance"
        Private _DiagnósticosNecesarios As Diagnóstico()
        Private _DiagnósticosComplementarios As Diagnóstico()
        Private _ManifestacionesObligatoria As Manifestación()
        Private _ManifestacionesComplementaria As Manifestación()
        Private _ManifestacionesOpcional As Manifestación()
#End Region

#Region "property"
        Public Overridable Property DiagnósticosNecesarios As Diagnóstico()
            Get
                Return _DiagnósticosNecesarios
            End Get
            Protected Set(value As Diagnóstico())
                Dim l As List(Of Diagnóstico) = _DiagnósticosNecesarios.ToList
                l.AddRange(value)
                _DiagnósticosNecesarios = l.ToArray
            End Set
        End Property

        Public Overridable Property DiagnósticosComplementarios As Diagnóstico()
            Get
                Return _DiagnósticosComplementarios
            End Get
            Protected Set(value As Diagnóstico())
                Dim l As List(Of Diagnóstico) = _DiagnósticosComplementarios.ToList
                l.AddRange(value)
                _DiagnósticosComplementarios = l.ToArray
            End Set
        End Property

        Public Overridable Property ManifestacionesObligatorias As Manifestación()
            Get
                Return _ManifestacionesObligatoria
            End Get
            Protected Set(value As Manifestación())
                Dim l As List(Of Manifestación) = _ManifestacionesObligatoria.ToList
                l.AddRange(value)
                _ManifestacionesObligatoria = l.ToArray
            End Set
        End Property

        Public Overridable Property ManifestacionesOpcionales As Manifestación()
            Get
                Return _ManifestacionesOpcional
            End Get
            Protected Set(value As Manifestación())
                Dim l As List(Of Manifestación) = _ManifestacionesOpcional.ToList
                l.AddRange(value)
                _ManifestacionesOpcional = l.ToArray
            End Set
        End Property

        Public Overridable Property ManifestacionesComplementarias As Manifestación()
            Get
                Return _ManifestacionesComplementaria
            End Get
            Protected Set(value As Manifestación())
                Dim l As List(Of Manifestación) = _ManifestacionesComplementaria.ToList
                l.AddRange(value)
                _ManifestacionesComplementaria = l.ToArray
            End Set
        End Property
#End Region

#Region "Functions"
        Friend Shared ReadOnly Property DiagnósticosDerivados As Type()
            Get
                Return AppDomain.CurrentDomain.GetAssemblies(). _
                    SelectMany(Function(a As System.Reflection.Assembly) a.GetTypes()). _
                    Where(Function(t As Type) t.IsSubclassOf(GetType(Diagnóstico))).ToArray
            End Get
        End Property

        Public Overridable Function Evaluar(texto As Texto) As Evaluación.EvaluaciónDeDiagnóstico 'Implements iEvaluable.Evaluar
            Dim n As New Evaluación.EvaluaciónDeDiagnóstico(Me.Nombre)

            If Not DiagnósticosNecesarios Is Nothing Then
                DiagnósticosNecesarios.ToList.ForEach( _
                    Sub(m As Diagnóstico)
                        If Not m.Existe(texto) Then
                            n.Errores.Add("Para poder diagnósticar" & Me.Nombre & _
                                          " es necesario el diagnóstico de : '" & m.Nombre & "'")
                        End If
                    End Sub)
            End If

            If Not DiagnósticosComplementarios Is Nothing Then
                DiagnósticosComplementarios.ToList.ForEach( _
                    Sub(m As Diagnóstico)
                        If Not m.Existe(texto) Then
                            n.Errores.Add("Para poder diagnósticar" & Me.Nombre & _
                                          " es necesario el diagnóstico de : '" & m.Nombre & "'")
                        End If
                    End Sub)
            End If

            If Not ManifestacionesObligatorias Is Nothing Then
                ManifestacionesObligatorias.ToList.ForEach( _
                    Sub(m As Manifestación)
                        If Not m.Existe(texto) Then
                            n.Errores.Add("No se mencionan datos acerca de: '" & m.Nombre & _
                                          "', necesarios para sostener el diagnóstico")
                        End If
                    End Sub)
            End If

            If Not ManifestacionesOpcionales Is Nothing Then
                ManifestacionesOpcionales.ToList.ForEach( _
                    Sub(m As Manifestación)
                        If Not m.Existe(texto) Then
                            n.Advertencias.Add("Deberían mencionarse datos acerca de: '" & m.Nombre _
                                               & "' para apoyar el diagnóstico")
                        End If
                    End Sub)
            End If


            If Not ManifestacionesComplementarias Is Nothing Then
                ManifestacionesComplementarias.ToList.ForEach( _
                    Sub(m As Manifestación)
                        If Not m.Existe(texto) Then
                            n.Mensajes.Add("Puede complementarse agregando datos de: '" & m.Nombre & "' ")
                        End If
                    End Sub)
            End If

            Return n
        End Function
#End Region

#Region "Constructor"
        Sub New(DiagnósticosNecesarios As Diagnóstico(),
                DiagnósticosComplementarios As Diagnóstico(),
                ManifestaciónObligatoria As Manifestación(),
                ManifestaciónComplementaria As Manifestación(),
                ManifestaciónOpcional As Manifestación())

            Me.DiagnósticosNecesarios = DiagnósticosNecesarios
            Me.DiagnósticosComplementarios = DiagnósticosComplementarios
            Me.ManifestacionesObligatorias = ManifestaciónObligatoria
            Me.ManifestacionesOpcionales = ManifestaciónOpcional
            Me.ManifestacionesComplementarias = ManifestaciónComplementaria

        End Sub

        Sub New()

        End Sub
#End Region

        Public Overrides Property Description As String

    End Class
End Namespace