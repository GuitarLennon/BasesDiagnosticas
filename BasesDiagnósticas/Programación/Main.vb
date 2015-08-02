Imports Diagnósticos.BasesDiagnósticas, Diagnósticos.Evaluación

Module Main

    Sub Main()
        Dim s As String = My.Resources.String1 '"Mujer de 86 años con edema de miembros inferiores"

        Dim h As New Diagnósticos.Ginecología.Preeclampsia

        Stop

        End
        Console.WriteLine(s)
        Console.WriteLine()

        Evaluación.EvaluaciónDeDiagnóstico.Evaluar(s).ToList. _
            ForEach(Sub(e As Evaluación.EvaluaciónDeDiagnóstico) Console.WriteLine(e.ToString))

        Console.Read()
    End Sub

End Module
