Module Mod_Factoraje
    Dim Mensaje As String = ""
    Sub EnviaCorreoNotificaFACTOR(Dias As Integer)
        Dim TaWEB As New WEB_FinagilDSTableAdapters.CorreosTableAdapter
        Dim TaNotifi As New ProduccionDSTableAdapters.SEG_FactorTableAdapter
        Dim Notifi As New ProduccionDS.SEG_FactorDataTable
        Dim r As ProduccionDS.SEG_FactorRow
        Dim Grupos As New WEB_FinagilDS.CorreosDataTable
        Dim rr As WEB_FinagilDS.CorreosRow
        If Dias = 15 Then
            TaNotifi.Fill15dias(Notifi)
        ElseIf Dias = 30 Then
            TaNotifi.Fill30dias(Notifi)
        End If
        If Notifi.Rows.Count > 0 Then
            TaWEB.Fill(Grupos, "SEG_FACTOR")
        End If
        For Each r In Notifi.Rows
            For Each rr In Grupos.Rows
                Mensaje = "Aviso de Vencimiento de poliza (Factoraje)<br><br>"
                Mensaje += "Cliente: " & r.Nombre & "<br>"
                Mensaje += "Deudor: " & r.Deudor & "<br>"
                Mensaje += "Aseguradora: " & r.Aseguradora & "<br>"
                Mensaje += "Endoso: " & r.Endoso & "<br>"
                Mensaje += "Suma Asegurada: " & r.Suma_Asegurada.ToString("n2") & "<br>"
                Mensaje += "Tipo de Seguro: " & r.Tipo_Seguro & "<br>"
                Mensaje += "Vigencia: " & r.Vigencia.ToShortDateString & "<br>"
                Mensaje += "Dias antes de vencer: " & r.Dias & "<br>"

                EnviacORREO(rr.Correo, Mensaje, "Aviso de Vencimiento de poliza (Factoraje): " & r.Nombre, "Notificaciones@finagil.com.mx")
            Next

            If Dias = 15 Then
                TaNotifi.Update15Dias(True, r.Id_Deudor)
            ElseIf Dias = 30 Then
                TaNotifi.Update30dias(True, r.Id_Deudor)
            End If
        Next

    End Sub

    Sub EnviaCorreoPagosFACTOR()
        '+++++++++++FACTORAJE+++++++++++++++++++++++++++++++
        Dim pag As New Factor100TableAdapters.PagosClientesTableAdapter
        Dim Tpag As New Factor100.PagosClientesDataTable
        pag.Fill(Tpag)
        If Tpag.Rows.Count > 0 Then
            Mensaje = "Pagos Registrados:<br><br><table border=1>"
            Mensaje += "<tr><td>Cliente</td><td>Factura</td><td>Importe</td><td>Fecha</td></tr>"
            For Each r As Factor100.PagosClientesRow In Tpag.Rows
                Mensaje += "<tr><td>" & r.Nombre & "</td><td>" & r.Factura & "</td><td style='text-align: right'>" & r.Importe.ToString("n2") & "</td><td>" & r.Fecha.ToShortDateString & "</td></tr>"
            Next
            Mensaje += "</table>"
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Notificación de Pagos de Clientes a PALM (Factoraje)", "Notificaciones@Finagil.com.mx")
            EnviacORREO("cordone@lamoderna.com.mx", Mensaje, "Notificación de Pagos de Clientes a PALM (Factoraje)", "Notificaciones@Finagil.com.mx")
            EnviacORREO("layala@finagil.com.mx", Mensaje, "Notificación de Pagos de Clientes a PALM (Factoraje)", "Notificaciones@Finagil.com.mx")
            pag.UpdateEnviados()
        End If
    End Sub

    Sub EnviaCorreoInteresBonificacion()
        Dim ta As New WEB_FinagilDSTableAdapters.Vw_WEBPagosFacturasTableAdapter
        Dim t As New WEB_FinagilDS.Vw_WEBPagosFacturasDataTable
        Dim Fecha As Date
        Dim Dias As Integer
        Dim InteBoni, TotalFIN, TotalPALM As Decimal
        Dim Correo, Cliente As String
        ta.Fill(t)
        If t.Rows.Count > 0 Then
            Correo = t.Rows(0).Item("Correo")
            Cliente = t.Rows(0).Item("Nombre")
            Fecha = t.Rows(0).Item("FechaPago")
            Mensaje = "Pagos Registrados:<br><br><table border=1>"
            Mensaje += "<tr><td>Cliente</td><td>Factura</td><td>Importe Anticipo</td><td>Fecha Inicio</td><td>Fecha Vencimiento</td><td>Fecha Pago</td><td>Importe Interés</td><td>Bonificación</td></tr>"
        End If
        For Each r As WEB_FinagilDS.Vw_WEBPagosFacturasRow In t.Rows
            If Correo <> r.Correo Then
                Mensaje += "</table>"
                Mensaje += "<br><b>TOTAL A PAGAR A FINAGIL: " & TotalFIN.ToString("n2") & "</b>"
                Mensaje += "<br><b>TOTAL DE BONIFICACION A PALM: " & TotalPALM.ToString("n2") & "</b>"
                EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Notificación de Interés-Bonificación (Factoraje)", "Notificaciones@Finagil.com.mx", "", True)
                EnviacORREO(r.Correo, Mensaje, "Notificación de Interés-Bonificación (Factoraje)", "Notificaciones@Finagil.com.mx", "", True)
                EnviacORREO("layala@finagil.com.mx", Mensaje, "Notificación de Interés-Bonificación (Factoraje)", "Notificaciones@Finagil.com.mx", "", True)
                Mensaje = "Pagos Registrados:<br><br><table border=1>"
                TotalFIN = 0
                TotalPALM = 0
            End If
            Dias = DateDiff(DateInterval.Day, r.FechaInicio, r.FechaPago)
            Dias -= DateDiff(DateInterval.Day, r.FechaInicio, r.FechaPagoFinagil)
            InteBoni = (r.ImporteAnticipo * r.Tasa) / 36000 * Dias
            ta.UpdateInteresBonificacion(InteBoni, r.Factura)
            ta.UpdateEnviadoInteBonif(True, r.Factura)
            Mensaje += "<tr><td>" & r.Nombre & "</td><td>" & r.Factura & "</td><td style='text-align: right'>" & r.ImporteAnticipo.ToString("n2") & "</td><td>" &
                r.FechaInicio.ToShortDateString & "</td><td>" & r.FechaPagoFinagil.ToShortDateString & "</td><td>" & r.FechaPago.ToShortDateString
            If InteBoni > 0 Then
                Mensaje += "</td><td style='text-align: right'>" & InteBoni.ToString("n2") & "</td><td style='text-align: right'>---</td></tr>"
            Else
                Mensaje += "</td><td style='text-align: right'>---</td><td style='text-align: right'>" & (InteBoni * -1).ToString("n2") & "</td></tr>"
            End If

            If InteBoni > 0 Then
                TotalFIN += InteBoni
            Else
                TotalPALM += InteBoni * -1
            End If

            Correo = r.Correo
            Cliente = r.Nombre
            Fecha = r.FechaPago
        Next
        If t.Rows.Count > 0 Then
            Mensaje += "</table>"
            Mensaje += "<br><b>TOTAL A PAGAR A FINAGIL: " & TotalFIN.ToString("n2") & "</b>"
            Mensaje += "<br><b>TOTAL DE BONIFICACION A PALM: " & TotalPALM.ToString("n2") & "</b>"
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Notificación de Interés-Bonificación (Factoraje)", "Notificaciones@Finagil.com.mx", "", True)
            EnviacORREO(Correo, Mensaje, "Notificación de Interés-Bonificación (Factoraje)", "Notificaciones@Finagil.com.mx", "", True)
            EnviacORREO("layala@finagil.com.mx", Mensaje, "Notificación de Interés-Bonificación (Factoraje)", "Notificaciones@Finagil.com.mx", "", True)
        End If
    End Sub

End Module
