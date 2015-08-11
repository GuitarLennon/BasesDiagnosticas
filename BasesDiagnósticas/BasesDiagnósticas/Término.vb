Option Explicit On
Option Infer Off
Option Strict On
Option Compare Text

Imports Diagnósticos.Programación

Namespace BasesDiagnósticas
    ''' <summary>
    ''' Define a un término médico o no médico cualquiera que se puede buscar
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class Término

#Region "Shared"
        Protected Shared myDiccionario As Dictionary(Of String, Type)
        Protected Shared myKeys As List(Of String)

        Friend Shared Function Diccionario() As IDictionary(Of String, Type)

            Return myDiccionario

        End Function

        Friend Shared Function Lista() As IList(Of String)

            Return myKeys

        End Function

        Shared Sub New()

            myDiccionario = _TérminosDerivados(True, True)
            myKeys = New List(Of String)

            myDiccionario.ToList.ForEach(
                Sub(kvp As KeyValuePair(Of String, Type))

                    myKeys.Add(kvp.Key)

                End Sub)

            myKeys.Sort(Function(x As String, y As String) y.Length.CompareTo(x.Length))

        End Sub

        Protected Friend Shared Function TérminosDerivadosType() As Type()

            Return AppDomain.CurrentDomain.GetAssemblies().
                SelectMany(Function(a As System.Reflection.Assembly) a.GetTypes()).
                Where(Function(t As Type) t.IsSubclassOf(GetType(Término))).ToArray

        End Function

        Private Shared Function _TérminosDerivados(IncluirSinónimos As Boolean, incluirAbstractos As Boolean) As Dictionary(Of String, Type)

            'Crear un diccionario
            Dim dic As New Dictionary(Of String, Type)

            Dim terms() As Type = TérminosDerivadosType()

            'Por cada término derivado
            For Each t As Type In terms

                'TérminosDerivadosType.ToList.ForEach(
                'Sub(t As Type)
                Try
                    'Si se puede crear la clase
                    If Not t.IsAbstract Then

                        'Crea la clase
                        Dim a As Término

                        a = CType(Activator.CreateInstance(t), Término)

                        'Agregalo al diccionario
                        Debug.Print(a.Nombre.ToLower)
                        dic.Add(a.Nombre.ToLower, t)

                        'Si nos piden incluir los sinónimos entonces
                        If IncluirSinónimos Then

                            'Pregunta los sinónimos y si existen
                            If a.Sinónimos IsNot Nothing Then

                                'por cada uno
                                a.Sinónimos.ToList.ForEach(
                                    Sub(active As String)

                                        'agrega una entrada al diccionario
                                        Debug.Print(active.ToLower)
                                        dic.Add(active.ToLower, t)

                                    End Sub)
                            End If

                            'destruye el objeto
                            a.Finalize()

                        Else 'Si no se puede crear el objeto (es abstracto)

                            'si nos piden incluir abstractos
                            If incluirAbstractos Then

                                'Agregalo al diccionario con el nombre de su clase
                                Debug.Print(t.Name.ToLower)
                                dic.Add(t.Name.ToLower, t)
                            End If

                        End If
                    End If
                Catch ex As Exception
                    Stop
                End Try
                'End Sub)
            Next

            'regresa el diccionario
            Return dic

        End Function

        Public Shared Function TérminosDerivados(Optional IncluirSinónimos As Boolean = False, Optional IncluirAbstractos As Boolean = False) As IDictionary(Of String, Type)

            Return _TérminosDerivados(IncluirSinónimos, IncluirAbstractos)

        End Function

        Public Shared Function TérminosDerivadosActivos() As Término()

            Dim l As New List(Of Término)

            TérminosDerivadosType.ToArray.ToList.ForEach(
                Sub(t As Type)
                    l.Add(CType(Activator.CreateInstance(t), Término))
                End Sub)

            Return l.ToArray

        End Function
#End Region

#Region "Instance"
        Public ubicaciónEnTexto As UbicaciónEnTexto()
#End Region

#Region "Property"

        ''' <summary>
        ''' Nombre del término, que será el mismo que el de la clase sin guiones bajos y no se podrá cambiar
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable ReadOnly Property Nombre As String
            Get

                Return MyClass.GetType.Name.Replace("_", " ")

            End Get
        End Property

        ''' <summary>
        ''' Otros nombres reconocidos por el programa además del término principal, se podrá editar
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Property Sinónimos As String()

        ''' <summary>
        ''' La descripción del término descrito por esta clase
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public MustOverride Property Description As String
#End Region

#Region "Constructors"

        Friend Sub New()

        End Sub

        'Public MustOverride Sub New(ubicación As UbicaciónEnTexto)
        'Me.ubicaciónEnTexto = ubicación
        'End Sub
#End Region

#Region "Functions"

        ''' <summary>
        ''' Obtiene la ubicación del término en el texto
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function ObtenerUbicaciones(texto As Texto) As UbicaciónEnTexto()

            Dim result As UbicaciónEnTexto() 'New List(Of UbicationOnText)
            result = texto.GetUbicationOnText(Nombre)
            If result IsNot Nothing AndAlso result.Count > 0 Then Return result

            For Each s As String In Sinónimos
                result = texto.GetUbicationOnText(s)
                If result IsNot Nothing AndAlso result.Count > 0 Then Return result
            Next

            Return Nothing

        End Function

        ''' <summary>
        ''' Determina si existe este término en el texto
        ''' </summary>
        ''' <param name="texto"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overridable Function Existe(texto As Texto) As Boolean

            Return texto.Exists(Nombre)

        End Function

#End Region

    End Class
End Namespace