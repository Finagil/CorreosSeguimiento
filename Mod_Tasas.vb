Module Mod_Tasas

    Sub EnviaCorreoTasas()
        Dim Mensaje As String = ""
        Dim Asunto As String
        Dim Aux As String = ""
        Dim pendientes As New ProduccionDSTableAdapters.GEN_PendientesTableAdapter
        Dim tpen As New ProduccionDS.GEN_PendientesDataTable
        Dim Btasas As New ProduccionDSTableAdapters.VWbloqueoTasasTableAdapter
        Dim bt As New ProduccionDS.VWbloqueoTasasDataTable
        Dim De As String = "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>"
        '************bloqueo de tasas********************
        Btasas.FillByReserva(bt)
        For Each r As ProduccionDS.VWbloqueoTasasRow In bt.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Asunto = "Se requiere Validacion de porcentaje Reserva (" & r.Cliente.Trim & ")"
            taMail.Insert(De, "ajoshin@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            taMail.Insert(De, "maria.bautista@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            taMail.Insert(De, "ecacerest@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            Btasas.Enviados(True, r.id)
        Next

        Btasas.FillRI(bt)
        For Each r As ProduccionDS.VWbloqueoTasasRow In bt.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Indicadores: " & r.Indicadores & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/2cf0d94b-dcd6.aspx?ID=" & r.id & "'>Liga de Autorización</A>"
            Asunto = "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")"
            taMail.Insert(De, "cmonroy@lamoderna.com.mx", Asunto, Mensaje, False, Date.Now, "")
            taMail.Insert(De, "ecacerest@lamoderna.com.mx", Asunto, Mensaje, False, Date.Now, "")
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Indicadores: " & r.Indicadores & "<br>"
            taMail.Insert(De, "ajoshin@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            taMail.Insert(De, "maria.bautista@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            taMail.Insert(De, "ecacerest@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/951sb999-7xx8.aspx?ID=" & r.id & "'>Liga de Autorización</A>"
            If r.Nombre_Sucursal.Trim = "NAVOJOA" Or r.Nombre_Sucursal.Trim = "MEXICALI" Then
                taMail.Insert(De, "mleal@finagil.com.mx", "Notificación de Tasa Especial (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Else
                taMail.Insert(De, "mleal@finagil.com.mx", "Notificación de Tasa Especial (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            End If

            taMail.Insert(De, "ecacerest@lamoderna.com.mx", "Notificación de Tasa Especial (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Btasas.Enviados(True, r.id)
        Next
        Btasas.FillByGD(bt)
        De = "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>"
        For Each r As ProduccionDS.VWbloqueoTasasRow In bt.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/552db804-70f8.aspx?ID=" & r.id & "'>Liga de Autorización</A>"
            Asunto = "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")"
            taMail.Insert(De, "gbello@Finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            taMail.Insert(De, "ecacerest@Finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
            Btasas.Enviados(True, r.id)
        Next
        Btasas.FillByPromo(bt)
        For Each r As ProduccionDS.VWbloqueoTasasRow In bt.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Comentario Riesgos: " & r.ComentarioRiesgos & "<br>"
            Asunto = "Tasa Autorizada  (" & r.Cliente.Trim & ")"

            Select Case r.Autoriza.Trim
                Case "DG"
                    De = "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>"
                    taMail.Insert(r.Correo, Asunto, Mensaje, "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>", False, Date.Now, "")
                    taMail.Insert("lmercado@finagil.com.mx", Asunto, Mensaje, "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>", False, Date.Now, "")
                    taMail.Insert("ecacerest@finagil.com.mx", Asunto, Mensaje, "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>", False, Date.Now, "")
                Case "RI"
                    De = "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>"
                    taMail.Insert(De, r.Correo, Asunto, Mensaje, False, Date.Now, "")
                    taMail.Insert(De, "lmercado@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
                    taMail.Insert(De, "ecacerest@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
                Case "RECHAZADO"
                    De = "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>"
                    Asunto = "Tasa RECHAZADA  (" & r.Cliente.Trim & ")"
                    taMail.Insert(De, r.Correo, Asunto, Mensaje, False, Date.Now, "")
                    taMail.Insert(De, "lmercado@finagil.com.mx", Asunto, Mensaje, False, Date.Now, "")
                    taMail.Insert(De, "ecacerest@finagil.com.mx", Mensaje, Asunto, False, Date.Now, "")
                    Aux = "R" & r.AnexoCon.Substring(1, 4) & r.AnexoCon.Substring(6, 4)
                    Btasas.RechazarAnexo(Aux, r.id)
            End Select
            Btasas.Enviados(True, r.id)
        Next
        '************bloqueo de tasas********************
        '+++++++++++sEGUIMIENTOS+++++++++++++++++++++++++++++++
        pendientes.Fill(tpen, "REC")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario: " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            De = CORREOS.ScalarCorreo(r.UsuarioORG)
            taMail.Insert(De, "ecacerest@lamoderna.com.mx", "Compromiso Rechazado por " & r.UsuarioNOM, Mensaje, False, Date.Now, "")
            taMail.Insert(De, CORREOS.ScalarCorreo(r.UsuarioFin), "Compromiso Rechazado por " & r.UsuarioNOM, Mensaje, False, Date.Now, "")
            pendientes.UpdateStatus("REX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "CAN")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario: " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            De = CORREOS.ScalarCorreo(r.UsuarioFin)
            taMail.Insert(De, "ecacerest@lamoderna.com.mx", Mensaje, "Compromiso Cancelado por " & r.UsuarioORG_NOM, False, Date.Now, "")
            taMail.Insert(De, CORREOS.ScalarCorreo(r.UsuarioORG), Mensaje, "Compromiso Cancelado por " & r.UsuarioORG_NOM, False, Date.Now, "")
            pendientes.UpdateStatus("CAX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "OLD")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario: " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            De = CORREOS.ScalarCorreo(r.UsuarioFin)
            taMail.Insert(De, "ecacerest@lamoderna.com.mx", Mensaje, "Compromiso Concluido por " & r.UsuarioORG_NOM, False, Date.Now, "")
            taMail.Insert(De, CORREOS.ScalarCorreo(r.UsuarioORG), Mensaje, "Compromiso Concluido por " & r.UsuarioORG_NOM, False, Date.Now, "")
            pendientes.UpdateStatus("OLX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "TMP")

        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario: " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            De = CORREOS.ScalarCorreo(r.UsuarioFin)
            taMail.Insert(De, "ecacerest@lamoderna.com.mx", Mensaje, "Compromiso por Aceptar de " & r.UsuarioORG_NOM, False, Date.Now, "")
            taMail.Insert(De, CORREOS.ScalarCorreo(r.UsuarioORG), Mensaje, "Compromiso por Aceptar de " & r.UsuarioORG_NOM, False, Date.Now, "")
            pendientes.UpdateStatus("TMX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "NEW")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario : " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            De = CORREOS.ScalarCorreo(r.UsuarioORG)
            taMail.Insert(De, "ecacerest@lamoderna.com.mx", Mensaje, "Compromiso hecho por " & r.UsuarioNOM, False, Date.Now, "")
            taMail.Insert(De, CORREOS.ScalarCorreo(r.UsuarioFin), Mensaje, "Compromiso hecho por " & r.UsuarioNOM, False, Date.Now, "")
            pendientes.UpdateStatus("NEX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "PCC")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario : " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            De = CORREOS.ScalarCorreo(r.UsuarioORG)
            taMail.Insert(De, "ecacerest@lamoderna.com.mx", Mensaje, "Alta de pagare Cuenta Corriente " & r.UsuarioNOM, False, Date.Now, "")
            taMail.Insert(De, CORREOS.ScalarCorreo(r.UsuarioFin), Mensaje, "Alta de pagare Cuenta Corriente " & r.UsuarioNOM, False, Date.Now, "")
            Dim correosX As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
            Dim Tmail As New ProduccionDS.CorreosFasesDataTable
            correosX.Fill(Tmail, "MESA_CONTROL")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                taMail.Insert(De, rrr.Correo, Mensaje, "Alta de pagare Cuenta Corriente " & r.UsuarioNOM, False, Date.Now, "")
            Next
            pendientes.UpdateStatus("OLX", r.id_seguimineto)
        Next
    End Sub

    Sub EnviaCorreoHC()
        Dim Mensaje As String = ""
        Dim Aux As String = ""
        Dim Users(2) As String
        Dim Aux1(10) As String
        Dim De As String = "HojasdeCambio@finagil.com.mx"
        Dim HojasCamb As New ProduccionDSTableAdapters.HojasCambiosTableAdapter
        Dim HCt As New ProduccionDS.HojasCambiosDataTable
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        '************bloqueo de tasas********************
        HojasCamb.Fill(HCt)
        For Each r As ProduccionDS.HojasCambiosRow In HCt.Rows
            Dim Sucursal As String = r.FirmaSubPromo.Trim
            Sucursal = Sucursal.Substring(0, Sucursal.Length - 1)

            correos.Fill(Tmail, "DG")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux1 = rrr.Correo.Split("<")
                Aux1 = Aux1(1).Split("@")
                Users(0) = Aux1(0)
            Next
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5159dx1-HCaut.aspx?User=" & Aux1(0) & "&ID=" & r.id_hojaCambios & "'>Liga de Autorización Hoja de Cambios</A>"
            taMail.Insert(De, Users(0) & "@finagil.com.mx", "Se requiere autorización de Hoja de Cambios. (" & r.Descr.Trim & ")", Mensaje, False, Date.Now, "")
            taMail.Insert(De, "ecacerest@finagil.com.mx", "Se requiere autorización de Hoja de Cambios (" & r.Descr.Trim & ")", Mensaje, False, Date.Now, "")

            correos.Fill(Tmail, "SUB_" & Sucursal)
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux1 = rrr.Correo.Split("<")
                Aux1 = Aux1(1).Split("@")
                Users(1) = Aux1(0)
            Next
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5159dx1-HCaut.aspx?User=" & Aux1(0) & "&ID=" & r.id_hojaCambios & "'>Liga de Autorización Hoja de Cambios</A>"

            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                taMail.Insert(De, rrr.Correo, "Se requiere autorización de Hoja de Cambios. (" & r.Descr.Trim & ")", Mensaje, False, Date.Now, "")
            Next
            taMail.Insert(De, "ecacerest@finagil.com.mx", "Se requiere autorización de Hoja de Cambios (" & r.Descr.Trim & ")", Mensaje, False, Date.Now, "")
            HojasCamb.UpdateHC(Users(1), Users(0), r.id_hojaCambios)
        Next

        HojasCamb.FillByAutorizados(HCt)
        For Each r As ProduccionDS.HojasCambiosRow In HCt.Rows

            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"

            taMail.Insert(De, r.Correo, "Hoja de Cambios Autorizada. (" & r.Descr.Trim & ")", Mensaje, False, Date.Now, "")
            taMail.Insert(De, "ecacerest@finagil.com.mx", "Hoja de Cambios Autorizada (" & r.Descr.Trim & ")", Mensaje, False, Date.Now, "")

            correos.Fill(Tmail, "MESA_CONTROL")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                taMail.Insert(De, rrr.Correo, "Hoja de Cambios Autorizada. (" & r.Descr.Trim & ")", Mensaje, False, Date.Now, "")
            Next
            HojasCamb.Confirmado(r.id_hojaCambios)

        Next

    End Sub

    Sub EnviaCorreoAutorizaIVA()
        Dim De As String = "CONTABILIDAD@finagil.com.mx"
        Dim x As Integer
        Dim Mensaje As String = ""
        Dim Aux As String = ""
        Dim Users(2) As String
        Dim Aux1(10) As String
        Dim taIVA As New ProduccionDSTableAdapters.VW_AutorizaIVATableAdapter
        Dim t_IVA As New ProduccionDS.VW_AutorizaIVADataTable
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        '************bloqueo de tasas********************
        taIVA.FillByUsuario(t_IVA, "contabilidadx")
        For Each r As ProduccionDS.VW_AutorizaIVARow In t_IVA.Rows
            correos.Fill(Tmail, "CONTABILIDAD")
            x = 0
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux1 = rrr.Correo.Split("<")
                Aux1 = Aux1(1).Split("@")
                Users(x) = Aux1(0)
                x += 1
            Next

            For y As Integer = 0 To x - 1
                Mensaje = "Contrato: " & r.AnexoCon & "<br>"
                Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
                Mensaje += "Ciudad: " & r.Ciudad & "<br>"
                Mensaje += "Código postal: " & r.CP & "<br>"
                Mensaje += "Producto: " & r.TipoCredito & "<br>"
                Mensaje += "IVA solicitado: " & r.IVA & "<br>"
                Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
                Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5159dx1-IVAaut.aspx?User=" & Users(y) & "&Anexo=X&Ciclo=X'>Liga de Autorización de Tasa de IVA</A>"
                taMail.Insert(De, Users(y) & "@finagil.com.mx", "Se requiere autorización de tasa de IVA. (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Next
            taMail.Insert(De, "ecacerest@finagil.com.mx", "Se requiere autorización de tasa de IVA (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "IVA solicitado: " & r.IVA & "<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            taMail.Insert(De, r.Correo, "Se requiere autorización de tasa de IVA. (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            taIVA.CorreoEnviado("Contabilidad", r.Anexo, r.Ciclo)
        Next

        taIVA.Fill(t_IVA)
        For Each r As ProduccionDS.VW_AutorizaIVARow In t_IVA.Rows
            correos.Fill(Tmail, "CONTABILIDAD")
            x = 0
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux1 = rrr.Correo.Split("<")
                Aux1 = Aux1(1).Split("@")
                Users(x) = Aux1(0)
                x += 1
            Next

            For y As Integer = 0 To x - 1
                Mensaje = "Contrato: " & r.AnexoCon & "<br>"
                Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
                Mensaje += "Ciudad: " & r.Ciudad & "<br>"
                Mensaje += "Código postal: " & r.CP & "<br>"
                Mensaje += "Producto: " & r.TipoCredito & "<br>"
                Mensaje += "IVA solicitado: " & r.IVA & "<br>"
                Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
                Mensaje += "Estatus: " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & "<br>"
                taMail.Insert(De, Users(y) & "@finagil.com.mx", "Tasa de IVA " & IIf(r.Autorizado = False, "Rechazada", "Autorizada") & ". (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Next
            taMail.Insert(De, "ecacerest@finagil.com.mx", "Tasa de IVA " & IIf(r.Autorizado = False, "Rechazada", "Autorizada") & " (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "IVA solicitado: " & r.IVA & "<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            Mensaje += "Estatus: " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & "<br>"
            taMail.Insert(De, r.Correo, "Tasa de IVA " & IIf(r.Autorizado = False, "Rechazada", "Autorizada") & " . (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Dim user As String = r.usuario.Trim
            user = Mid(user, 1, user.Length - 1)
            taIVA.CorreoEnviado(User, r.Anexo, r.Ciclo)
        Next

    End Sub

    Sub EnviaCorreoAutorizaIVA_Interes()
        Dim De As String = "CONTABILIDAD@finagil.com.mx"
        Dim x As Integer
        Dim Mensaje As String = ""
        Dim Aux As String = ""
        Dim Users(2) As String
        Dim Aux1(10) As String
        Dim taIVA As New ProduccionDSTableAdapters.VW_AutorizaIVA_InteresTableAdapter
        Dim t_IVA As New ProduccionDS.VW_AutorizaIVA_InteresDataTable
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        '************bloqueo de tasas********************
        taIVA.FillByUsuario(t_IVA, "contabilidadx")
        For Each r As ProduccionDS.VW_AutorizaIVA_InteresRow In t_IVA.Rows
            correos.Fill(Tmail, "CONTABILIDAD")
            x = 0
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux1 = rrr.Correo.Split("<")
                Aux1 = Aux1(1).Split("@")
                Users(x) = Aux1(0)
                x += 1
            Next

            For y As Integer = 0 To x - 1
                Mensaje = "Contrato: " & r.AnexoCon & "<br>"
                Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
                Mensaje += "Ciudad: " & r.Ciudad & "<br>"
                Mensaje += "Código postal: " & r.CP & "<br>"
                Mensaje += "Producto: " & r.TipoCredito & "<br>"
                Mensaje += "Solicitud: NO COBRAR IVA de los intereses.<br>"
                Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
                Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/5159Inte-IVAaut.aspx?User=" & Users(y) & "&Anexo=X&Ciclo=X'>Liga de Autorización de Tasa de IVA</A>"
                taMail.Insert(De, Users(y) & "@finagil.com.mx", "Se requiere autorización para NO COBRO de IVA de los intereses. (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Next
            taMail.Insert(De, "ecacerest@finagil.com.mx", "Se requiere autorización para NO COBRO de IVA de los intereses.(" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Solicitud: NO COBRAR IVA de los intereses.<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            taMail.Insert(De, r.Correo, "Se requiere autorización para NO COBRO de IVA de los intereses. (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            taIVA.CorreoEnviado("Contabilidad", r.Anexo, r.Ciclo)
        Next

        taIVA.Fill(t_IVA)
        For Each r As ProduccionDS.VW_AutorizaIVA_InteresRow In t_IVA.Rows
            correos.Fill(Tmail, "CONTABILIDAD")
            x = 0
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux1 = rrr.Correo.Split("<")
                Aux1 = Aux1(1).Split("@")
                Users(x) = Aux1(0)
                x += 1
            Next

            For y As Integer = 0 To x - 1
                Mensaje = "Contrato: " & r.AnexoCon & "<br>"
                Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
                Mensaje += "Ciudad: " & r.Ciudad & "<br>"
                Mensaje += "Código postal: " & r.CP & "<br>"
                Mensaje += "Producto: " & r.TipoCredito & "<br>"
                Mensaje += "Solicitud: NO COBRAR IVA de los intereses.<br>"
                Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
                Mensaje += "Estatus: " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & "<br>"
                taMail.Insert(De, Users(y) & "@finagil.com.mx", "IVA de los Intereses " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & ". (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Next
            taMail.Insert(De, "ecacerest@finagil.com.mx", "IVA de los Intereses " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & " (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Solicitud: NO COBRAR IVA de los intereses.<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            Mensaje += "Estatus: " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & "<br>"
            taMail.Insert(De, r.Correo, "IVA de los Intereses " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & " . (" & r.Cliente.Trim & ")", Mensaje, False, Date.Now, "")
            Dim user As String = r.usuario.Trim
            user = Mid(user, 1, user.Length - 1)
            taIVA.CorreoEnviado(user, r.Anexo, r.Ciclo)
        Next

    End Sub

End Module
