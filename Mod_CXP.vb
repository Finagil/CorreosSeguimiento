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
            If InStr(Correo, "<") Then
                Aux = Correo.Split("<")
                Aux = Aux(1).Split(">")
                Correo = Aux(0)
            End If
            Archivo = "CXP\" & CInt(r.idEmpresa).ToString & "-" & CInt(r.Solicitud).ToString & ".pdf"

            Asunto = "Se requiere Autorización de Gastos o Facturas de " & r.NombreCorto & " (" & r.Solicitud & ")"
            Mensaje = "Empresa: " & r.NombreCorto & "<br>"
            Mensaje += "Número de Solicitud: " & r.Solicitud & "<br>"
            Mensaje += "Solicitante: " & r.Solicita & "<br>"
            Mensaje += "Importe Total: " & CDec(r.Total).ToString("n2") & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5Afdb804-7cXp.aspx?User=" & Correo & "&ID1=0&ID2=0&ID3=0'>Liga para Autorización.</A>"

            For Each rr As ProduccionDS.CorreosFasesRow In Tmail.Rows()
                taCorreos.Insert("Gastos@finagil.com.mx", rr.Correo, Asunto, Mensaje, False, Archivo)
            Next
            If InStr(Correo, "ecacerest") > 0 Then
                taCorreos.Insert("Gastoss@finagil.com.mx", Correo, Asunto, Mensaje, False, Archivo)
            Else
                taCorreos.Insert("Gastos@finagil.com.mx", Correo, Asunto, Mensaje, False, Archivo)
            End If

            If Autoriza = 1 Then
                solicitud.Enviado1(Correo, r.idEmpresa, r.Solicitud)
            ElseIf Autoriza = 2 Then
                solicitud.Enviado2(Correo, r.idEmpresa, r.Solicitud)
            End If
        Next

    End Sub

     Public Sub EnviaAitorizacionPagos(Autoriza As Integer)
        '************Solucitud Avio********************
        Dim solicitud As New ProduccionDSTableAdapters.Vw_CXP_ComprobacionGastosTableAdapter
        Dim tsoli As New ProduccionDS.Vw_CXP_ComprobacionGastosDataTable
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

        For Each r As ProduccionDS.Vw_CXP_ComprobacionGastosRow In tsoli.Rows
            Correo = r.Correo.Substring(1, r.Correo.Length - 1)
            If InStr(Correo, "<") Then
                Aux = Correo.Split("<")
                Aux = Aux(1).Split(">")
                Correo = Aux(0)
            End If
            Archivo = "GTS\" & CInt(r.idEmpresa).ToString & "-" & CInt(r.folioComprobacion).ToString & ".pdf"

            Asunto = "Se requiere Autorización de Pagos de " & r.Empresa & " (" & r.folioComprobacion & ")"
            Mensaje = "Empresa: " & r.Empresa & "<br>"
            Mensaje += "Número de Comprobación: " & r.folioComprobacion & "<br>"
            Mensaje += "Solicitante: " & r.Solicita & "<br>"
            Mensaje += "Importe Total: " & CDec(r.Total).ToString("n2") & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5Afdb804-8cXp.aspx?User=" & Correo & "&ID1=0&ID2=0'>Liga para Autorización.</A>"

            For Each rr As ProduccionDS.CorreosFasesRow In Tmail.Rows()
                taCorreos.Insert("Pagos@finagil.com.mx", rr.Correo, Asunto, Mensaje, False, Archivo)
            Next
            If InStr(Correo, "ecacerest") > 0 Then
                taCorreos.Insert("Pagoss@finagil.com.mx", Correo, Asunto, Mensaje, False, Archivo)
            Else
                taCorreos.Insert("Pagos@finagil.com.mx", Correo, Asunto, Mensaje, False, Archivo)
            End If

            If Autoriza = 1 Then
                solicitud.Enviado1(Correo, r.folioComprobacion, r.idEmpresa)
            ElseIf Autoriza = 2 Then
                solicitud.Enviado2(Correo, r.folioComprobacion, r.idEmpresa)
            End If
        Next

    End Sub

End Module
