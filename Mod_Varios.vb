Module Mod_Varios
    Dim Mensaje As String = ""
    Sub EnviaCorreoCarta()
        Dim Ta As New ProduccionDSTableAdapters.Vw_ClientesCartaSATTableAdapter
        Dim T As New ProduccionDS.Vw_ClientesCartaSATDataTable
        Dim r As ProduccionDS.Vw_ClientesCartaSATRow
        Dim ar As New System.IO.StreamReader(My.Computer.FileSystem.CurrentDirectory & "\Carta.txt")
        Dim Linea As String
        Dim x As Integer



        Ta.Fill(T)
        For Each r In T.Rows

            Mensaje = ""
            ar = New System.IO.StreamReader(My.Computer.FileSystem.CurrentDirectory & "\Carta.txt")
            While Not ar.EndOfStream
                Linea = ar.ReadLine
                If Linea = "<p>Apreciable Cliente:</p>" Then
                    Linea = "<p>Apreciable Cliente: " & r.Cliente.Trim & "</p>"
                End If
                Mensaje += Linea
            End While
            x += 1
            'EnviacORREO("atorres@finagil.com.mx", Mensaje, "Confirmación de Métodos de Pago (FINAGIL) ", "Atorres@finagil.com.mx")
            'EnviacORREO("vcruz@finagil.com.mx", Mensaje, "Confirmación de Métodos de Pago (FINAGIL) ", "Atorres@finagil.com.mx")
            'EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Confirmación de Métodos de Pago (FINAGIL) ", "Atorres@finagil.com.mx")
            Console.WriteLine(r.Cliente.Trim)
            If r.EMail1.Trim <> "" And InStr(r.EMail1.Trim, "@") > 0 Then
                EnviacORREO(r.EMail1, Mensaje, "Confirmación de Métodos de Pago (FINAGIL) ", "Abraham Torres (Finagil) <atorres@finagil.com.mx>")
            End If
            If r.EMail2.Trim <> "" And InStr(r.EMail2.Trim, "@") > 0 Then
                EnviacORREO(r.EMail2, Mensaje, "Confirmación de Métodos de Pago (FINAGIL) ", "Abraham Torres (Finagil) <atorres@finagil.com.mx>")
            End If


        Next
    End Sub

    Sub EnviaCorreoCierreDiario()
        Dim TaWEB As New WEB_FinagilDSTableAdapters.CorreosTableAdapter
        Dim TaFechas As New ProduccionDSTableAdapters.FechasAplicacionTableAdapter
        Dim Cierres As New ProduccionDS.FechasAplicacionDataTable
        Dim r As ProduccionDS.FechasAplicacionRow
        Dim Grupos As New WEB_FinagilDS.CorreosDataTable
        Dim rr As WEB_FinagilDS.CorreosRow
        TaFechas.Fill(Cierres, "Cerrado")
        If Cierres.Rows.Count > 0 Then
            TaWEB.Fill(Grupos, "CIERRE_DIARIO")
        End If
        For Each r In Cierres.Rows
            For Each rr In Grupos.Rows
                Mensaje = "El cierre de operaciones diario se ha realizado: " & r.Fecha.ToShortDateString & "<br>"
                Mensaje += "Ya no es posible relizar aplicaciones con esta fecha.<br>"
                EnviacORREO(rr.Correo, Mensaje, "Cierre Diario de Operaciones realizado: " & r.Fecha.ToShortDateString, "Notificaciones@finagil.com.mx")
                Shell("C:\Jobs\TraspasosCartera.exe V", AppWinStyle.Hide, True) ' se activa traspasos de cartera
            Next
            TaFechas.Update(r.Fecha, "Enviado", r.Fecha, r.Estatus)
        Next

    End Sub

End Module
