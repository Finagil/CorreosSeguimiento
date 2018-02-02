﻿

Module Mod_Main
    Sub Main()

        Try
            Console.WriteLine("Inicio")
            Console.WriteLine("Vobo Avio")
            EnviaCorreoAVIO()
            Console.WriteLine("Seguimiento de Crédito")
            Console.WriteLine(Date.Now.Hour)
            If Date.Now.Hour = 7 And Date.Now.Minute <= 5 Then 'se ejecutan una sola ves al dia a las 6 am
                EnviaCorreoSEGUI_CRED("DIA", -3)
                EnviaCorreoSEGUI_CRED("DIA", 0)
                If Date.Now.DayOfWeek = DayOfWeek.Monday Then
                    EnviaCorreoSEGUI_CRED_SUC("Toluca")
                    EnviaCorreoSEGUI_CRED_SUC("Irapuato")
                    EnviaCorreoSEGUI_CRED_SUC("Navojoa")
                    EnviaCorreoSEGUI_CRED_SUC("Mexicali")
                    EnviaCorreoSEGUI_CRED_SUC("CD.MEXICO")
                End If
            End If
            Console.WriteLine("Cierre Diario")
            Call EnviaCorreoCierreDiario()
            EnviaCorreoNotificaFACTOR(15)
            Console.WriteLine("Factoraje 15")
            Console.WriteLine("Factoraje 30")
            EnviaCorreoNotificaFACTOR(30)
            Console.WriteLine("Bloqueo de Tasas")
            EnviaCorreoTasas()
            Console.WriteLine("Hojas de Cambio")
            EnviaCorreoHC()
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
