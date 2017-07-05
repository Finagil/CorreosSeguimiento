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

End Module
