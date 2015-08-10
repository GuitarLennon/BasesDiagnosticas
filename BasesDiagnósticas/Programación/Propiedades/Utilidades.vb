Imports System.Runtime.CompilerServices

Namespace Programación.Utilidades

    Public Module Utilidades
        Public Const c As String = ", "
        Public Const propiedad As String = "."
        Public Const e As String = " "
        '        Public Const defaultfile As String = "TestDB.mdb"
        Public Const Cm As String = Chr(22)

        Public DebugMode As Boolean = False

        Function comma(ByVal ParamArray Par() As String) As String
            If Not (Not Par Is Nothing AndAlso Not Par.Count = 0) Then Return Nothing
            If Par.Count = 1 Then Return Par(0)
            Dim i%, s$
            s = Par(i)
            For i = 1 To Par.Count - 1
                s = s & c & Par(i)
            Next
            s = s & propiedad
            Return s
        End Function

        Function comma(ByVal ParamArray Par() As Object) As String
            If Not (Not Par Is Nothing AndAlso Not Par.Count = 0) Then Return Nothing
            If Par.Count = 1 Then Return CStr(Par(0))
            Dim i As Integer = 0, s As Object = Nothing, cad As String = ""
            If Not Par(i) Is Nothing Then _
            s = Par(i)
            cad = CType(Par(i), String)
            For i = 1 To Par.Count - 1
                If Not Par(i) Is Nothing Then _
                cad = cad & c & CType(Par(i), String)
            Next
            cad = cad & propiedad
            Return CStr(cad)
        End Function

        Function Separar(ByVal separador As String, ByVal ParamArray Par() As String) As String
            If Par Is Nothing Then Return Nothing
            If Par.Count = 0 Then Return Nothing
            Dim i%, s$
            s = Par(i)
            For i = 1 To Par.Count - 1
                If Not String.IsNullOrEmpty(Par(i)) Then _
                s = s & separador & Par(i)
            Next
            If Not String.IsNullOrEmpty(s) Then _
        s = s & propiedad
            Return s
        End Function

        Function Espaciar(ByVal ParamArray Par() As String) As String
            If Par Is Nothing Then Return Nothing
            Dim i%, s$
            s = Par(i)
            For i = 1 To Par.Count - 1
                If Not String.IsNullOrEmpty(Par(i)) Then _
            s = s & e & Par(i)
            Next
            If Not String.IsNullOrEmpty(s) Then _
            s = s & propiedad
            Return s
        End Function

        Function MaxLengthOfStrings(strings As String(), Optional ByRef WinnerString As String = Nothing) As Integer
            Dim maxl As Integer
            For Each s As String In strings
                If s.Length > maxl Then
                    maxl = s.Length
                    WinnerString = s
                End If
            Next
            Return maxl
        End Function

        Function Fibonacci(Iterator As Integer) As Long
            'Dim trk As New Tracker("Fibonacci", "Iterator = " & Iterator)
            'Thread.Sleep(100)
            If Iterator <= 0 Then Return 0
            If Iterator = 1 Then Return 1
            Dim result As Long = Fibonacci(Iterator - 1) + Fibonacci(Iterator - 2)
            'trk.TerminarMétodo()
            Return result
        End Function

        Function Fibonacci2(Iterator As Integer) As Long
            If Iterator <= 0 Then Return 0
            If Iterator = 1 Then Return 1
            Dim result As Long = Fibonacci2(Iterator - 1) + Fibonacci2(Iterator - 2)
            Return result
        End Function

        Function Recursiva(Recursiones As Integer) As Long
            'Dim trk As New Tracker("Recursiva: Iterator =" & Recursiones)
            If Recursiones = 0 Then Return 0
            Dim result As Long = Recursiva(Recursiones - 1)
            'trk.TerminarMétodo()
            Return result
        End Function

        Function StandarizedPreferedHeight(Preferido As Integer, mínimo As Integer, Optional máximo As Integer = 0) As Integer
            If Preferido < mínimo Then Return mínimo
            If Not máximo = 0 And Preferido > máximo Then Return máximo
            Return Preferido
        End Function

        <Runtime.CompilerServices.Extension>
        Function isNotEmpty(Array As Array) As Boolean
            If Array Is Nothing Then Return False
            If Array.Length = 0 Then Return False
            Return True
        End Function

        <Runtime.CompilerServices.Extension>
        Function isNotEmpty(iCollection As ICollection) As Boolean
            Return Not (Not iCollection Is Nothing AndAlso Not iCollection.Count = 0)
        End Function

        <Runtime.CompilerServices.Extension>
        Function isEmpty(iCollection As ICollection) As Boolean
            Return Not (Not iCollection Is Nothing AndAlso Not iCollection.Count = 0)
        End Function

        '<Runtime.CompilerServices.Extension> _
        'Function isEmpty(Of T)(iCollection As IEnumerable(Of T)) As Boolean
        '    Return Not (Not iCollection Is Nothing AndAlso Not iCollection.Count = 0)
        'End Function

        <Runtime.CompilerServices.Extension>
        Function isEmpty(iCollection As Array) As Boolean
            Return Not (Not iCollection Is Nothing AndAlso Not iCollection.Length = 0)
        End Function

        '<Runtime.CompilerServices.Extension> _
        'Public Function IListIsEmpty(list As IList) As Boolean
        '    Return Not (Not list Is Nothing AndAlso Not list.Count = 0)
        'End Function

        <Runtime.CompilerServices.Extension>
        Public Function IListIsEmpty(Of T)(list As IList(Of T)) As Boolean
            Return Not (Not list Is Nothing AndAlso Not list.Count = 0)
        End Function

        '<Runtime.CompilerServices.Extension> _
        'Function isEmpty(Of T)(col As IList(Of T)) As Boolean
        '    Return Not (Not col Is Nothing AndAlso Not col.Count = 0)
        'End Function

        Sub WaitUntil(ByRef Valor As Integer, ByRef referencia As Integer, Optional AbortMs As Long = -1)
            Dim watch As New Stopwatch
            watch.Start()
            Do Until Valor = referencia
                If Not AbortMs = -1 Then
                    If watch.ElapsedMilliseconds > AbortMs Then Exit Do
                End If
            Loop
        End Sub

        Sub WaitUntil(ByRef Valor As Boolean, Optional ByRef referencia As Boolean = True, Optional AbortMs As Long = -1)
            Dim watch As New Stopwatch
            watch.Start()
            Do Until Valor = referencia
                If Not AbortMs = -1 Then
                    If watch.ElapsedMilliseconds > AbortMs Then Exit Do
                Else
                    If watch.ElapsedMilliseconds > 1000 Then Exit Do
                End If
            Loop
        End Sub
        <Extension>
        Sub Wait(ByRef condition As Boolean)
Check:
            If Not condition Then
                Threading.Thread.Sleep(1)
                Debug.Print("WAITING")
                GoTo Check
            End If
        End Sub

        Sub Wait(ConditionChecker As Func(Of Boolean))
Check:
            Dim condition As Boolean = ConditionChecker.Invoke
            If Not condition Then
                Threading.Thread.Sleep(5)
                GoTo Check
            End If
        End Sub

        <Runtime.CompilerServices.Extension()>
        Public Function RemoveNullItems(Of T)(array As IEnumerable) As T()
            Dim l As New List(Of T)
            For Each i As T In array
                If i Is Nothing Then
                    'no hagas nada
                Else
                    l.Add(i)
                End If
            Next
            Return l.ToArray
        End Function

        <Runtime.CompilerServices.Extension()>
        Public Function ObtenerÚltimoNúmero(Cadena As String) As Integer
            Dim resultado As String = Nothing, nuevo_resultado As String = Nothing

            If IsNumeric(Cadena) Then Return CInt(Cadena)
            For i = Cadena.Length To 1 Step -1
                resultado = Cadena(i) & resultado
                If IsNumeric(resultado) Then
                    nuevo_resultado = resultado
                Else
                    Exit For
                End If
            Next
            Return CInt(nuevo_resultado)
        End Function

        Sub cambiar_valor(ByRef variable As Object, ByVal valor As Object, ByVal override As Boolean, ByVal silence As Boolean)
            If Not variable Is valor Then
                If override Then
                    variable = valor
                ElseIf Not silence Then
                    Throw New Exception(variable.ToString)
                End If
            End If
        End Sub

        Function No(ByVal Condición As Boolean) As String
            If Condición Then
                Return ""
            End If
            Return "no"
        End Function

        Function F(ByVal cadena As String, Optional ByVal c1 As String = "_", Optional ByVal c2 As String = " ") As String
            Return Strings.Replace(cadena, c1, c2)
        End Function

        Function F2(ByVal cadena As String, Optional ByVal c1 As String = "_", Optional ByVal c2 As String = " ") As String
            Return Strings.Replace(cadena, c2, c1)
        End Function

        <System.Runtime.CompilerServices.Extension()>
        Function RemoverItem(Of T)(ByVal arr As T(), ByVal index As Integer) As T()
            If arr Is Nothing Then Return Nothing
            Dim uBound = arr.GetUpperBound(0)
            Dim lBound = arr.GetLowerBound(0)
            Dim arrLen = uBound - lBound

            If index < lBound OrElse index > uBound Then
                Throw New ArgumentOutOfRangeException(
            String.Format(System.Globalization.CultureInfo.CurrentCulture, "Index must be from {0} to {1}.", lBound, uBound))

            Else
                'Si la matriz sólo es de un elemento, no devuelvas nada
                If uBound = lBound Then Return Nothing
                'create an array 1 element less than the input array
                Dim outArr(arrLen - 1) As T
                'copy the first part of the input array
                Array.Copy(arr, 0, outArr, 0, index)
                'then copy the second part of the input array
                Array.Copy(arr, index + 1, outArr, index, uBound - index)

                Return outArr
            End If
        End Function

        Function JuntarArray(Of T)(ByRef Array1 As T(), ByVal Array2 As T()) As T()
            'Si los dos parámetros son nulos, regresa nada
            If Array1 Is Nothing And Array2 Is Nothing Then
                Return Nothing
                'Si el parámetro 1 es nada, entonces devuelve el 2 solito
            ElseIf Array1 Is Nothing Then
                Return Array2
                'Si el parámetro 2 es nada, entonces devuelve el 1 solito
            ElseIf Array2 Is Nothing Then
                Return Array1
            End If

            Dim i As Integer = Array1.Length
            Array.Resize(Array1, i + Array2.Length)
            Array.Copy(Array2, 0, Array1, i, Array2.Length)
            Return Array1
        End Function

        Sub JuntarArray(ByRef array1() As String, ByVal Array2 As String()) 'As Object()
            Dim i As Integer = array1.Length
            Array.Resize(array1, i + Array2.Length)
            Array.Copy(Array2, 0, array1, i, Array2.Length)
        End Sub

        <Extension>
        Function Convertir_Array(Of T)(ByVal Obj As IEnumerable) As T()
            If Obj Is Nothing Then Return Nothing
            Dim ret As T(), i As Integer
            ReDim ret(0)
            For Each o As T In Obj
                If i > ret.GetUpperBound(0) Then _
            ReDim Preserve ret(i)
                ret(i) = o
                i += 1
            Next
            Return ret
        End Function

        Function Eliminar_Null(Of T)(ByVal Matriz() As T) As T()
            If Matriz Is Nothing Then Return Nothing
start:
            For i As Integer = Matriz.GetLowerBound(0) To Matriz.GetUpperBound(0)
                If Matriz(i) Is Nothing Then
                    If Matriz.Length = 1 Then
                        Return Nothing
                    Else
                        Matriz = RemoverItem(Matriz, i)
                        GoTo start
                    End If
                End If
            Next
            Return Matriz
        End Function

        <Extension> Function AnexarItem(Of T)(array As T(), item As T) As T()
            Dim new_array As T(), i As Integer = array.GetUpperBound(0) + 1
            ReDim new_array(i)
            new_array(i) = item
            Return new_array
        End Function

        <Extension> Sub AddRange(Of T)(Lista As IList(Of T), items As IEnumerable(Of T))
            If Lista Is Nothing Then Exit Sub
            If items Is Nothing Then Exit Sub
            If items.Count = 0 Then Exit Sub
            For Each item As T In items
                Lista.Add(item)
            Next
        End Sub

        <Serializable()> Enum Signos
            DESCONOCIDO
            Ratón
            Res
            Tigre
            Conejo
            Dragón
            Serpiente
            Caballo
            Borrego
            Mono
            Gallo
            Perro
            Jabalí
        End Enum

        <System.Runtime.CompilerServices.Extension>
        Function GetSigno(Fecha As Date) As Signos
            Dim x As New Globalization.ChineseLunisolarCalendar
            Dim año As Integer = x.GetSexagenaryYear(Fecha)
            Return CType(x.GetTerrestrialBranch(año), Signos)
        End Function

    End Module

    Public Module FechaYHora
        'Public Const FormatoFecha As String = 
        Public Const FormatoFecha2 As String = "dd/MM/yyyy'"
        Public FechaLímiteInferior As New Date(1910, 1, 1)
        Public FechaLímiteSuperior As New Date(2099, 12, 31)
        Private _Días_Festivos As Date()
        Public Property Días_Festivos As Date()
            Get
                Return _Días_Festivos
            End Get
            Set(value As Date())
                If value.isEmpty Then _Días_Festivos = Nothing : Exit Property
                Dim l As New List(Of Date)
                For Each Dat As Date In value
                    l.Add(Dat.Date)
                Next
                l.Sort()
                _Días_Festivos = l.ToArray
            End Set
        End Property
        Public Property Fecha_de_trabajo() As Date
            Get
                If Usar_Date_Actual Then
                    Return Now
                End If
                Return _Fecha_de_trabajo
            End Get
            Set(ByVal value As Date)
                _Fecha_de_trabajo = value
            End Set
        End Property
        Private _Fecha_de_trabajo As Date = Now
        Public Usar_Date_Actual As Boolean = True

        <Extension>
        Function ObtenerEdad(ByVal fecha_de_nacimiento As Date) As Integer
            Dim años As Integer = Fecha_de_trabajo.Year - fecha_de_nacimiento.Year
            Dim meses As Integer = Fecha_de_trabajo.Month - fecha_de_nacimiento.Month
            Dim días As Integer = Fecha_de_trabajo.Day - fecha_de_nacimiento.Day
            If días < 0 Then
                días = días + Date.DaysInMonth(Fecha_de_trabajo.Year, Fecha_de_trabajo.Month)
                meses = meses - 1
            End If
            If meses < 0 Then
                meses = meses + 12
                años = años - 1
            End If
            Return años
        End Function

        <Extension>
        Public Function AjustarFechas(ByRef fecha As Date) As Date
            If fecha < FechaLímiteInferior Then Return FechaLímiteInferior
            If fecha > FechaLímiteSuperior Then Return FechaLímiteSuperior
            Return fecha
        End Function

        <Extension>
        Public Function EsFechaPorDefecto(ByRef fecha As Date) As Boolean
            If fecha = FechaLímiteInferior Then Return True
            If fecha = FechaLímiteSuperior Then Return True
            Return False
        End Function

        <Extension>
        Public Function DaysInMonth(Fecha As Date) As Integer
            Return Date.DaysInMonth(Fecha.Year, Fecha.Month)
        End Function

        <Extension>
        Public Function EsFestivo(Fecha As Date) As Boolean
            If Fecha.DayOfWeek = DayOfWeek.Sunday Then Return True
            If Fecha.DayOfWeek = DayOfWeek.Saturday Then Return True
            If Días_Festivos.isEmpty Then Return False
            If Días_Festivos.Contains(Fecha.Date) Then Return True
            Return False
        End Function

        <Extension>
        Public Function Próximo_Habil_del_Próximo_mes(fecha_actual As Date,
                                                      Optional DíaOrdinario As Integer = 1) As Date
            'Inicia el próximo mes
            Dim año As Integer = fecha_actual.Year
            Dim mes As Integer = fecha_actual.Month
            Dim día As Integer = 1
            If mes = 12 Then
                mes = 1
                año = año + 1
            Else
                mes = mes + 1
            End If
            'Crea una fecha de trabajo
            Dim fecha As New Date(año, mes, día)
            'Busca i días ordinarios
            For i As Integer = 1 To DíaOrdinario
                Do While fecha.EsFestivo
                    'Si es festivo, agerga otro hasta que no lo sea
                    fecha = fecha.AddDays(1)
                Loop
            Next
            Return fecha
        End Function
    End Module

    <Serializable> Public Class Avisos : Implements IList(Of Aviso)
        'colección de aviso
        Private Innercol As New List(Of Aviso)

        Public ReadOnly Property Avisos As String()
            Get
                Dim s As New List(Of String)
                For i = 0 To Innercol.Count - 1
                    s.Add(i & ".- " & Innercol(i).Aviso)
                Next
                Return s.ToArray
            End Get
        End Property

        'Clase auxiliar

        Public Sub Add(item As Aviso) Implements ICollection(Of Aviso).Add
            If Not Innercol.Contains(item) Then Innercol.Add(item)
        End Sub

        Public Sub Clear() Implements ICollection(Of Aviso).Clear
            Innercol.Clear()
        End Sub

        Public Function Contains(item As Aviso) As Boolean Implements ICollection(Of Aviso).Contains
            Return Innercol.Contains(item)
        End Function

        Public Sub CopyTo(array() As Aviso, arrayIndex As Integer) Implements ICollection(Of Aviso).CopyTo
            Innercol.CopyTo(array, arrayIndex)
        End Sub

        Public ReadOnly Property Count As Integer Implements ICollection(Of Aviso).Count
            Get
                Return Innercol.Count
            End Get
        End Property

        Public ReadOnly Property IsReadOnly As Boolean Implements ICollection(Of Aviso).IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public Function Remove(item As Aviso) As Boolean Implements ICollection(Of Aviso).Remove
            Return Innercol.Remove(item)
        End Function

        Public Function GetEnumerator() As IEnumerator(Of Aviso) Implements IEnumerable(Of Aviso).GetEnumerator
            Return Innercol.GetEnumerator
        End Function

        Public Function IndexOf(item As Aviso) As Integer Implements IList(Of Aviso).IndexOf
            Return Innercol.IndexOf(item)
        End Function

        Public Sub Insert(index As Integer, item As Aviso) Implements IList(Of Aviso).Insert
            Innercol.Insert(index, item)
        End Sub

        Default Public Property Item(index As Integer) As Aviso Implements IList(Of Aviso).Item
            Get
                Return Innercol.Item(index)
            End Get
            Set(value As Aviso)
                Innercol.Item(index) = value
            End Set
        End Property

        Public Sub RemoveAt(index As Integer) Implements IList(Of Aviso).RemoveAt
            Innercol.RemoveAt(index)
        End Sub

        Public Function GetEnumerator1() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Innercol.GetEnumerator()
        End Function
    End Class

    Public Structure Aviso
        Public Enum Gravedades
            No_especificado = 0
            No_grave = 1
            Gravedad_leve = 2
            Gravedad_Moderada = 3
            Gravedad_severa = 4
        End Enum
        Public Mensaje As String
        Public Solución As String
        Public Gravedad As Gravedades
        ReadOnly Property Aviso As String
            Get
                Dim Returning As String = Nothing

                Returning = Strings.StrDup(Gravedad, "!") & " "

                If String.IsNullOrEmpty(Mensaje) Then
                    Returning &= "(Aviso vacío)"
                Else
                    Returning &= Mensaje
                End If
                If Not String.IsNullOrEmpty(Solución) Then
                    Returning &= " Solución: " & Solución
                End If
                Return Returning
            End Get
        End Property
        Sub New(Mensaje As String,
            Optional solución As String = Nothing,
            Optional Gravedad As Gravedades = Gravedades.No_especificado)
            Me.Mensaje = Mensaje
            Me.Solución = solución
            Me.Gravedad = Gravedad
        End Sub
    End Structure

    Public Class IEventoTemporal
        'Propiedades
        Public Property Inicio As Date = FechaLímiteInferior
        Public Property Final As Date = FechaLímiteInferior
        Public Property Duración_Minutos As Long
        Public Overridable Property Valor As Object
        'Campos públicos
        Public Pinned_duration As Boolean = False
        'Constructores y destructores
        'Public Sub New()
        '    MyBase.New()
        'End Sub
        'Public Sub New(ByVal Inicio As Date, ByVal duración_en_minutos As Long)
        '    _Inicio = Inicio
        '    Me.Duración_Minutos = duración_en_minutos
        'End Sub
        'Public Sub New(ByVal Inicio As Date, ByVal Final As Date)
        '    _Inicio = Inicio
        '    _Final = Final
        'End Sub
        ''Funciones compartidas
        Public Shared Function Ordenar(ByVal IEventoTemporal() As IEventoTemporal) As IEventoTemporal()
            If IEventoTemporal.isEmpty Then Return Nothing
            Dim list As List(Of IEventoTemporal) = IEventoTemporal.ToList
            list.Sort(New IEventoTemporalComparer)
            Return list.ToArray
        End Function
        Public Shared Function ObtenerFechasInicio(ByVal IEventoTemporal() As IEventoTemporal) As Date()
            If IEventoTemporal.isEmpty Then Return Nothing
            Dim Ordenadas() As Date, i As Integer
            ReDim Ordenadas(IEventoTemporal.GetUpperBound(0))
            For Each C As IEventoTemporal In IEventoTemporal
                If Ordenadas.GetUpperBound(0) < i Then ReDim Preserve Ordenadas(i)
                Ordenadas(i) = C.Inicio
                i = i + 1
            Next
            Return Ordenadas
        End Function
        Public Shared Function ObtenerFechasFinal(ByVal IEventoTemporal() As IEventoTemporal) As Date()
            Dim Ordenadas() As Date, i As Integer
            ReDim Ordenadas(IEventoTemporal.GetUpperBound(0))
            For Each C As IEventoTemporal In IEventoTemporal
                If Ordenadas.GetUpperBound(0) < i Then ReDim Preserve Ordenadas(i)
                Ordenadas(i) = C.Final
                i = i + 1
            Next
            Return Ordenadas
        End Function
        Public Shared Function FechaÚltimoElemento(ByVal IEventoTemporal() As IEventoTemporal) As Date
            If IEventoTemporal.isEmpty Then Return FechaLímiteInferior
            Dim fechas() As Date = ObtenerFechasInicio(IEventoTemporal)
            Array.Sort(fechas)
            Array.Reverse(fechas)
            For i As Integer = fechas.GetUpperBound(0) To 0
                If fechas(i) < Fecha_de_trabajo Then Return fechas(i)
            Next
            Return FechaLímiteInferior
        End Function
        Public Shared Function EventosDelDía(ByVal Ieventotemporal As IEventoTemporal(), ByVal Día As Date) As IEventoTemporal()
            Dim Output As IEventoTemporal() = Nothing, i As Integer

            If Ieventotemporal Is Nothing Then Return Nothing

            For Each Evento As IEventoTemporal In Ieventotemporal
                If DateDiff(DateInterval.Day, Día, Evento.Inicio) = 0 Then
                    If Output Is Nothing Then ReDim Output(0)
                    If Output.GetUpperBound(0) > i Then _
                                ReDim Preserve Output(i)
                    Output(i) = Evento
                End If
            Next
            Return Output
        End Function
        Public Shared Function EventosQueInicienEnElMomento(ByVal Ieventotemporal As IEventoTemporal(), ByVal Momento As IEventoTemporal) As IEventoTemporal()
            Dim Output As IEventoTemporal() = Nothing, i As Integer

            If Ieventotemporal Is Nothing Then Return Nothing

            For Each Evento As IEventoTemporal In Ieventotemporal
                If Evento._Inicio >= Momento._Inicio And Evento._Inicio < Momento.Final Then
                    If Output Is Nothing Then ReDim Output(0)
                    If Output.GetUpperBound(0) > i Then _
                                ReDim Preserve Output(i)
                    Output(i) = Evento
                End If
            Next
            Return Output
        End Function

        ''' <summary>
        ''' Determina si una fecha es apta para una consulta (Día Habil)
        ''' </summary>
        ''' <param name="fecha"></param>
        ''' <param name="festivos"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function EsFechaHabil(ByVal fecha As Date,
                                        ByVal festivos() As Date) As Boolean
            If fecha.DayOfWeek = DayOfWeek.Saturday Then Return False
            If fecha.DayOfWeek = DayOfWeek.Sunday Then Return False
            If festivos.Contains(fecha) Then Return False
            Return True
        End Function
        'Comparadores
        Public Class IEventoTemporalComparer
            Implements IComparer(Of IEventoTemporal)
            Public Function Compare(x As IEventoTemporal, y As IEventoTemporal) As Integer Implements IComparer(Of IEventoTemporal).Compare
                Return x.Inicio.CompareTo(y.Inicio)
            End Function
        End Class
    End Class

End Namespace