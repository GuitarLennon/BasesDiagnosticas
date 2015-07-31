Imports Diagnósticos.BasesDiagnósticas

Module Main

    Sub Main()
        Dim s As String = My.Resources.String1 '"Mujer de 86 años con edema de miembros inferiores"

        Console.WriteLine(s)
        Console.WriteLine()

        Evaluación.Evaluar(s).ToList.ForEach(Sub(e As Evaluación) Console.WriteLine(e.ToString))

        Console.Read()
    End Sub

End Module
