﻿Module Mod_Varios
    Dim Mensaje As String = ""
    Sub EnviaCorreoCarta()
        Dim Ta As New ProduccionDSTableAdapters.Vw_ClientesCartaSATTableAdapter
        Dim T As New ProduccionDS.Vw_ClientesCartaSATDataTable
        Dim r As ProduccionDS.Vw_ClientesCartaSATRow
        Dim ar As New System.IO.StreamReader(My.Computer.FileSystem.CurrentDirectory & "\Carta.txt")
        Dim Linea, Asunto As String
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
            Console.WriteLine(r.Cliente.Trim)
            Asunto = "Confirmación de Métodos de Pago (FINAGIL) "
            If r.EMail1.Trim <> "" And InStr(r.EMail1.Trim, "@") > 0 Then
                taMail.Insert("Abraham Torres (Finagil) <atorres@finagil.com.mx>", r.EMail1, Asunto, Mensaje, False, Date.Now, "")
            End If
            If r.EMail2.Trim <> "" And InStr(r.EMail2.Trim, "@") > 0 Then
                taMail.Insert("Abraham Torres (Finagil) <atorres@finagil.com.mx>", r.EMail2, Asunto, Mensaje, False, Date.Now, "")
            End If


        Next
    End Sub

    Sub EnviaCorreoCierreDiario()
        Dim Asunto As String
        Dim TaFechas As New ProduccionDSTableAdapters.FechasAplicacionTableAdapter
        Dim Cierres As New ProduccionDS.FechasAplicacionDataTable
        Dim r As ProduccionDS.FechasAplicacionRow
        Dim Grupos As New ProduccionDS.CorreosFasesDataTable
        Dim rr As ProduccionDS.CorreosFasesRow
        TaFechas.Fill(Cierres, "Cerrado")
        If Cierres.Rows.Count > 0 Then
            CORREOS_FASE.Fill(Grupos, "CIERRE_DIARIO")
        End If
        For Each r In Cierres.Rows
            For Each rr In Grupos.Rows
                Mensaje = "El cierre de operaciones diario se ha realizado: " & r.Fecha.ToShortDateString & "<br>"
                Mensaje += "Ya no es posible relizar aplicaciones con esta fecha.<br>"
                asunto = "Cierre Diario de Operaciones realizado: " & r.Fecha.ToShortDateString
                taMail.Insert("Notificaciones@finagil.com.mx", rr.Correo, Asunto, Mensaje, False, Date.Now, "")
            Next
            Shell("C:\Jobs\TraspasosCartera.exe", AppWinStyle.Hide, True) ' se activa traspasos de cartera
            Shell("C:\Jobs\TraspasosCartera.exe V", AppWinStyle.Hide, True) ' se activa traspasos de cartera
            TaFechas.Update(r.Fecha, "Enviado", r.Fecha, r.Estatus)
        Next

    End Sub

End Module
