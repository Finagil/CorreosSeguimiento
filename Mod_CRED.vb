﻿Module Mod_CRED

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
            & "<td><strong>Días de Retraso</strong></td><td><strong>Area</strong></td><td><td><strong>Notas</strong></td><td></tr>"

            For Each r As ProduccionDS.CRED_SeguimientosRow In tsol.Rows
                Mensaje += "<tr><td>" & r.Anexo & "</td>"
                Mensaje += "<td>" & r.Cliente & "</td>"
                Mensaje += "<td>" & r.Responsable & "</td>"
                Mensaje += "<td>" & r.Compromiso & "</td>"
                Mensaje += "<td>" & r.DiasRetraso & "</td>"
                If r.Analista = "araceli.sanchez" Then
                    Mensaje += "<td>Seguros</td>"
                Else
                    Mensaje += "<td>Crédito</td>"
                End If
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

End Module
