Option Explicit On
Option Infer Off
Option Strict On
Option Compare Text

Namespace Programación

    <Flags>
    Public Enum Referencias As Integer
        Jinich = 1
    End Enum

    ''' <summary>
    ''' Almacena la referencia de una clase, propiedad, enumeración... etc.
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.All, allowmultiple:=True, inherited:=False)>
    Public Class Referencia
        Inherits Attribute

        Public Property Referencia As Referencias
    End Class

    ''' <summary>
    ''' Almacena el nomre del autor o los autores de determinada clase
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Class, allowmultiple:=True, inherited:=False)>
    Public Class Autor
        Inherits Attribute

        <Flags>
        Public Enum Autores As Integer
            Arturo = 1
            Roxana = 2
        End Enum

        Public Property Autor As Autores
    End Class

End Namespace