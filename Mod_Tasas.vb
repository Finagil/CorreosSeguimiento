﻿Module Mod_Tasas

    Sub EnviaCorreoTasas()
        Dim Mensaje As String = ""
        Dim Aux As String = ""
        Dim pendientes As New ProduccionDSTableAdapters.GEN_PendientesTableAdapter
        Dim tpen As New ProduccionDS.GEN_PendientesDataTable
        Dim Btasas As New ProduccionDSTableAdapters.VWbloqueoTasasTableAdapter
        Dim bt As New ProduccionDS.VWbloqueoTasasDataTable

        '************bloqueo de tasas********************
        Btasas.FillByReserva(bt)
        For Each r As ProduccionDS.VWbloqueoTasasRow In bt.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            EnviacORREO("ajoshin@finagil.com.mx", Mensaje, "Se requiere Validacion de porcentaje Reserva (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            EnviacORREO("maria.bautista@finagil.com.mx", Mensaje, "Se requiere Validacion de porcentaje Reserva (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere Validacion de porcentaje Reserva (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
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
            EnviacORREO("cmonroy@lamoderna.com.mx", Mensaje, "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Indicadores: " & r.Indicadores & "<br>"
            EnviacORREO("ajoshin@finagil.com.mx", Mensaje, "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            EnviacORREO("maria.bautista@finagil.com.mx", Mensaje, "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/951sb999-7xx8.aspx?ID=" & r.id & "'>Liga de Autorización</A>"
            If r.Nombre_Sucursal.Trim = "NAVOJOA" Or r.Nombre_Sucursal.Trim = "MEXICALI" Then
                EnviacORREO("mleal@finagil.com.mx", Mensaje, "Notificación de Tasa Especial (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            Else
                EnviacORREO("mleal@finagil.com.mx", Mensaje, "Notificación de Tasa Especial (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            End If

            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Notificación de Tasa Especial (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
            Btasas.Enviados(True, r.id)
        Next
        Btasas.FillByGD(bt)
        For Each r As ProduccionDS.VWbloqueoTasasRow In bt.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "<A HREF='https://finagil.com.mx/WEBtasas/552db804-70f8.aspx?ID=" & r.id & "'>Liga de Autorización</A>"
            EnviacORREO("gbello@Finagil.com.mx", Mensaje, "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")", "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>")
            EnviacORREO("ecacerest@Finagil.com.mx", Mensaje, "Se requiere autorización de Tasas (" & r.Cliente.Trim & ")", "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>")
            Btasas.Enviados(True, r.id)
        Next
        Btasas.FillByPromo(bt)
        For Each r As ProduccionDS.VWbloqueoTasasRow In bt.Rows
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Comentario Riesgos: " & r.ComentarioRiesgos & "<br>"
            Select Case r.Autoriza.Trim
                Case "DG"
                    EnviacORREO(r.Correo, Mensaje, "Tasa Autorizada  (" & r.Cliente.Trim & ")", "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>")
                    EnviacORREO("lmercado@finagil.com.mx", Mensaje, "Tasa Autorizada  (" & r.Cliente.Trim & ")", "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>")
                    EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Tasa Autorizada  (" & r.Cliente.Trim & ")", "Gabriel Bello (Finagil) <gbello@lamoderna.com.mx>")
                Case "RI"
                    EnviacORREO(r.Correo, Mensaje, "Tasa Autorizada  (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
                    EnviacORREO("lmercado@finagil.com.mx", Mensaje, "Tasa Autorizada  (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
                    EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Tasa Autorizada  (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
                Case "RECHAZADO"
                    EnviacORREO(r.Correo, Mensaje, "Tasa RECHAZADA  (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
                    EnviacORREO("lmercado@finagil.com.mx", Mensaje, "Tasa RECHAZADA  (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
                    EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Tasa RECHAZADA  (" & r.Cliente.Trim & ")", "Carlos E Monroy (Finagil) <cmonroy@finagil.com.mx>")
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
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Compromiso Rechazado por " & r.UsuarioNOM, correos.ScalarCorreo(r.UsuarioORG))
            EnviacORREO(correos.ScalarCorreo(r.UsuarioFin), Mensaje, "Compromiso Rechazado por " & r.UsuarioNOM, correos.ScalarCorreo(r.UsuarioORG))
            pendientes.UpdateStatus("REX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "CAN")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario: " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Compromiso Cancelado por " & r.UsuarioORG_NOM, correos.ScalarCorreo(r.UsuarioFin))
            EnviacORREO(correos.ScalarCorreo(r.UsuarioORG), Mensaje, "Compromiso Cancelado por " & r.UsuarioORG_NOM, correos.ScalarCorreo(r.UsuarioFin))
            pendientes.UpdateStatus("CAX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "OLD")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario: " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Compromiso Concluido por " & r.UsuarioORG_NOM, correos.ScalarCorreo(r.UsuarioFin))
            EnviacORREO(correos.ScalarCorreo(r.UsuarioORG), Mensaje, "Compromiso Concluido por " & r.UsuarioORG_NOM, correos.ScalarCorreo(r.UsuarioFin))
            pendientes.UpdateStatus("OLX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "TMP")

        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario: " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Compromiso por Aceptar de " & r.UsuarioORG_NOM, correos.ScalarCorreo(r.UsuarioFin))
            EnviacORREO(correos.ScalarCorreo(r.UsuarioORG), Mensaje, "Compromiso por Aceptar de " & r.UsuarioORG_NOM, correos.ScalarCorreo(r.UsuarioFin))
            pendientes.UpdateStatus("TMX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "NEW")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario : " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Compromiso hecho por " & r.UsuarioNOM, correos.ScalarCorreo(r.UsuarioORG))
            EnviacORREO(correos.ScalarCorreo(r.UsuarioFin), Mensaje, "Compromiso hecho por " & r.UsuarioNOM, correos.ScalarCorreo(r.UsuarioORG))
            pendientes.UpdateStatus("NEX", r.id_seguimineto)
        Next
        pendientes.Fill(tpen, "PCC")
        For Each r As ProduccionDS.GEN_PendientesRow In tpen.Rows
            Mensaje = "Usuario : " & r.UsuarioNOM & "<br>"
            Mensaje += "Cliente: " & r.Descr & "<br>"
            Mensaje += "Mensaje: " & r.Asunto & "<br>"
            EnviacORREO("ecacerest@lamoderna.com.mx", Mensaje, "Alta de pagare Cuenta Corriente " & r.UsuarioNOM, correos.ScalarCorreo(r.UsuarioORG))
            EnviacORREO(correos.ScalarCorreo(r.UsuarioFin), Mensaje, "Alta de pagare Cuenta Corriente " & r.UsuarioNOM, correos.ScalarCorreo(r.UsuarioORG))
            Dim correosX As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
            Dim Tmail As New ProduccionDS.CorreosFasesDataTable
            correosX.Fill(Tmail, "MESA_CONTROL")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, "Alta de pagare Cuenta Corriente " & r.UsuarioNOM, correos.ScalarCorreo(r.UsuarioORG))
            Next
            pendientes.UpdateStatus("OLX", r.id_seguimineto)
        Next
    End Sub

    Sub EnviaCorreoHC()
        Dim Mensaje As String = ""
        Dim Aux As String = ""
        Dim Users(2) As String
        Dim Aux1(10) As String
        'Dim HojasCamb As New ProduccionDSTableAdapters.HojasCambiosTableAdapter
        'Dim tpen As New ProduccionDS.HojasCambiosDataTable
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
            EnviacORREO(Users(0) & "@finagil.com.mx", Mensaje, "Se requiere autorización de Hoja de Cambios. (" & r.Descr.Trim & ")", "HojasdeCambio@finagil.com.mx")
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere autorización de Hoja de Cambios (" & r.Descr.Trim & ")", "HojasdeCambio@finagil.com.mx")

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
                EnviacORREO(rrr.Correo, Mensaje, "Se requiere autorización de Hoja de Cambios. (" & r.Descr.Trim & ")", "HojasdeCambio@finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere autorización de Hoja de Cambios (" & r.Descr.Trim & ")", "HojasdeCambio@finagil.com.mx")
            HojasCamb.UpdateHC(Users(1), Users(0), r.id_hojaCambios)
        Next

        HojasCamb.FillByAutorizados(HCt)
        For Each r As ProduccionDS.HojasCambiosRow In HCt.Rows

            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Promotor: " & r.Promotor & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"

            EnviacORREO(r.Correo, Mensaje, "Hoja de Cambios Autorizada. (" & r.Descr.Trim & ")", "HojasdeCambio@finagil.com.mx")
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Hoja de Cambios Autorizada (" & r.Descr.Trim & ")", "HojasdeCambio@finagil.com.mx")

            correos.Fill(Tmail, "MESA_CONTROL")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, "Hoja de Cambios Autorizada. (" & r.Descr.Trim & ")", "HojasdeCambio@finagil.com.mx")
            Next
            HojasCamb.Confirmado(r.id_hojaCambios)

        Next

    End Sub

    Sub EnviaCorreoAutorizaIVA()
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
                EnviacORREO(Users(y) & "@finagil.com.mx", Mensaje, "Se requiere autorización de tasa de IVA. (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere autorización de tasa de IVA (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "IVA solicitado: " & r.IVA & "<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            EnviacORREO(r.Correo, Mensaje, "Se requiere autorización de tasa de IVA. (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
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
                EnviacORREO(Users(y) & "@finagil.com.mx", Mensaje, "Tasa de IVA " & IIf(r.Autorizado = False, "Rechazada", "Autorizada") & ". (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Tasa de IVA " & IIf(r.Autorizado = False, "Rechazada", "Autorizada") & " (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "IVA solicitado: " & r.IVA & "<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            Mensaje += "Estatus: " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & "<br>"
            EnviacORREO(r.Correo, Mensaje, "Tasa de IVA " & IIf(r.Autorizado = False, "Rechazada", "Autorizada") & " . (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
            Dim user As String = r.usuario.Trim
            user = Mid(user, 1, user.Length - 1)
            taIVA.CorreoEnviado(User, r.Anexo, r.Ciclo)
        Next

    End Sub

    Sub EnviaCorreoAutorizaIVA_Interes()
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
                EnviacORREO(Users(y) & "@finagil.com.mx", Mensaje, "Se requiere autorización para NO COBRO de IVA de los intereses. (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere autorización para NO COBRO de IVA de los intereses.(" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Solicitud: NO COBRAR IVA de los intereses.<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            EnviacORREO(r.Correo, Mensaje, "Se requiere autorización para NO COBRO de IVA de los intereses. (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
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
                EnviacORREO(Users(y) & "@finagil.com.mx", Mensaje, "IVA de los Intereses " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & ". (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "IVA de los Intereses " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & " (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")

            'correo al promotor
            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Cliente.Trim & "<br>"
            Mensaje += "Ciudad: " & r.Ciudad & "<br>"
            Mensaje += "Código postal: " & r.CP & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"
            Mensaje += "Solicitud: NO COBRAR IVA de los intereses.<br>"
            Mensaje += "Monto Financiado: " & CDec(r.MontoFinanciado).ToString("n2") & "<br>"
            Mensaje += "Estatus: " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & "<br>"
            EnviacORREO(r.Correo, Mensaje, "IVA de los Intereses " & IIf(r.Autorizado = False, "Rechazado", "Autorizado") & " . (" & r.Cliente.Trim & ")", "CONTABILIDAD@finagil.com.mx")
            Dim user As String = r.usuario.Trim
            user = Mid(user, 1, user.Length - 1)
            taIVA.CorreoEnviado(user, r.Anexo, r.Ciclo)
        Next

    End Sub

End Module
