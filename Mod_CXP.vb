Module Mod_CXP

    Public Sub EnviaAutorizacion(Autoriza As Integer)
        '************Solucitud Avio********************
        Dim solicitud As New ProduccionDSTableAdapters.Vw_CXP_AutorizacionesTableAdapter
        Dim tsoli As New ProduccionDS.Vw_CXP_AutorizacionesDataTable
        Dim Aux(10) As String
        Dim Archivo As String = ""
        Dim Mensaje As String = ""
        Dim MensajeSinLiga As String = ""
        Dim Asunto As String = ""
        Dim AsuntoSinLiga As String = ""
        Dim Correo As String
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable

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

            Asunto = "Se requiere Autorización de pagos: " & r.NombreCorto & " (" & r.Solicitud & ")"
            AsuntoSinLiga = "Liberación solictada a MC: " & r.NombreCorto & " (" & r.Solicitud & ")"
            Mensaje = "Empresa: " & r.NombreCorto & "<br>"
            Mensaje += "Número de Solicitud: " & r.Solicitud & "<br>"
            Mensaje += "Solicitante: " & r.Solicita & "<br>"
            If r.IsDescrNull = False Then
                Mensaje += "Cliente: " & r.Descr & "<br>"
            End If
            Mensaje += "Proveedor: " & r.razonSocial & "<br>"
            Mensaje += "Importe Total: " & CDec(r.Total).ToString("n2") & "<br>"
            MensajeSinLiga = Mensaje
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5Afdb804-7cXp.aspx?User=" & Correo & "&ID1=0&ID2=0&ID3=0'>Liga para Autorización.</A>"

            For Each rr As ProduccionDS.CorreosFasesRow In Tmail.Rows()
                taMail.Insert("Pagos@finagil.com.mx", rr.Correo, Asunto, Mensaje, False, Date.Now, Archivo)
            Next
            If InStr(Correo, "ecacerest") > 0 Then
                taMail.Insert("Pagoss@finagil.com.mx", Correo, Asunto, Mensaje, False, Date.Now, Archivo)
            Else
                taMail.Insert("Pagos@finagil.com.mx", Correo, Asunto, Mensaje, False, Date.Now, Archivo)
            End If

            If Autoriza = 1 Then
                solicitud.Enviado1(Correo, r.idEmpresa, r.Solicitud)
                If r.IsnoContratoNull = False Then
                    If solicitud.SacaTipar(r.noContrato) = "L" Then
                        correos.Fill(Tmail, "TESORERIA_CXP")
                        For Each rr As ProduccionDS.CorreosFasesRow In Tmail.Rows()
                            taMail.Insert("Pagos@finagil.com.mx", rr.Correo, Asunto, MensajeSinLiga, False, Date.Now, Archivo)
                        Next
                    End If
                End If
            ElseIf Autoriza = 2 Then
                solicitud.Enviado2(Correo, r.idEmpresa, r.Solicitud)
            End If
        Next

    End Sub

    Public Sub EnviaAutorizacionPagos(Autoriza As Integer)
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

            Asunto = "Se requiere Autorización de gastos: " & r.Empresa & " (" & r.folioComprobacion & ")"
            Mensaje = "Empresa: " & r.Empresa & "<br>"
            Mensaje += "Número de Comprobación: " & r.folioComprobacion & "<br>"
            Mensaje += "Solicitante: " & r.Solicita & "<br>"
            Mensaje += "Importe Total: " & CDec(r.Total).ToString("n2") & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5Afdb804-8cXp.aspx?User=" & Correo & "&ID1=0&ID2=0'>Liga para Autorización.</A>"

            For Each rr As ProduccionDS.CorreosFasesRow In Tmail.Rows()
                taMail.Insert("Gastos@finagil.com.mx", rr.Correo, Asunto, Mensaje, False, Date.Now, Archivo)
            Next
            If InStr(Correo, "ecacerest") > 0 Then
                taMail.Insert("Gastoss@finagil.com.mx", Correo, Asunto, Mensaje, False, Date.Now, Archivo)
            Else
                taMail.Insert("Gastos@finagil.com.mx", Correo, Asunto, Mensaje, False, Date.Now, Archivo)
            End If

            If Autoriza = 1 Then
                solicitud.Enviado1(Correo, r.folioComprobacion, r.idEmpresa)
            ElseIf Autoriza = 2 Then
                solicitud.Enviado2(Correo, r.folioComprobacion, r.idEmpresa)
            End If
        Next

    End Sub

End Module
