

Module Mod_Main

    Sub Main()
        Console.WriteLine("Inicio")
        Console.WriteLine("Vobo Avio")
        EnviaCorreoAVIO()
        Try
            Console.WriteLine("Cierre Diario")
            Call EnviaCorreoCierreDiario()
            EnviaCorreoNotificaFACTOR(15)
            Console.WriteLine("Factoraje 15")
            Console.WriteLine("Factoraje 30")
            EnviaCorreoNotificaFACTOR(30)
            Console.WriteLine("Bloqueo de Tasas")
            EnviaCorreoTasas()
            Console.WriteLine("Bitacora MC")
            EnviaCorreoBitacoraMC(True)
            EnviaCorreoBitacoraMC(False)
            Console.WriteLine("Sistema Finagil")
            CorreosSistemaFinagil()
            Console.WriteLine("Terminado")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos", "Correos@finagil.com.mx")
        End Try

        'EnviaCorreoCarta()
    End Sub








End Module
