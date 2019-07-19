Module Mod_CXP

    Private Sub EnviaAitorizacion(Autoriza As Integer)
        '************Solucitud Avio********************
        Dim solicitud As New ProduccionDSTableAdapters.Vw_CXP_AutorizacionesTableAdapter
        Dim tsoli As New ProduccionDS.Vw_CXP_AutorizacionesDataTable
        Dim Aux(10) As String
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable

        If Autoriza = 1 Then
            solicitud.FillAutoriza1(tsoli)
        ElseIf Autoriza = 2 Then
            solicitud.FillAutoriza1(tsoli)
        End If

        For Each r As ProduccionDS.Vw_CXP_AutorizacionesRow In tsoli.Rows


            Mensaje = "Empresa: " & r.Empresa & "<br>"
            Mensaje += "Número de Solicitud: " & r.Solicitud & "<br>"
            Mensaje += "Solicitante: " & r.Solicita & "<br>"
            Mensaje += "Importe Total: " & CDec(r.Total).ToString("n2") & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5Afdb804-7cXp.aspx?User=" & r.Correo & "'>Liga para Autorización.</A>"

            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se Autorización de Gastos o Facturas de " & r.Empresa & " (" & r.Solicitud & ")", "Gastos@finagil.com.mx")
            EnviacORREO(r.Correo, Mensaje, "Se requiere Autorización de Gastos o Facturas de " & r.Empresa & " (" & r.Solicitud & ")", "Gastos@finagil.com.mx")

            If Autoriza = 1 Then
                solicitud.CorreoEnviado1(r.Correo.Substring(1, r.Correo.Length), r.idEmpresa, r.Solicitud)
            ElseIf Autoriza = 2 Then
                solicitud.CorreoEnviado2(r.Correo.Substring(1, r.Correo.Length), r.idEmpresa, r.Solicitud)
            End If
        Next

    End Sub

End Module
