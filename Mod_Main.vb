

Module Mod_Main
    Sub Main()

        Try
            Console.WriteLine("Inicio")

            Console.WriteLine("Autorizaciones CXP Gastos")
            Mod_CXP.EnviaAitorizacion(1)
            Mod_CXP.EnviaAitorizacion(2)

            Console.WriteLine("Autorizaciones CXP Pagos")
             Mod_CXP.EnviaAitorizacionPagos(1)
            Mod_CXP.EnviaAitorizacionPagos(2)


            Console.WriteLine("Facturas sin Movimientos contables")
            If Date.Now.Minute <= 1 Then 'se ejecutan cada hora
                CorreosSistemaFinagil_FactSinConta()
            End If

            Console.WriteLine("Vobo Avio")
            EnviaCorreoAVIO()
            Console.WriteLine("Seguimiento de Crédito")
            Console.WriteLine(Date.Now.Hour)
            If Date.Now.Hour = 7 And Date.Now.Minute = 10 Then 'se ejecutan una sola ves al dia a las 6 am
                EnviaCorreoAvio_TESO_Aviso()
                EnviaCorreoPAGOS_PASIVO(Date.Now.Date)
                EnviaCorreoSEGUI_CRED("DIA", -5, 0, 14)
                EnviaCorreoSEGUI_CRED("DIA", 0, 0, 14)

                EnviaCorreoSEGUI_CRED("DIA", -15, 15, 29)
                EnviaCorreoSEGUI_CRED("DIA", -10, 15, 29)
                EnviaCorreoSEGUI_CRED("DIA", -5, 15, 29)
                EnviaCorreoSEGUI_CRED("DIA", -0, 15, 29)

                EnviaCorreoSEGUI_CRED("DIA", -15, 30, 9999)
                EnviaCorreoSEGUI_CRED("DIA", -10, 30, 9999)
                EnviaCorreoSEGUI_CRED("DIA", -5, 30, 9999)
                EnviaCorreoSEGUI_CRED("DIA", -0, 30, 9999)

                If Date.Now.DayOfWeek = DayOfWeek.Monday Then
                    EnviaCorreoSEGUI_CRED_SUC("Toluca")
                    EnviaCorreoSEGUI_CRED_SUC("Irapuato")
                    EnviaCorreoSEGUI_CRED_SUC("Navojoa")
                    EnviaCorreoSEGUI_CRED_SUC("Mexicali")
                    EnviaCorreoSEGUI_CRED_SUC("CD.MEXICO")
                    EnviaCorreoSEGUI_CRED_SUC("SAN LUIS")
                End If
                Dim hOY As Date = Date.Now.Date
                EnviaCorreoLINEAS_CRED("NO_DISPUESTO", 1, 0, "FACTORAJE", hOY)
                EnviaCorreoLINEAS_CRED("LINEA_VENCIDA", 0, 0, "FACTORAJE", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 0, "FACTORAJE", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 1, "FACTORAJE", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 2, "FACTORAJE", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 3, "FACTORAJE", hOY)
                EnviaCorreoLINEAS_CRED("CONTRATO_VENCIDO", 0, 0, "FACTORAJE", hOY)

                EnviaCorreoLINEAS_CRED("NO_DISPUESTO", 1, 0, "CC", hOY)
                EnviaCorreoLINEAS_CRED("LINEA_VENCIDA", 0, 0, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 0, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 2, 0, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 3, 0, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 1, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 2, 1, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 3, 1, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 2, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 2, 2, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 3, 2, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 1, 3, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 2, 3, "CC", hOY)
                EnviaCorreoLINEAS_CRED("FECHA_REVISION", 3, 3, "CC", hOY)
                EnviaCorreoLINEAS_CRED("CONTRATO_VENCIDO", 0, 0, "CC", hOY)
                EnviacORREO("ecacerest@finagil.com.mx", Date.Now.ToShortDateString, "EnviaCorreoLINEAS_CRED", "Correos@finagil.com.mx")
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
            EnviaCorreoBitacoraMC_Autorizacion()

            Console.WriteLine("Autoriza IVA")
            EnviaCorreoAutorizaIVA()
            Console.WriteLine("Autoriza IVA Interes")
            EnviaCorreoAutorizaIVA_Interes()

            Console.WriteLine("Correos Masivos")
            CorreosMasivosSistemaFinagil()

            'SIMEPRE AL FINAL+++++++++++++
            Console.WriteLine("Sistema Finagil")
            CorreosSistemaFinagil("DG_LIQ_SIN")

            If Date.Now.Hour = 9 And Date.Now.Minute <= 1 Then
                CorreosSistemaFinagil("DG_LIQ")
            ElseIf Date.Now.Hour = 12 And Date.Now.Minute <= 1 Then
                CorreosSistemaFinagil("DG_LIQ")
            ElseIf Date.Now.Hour = 17 And Date.Now.Minute <= 1 Then
                CorreosSistemaFinagil("DG_LIQ")
            End If

            Console.WriteLine("DEYEL")
            CorreosSistemaFinagil("DEYEL")
            'SIMEPRE AL FINAL+++++++++++++

            Console.WriteLine("Terminado")
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos", "Correos@finagil.com.mx")
        End Try

        'EnviaCorreoCarta()
    End Sub








End Module
