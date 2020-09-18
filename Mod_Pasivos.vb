Module Mod_Pasivos

    Sub EnviaCorreoPAGOS_PASIVO(Fecha As Date)
        Dim Mensaje As String = ""
        Dim Aux As String = ""
        Dim Users(2) As String
        Dim Aux1(10) As String
        Dim Ta_PagosAUT As New WEB_FinagilDSTableAdapters.PagosAutomaticosTableAdapter
        Dim tabla As New WEB_FinagilDS.PagosAutomaticosDataTable
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        Dim Asunto As String
        '************bloqueo de tasas********************
        Ta_PagosAUT.Fill(tabla, Fecha)
        For Each r As WEB_FinagilDS.PagosAutomaticosRow In tabla.Rows
            Mensaje = "Fondeador: " & r.Fondeador & "<br>"
            Mensaje += "Fecha de Pago: " & r.FechaPago.ToShortDateString & "<br>"
            Mensaje += "Tipo Tasa: " & r.TipoTasa & "<br>"
            Mensaje += "Tasa: " & r.TasaDiferencial.ToString("n4") & "<br>"
            Mensaje += "Capital: " & r.Capital.ToString("n2") & "<br>"
            Mensaje += "Interes: " & CDec(r.Interes * -1).ToString("n2") & "<br>"
            Asunto = "Pago de Pasivo Bancario (" & r.Fondeador.Trim & ")"
            correos.Fill(Tmail, "TESORERIA")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                taMail.Insert("Pasivos@finagil.com.mx", rrr.Correo, Asunto, Mensaje, False, Date.Now, "")
            Next
            taMail.Insert("Pasivos@finagil.com.mx", "ecacerest@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
        Next
    End Sub
End Module
