Imports Diagnósticos.BasesDiagnósticas, Diagnósticos.Evaluación

Module Main

    Sub Main()
        Dim s As String = My.Resources.String1 '"Mujer de 86 años con edema de miembros inferiores"

        'Dim x As New Programación.myHtml.HtmlDocument(s)
        Dim x As New TextoMédico(s)

        Console.WriteLine("Texto evaluado: " & vbCrLf)
        Console.WriteLine(s)
        Console.WriteLine()
        Console.WriteLine()
        Console.WriteLine("Diagnósticos encontrados")
        x.TérminosMédicos.ToList.ForEach(
            Sub(t As Término)

                Console.WriteLine(" * " & t.Nombre)
            End Sub)

        Console.WriteLine()
        Console.WriteLine("Resultado de evaluación")

        Evaluación.EvaluaciónDeDiagnóstico.Evaluar(x).ToList.ForEach(
            Sub(e As Evaluación.Evaluación) Console.WriteLine(e.ToString))

        Console.Read()

        Console.WriteLine(x.Texto)

        Console.Read()
    End Sub

End Module
