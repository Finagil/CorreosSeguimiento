

Module Mod_Main
    Sub Main()
        Dim Cadena As String = ""
        Console.WriteLine("Inicio")
        'EnviacORREO("edgar_caceres@hotmail.com", "Aviso", "Aviso", "Avisos@finagil.com.mx")
        Try
            Cadena = "Autorizaciones CXP Gastos"
            Console.WriteLine(Cadena)
            Mod_CXP.EnviaAitorizacion(1)
            Mod_CXP.EnviaAitorizacion(2)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Autorizaciones CXP Pagos"
            Console.WriteLine(Cadena)
            Mod_CXP.EnviaAitorizacionPagos(1)
            Mod_CXP.EnviaAitorizacionPagos(2)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            If Date.Now.Minute <= 1 Then 'se ejecutan cada hora
                Cadena = "Facturas sin Movimientos contables"
                Console.WriteLine(Cadena)
                CorreosSistemaFinagil_FactSinConta()
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Vobo Avio"
            Console.WriteLine(Cadena)
            EnviaCorreoAVIO()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Seguimiento de Crédito"
            Console.WriteLine(Cadena)
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
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Cierre Diario"
            Console.WriteLine(Cadena)
            Call EnviaCorreoCierreDiario()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Factoraje 15"
            Console.WriteLine(Cadena)
            EnviaCorreoNotificaFACTOR(15)
            Cadena = "Factoraje 30"
            Console.WriteLine(Cadena)
            EnviaCorreoNotificaFACTOR(30)
            Cadena = "Factoraje correoPagos"
            Console.WriteLine(Cadena)
            EnviaCorreoPagosFACTOR()
            Cadena = "Factoraje Notifica InteresBonificacion"
            Console.WriteLine(Cadena)
            EnviaCorreoInteresBonificacion()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Bloqueo de Tasas"
            Console.WriteLine(Cadena)
            EnviaCorreoTasas()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Hojas de Cambio"
            Console.WriteLine(Cadena)
            EnviaCorreoHC()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Bitacora MC"
            Console.WriteLine(Cadena)
            EnviaCorreoBitacoraMC(True)
            EnviaCorreoBitacoraMC(False)
            EnviaCorreoBitacoraMC_Autorizacion()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Autoriza IVA"
            Console.WriteLine(Cadena)
            EnviaCorreoAutorizaIVA()
            Cadena = "Autoriza IVA Interes"
            Console.WriteLine(Cadena)
            EnviaCorreoAutorizaIVA_Interes()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "Correos Masivos"
            Console.WriteLine(Cadena)
            CorreosMasivosSistemaFinagil()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            Cadena = "DEYEL"
            Console.WriteLine(Cadena)
            CorreosSistemaFinagil(Cadena)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try

        Try
            'SIMEPRE AL FINAL+++++++++++++
            Cadena = "Sistema Finagil"
            Console.WriteLine(Cadena)
            CorreosSistemaFinagil("DG_LIQ_SIN")
            If Date.Now.Hour = 9 And Date.Now.Minute <= 1 Then
                CorreosSistemaFinagil("DG_LIQ")
            ElseIf Date.Now.Hour = 12 And Date.Now.Minute <= 1 Then
                CorreosSistemaFinagil("DG_LIQ")
            ElseIf Date.Now.Hour = 17 And Date.Now.Minute <= 1 Then
                CorreosSistemaFinagil("DG_LIQ")
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos:" & Cadena, "Correos@finagil.com.mx")
        End Try
        Console.WriteLine("Terminado")
        'EnviaCorreoCarta()
    End Sub








End Module
