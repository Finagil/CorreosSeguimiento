Module Mod_MCBitacora

    Public Sub EnviaCorreoBitacoraMC(BanderaVOBO As Boolean)
        '************Solucitud de Documentos MC********************
        Dim solicitudesMC As New ProduccionDSTableAdapters.MC_BitacoraTableAdapter
        Dim tsol As New ProduccionDS.MC_BitacoraDataTable
        Dim Vobo As String = ""
        Dim Autoriza As String = ""
        Dim Mensaje As String = ""
        Dim MensajeVobo As String = ""
        Dim MensajeAutoriza As String = ""
        Dim MensajeAAutoriza As String = ""
        Dim PLDB As Boolean = True
        Dim PLDX As String = "PLD"

        If BanderaVOBO = True Then
            solicitudesMC.Fill(tsol)
        Else
            solicitudesMC.FillByAuto(tsol)
        End If
        For Each r As ProduccionDS.MC_BitacoraRow In tsol.Rows
            MensajeAutoriza = ""
            MensajeAAutoriza = ""
            MensajeVobo = ""
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Solicita: " & r.Solicito & "<br>"
            Mensaje += "Documentos: <br>"
            If r.Contrato = True Then Mensaje += vbTab & "Contrato<br>"
            If r.Pagare = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Pagare<br>"
            If r.Garantias = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Garantias<br>"
            If r.Facturas = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Facturas<br>"
            If r.Convenio = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Convenio<br>"
            If r.Escritura = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Escritura<br>"
            Mensaje += "Justificación: " & r.Justificacion & "<br>"
            Select Case LCase(Trim(r.Solicito))
                Case "vcruz", "mbautista", "asagar", "gramirez", "kvazquez"
                    If r.AuditoriaExterna = False Then
                        Vobo = "epineda"
                        Autoriza = "epineda"
                    Else
                        Vobo = "epineda"
                        Autoriza = "epineda"
                    End If
                Case "lhernandeX", "gisvazquez", "elizabeth.romero"
                    Vobo = "atorres"
                    Autoriza = "atorres"
                    PLDB = False
                    PLDX = "PLDX"
                Case Else
                    If CORREOS.ScalarDepto(r.Solicito) = "PROMOCION" Then
                        Vobo = "epineda"
                        Autoriza = "epineda"
                    ElseIf CORREOS.ScalarDepto(r.Solicito) = "JURIDICO" Then
                        Vobo = "jjavier"
                        Autoriza = "jjavier"
                    Else
                        Vobo = "epineda"
                        Autoriza = "epineda"
                    End If
            End Select

            'Vobo = "ecacerest"
            'Autoriza = "ecacerest"

            MensajeAAutoriza += "<A HREF='https://finagil.com.mx/WEBtasas/552db804-scod.aspx?ID=" & r.Id_Bitacora & "&User=" & Autoriza & "&Tipo=AA'>Liga de Autorización</A>"
            MensajeAutoriza += "<A HREF='https://finagil.com.mx/WEBtasas/552db804-scod.aspx?ID=" & r.Id_Bitacora & "&User=" & Autoriza & "&Tipo=A'>Liga de Autorización</A>"
            MensajeVobo += "<A HREF='https://finagil.com.mx/WEBtasas/552db804-scod.aspx?ID=" & r.Id_Bitacora & "&User=" & Vobo & "&Tipo=V'>Liga para visto bueno.</A>"
            Dim asunto As String = "Se requiere autorización para Solicitar Documentos(" & r.Descr.Trim & ")"
            If asunto.Length > 100 Then asunto = asunto.Substring(0, 100)
            If Vobo = Autoriza Then
                taMail.Insert("BitacoraMC@lamoderna.com.mx", Autoriza & "@finagil.com.mx", asunto, Mensaje & MensajeAAutoriza, False, Date.Now, "")
                solicitudesMC.UpdateEnviadoVOBO(Vobo, PLDB, PLDX, r.Id_Bitacora, r.Id_Bitacora)
                solicitudesMC.UpdateEnviadoAUTO(Autoriza, PLDB, PLDX, r.Id_Bitacora, r.Id_Bitacora)
            Else
                If BanderaVOBO = True Then
                    asunto = "Se requiere visto bueno para Solicitar Documentos(" & r.Descr.Trim & ")"
                    taMail.Insert("BitacoraMC@lamoderna.com.mx", Autoriza & "@finagil.com.mx", asunto, Mensaje & MensajeVobo, False, Date.Now, "")
                    solicitudesMC.UpdateEnviadoVOBO(Vobo, PLDB, PLDX, r.Id_Bitacora, r.Id_Bitacora)
                Else
                    taMail.Insert("BitacoraMC@lamoderna.com.mx", Autoriza & "@finagil.com.mx", asunto, Mensaje & MensajeAutoriza, False, Date.Now, "")
                    solicitudesMC.UpdateEnviadoAUTO(Autoriza, PLDB, PLDX, r.Id_Bitacora, r.Id_Bitacora)
                End If
            End If
            CORREOS_FASE.Fill(TMAIL, "PLD")
            For Each rrr As ProduccionDS.CorreosFasesRow In TMAIL.Rows
                taMail.Insert("BitacoraMC@lamoderna.com.mx", rrr.Correo, asunto, Mensaje, False, Date.Now, "")
            Next
        Next

    End Sub

    Public Sub EnviaCorreoBitacoraMC_Autorizacion()
        '************Solucitud de Documentos MC********************
        Dim solicitudesMC As New ProduccionDSTableAdapters.MC_BitacoraTableAdapter
        Dim tsol As New ProduccionDS.MC_BitacoraDataTable
        Dim Vobo As String = ""
        Dim Autoriza As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""

        solicitudesMC.FillBySinPLD(tsol)
        For Each r As ProduccionDS.MC_BitacoraRow In tsol.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Solicita: " & r.Solicito & "<br>"
            Mensaje += "Vobo: " & r.vobo & "<br>"
            Mensaje += "Autoriza: " & r.Autoriza & "<br>"
            Mensaje += "Autoriza PLD: " & IIf(r.PldB = True, "SI", "NO") & "<br>"
            Mensaje += "Documentos: <br>"
            If r.Contrato = True Then Mensaje += vbTab & "Contrato<br>"
            If r.Pagare = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Pagare<br>"
            If r.Garantias = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Garantias<br>"
            If r.Facturas = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Facturas<br>"
            If r.Convenio = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Convenio<br>"
            If r.Escritura = True Then Mensaje += "&nbsp&nbsp&nbsp&nbsp Escritura<br>"
            Mensaje += "Justificación: " & r.Justificacion & "<br>"


            Dim Fase As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
            Dim FaseT As New ProduccionDS.CorreosFasesDataTable
            Dim f As ProduccionDS.CorreosFasesRow
            Asunto = "Solicitud de Documentos Autorizada(" & r.Descr.Trim & ")"
            Fase.Fill(FaseT, "MESA_CONTROL")
            For Each f In FaseT.Rows
                taMail.Insert("BitacoraMC@lamoderna.com.mx", f.Correo, Asunto, Mensaje, False, Date.Now, "")
            Next
            Fase.Fill(FaseT, "PLD")
            For Each f In FaseT.Rows
                taMail.Insert("BitacoraMC@lamoderna.com.mx", f.Correo, Asunto, Mensaje, False, Date.Now, "")
            Next
            Fase.Fill(FaseT, "GV_" & r.Nombre_Sucursal.Trim)
            For Each f In FaseT.Rows
                taMail.Insert("BitacoraMC@lamoderna.com.mx", f.Correo, Asunto, Mensaje, False, Date.Now, "")
            Next
            taMail.Insert("BitacoraMC@lamoderna.com.mx", r.Solicito.Trim & "@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            taMail.Insert("BitacoraMC@lamoderna.com.mx", "ecacerest@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            solicitudesMC.UpdateEnviadoAUTO(r.Autoriza, r.PldB, r.Pld.Substring(0, r.Pld.Length - 1), r.Id_Bitacora, r.Id_Bitacora)
        Next

    End Sub
End Module
