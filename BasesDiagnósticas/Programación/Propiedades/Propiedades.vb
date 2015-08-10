Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Diagnósticos.Programación.Utilidades

<Assembly: CLSCompliant(True)>

Namespace Programación.Propiedades
    'ESTRUCTURA DE TRABAJO
    ''' <summary>
    ''' Estructura de la propiedad
    ''' </summary>
    ''' <remarks></remarks>
    Public Structure Propiedad
        Implements IEquatable(Of Propiedad)
        Property Nombre As String
        Property Valor As Object
        Property Tipo As Type
        Property SoloLectura As Boolean
        Private inneratributos As List(Of Type)
        ReadOnly Property Atributos As IList(Of Type)
            Get
                Return inneratributos
            End Get
        End Property
        ReadOnly Property TypeCode As TypeCode
            Get
                Return Type.GetTypeCode(Tipo)
            End Get
        End Property

        Public Overrides Function ToString() As String
            Dim sólolectura As String = "", val As String = ""
            If SoloLectura Then sólolectura = "(Solo lectura)"
            If Not Valor Is Nothing Then
                val = Convert.ToString(Valor)
            Else
                val = "Ninguno"
            End If
            Return String.Format(System.Globalization.CultureInfo.CurrentCulture, "{0}, tipo: {1} {2}, valor: {3}", Nombre, Tipo.Name, sólolectura, val)
        End Function

        Sub New(ByVal Nombre As String,
                Optional ByVal valor As Object = Nothing,
                Optional ByVal tipo As Type = Nothing,
                Optional ByVal SoloLectura As Boolean = False,
                Optional ByVal Atributos As Type() = Nothing)
            Me.Nombre = Nombre
            Me.Valor = valor
            If tipo Is Nothing Then
                If valor Is Nothing Then
                    Me.Tipo = Nothing
                Else
                    Me.Tipo = valor.GetType
                End If
            Else
                Me.Tipo = tipo
            End If
            Me.SoloLectura = SoloLectura
            Me.SetAttributes(New List(Of CustomAttributeData))
            If Atributos.isEmpty Then
                Me.Atributos.AddRange(Atributos)
            End If
        End Sub

        Public Sub SetAttributes(atributos As IEnumerable(Of CustomAttributeData))
            If inneratributos Is Nothing Then inneratributos = New List(Of Type)
            inneratributos.Clear()
            If atributos Is Nothing Then Exit Sub
            If atributos.Count = 0 Then Exit Sub

            For Each x As CustomAttributeData In atributos
                inneratributos.Add(x.AttributeType)
            Next

        End Sub

        Public Function Equals1(other As Propiedad) As Boolean Implements IEquatable(Of Propiedad).Equals
            If Nombre = Nombre Then
                If Valor Is Valor Then
                    Return True
                End If
            End If
            Return False
        End Function
    End Structure

    Public Module Propiedades
        'OBTENER CAMPOS
        ''' <summary>
        ''' Devuelve y/o asigna el valor del campo Nombre en el objeto Obj
        ''' </summary>
        ''' <param name="Obj">Objeto de las propiedades</param>
        ''' <param name="nombre">Nombre de la propiedad</param>
        ''' <param name="value">Valor para asignar </param>
        ''' <returns>Valor de la propiedad del objeto</returns>
        ''' <remarks></remarks>
        Function Campo(ByRef Obj As Object, ByVal nombre As String, Optional ByVal value As Object = Nothing) As Object
            Dim y() As System.Reflection.FieldInfo = Obj.GetType.GetFields
            If Not value Is Nothing Then Obj.GetType.GetField(nombre).SetValue(Obj, value)
            Return Obj.GetType.GetField(nombre).GetValue(Obj)
        End Function
        ''' <summary>
        ''' Devuelve los valores de los campos del objeto Obj (y los nombres de los campos)
        ''' </summary>
        ''' <param name="Obj">Dueño de las propiedades</param>
        ''' <param name="fieldnames">Matriz donde se depositarán los nombres</param>
        ''' <returns>Matriz donde los nombres son almacenados</returns>
        ''' <remarks></remarks>
        Function Campo(ByRef Obj As Object, ByRef fieldnames() As String) As String()
            Dim x As Type = Obj.GetType
            Dim y() As System.Reflection.FieldInfo = Obj.GetType.GetFields()
            Dim i As Integer, ii() As String
            ReDim ii(0), fieldnames(0)
            For Each member As System.Reflection.FieldInfo In y
                If ii.GetUpperBound(0) < i Then _
                ReDim Preserve ii(i), fieldnames(i)
                ii(i) = CStr(member.GetValue(Obj))
                fieldnames(i) = member.Name
                i = i + 1
            Next
            Return ii
        End Function
        ''' <summary>
        ''' Devuelve las propiedades (propiedad) del objeto
        ''' </summary>
        ''' <param name="Obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Campos(ByRef Obj As Object) As Propiedad()
            Dim x As Type = Obj.GetType
            Dim y() As System.Reflection.FieldInfo = Obj.GetType.GetFields
            Dim i As Integer, ii() As Propiedad
            ReDim ii(0)
            For Each member As System.Reflection.FieldInfo In y
                ReDim Preserve ii(ii.Count)
                ii(i).Valor = member.GetValue(Obj)
                ii(i).Nombre = member.Name
                ii(i).Tipo = member.FieldType
                ii(i).SoloLectura = False
                i = i + 1
            Next
            Return ii
        End Function
        ''' <summary>
        ''' Devuelve los campos especificados en los parámetros
        ''' </summary>
        ''' <param name="obj">Dueño de las propiedades</param>
        ''' <param name="ParNom ">Parámetros solicitados</param>
        ''' <returns>Propiedades específicas</returns>
        ''' <remarks></remarks>
        Function Campos(ByVal obj As Object, ByVal ParNom() As String) As Propiedad()
            'OBTENER LOS CAMPOS DEL OBJETO
            Dim y() As System.Reflection.FieldInfo = obj.GetType.GetFields, x() As Propiedad
            ReDim x(ParNom.GetUpperBound(0))

            'Por cada item, generar un propiedad
            For i As Integer = 0 To ParNom.GetUpperBound(0)
                x(i).Nombre = ParNom(i)
                x(i).Valor = obj.GetType.GetField(ParNom(i)).GetValue(obj)
            Next
            Return x
        End Function
        'OBTENER PROPIEDADES
        ''' <summary>
        ''' Devuelve los campos (propiedad) del objeto
        ''' </summary>
        ''' <param name="Obj">Objeto</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Propiedades(ByVal Obj As Object, Optional index() As Object = Nothing) As Propiedad()
            If Obj Is Nothing Then Return Nothing
            Dim x As Type = Obj.GetType
            Dim y() As System.Reflection.PropertyInfo = Obj.GetType.GetProperties()
            Dim i As Integer = 0, ii As New List(Of Propiedad)
            'ReDim ii(0)
            For Each member As System.Reflection.PropertyInfo In y
                Try
                    'If i > ii.GetUpperBound(0) Then _
                    'ReDim Preserve ii(i)
                    Dim propiedad As New Propiedad
                    propiedad.Nombre = member.Name
                    If member.CanRead Then propiedad.Valor = member.GetValue(Obj, index)
                    propiedad.Tipo = member.PropertyType
                    propiedad.SoloLectura = Not member.CanWrite
                    propiedad.SetAttributes(member.CustomAttributes)
                    ii.Add(propiedad)
                Catch ex As Exception
                    Stop
                End Try
            Next
            Return ii.ToArray
        End Function
        ''' <summary>
        ''' Devuelve el VALOR de la propiedad
        ''' </summary>
        ''' <param name="obj"></param>
        ''' <param name="nombre"></param>
        ''' <param name="index"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function GetPropiedad(ByVal obj As Object, ByVal nombre As String, Optional ByVal index() As Object = Nothing) As Propiedad
            If nombre Is Nothing Then Return Nothing
            Dim propiedad As New Propiedad
            Dim x As Type = obj.GetType
            Dim y As System.Reflection.PropertyInfo = obj.GetType.GetProperty(nombre)
            If y Is Nothing Then Return Nothing
            propiedad.Nombre = y.Name
            If y.CanRead Then propiedad.Valor = y.GetValue(obj, index)
            propiedad.Tipo = y.PropertyType
            propiedad.SoloLectura = Not y.CanWrite
            propiedad.SetAttributes(y.CustomAttributes)
            Return propiedad
        End Function
        ''' <summary>
        ''' Devuelve las propiedades especificadas en los parámetros
        ''' </summary>
        ''' <param name="obj">Dueño de las propiedades</param>
        ''' <param name="ParNom ">Parámetros solicitados</param>
        ''' <returns>Propiedades específicas</returns>
        ''' <remarks></remarks>
        Function Propiedades(ByVal obj As Object, ByVal ParNom() As String) As Propiedad()
            If obj Is Nothing Then Return Nothing
            'DEFINIR SI HAY PARÁMETROS PARNOM
            If ParNom Is Nothing Then Return Propiedades(obj)
            If ParNom.Length = 0 Then Return Propiedades(obj)
            'OBTENER LOS CAMPOS DEL OBJETO
            Dim y() As System.Reflection.PropertyInfo = obj.GetType.GetProperties(), x() As Propiedad
            ReDim x(ParNom.GetUpperBound(0))

            'Por cada item, generar un propiedad
            For i As Integer = 0 To ParNom.GetUpperBound(0)
                Dim a As PropertyInfo = obj.GetType.GetProperty(ParNom(i))
                If Not a Is Nothing Then
                    x(i).Nombre = ParNom(i)
                    x(i).Tipo = a.PropertyType
                    If a.CanRead Then x(i).Valor = a.GetValue(obj, Nothing)
                    x(i).SoloLectura = Not a.CanWrite
                End If
            Next
            Return x
        End Function
        ''' <summary>
        ''' Devuelve las propiedades de un tipo
        ''' </summary>
        ''' <param name="tipo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function PropiedadesFromType(tipo As Type) As Propiedad()
            'Dim trk As New Tracker("PropiedadesFromType", tipo.Name)
            Dim l As New List(Of Propiedad)
            Dim properties() As PropertyInfo = tipo.GetProperties
            For Each prop As PropertyInfo In properties
                Dim propiedad As New Propiedad(prop.Name, Nothing, prop.PropertyType, prop.CanWrite, CType(prop.CustomAttributes, Type()))
                l.Add(propiedad)
                'trk.ActualizarMétodo(propiedad.ToString)
            Next
            'trk.TerminarMétodo()
            Return l.ToArray
        End Function
        'OBTENER VALORES
        ''' <summary>
        ''' Devuelve una matriz de valores
        ''' </summary>
        ''' <param name="propiedades">Propiedades origen</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DevolverValores(ByVal propiedades() As Propiedad, Optional ByVal Eliminar_Guión_bajo As Boolean = False) As Object()
            If propiedades.Length = 0 Then Return Nothing
            Dim i() As Object
            ReDim i(propiedades.GetUpperBound(0))
            For ii As Integer = 0 To propiedades.GetUpperBound(0)
                If Eliminar_Guión_bajo Then
                    If propiedades(ii).Valor Is Nothing Then
                        i(ii) = Nothing
                    Else
                        i(ii) = F(propiedades(ii).Valor.ToString)
                    End If
                Else
                    i(ii) = propiedades(ii).Valor
                End If
            Next
            Return i
        End Function
        Function DevolverValoresString(ByVal propiedades() As Propiedad) As String()
            Dim i() As String, ii As Integer = 0
            If propiedades.Length = 0 Then Return Nothing
            ReDim i(propiedades.GetUpperBound(0))
            For Each pr As Propiedad In propiedades
                i(ii) = CStr(pr.Valor)
                ii += 1
            Next
            Return i
        End Function
        ''' <summary>
        ''' Devuelve una matriz con los nombres de los campos
        ''' </summary>
        ''' <param name  ="Propiedades">Propiedades Origen</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DevolverNombres(ByVal Propiedades() As Propiedad) As String()
            Dim i() As String
            ReDim i(Propiedades.GetUpperBound(0))
            For ii As Integer = 0 To Propiedades.GetUpperBound(0)
                i(ii) = Propiedades(ii).Nombre
            Next
            Return i
        End Function

        Function DevolverEnumeración(ByVal objeto As Object, módulo As [Module]) As List(Of String)
            Dim s() As String = DevolverEnumeración_str(CType(objeto, String), módulo)
            If s Is Nothing Then Return Nothing
            Return New List(Of String)(s)
        End Function

        Function DevolverEnumeración(ByVal objeto As Object) As List(Of String)
            Dim s() As String = DevolverEnumeración_str(CType(objeto, String), objeto.GetType.Module)
            If s Is Nothing Then Return Nothing
            Return New List(Of String)(s)
        End Function

        Function DevolverEnumeración_Obj(ByVal objeto As Object) As Array
            Dim t As Type = objeto.GetType
            If objeto Is Nothing Then Throw New ArgumentNullException
            If Not t.IsEnum Then Throw New ArgumentException
            Return t.GetEnumValues()
        End Function

        ''' <summary>
        ''' Devuelve una enumeración especificada
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DevolverEnumeración_str(ByVal objeto As Object) As String()
            Dim t As Type = objeto.GetType
            Dim members() As MemberInfo, collection As New Collections.Specialized.StringCollection
            If t.IsEnum Then members = t.GetMembers() Else Return Nothing
            'Stop
            For Each member As MemberInfo In members
                If TypeOf (member) Is FieldInfo Then
                    collection.Add(member.Name)
                End If
            Next
            Dim output As String() = Nothing
            ReDim output(collection.Count - 1)
            collection.CopyTo(output, 0)
            Return output
        End Function
        Function DevolverEnumeración_str(ByVal nombre As String, módulo As [Module]) As String()
            Dim types() As Type = módulo.GetTypes()
            For Each t As Type In types
                If t.Name = nombre And t.IsEnum Then
                    Return [Enum].GetNames(t)
                End If
            Next
            Return Nothing
        End Function
        'OBTENER MÉTODOS
        ''' <summary>
        ''' Devuelve los métodos del objeto
        ''' </summary>
        ''' <param name="Obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DevolverMétodos(ByVal Obj As Object) As String()
            If Obj Is Nothing Then Return Nothing
            Dim x As Type = Obj.GetType
            Dim y() As System.Reflection.MethodInfo
            Dim i As Integer, ii() As String
            y = Obj.GetType.GetMethods((BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.DeclaredOnly))
            If y.Length = 0 Then Return Nothing
            ReDim ii(y.GetUpperBound(0))
            For Each m As System.Reflection.MethodInfo In y
                ii(i) = m.Name
                i = i + 1
            Next
            Return ii
        End Function
        Function DevolverMétodos(ByVal Obj As Object, ByVal category As String) As String()
            If Obj Is Nothing Then Return Nothing
            Dim x As Type = Obj.GetType
            Dim y() As System.Reflection.MethodInfo
            Dim i As Integer, ii() As String = Nothing
            y = Obj.GetType.GetMethods((BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.DeclaredOnly))
            If y.Length = 0 Then Return Nothing
            For Each m As System.Reflection.MethodInfo In y
                Dim o() As Object = m.GetCustomAttributes(GetType(System.ComponentModel.CategoryAttribute), False)
                If Not o Is Nothing Then
                    For Each o0 As System.ComponentModel.CategoryAttribute In o
                        If o0.Category = category Then
                            If ii Is Nothing Then ReDim ii(0)
                            If ii.GetUpperBound(0) < i Then ReDim Preserve ii(i)
                            ii(i) = m.Name
                            i = i + 1
                            Exit For
                        End If
                    Next
                End If
            Next
            Return ii
        End Function
        Public Function DevolverMétodos(obj As Object,
                              Categoria As Type) As String()
            If obj Is Nothing Then Return Nothing
            Dim x As Type = obj.GetType
            Dim y() As System.Reflection.MethodInfo
            Dim ii As New List(Of String)
            y = obj.GetType.GetMethods()
            If y.Length = 0 Then Return Nothing
            For Each m As System.Reflection.MethodInfo In y
                If m.HasAttribute(Categoria) Then
                    ii.Add(m.Name)
                End If
            Next
            Return ii.ToArray
        End Function

        <Extension>
        Public Function MétodosDelTipo(Métodos As IEnumerable(Of MethodInfo), AtributoPersonalizado As Type) As IEnumerable(Of MethodInfo)
            If Métodos Is Nothing Then Return Nothing
            Dim ii As New List(Of MethodInfo)
            For Each m As System.Reflection.MethodInfo In Métodos
                If m.HasAttribute(AtributoPersonalizado) Then
                    ii.Add(m)
                End If
            Next
            Return ii.ToArray
        End Function

        ''' <summary>
        ''' Ejecuta un método en el objeto por su nombre
        ''' </summary>
        ''' <param name="Obj"></param>
        ''' <param name="método"></param>
        ''' <param name="parámetros"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function LlamarMétodo(ByVal Obj As Object, ByVal método As String, ByVal ParamArray parámetros() As Object) As Object
            If Obj Is Nothing Then Return "OBJETO IS NOTHING"
            Dim x As Type = Obj.GetType
            Dim y As System.Reflection.MethodInfo = Obj.GetType.GetMethod(método)
            If y Is Nothing Then Return "MÉTODO NO EXISTE"
            Try
                If y.GetParameters.Length > 0 Then
                    Return y.Invoke(Obj, parámetros)
                Else
                    Return y.Invoke(Obj, Nothing)
                End If
            Catch ex As Exception
                Return "ERROR DE INVOCACIÓN"
            End Try
        End Function
        'OBTENER TYPES
        ''' <summary>
        ''' Devuelve los sub-TYPE del objeto
        ''' </summary>
        ''' <param name="Obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function DevolverTypes(ByVal ParamArray Obj() As Type) As Type()
            Dim t() As Type, tt() As Type, propiedad As Integer = 0
            ReDim t(0)
            'Dim PO As New
            For Each o As Type In Obj
                tt = o.GetNestedTypes
                If Not tt.Length = 0 Then
                    propiedad = t.Length - 1
                    ReDim Preserve t(propiedad + tt.Length)
                    For i As Integer = 0 To tt.Length - 1
                        t(propiedad + i) = tt(i)
                    Next
                End If
            Next
            While t(t.Length - 1) Is Nothing
                ReDim Preserve t(t.Length - 2)
            End While
            Return t
        End Function
        Function DevolverTypes(obj As Object) As Type()
            Dim t As New List(Of Type)
            Dim tp As Type() = DevolverTypes(obj.GetType)
            If Not tp Is Nothing Then t.AddRange(tp)
            t.Add(obj.GetType)
            Return t.ToArray
        End Function
        Function DevolverTypes(Type As Type) As Type()
            Dim ret As New List(Of Type)
            Dim Types As Type() = Type.GetNestedTypes()
            If Types Is Nothing Then Return Nothing
            If Types.Length = 0 Then Return Nothing
            ret.AddRange(Types)
            For Each T As Type In Types
                Dim w As Type() = T.GetNestedTypes
                If Not w Is Nothing AndAlso Not w.Count = 0 Then
                    ret.AddRange(DevolverTypes(T))
                End If
            Next

            Return ret.ToArray
        End Function

        Function DevolverTypes2(Type As Type) As Type()
            Dim l As New List(Of Type)
            Dim properties() As PropertyInfo = Type.GetProperties
            If properties.IListIsEmpty Then Return Nothing
            For Each propiedad As PropertyInfo In properties
                If Not l.Contains(propiedad.PropertyType) Then l.Add(propiedad.PropertyType)
                Dim s() As Type = DevolverTypes(propiedad.PropertyType)
                If Not s.isEmpty Then l.AddRange(s)
            Next
            Return l.ToArray
        End Function
        'EVALUAR
        Function Cumple_Propiedad(ByVal obj As Object, ByVal propiedad As Propiedad) As Boolean
            Return GetPropiedad(obj, propiedad.Nombre).Valor Is propiedad.Valor
        End Function
        'ÍNDICES
        Function DevolverÍndice(ByVal objeto As Object) As Integer
            Dim o() As Object
            Select Case objeto.GetType.Name
                Case "String", "Integer", "Int16", "Int32", "Int64"
                    Return Nothing
                Case Else
                    o = CType(DevolverEnumeración_str(objeto.GetType.Name), Object())
                    For Each ii As Object In o

                        If CBool(Strings.InStr(ii.ToString, objeto.ToString)) Then
                            'Stop
                            'Sexos.
                            Return CInt(o(Array.IndexOf(o, ii)))
                            Exit For
                        End If
                    Next
            End Select
            Return -1
        End Function
        'MODIFICAR PROPIEDADES
        ''' <summary>
        ''' Modifica las propiedades (propiedad) en el objeto OBJ
        ''' </summary>
        ''' <param name="Obj">Dueño de las propiedades</param>
        ''' <param name="Propiedades">Nuevas Propiedades</param>
        ''' <remarks></remarks>
        Sub AplicarParámetro(ByRef Obj As Object, ByVal ParamArray Propiedades() As Propiedad)
            'OBTENER LOS CAMPOS DEL OBJETO
            Dim y() As System.Reflection.FieldInfo = Obj.GetType.GetFields

            'Por cada propiedad
            For Each propiedad As Propiedad In Propiedades
                Obj.GetType.GetField(propiedad.Nombre).SetValue(Obj, propiedad.Valor)
            Next
        End Sub

        'Public Function AplicarPropiedades(Obj As Object, propiedad As Propiedad) As Object
        '    'OBTENER LOS CAMPOS DEL OBJETO
        '    Dim y() As System.Reflection.PropertyInfo = Obj.GetType.GetProperties
        '    Dim x As System.Reflection.PropertyInfo, t As Type

        '    'Por cada propiedad a establecer

        '    'Si la propiedad tiene nombre
        '    If Not String.IsNullOrEmpty(propiedad.Nombre) Then
        '        'Recupera la propiedad llamada "nombre"
        '        x = Obj.GetType.GetProperty(propiedad.Nombre)
        '        ' y su tipo
        '        t = x.PropertyType
        '        'Si su tipo es una enumeración
        '        If t.IsEnum Then
        '            'Si el valor existe en la numeración
        '            If t.IsEnumDefined(propiedad.Valor) Then
        '                'Obten su valor
        '                propiedad.Valor = [Enum].Parse(t, propiedad.Valor)
        '                'Establécelo
        '                If x.CanWrite Then x.SetValue(Obj, propiedad.Valor, Nothing)
        '            End If
        '        Else 'Si no es enumeración
        '            Try 'Intenta
        '                If propiedad.Valor.GetType Is t Then 'Si son del mismo tipo
        '                    'Si no es un tipo de valor entonces
        '                    'If Not t.IsValueType Then
        '                    'Establécelo sin problemas
        '                    '    If x.CanWrite Then x.SetValue(Obj, propiedad.Valor, Nothing)
        '                    'Else
        '                    Dim Box As ValueType ', prop As System.Reflection.PropertyInfo
        '                    Box = Obj
        '                    x.SetValue(Box, propiedad.Valor)
        '                    Return Box
        '                    'End If
        '                ElseIf t.IsArray And propiedad.Tipo.IsArray Then 'Si ambos son array
        '                    Dim array As Array = ConvertirMatriz(t, propiedad.Valor)
        '                    If array.GetType Is t Then _
        '                        x.SetValue(Obj, array, Nothing) Else Stop
        '                ElseIf t.IsArray And Not propiedad.Tipo.IsArray Then 'Si solo el destino es array
        '                    Dim array As Array
        '                    If TypeOf (propiedad.Valor) Is IList Then
        '                        Dim i As IList = propiedad.Valor
        '                        Dim k(i.Count - 1)
        '                        i.CopyTo(k, 0)
        '                        array = ConvertirMatriz(t, k)
        '                    Else
        '                        array = ConvertirMatriz(t, propiedad.Valor)
        '                    End If
        '                    If x.CanWrite Then _
        '                        x.SetValue(Obj, array, Nothing)
        '                Else
        '                    If x.CanWrite Then _
        '                        x.SetValue(Obj, CTypeDynamic(propiedad.Valor, t), Nothing)
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try
        '        End If
        '    End If
        '    Return Obj
        'End Function

        Sub AplicarPropiedades(ByRef Obj As Object, ByVal Propiedades() As Propiedad)

            'OBTENER LOS CAMPOS DEL OBJETO
            Dim y() As System.Reflection.PropertyInfo = Obj.GetType.GetProperties
            Dim x As System.Reflection.PropertyInfo, t As Type

            'Por cada propiedad a establecer
            For Each propiedad As Propiedad In Propiedades
                'Si la propiedad tiene nombre
                If Not String.IsNullOrEmpty(propiedad.Nombre) Then
                    'Recupera la propiedad llamada "nombre"
                    x = Obj.GetType.GetProperty(propiedad.Nombre)
                    ' y su tipo
                    t = x.PropertyType
                    'Si su tipo es una enumeración
                    If t.IsEnum Then
                        'Si el valor existe en la numeración
                        If t.IsEnumDefined(propiedad.Valor) Then
                            'Obten su valor
                            propiedad.Valor = [Enum].Parse(t, CType(propiedad.Valor, String))
                            'Establécelo
                            If x.CanWrite Then x.SetValue(Obj, propiedad.Valor, Nothing)
                        End If
                    Else 'Si no es enumeración
                        Try 'Intenta
                            If propiedad.Valor.GetType Is t Then 'Si son del mismo tipo
                                'Si no es un tipo de valor entonces
                                If Not t.IsValueType Then
                                    'Establécelo sin problemas
                                    If x.CanWrite Then x.SetValue(Obj, propiedad.Valor, Nothing)
                                Else
                                    Dim Box As ValueType ', prop As System.Reflection.PropertyInfo
                                    Box = CType(Obj, ValueType)
                                    x.SetValue(Box, propiedad.Valor)
                                End If
                            ElseIf t.IsArray And propiedad.Tipo.IsArray Then 'Si ambos son array
                                Dim array As Array = ConvertirMatriz(CType(propiedad.Valor, Array), t)
                                If array.GetType Is t Then _
                                x.SetValue(Obj, array, Nothing) Else Stop
                            ElseIf t.IsArray And Not propiedad.Tipo.IsArray Then 'Si solo el destino es array
                                Dim array As Array
                                If TypeOf (propiedad.Valor) Is IList Then
                                    Dim i As IList = CType(propiedad.Valor, IList)
                                    Dim k(i.Count - 1) As Object
                                    i.CopyTo(k, 0)
                                    array = ConvertirMatriz(k, t)
                                Else
                                    array = ConvertirMatriz(CType(propiedad.Valor, Array), t)
                                End If
                                If x.CanWrite Then _
                                x.SetValue(Obj, array, Nothing)
                            Else
                                If x.CanWrite Then _
                                x.SetValue(Obj, CTypeDynamic(propiedad.Valor, t), Nothing)
                            End If
                        Catch ex As Exception

                            Throw
                        End Try
                    End If
                End If
            Next

        End Sub

        Sub AplicarPropiedades(Of Tipo)(ByRef Obj As Tipo, ByVal ParamArray Propiedades() As Propiedad)
            'OBTENER LOS CAMPOS DEL OBJETO
            Dim y() As System.Reflection.PropertyInfo = Obj.GetType.GetProperties
            Dim x As System.Reflection.PropertyInfo, t As Type
            'Por cada propiedad

            For Each propiedad As Propiedad In Propiedades
                If Not propiedad.Nombre Is Nothing Then
                    x = Obj.GetType.GetProperty(propiedad.Nombre)
                    t = Obj.GetType.GetProperty(propiedad.Nombre).PropertyType
                    If x.CanWrite Then x.SetValue(Obj, propiedad.Valor, Nothing)
                End If
            Next
        End Sub
        ''' <summary>
        ''' Devuelve una cadena con los valores legibles del objeto
        ''' </summary>
        ''' <param name="Obj"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function ObjectToString(ByVal Obj As Object) As String
            With Obj
                Return comma(DevolverValores(Propiedades(Obj)).ToString)
            End With
        End Function
        '   COMPARAR PROPIEDADES
        ''' <summary>
        ''' Devuelve las propiedades en las que difieren dos propiedades
        ''' </summary>
        ''' <param name="Propiedades1">Objeto 1, sus propiedades se devolveran</param>
        ''' <param name="Propiedades2">Objeto de referencia, sus propiedades se pierden</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function Comparar_Propiedades(Propiedades1 As Propiedad(), Propiedades2 As Propiedad()) As Propiedad()
            'Dim trk As New Tracker("Comparar Propiedades")
            Dim ret As Object
            Dim output As New List(Of Propiedad)
            'Si no hay primer objeto de referencia no regreses nada
            If Propiedades1 Is Nothing Then GoTo Null_Exit
            If Propiedades2 Is Nothing Then GoTo Alternate_Exit
            For Each Esta_Propiedad As Propiedad In Propiedades1
                'For Each Otra_Propiedad As Propiedad In Propiedades2
                Dim Otra_Propiedad As Propiedad = Propiedades2.PorNombre(Esta_Propiedad.Nombre)
                If Not Esta_Propiedad.Equals(Otra_Propiedad) Then
                    '(" "Not equual Esta_Propiedad " & Esta_Propiedad.Nombre)
                    'trk.ActualizarMétodo("Not equual Esta_Propiedad " & Esta_Propiedad.Nombre)
                    output.Add(Esta_Propiedad)
                End If

                'Next
            Next
            'trk.ActualizarMétodo("Propiedades comparadas exitosamente, regresando " & output.Count)
            ret = output.ToArray
            GoTo [exit]
Null_Exit:
            ret = Nothing
            'trk.ActualizarMétodo("Propiedades1 vacío")
            GoTo [exit]
Alternate_Exit:
            ret = Propiedades1
            ' trk.ActualizarMétodo("Propiedades2 vacío, se regresa prop1")
            GoTo [exit]
[exit]:
            'trk.TerminarMétodo()
            Return CType(ret, Propiedad())
        End Function

        <System.Runtime.CompilerServices.Extension>
        Function PorNombre(Propiedades As Propiedad(), nombre As String) As Propiedad
            For Each Propiedad As Propiedad In Propiedades
                If Propiedad.Nombre = nombre Then Return Propiedad
            Next
            Return Nothing
        End Function

        'AUTOCOMPLETAR
        Function GetAutoComplete(ByVal key As String) As Specialized.StringCollection
            If key Is Nothing Then Return Nothing
            Dim g As New Specialized.StringCollection
            Dim x() As String = Strings.Split(GetSetting(My.Application.Info.ProductName, "AutoComplete", key, ""), ",")
            If x(0) = "" Then Return Nothing
            'For Each s As String In x
            g.AddRange(x)
            'Next
            Return g
        End Function
        'Function GetAutoComplete_Custom(ByVal key As String) As Windows.Forms.AutoCompleteStringCollection
        '    If key Is Nothing Then Return Nothing
        '    Dim g As New AutoCompleteStringCollection
        '    Dim x() As String = Strings.Split(GetSetting(My.Application.Info.ProductName, "AutoComplete", key, ""), ",")
        '    If x(0) = "" Then Return Nothing
        '    'For Each s As String In x
        '    g.AddRange(x)
        '    'Next
        '    Return g
        'End Function
        Sub SetAutoComplete(ByVal key As String, ByVal value As Specialized.StringCollection)
            'If value Is Nothing Then Exit Sub
            If key Is Nothing Then Exit Sub
            Dim x(value.Count - 1) As String, i As Integer
            For Each g As String In value
                x(i) = g
                i = i + 1
            Next
            SaveSetting(My.Application.Info.ProductName, "AutoComplete", key, ArrayToString(x))
        End Sub
        'Sub SetAutoComplete_Custom(ByVal key As String, ByVal value As AutoCompleteStringCollection)
        '    'If value Is Nothing Then Exit Sub
        '    If key Is Nothing Then Exit Sub
        '    Dim list As New List(Of String)
        '    'list.AddRange(value)
        '    For Each s As String In value
        '        list.Add(s)
        '    Next
        '    SaveSetting(My.Application.Info.ProductName, "AutoComplete", key, ArrayToString(list.ToArray))
        'End Sub
        Sub DeleteAutoComplete()
            DeleteSetting(My.Application.Info.ProductName, "AutoComplete")
        End Sub
        Function ArrayToString(ByVal array() As String, Optional ByVal separador As String = ",") As String
            Dim Cadena As String = ""
            System.Array.Reverse(array)
            For Each s As String In array
                Cadena = s & "," & Cadena
            Next
            If Cadena.Length = 0 Then Return ""
            Return Strings.Left(Cadena, Cadena.Length - 1)
        End Function
        Function ArrayToString(Of T)(ByVal array() As T, Optional ByVal separador As String = ",",
                                 Optional ByVal MensajeDeError As String = "", Optional ByVal Viñetas As Boolean = False
                                 ) As String
            If array Is Nothing Then Return MensajeDeError
            Dim Cadena As String = "", i As Integer = array.Count
            'System.Array(array).reverse
            For Each s As T In array
                If Viñetas Then
                    Cadena = i & separador & s.ToString & vbCrLf & Cadena
                Else
                    Cadena = s.ToString & separador & Cadena
                End If
                i -= 1
            Next
            If Cadena.Length = 0 Then Return ""
            Return Strings.Left(Cadena, Cadena.Length - 1)
        End Function
        Sub Add(ByRef array As Object(), elemento As Object)
            If elemento Is Nothing Then Exit Sub
            If array Is Nothing Then
                ReDim array(0)
            Else
                ReDim Preserve array(array.GetUpperBound(0) + 1)
            End If
            array(array.GetUpperBound(0)) = elemento
        End Sub
        'INHERITANCE Y CREACIÓN DE OBJETOS
        Function GetInheritedClasses(type As Type, Modules As [Module]()) As Type()
            Dim Lista As New List(Of Type), Output As New List(Of Type)
            For Each M As [Module] In Modules
                Lista.AddRange(M.GetTypes())
            Next
            For Each t As Type In Lista
                If t.IsSubclassOf(type) Then
                    Output.Add(t)
                End If
            Next
            'Output.Add(type)
            Return Output.ToArray
        End Function
        Function GetInheritedClasses(type As Type) As Type()
            Return GetInheritedClasses(type, Assembly.GetAssembly(type).GetModules)
        End Function
        Event Multiple_Constructors As EventHandler(Of Multiple_constructorsEventArgs)
        Class Multiple_constructorsEventArgs
            Inherits EventArgs
            Property constructores As String()
            Property seleccionado As Integer
        End Class

        'Event Multiple_Constructors(Constructores() As String, ByRef Seleccionado As Integer)
        Function NewInheritedObject(Tipo As Type, Optional askUser As Boolean = True) As Object
            'Dim Trk As New Tracker("NewInheritedObject", comma(Tipo.Name, askUser.ToString))
            Dim workType As Type = Nothing, returning As Object = Nothing

            'Si no hay tipo, no puede haber objeto...
            If Tipo Is Nothing Then GoTo exiting
            'If Not Tipo.IsClass And Not Tipo.IsEnum Then Stop
            'If Tipo.IsGenericType Then Stop
            'Obten todas las clases
            Dim t() As Type = GetInheritedClasses(Tipo)
            Dim classes As New List(Of Type)
            If Not t Is Nothing Then classes.AddRange(t)
            'Crea un listado de clases
            Dim str As New List(Of String)
            'Si tipo no es una clase abstracta (es decir, que se puede crear) entonces añádelo a la lista
            If Not Tipo.IsAbstract Then classes.Add(Tipo)
            'Añade todos los demás a la lista
            For Each [class] As Type In classes
                str.Add([class].Name)
            Next

            If str.Count = 0 Then 'No se puede crear ningún objeto
                'Trk.TerminarMétodo(String.Format(System.Globalization.CultureInfo.CurrentCulture, "No se pueden crear objetos del tipo {0}", Tipo.Name))
                Return Nothing
            End If

            If str.Count = 1 Then 'Solo se puede crear un objeto
                'Define cuál es el único tipo
                workType = classes(0)
                'Encuentra el constructor
                Dim constructor As ConstructorInfo = workType.GetConstructor(New System.Type() {}) '.Invoke(New Object() {})
                'Dim constructor As ConstructorInfo = Tipo.GetConstructor()
                If Not constructor Is Nothing Then 'Si existe un constructor
                    'Return constructor.Invoke(New Object() {})
                    returning = Activator.CreateInstance(workType)
                Else 'Si no existe un constructor
                    returning = Activator.CreateInstance(workType)
                End If
            ElseIf askUser Then  'Se necesita escoger (Solicitar al usuario)
                'Dim f As New SelectItem(str.ToArray)
                'f.ShowDialog()
                'If f.DialogResult = Windows.Forms.DialogResult.OK Then
                '    'Stop
                '    workType = classes(str.IndexOf(f.ListBox1.SelectedItem))
                '    returning = Activator.CreateInstance(workType)
                'End If
            Else
                Dim seleccionado As Integer
                RaiseEvent Multiple_Constructors(Nothing, New Multiple_constructorsEventArgs _
                                             With {.constructores = str.ToArray, .seleccionado = seleccionado})
                workType = classes(seleccionado)
                returning = Activator.CreateInstance(workType)
            End If
exiting:
            Dim retwt As String
            If workType Is Nothing Then retwt = "(Ninguno)" Else retwt = workType.Name
            'Trk.TerminarMétodo(String.Format(System.Globalization.CultureInfo.CurrentCulture, "Objeto creado del tipo {0}", retwt))
            Return returning
        End Function
        'CONVERSIÓN DE MATRICES
        <System.Runtime.CompilerServices.Extension>
        Function ConvertirMatriz(Of T)(matriz As Array) As T()
            'Dim lista As New List(Of T)
            'lista.AddRange(matriz.)
            'Return lista.ToArray
            Return CType(matriz.Cast(Of T)(), T())
        End Function
        <System.Runtime.CompilerServices.Extension>
        Function ConvertirMatriz(matriz As Array, tipo As Type) As Array
            'Haz una lista
            Dim List As Object
            'Agrega el tipo genérico
            Dim topo As Type
            If tipo.IsArray Then
                topo = GetType(List(Of )).MakeGenericType(tipo.GetElementType)
            Else
                topo = GetType(List(Of )).MakeGenericType(tipo)
            End If
            'Crea la lista
            List = NewInheritedObject(topo)
            'Establece una interfaz de lista
            Dim inter As List(Of Object) = CType(List, List(Of Object))
            'agrega los valores
            For Each o As Object In matriz
                inter.Add(o)
            Next
            Return CType(List, List(Of Object)).ToArray
        End Function
        ' Atributos
        <System.Runtime.CompilerServices.Extension>
        Public Function HasAttribute(tipo As Type, atributoPersonalizado As Type) As Boolean
            Dim atributos() As CustomAttributeData = tipo.CustomAttributes.ToArray
            For Each atributo As CustomAttributeData In atributos
                If atributo.AttributeType = atributoPersonalizado Then Return True
            Next
            Return False
        End Function

        <System.Runtime.CompilerServices.Extension>
        Public Function HasAttribute(tipo As MemberInfo, atributoPersonalizado As Type) As Boolean
            Dim atributos() As CustomAttributeData = tipo.CustomAttributes.ToArray
            For Each atributo As CustomAttributeData In atributos
                'If tipo.Name = "MostrarTodo" Then Stop
                If atributo.AttributeType Is atributoPersonalizado Then Return True
            Next
            Return False
        End Function

        Public Function GetAttributes(memberinfo As MemberInfo) As Type()
            Dim l As New List(Of Type)
            For Each atributo As CustomAttributeData In memberinfo.CustomAttributes.ToArray
                l.Add(atributo.AttributeType)
            Next
            Return l.ToArray
        End Function

        'EVENT HANDLERS y similares
        <Extension>
        Public Sub HandleIt(método As MethodInfo, control As Object,
                        Optional eventName As String = "Click",
                        Optional SpecialTarget As Object = Nothing)

            'Comprueba los argumentos
            If método Is Nothing Then Throw New ArgumentNullException("método")
            If control Is Nothing Then Throw New ArgumentNullException("control")
            If String.IsNullOrWhiteSpace(eventName) Then Throw New ArgumentNullException("eventName")

            'Crea el evento
            Dim evento As EventInfo = control.GetType.GetEvent(eventName)

            'Comprueba que el evento existe
            If evento Is Nothing Then Throw New MissingMemberException("No se encuentra el evento: " & eventName)

            Try
                'obten el eventhandler
                Dim eventodelegate As Type = evento.EventHandlerType

                If evento.IsCompatible(método) Then
                    'Crea el delegado
                    Dim d As [Delegate]
                    If SpecialTarget Is Nothing Then
                        d = [Delegate].CreateDelegate(evento.EventHandlerType, método)
                    Else
                        d = [Delegate].CreateDelegate(evento.EventHandlerType, SpecialTarget, método)
                    End If

                    'Crea el método de adición
                    Dim miAddHandler As Reflection.MethodInfo = evento.GetAddMethod()
                    'Crea los argumentos
                    Dim addHandlerArgs() As Object = {d}
                    'controla el evento
                    miAddHandler.Invoke(control, addHandlerArgs)
                Else
                    Dim g As EventDelegateAdapter
                    g = New EventDelegateAdapter(método, control)
                    g.Target = SpecialTarget
                End If
            Catch ex As System.ArgumentException
                Throw New InvalidOperationException("No se ha establecido el método, probablemente requiera un specialTarget", ex)
            End Try
        End Sub

        Public Class EventDelegateAdapter
            Property DelegadoOriginal As [Delegate]
            Property Methodinfo As MethodInfo
            Property Target As Object '= Nothing
            Property Parámeters As Object() '= Nothing

            Public Sub New(methodinfo As MethodInfo, control As Object, Optional eventName As String = "Click")
                Me.Methodinfo = methodinfo
                Me.GetType.GetMethod("LlamarMétodo").HandleIt(control, eventName, Me)
            End Sub

            Public Sub LlamarMétodo(sender As Object, e As EventArgs)
                Methodinfo.Invoke(Target, Parámeters)
            End Sub
        End Class

        <Extension>
        Public Function IsCompatible(evento As EventInfo, método As MethodInfo) As Boolean
            Dim p1 As ParameterInfo() = evento.EventHandlerType.GetMethod("Invoke").GetParameters
            Dim p2 As ParameterInfo() = método.GetParameters
            If Not p1.Count = p2.Count Then Return False
            For i As Integer = 0 To p1.Count - 1
                If Not p1(i).ParameterType = p2(i).ParameterType Then Return False
            Next
            Return True
        End Function

        Public Sub UnHandleIt(método As MethodInfo, control As Object,
                        Optional eventName As String = "Click",
                        Optional SpecialTarget As Object = Nothing)
            Throw New NotImplementedException
        End Sub

        'Puiblicadas
        ''' <summary>
        ''' Establece el -valor- en la -propiedad(Index)- en el -objeto-
        ''' Sets Value in Objeto.[propertyname](Index)
        ''' </summary>
        ''' <param name="objeto">Object where we will set this property</param>
        ''' <param name="Propiedad">Name of the property</param>
        ''' <param name="valor">New Value of the property</param>
        ''' <returns>Object with changed property</returns>
        ''' <remarks>It works on structures!</remarks>
        Function Establecer_propiedad(objeto As Object, Propiedad As String, valor As Object) As Object
            'Check arguments
            If objeto Is Nothing Then Throw New ArgumentNullException("Objeto")
            If String.IsNullOrWhiteSpace(Propiedad) Then Throw New ArgumentNullException("Propiedad")
            'Get the object type
            Dim t As Type = objeto.GetType
            'Get the propertyInfo by its name
            Dim prop As PropertyInfo = t.GetProperty(Propiedad)
            'Check if the property exist
            If prop Is Nothing Then Throw New InvalidOperationException("Property does not exist")
            If Not prop.CanWrite Then Throw New InvalidOperationException("Property is read only")
            'Determine if it is a class or a structure
            If Not t.IsValueType Then ' (it is a reference value)
                'Set without troubles
                If prop.CanWrite Then prop.SetValue(objeto, valor)
                'Return object
                Return objeto
            Else '(It is a structure)
                'Create a box using a valuetype
                'It doesnot work in object
                Dim Box As ValueType
                'Set item in box
                Box = CType(objeto, ValueType)
                'Set value in box
                prop.SetValue(Box, valor)
                'Return box
                Return Box
            End If
        End Function

        '<Extension> _
        'Function AddToolStripMethods(tools As ToolStrip, Obj As Object) As ToolStrip
        '    Dim s As Reflection.MethodInfo() = MétodosDelTipo(Obj.GetType.GetMethods,
        '                                                          GetType(UserHandled))
        '    If s.isNotEmpty Then
        '        For Each MTI As Reflection.MethodInfo In s
        '            Dim button As New ToolStripButton With {.Visible = True, .DisplayStyle = ToolStripItemDisplayStyle.Text}
        '            button.Name = F(MTI.Name)
        '            button.Text = F(MTI.Name)
        '            MTI.HandleIt(button, SpecialTarget:=Obj)
        '            button.Visible = True
        '            tools.Items.Add(button)
        '        Next
        '    End If
        '    Return tools
        'End Function


        'Function RemoveToolStripMethods(tools As ToolStrip, Obj As Object) As ToolStrip
        '    Dim s As Reflection.MethodInfo() = MétodosDelTipo(Obj.GetType.GetMethods,
        '                                                          GetType(UserHandled))
        '    If s.isNotEmpty Then
        '        For Each MTI As Reflection.MethodInfo In s
        '            Dim button As ToolStripButton = tools.Items(F(MTI.Name))
        '            If Not button Is Nothing Then
        '                tools.Items.Remove(button)
        '                button.Dispose()
        '                button = Nothing
        '            End If
        '        Next
        '    End If
        '    Return tools
        'End Function

        '<Extension>
        'Sub AddToolMenuMethods(ToolMenu As Windows.Forms.ToolStripMenuItem, Métodos As Reflection.MethodInfo())
        '    If Métodos Is Nothing Then Exit Sub
        '    'Por cada método
        '    For Each cl As Reflection.MethodInfo In Métodos
        '        ''Crea un toolmenuitem
        '        Dim toolmenuitem As New Windows.Forms.ToolStripMenuItem(F(cl.Name))
        '        'Handle It
        '        cl.HandleIt(toolmenuitem)
        '        'Agrégalo
        '        ToolMenu.DropDownItems.Add(toolmenuitem)
        '    Next
        'End Sub

        '<Extension>
        'Sub RemoveToolMenuMethods(ToolMenu As Windows.Forms.ToolStripMenuItem, Métodos As Reflection.MethodInfo())
        '    For Each cl As Reflection.MethodInfo In Métodos
        '        ''Encuentralo un toolmenuitem
        '        Dim toolmenuitem As Windows.Forms.ToolStripMenuItem = ToolMenu.DropDownItems(F(cl.Name))
        '        'Elimínalo
        '        If Not toolmenuitem Is Nothing Then
        '            ToolMenu.DropDownItems.Remove(toolmenuitem)
        '            toolmenuitem.Dispose()
        '            toolmenuitem = Nothing
        '        End If
        '    Next
        'End Sub

    End Module

End Namespace