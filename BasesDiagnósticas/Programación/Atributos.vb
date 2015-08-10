Option Explicit On
Option Infer Off
Option Strict On
Option Compare Text

Namespace Programación

    ''' <summary>
    ''' Almacena la referencia de una clase, propiedad, enumeración... etc.
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.All, AllowMultiple:=True, Inherited:=False)>
    Public Class Referencia
        Inherits Attribute

        Public Property Referencia As Referencias

        Public ReadOnly Property ReferenciaEnTexto As String
            Get
                Return Nothing
            End Get
        End Property

    End Class

    <Flags>
    Public Enum Referencias As Integer
        Ninguna = 0
        Jinich = 1
    End Enum



    <Flags>
    Public Enum Autores As Byte
        Arturo = 1
        Roxana = 2
    End Enum

    ''' <summary>
    ''' Almacena el nomre del autor o los autores de determinada clase
    ''' </summary>
    ''' <remarks></remarks>
    <AttributeUsage(AttributeTargets.Class, AllowMultiple:=True, Inherited:=False)>
    Public Class Autor
        Inherits Attribute

        Public Property Autor As Autores

    End Class

End Namespace