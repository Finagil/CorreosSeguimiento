Module Mod_CXP

    Public Sub EnviaAitorizacion(Autoriza As Integer)
        '************Solucitud Avio********************
        Dim solicitud As New ProduccionDSTableAdapters.Vw_CXP_AutorizacionesTableAdapter
        Dim tsoli As New ProduccionDS.Vw_CXP_AutorizacionesDataTable
        Dim Aux(10) As String
        Dim Anexo As String = ""
        Dim Archivo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim Correo As String
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        Dim taCorreos As New ProduccionDSTableAdapters.CorreosSistemaFinagilTableAdapter

        correos.Fill(Tmail, "SISTEMAS_CXP")
        If Autoriza = 1 Then
            solicitud.FillAutoriza1(tsoli)
        ElseIf Autoriza = 2 Then
            solicitud.FillAutoriza2(tsoli)
        End If

        For Each r As ProduccionDS.Vw_CXP_AutorizacionesRow In tsoli.Rows
            Correo = r.Correo.Substring(1, r.Correo.Length - 1)
            Archivo = "CXP\" & CInt(r.idEmpresa).ToString & "-" & CInt(r.Solicitud).ToString & ".pdf"

            Asunto = "Se requiere Autorización de Gastos o Facturas de " & r.Empresa & " (" & r.Solicitud & ")"
            Mensaje = "Empresa: " & r.Empresa & "<br>"
            Mensaje += "Número de Solicitud: " & r.Solicitud & "<br>"
            Mensaje += "Solicitante: " & r.Solicita & "<br>"
            Mensaje += "Importe Total: " & CDec(r.Total).ToString("n2") & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5Afdb804-7cXp.aspx?User=" & Correo & "&ID1=0&ID2=0'>Liga para Autorización.</A>"

            For Each rr As ProduccionDS.CorreosFasesRow In Tmail.Rows()
                taCorreos.Insert("Gastos@finagil.com.mx", rr.Correo, Asunto, Mensaje, False, Archivo)
            Next
            taCorreos.Insert("Gastos@finagil.com.mx", Correo, Asunto, Mensaje, False, Archivo)

            If Autoriza = 1 Then
                solicitud.Enviado1(Correo, r.idEmpresa, r.Solicitud)
            ElseIf Autoriza = 2 Then
                solicitud.Enviado2(Correo, r.idEmpresa, r.Solicitud)
            End If
        Next

    End Sub

End Module
