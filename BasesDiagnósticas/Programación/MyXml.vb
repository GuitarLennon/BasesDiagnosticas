Option Strict On
Option Explicit On
Option Compare Text
Option Infer Off

Imports System.Xml

Namespace Programación.myXml

    Class myXmlDocument
        Dim innerdocument As XmlDocument

        Sub New(texto As String)
            innerdocument = New XmlDocument()

            Dim rootNode As XmlNode = innerdocument.CreateElement("Historia_clinica")

            innerdocument.AppendChild(rootNode)

            innerdocument.CreateAttribute("Historia_Clinica")

            Dim t As New Texto(texto)

            t.Líneas.ToList.ForEach(
                Sub(x As String)
                    Dim líneaText As XmlNode = innerdocument.CreateElement("línea")

                    rootNode.AppendChild(líneaText)

                    Dim línea As XmlAttribute = innerdocument.CreateAttribute("linea")
                    línea.Value = x
                    líneaText.Attributes.Append(línea)



                End Sub)


            innerdocument.Save("Historia_Clinica.xml")

            My.Computer.FileSystem.OpenTextFileReader("Historia_Clinica.xml")

        End Sub

        ReadOnly Property xmlDocument As XmlDocument
            Get
                Return innerdocument
            End Get
        End Property

    End Class

End Namespace