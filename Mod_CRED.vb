Module Mod_CRED

    Public Sub EnviaCorreoSEGUI_CRED(Opcion As String, Dias As Integer, Dias1 As Integer, Dias2 As Integer)
        '************Solucitud de Documentos MC********************
        Dim solicitudes As New ProduccionDSTableAdapters.CRED_SeguimientosTableAdapter
        Dim tsol As New ProduccionDS.CRED_SeguimientosDataTable
        Dim Resposble As String = ""
        Dim Asunto As String = ""
        Dim Mensaje As String = ""


        Select Case Opcion.ToUpper
            Case "VENCIDO"
                solicitudes.Fill_Vencidos(tsol)
                Asunto = "Notificación de Seguimiento: VENCIDOS"
            Case "DIA"
                solicitudes.Fill_PorVencer(tsol, Dias, Dias1, Dias2)
                If Dias = 0 Then
                    Asunto = "Notificación de Seguimiento: VENCEN HOY "
                Else
                    Asunto = "Notificación de Seguimiento: VENCE EN " & Dias & " DIAS."
                End If
        End Select

        For Each r As ProduccionDS.CRED_SeguimientosRow In tsol.Rows
            Mensaje = "Contrato: " & r.Anexo & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Responsable: " & r.Responsable & "<br>"
            Mensaje += "Compromiso: " & r.Compromiso & "<br>"
            Mensaje += "Días de Retraso: " & r.DiasRetraso & "<br>"
            Mensaje += "Notas: " & r.Notas & "<br>"
            Resposble = CORREOS.ScalarCorreo(r.Responsable)
            EnviacORREO(Resposble, Mensaje, Asunto, "Seguimiento@finagil.com.mx")
            Resposble = CORREOS.ScalarCorreo(r.Analista)
            EnviacORREO(Resposble, Mensaje, Asunto, "Seguimiento@finagil.com.mx")
        Next
        EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Seguimiento@finagil.com.mx")

    End Sub

    Public Sub EnviaCorreoSEGUI_CRED_SUC(Sucursal As String)
        '************Solucitud de Documentos MC********************
        Dim solicitudes As New ProduccionDSTableAdapters.CRED_SeguimientosTableAdapter
        Dim tsol As New ProduccionDS.CRED_SeguimientosDataTable
        Dim Resposble As String = ""
        Dim Asunto As String = ""
        Dim Mensaje As String = ""

        solicitudes.Fill_Sucursal(tsol, Sucursal)
        If tsol.Rows.Count > 0 Then
            Asunto = "Notificación Semanal de Seguimiento."
            Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Responsable</strong></td><td><strong>Compromiso</strong></td>" _
            & "<td><strong>Días de Retraso</strong></td><td><strong>Area</strong></td><td><strong>Notas</strong></td><td></tr>"

            For Each r As ProduccionDS.CRED_SeguimientosRow In tsol.Rows
                Mensaje += "<tr><td>" & r.Anexo & "</td>"
                Mensaje += "<td>" & r.Cliente & "</td>"
                Mensaje += "<td>" & r.Responsable & "</td>"
                Mensaje += "<td>" & r.Compromiso & "</td>"
                Mensaje += "<td>" & r.DiasRetraso & "</td>"
                Mensaje += "<td>" & r.Tipo & "</td>"
                Mensaje += "<td>" & r.Notas & "</td></tr>"
                Resposble = CORREOS.ScalarCorreo(r.Analista)
            Next
            Mensaje += "</table>"
            CORREOS_FASE.Fill(TMAIL, "JEFE_" & Sucursal)
            For Each rrr As ProduccionDS.CorreosFasesRow In TMAIL.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Seguimiento@finagil.com.mx")
            Next
            EnviacORREO(Resposble, Mensaje, Asunto, "Seguimiento@finagil.com.mx")
        End If
        EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Seguimiento@finagil.com.mx")
    End Sub

    Public Sub EnviaCorreoLINEAS_CRED(Tipo As String, MesMas As Integer, AñoMas As Integer, Porducto As String)
        '************Solucitud de Documentos MC********************
        Dim Lienas As New vw_Prod_DSTableAdapters.Vw_CRED_LienasFactorCCTableAdapter
        Dim tlin As New vw_Prod_DS.Vw_CRED_LienasFactorCCDataTable
        Dim Aux() As String
        Dim Cad As String = ""
        Dim Resposble As String = ""
        Dim Asunto As String = ""
        Dim Mensaje As String = ""
        Dim fecha1 As Date = Date.Now.Date
        Dim fecha2 As Date = Date.Now.Date
        Dim CRED As Boolean = True

        Select Case Tipo.ToUpper
            Case "NO_DISPUESTO"
                fecha1 = fecha1.AddMonths(MesMas)
                Lienas.FillByNoDispuesto(tlin, fecha1, Porducto)
            Case "FECHA_REVISION"
                fecha1 = fecha1.AddMonths(MesMas)
                fecha1 = fecha1.AddYears(AñoMas)
                Lienas.FillByFechaFin(tlin, fecha1, Porducto)
            Case "LINEA_VENCIDA"
                Lienas.FillByNoDispuesto(tlin, fecha1, Porducto)
            Case "CONTRATO_VENCIDO"
                CRED = False
                Lienas.FillByFechaFin(tlin, fecha1, Porducto)
            Case "LIMITE_DISPOSICION"
                CRED = False
                Lienas.FillByFechaIni(tlin, fecha1, Porducto)
                If MesMas = 0 Then
                    Cad = " -Si el cliente NO a realizado su primer descuento, ya no podra disponer a partir de esta fecha."
                ElseIf MesMas = 1 Then
                    Cad = " -quedan 1 mes para que el cliente de realice su primer descuento."
                End If
        End Select

        For Each r As vw_Prod_DS.Vw_CRED_LienasFactorCCRow In tlin.Rows

            Select Case Tipo.ToUpper
                Case "NO_DISPUESTO"
                    Asunto = "Linea de " & r.TipoLinea & " no dispuesta."
                Case "FECHA_REVISION"
                    Asunto = "Revisión de Linea de " & r.TipoLinea & " proxima a vencer."
                Case "LINEA_VENCIDA"
                    Asunto = "Linea Vencida de " & r.TipoLinea & "."
                Case "CONTRATO_VENCIDO"
                    Asunto = "Contrato Vencido de " & r.TipoLinea & "."
            End Select

            Mensaje = "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Tipo de Linea: " & r.TipoLinea & "<br>"
            Mensaje += "Importe de la Linea: " & CDec(r.MontoLinea).ToString("n2") & "<br>"
            Mensaje += "Vigencia: " & r.Vigencia.ToShortDateString & "<br>"
            Mensaje += "Fecha Inicio de Contrato: " & r.FechaInicio.ToShortDateString & "<br>"
            Mensaje += "Fecha Fin de Contrato: " & r.FechaFin.ToShortDateString & "<br>"
            Mensaje += "Notas: " & r.Notas & cad & "<br>"

            EnviacORREO(r.Correo, Mensaje, Asunto, "Credito@finagil.com.mx") 'PROMOTOR
            CORREOS_FASE.Fill(TMAIL, "JEFE_" & r.Nombre_Sucursal.Trim)
            For Each rrr As ProduccionDS.CorreosFasesRow In TMAIL.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Credito@finagil.com.mx") 'JEFE
            Next
            CORREOS_FASE.Fill(TMAIL, "SISTEMAS")
            For Each rrr As ProduccionDS.CorreosFasesRow In TMAIL.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Credito@finagil.com.mx") 'JEFE
            Next
            If CRED Then
                CORREOS_FASE.Fill(TMAIL, "CREDITO")
                For Each rrr As ProduccionDS.CorreosFasesRow In TMAIL.Rows
                    EnviacORREO(rrr.Correo, Mensaje, Asunto, "Credito@finagil.com.mx") 'CREDITO
                Next
            End If
        Next


    End Sub

End Module
