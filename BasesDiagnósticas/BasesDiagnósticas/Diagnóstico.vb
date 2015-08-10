Option Strict On    'Dice que tu programación debe ser estricta (sin errores)
Option Explicit On  'Dice que tu programación debe ser explícita

Imports Diagnósticos.Programación 'Importa los recursos de Diagnósticos.Programación


Namespace BasesDiagnósticas

    ''' <summary>
    ''' Abstracto de los diagnósticos
    ''' </summary>
    Public MustInherit Class Diagnóstico
        Inherits Término

#Region "instance"
        Private _DiagnósticosNecesarios As Diagnóstico()
        Private _DiagnósticosComplementarios As Diagnóstico()
        Private _ManifestacionesObligatoria As Manifestación()
        Private _ManifestacionesComplementaria As Manifestación()
        Private _ManifestacionesOpcional As Manifestación()
#End Region

#Region "property"
        ''' <summary>
        ''' Establece los diagnósticos mínimos necesarios para establecer este diagnóstico
        ''' </summary>
        ''' <returns></returns>
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

        ''' <summary>
        ''' Establece los diagnósticos que complementarán este diagnóstico
        ''' </summary>
        ''' <returns></returns>
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

        ''' <summary>
        ''' Establece las manifestaciones mínimas necesarios para establecer este diagnóstico
        ''' </summary>
        ''' <returns></returns>
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

        ''' <summary>
        ''' Establece las manifestaciones que deben mencionarse para establecer este diagnóstico;
        ''' pero que no son completamente necesarias para el mismo
        ''' </summary>
        ''' <returns></returns>
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

        ''' <summary>
        ''' Establece las manifestaciones que podrían complementar este diagnóstico, pero que son 
        ''' prescindibles
        ''' </summary>
        ''' <returns></returns>
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

        Public MustOverride Property Cie10 As String
#End Region

#Region "Functions"
        ''' <summary>
        ''' Obtiene todos los diagnósticos que se derivan de la clase diagnóstico
        ''' </summary>
        ''' <returns></returns>
        Friend Shared Function DiagnósticosDerivados() As Type()
            Return AppDomain.CurrentDomain.GetAssemblies().
                SelectMany(Function(a As System.Reflection.Assembly) a.GetTypes()).
                Where(Function(t As Type) t.IsSubclassOf(GetType(Diagnóstico))).ToArray

        End Function

        ''' <summary>
        ''' Evalua un diagnóstico
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns></returns>
        Public Overridable Function Evaluar(texto As TextoMédico) As Evaluación.EvaluaciónDeDiagnóstico 'Implements iEvaluable.Evaluar
            Dim n As New Evaluación.EvaluaciónDeDiagnóstico(Me.Nombre)

            If Not DiagnósticosNecesarios Is Nothing Then
                DiagnósticosNecesarios.ToList.ForEach(
                    Sub(m As Diagnóstico)
                        If Not texto.TérminosMédicos.Contains(m) Then
                            n.Errores.Add("Para poder diagnósticar" & Me.Nombre &
                                              " es necesario el diagnóstico de : '" & m.Nombre & "'")
                        End If

                        'If Not m.Existe(texto) Then
                        '    n.Errores.Add("Para poder diagnósticar" & Me.Nombre &
                        '                  " es necesario el diagnóstico de : '" & m.Nombre & "'")
                        'End If
                    End Sub)
            End If

            If Not DiagnósticosComplementarios Is Nothing Then
                DiagnósticosComplementarios.ToList.ForEach(
                    Sub(m As Diagnóstico)
                        If Not texto.TérminosMédicos.Contains(m) Then
                            n.Advertencias.Add("Para un buen diagnóstico de" & Me.Nombre &
                                              " conviene mencionar diagnóstico de : '" & m.Nombre & "'")
                        End If
                        'If Not m.Existe(texto) Then
                        '    n.Errores.Add("Para poder diagnósticar" & Me.Nombre &
                        '                  " es necesario el diagnóstico de : '" & m.Nombre & "'")
                        'End If
                    End Sub)
            End If

            If Not ManifestacionesObligatorias Is Nothing Then
                ManifestacionesObligatorias.ToList.ForEach(
                    Sub(m As Manifestación)
                        If Not texto.TérminosMédicos.Contains(m) Then
                            n.Errores.Add("No se mencionan datos acerca de: '" & m.Nombre &
                                          "', necesarios para sostener el diagnóstico")
                        End If
                        'If Not m.Existe(texto) Then
                        '    n.Errores.Add("No se mencionan datos acerca de: '" & m.Nombre &
                        '                  "', necesarios para sostener el diagnóstico")
                        'End If
                End Sub)
            End If

            If Not ManifestacionesOpcionales Is Nothing Then
                ManifestacionesOpcionales.ToList.ForEach(
                    Sub(m As Manifestación)
                        If Not texto.TérminosMédicos.Contains(m) Then
                            n.Errores.Add("No se mencionan datos acerca de: '" & m.Nombre &
                                          "', necesarios para sostener el diagnóstico")
                        End If
                        'If Not m.Existe(texto) Then
                        '    n.Advertencias.Add("Deberían mencionarse datos acerca de: '" & m.Nombre _
                        '                       & "' para apoyar el diagnóstico")
                        'End If
                End Sub)
            End If


            If Not ManifestacionesComplementarias Is Nothing Then
                ManifestacionesComplementarias.ToList.ForEach(
                    Sub(m As Manifestación)
                        If Not texto.TérminosMédicos.Contains(m) Then
                            n.Mensajes.Add("Puede complementarse agregando datos de: '" & m.Nombre & "' ")
                        End If
                        'If Not m.Existe(texto) Then
                        ' n.Mensajes.Add("Puede complementarse agregando datos de: '" & m.Nombre & "' ")
                        ' End If
                End Sub)
            End If

            Return n
        End Function
#End Region

#Region "Constructor"
        'Sub New(DiagnósticosNecesarios As Diagnóstico(),
        '        DiagnósticosComplementarios As Diagnóstico(),
        '        ManifestaciónObligatoria As Manifestación(),
        '        ManifestaciónComplementaria As Manifestación(),
        '        ManifestaciónOpcional As Manifestación())

        '    Me.DiagnósticosNecesarios = DiagnósticosNecesarios
        '    Me.DiagnósticosComplementarios = DiagnósticosComplementarios
        '    Me.ManifestacionesObligatorias = ManifestaciónObligatoria
        '    Me.ManifestacionesOpcionales = ManifestaciónOpcional
        '    Me.ManifestacionesComplementarias = ManifestaciónComplementaria

        'End Sub

        Sub New()

        End Sub
#End Region

    End Class
End Namespace