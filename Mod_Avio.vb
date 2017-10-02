Module Mod_Avio

    Public Sub EnviaCorreoAVIO()
        Try
            Console.WriteLine("vobo")
            EnviaCorreoAvio_VOBO()
            EnviaCorreoAvio_VOBO2()
            Console.WriteLine("Sub")
            EnviaCorreoAvio_SUB()
            Console.WriteLine("DG")
            EnviaCorreoAvio_DG()
            Console.WriteLine("MC")
            EnviaCorreoAvio_MC()
            Console.WriteLine("Sucursales")
            EnviaCorreoAvio_SUC("Irapuato")
            EnviaCorreoAvio_SUC("Navojoa")
            EnviaCorreoAvio_SUC("Mexicali")
            Console.WriteLine("Fira")
            EnviaCorreoAvio_FIRA()
            Console.WriteLine("Tesoreria")
            EnviaCorreoAvio_TESO()
            Console.WriteLine("Sucursales")
            EnviaCorreoAvio_PAG("Irapuato")
            EnviaCorreoAvio_PAG("Navojoa")
            EnviaCorreoAvio_PAG("Mexicali")
            EnviaCorreo_PAG_CC()
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
        End Try
    End Sub

    Private Sub EnviaCorreoAvio_VOBO()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim DetAvio As New ProduccionDSTableAdapters.AviosVoboTableAdapter
        Dim tAnex As New ProduccionDS.AviosVoboDataTable
        Dim Aux(10) As String
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        'solicitudAVIO.PasaCC()
        solicitudAVIO.FillVobo(tsol)

        For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows

            correos.Fill(Tmail, "JEFE_" & r.Nombre_Sucursal.Trim)
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux = rrr.Correo.Split("<")
                Aux = Aux(1).Split("@")
            Next

            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"

            DetAvio.FillByAnexo(tAnex, r.Anexo)
            For Each rr As ProduccionDS.AviosVoboRow In tAnex.Rows
                Mensaje += rr.Documento.Trim & ": " & rr.Importe.ToString("n2") & "<br>"
            Next
            Mensaje += "<br>Importe Total: " & r.Importe.ToString("n2") & "<br>"
            Mensaje += "<A HREF='http://finagil.com.mx/WEBtasas/232db951-oiva.aspx?User=" & Aux(0) & "&Anexo=0&ID=0'>Liga para visto bueno " & r.TipoCredito & " .</A>"

            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, "Se requiere visto bueno para Solicitar Ministración (" & r.Descr.Trim & ") " & r.TipoCredito, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere visto bueno para Solicitar Ministración (" & r.Descr.Trim & ")" & r.TipoCredito, "Avio@Finagil.com.mx")
            solicitudAVIO.VoboMail(Aux(0), r.Anexo)
        Next

    End Sub

    Private Sub EnviaCorreoAvio_VOBO2()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim DetAvio As New ProduccionDSTableAdapters.AviosVoboTableAdapter
        Dim tAnex As New ProduccionDS.AviosVoboDataTable
        Dim Aux(10) As String
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillVobo2(tsol)

        For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows

            correos.Fill(Tmail, "ESTRATEGIAS")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux = rrr.Correo.Split("<")
                Aux = Aux(1).Split("@")
            Next

            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"

            DetAvio.FillByAnexo(tAnex, r.Anexo)
            For Each rr As ProduccionDS.AviosVoboRow In tAnex.Rows
                Mensaje += rr.Documento.Trim & ": " & rr.Importe.ToString("n2") & "<br>"
            Next
            Mensaje += "<br>Importe Total: " & r.Importe.ToString("n2") & "<br>"
            Mensaje += "<A HREF='http://finagil.com.mx/WEBtasas/232db951-oiva.aspx?User=" & Aux(0) & "&Anexo=0&ID=0'>Liga para visto bueno " & r.TipoCredito & " .</A>"

            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, "Se requiere visto bueno para Solicitar Ministración (" & r.Descr.Trim & ") " & r.TipoCredito, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere visto bueno para Solicitar Ministración (" & r.Descr.Trim & ") " & r.TipoCredito, "Avio@Finagil.com.mx")
            solicitudAVIO.VoboMail(Aux(0), r.Anexo)
        Next

    End Sub

    Private Sub EnviaCorreoAvio_MC()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillMC(tsol)
        If tsol.Rows.Count > 0 Then
            Asunto = "Se requiere revisión para Ministración (" & tsol.Rows.Count & " solicitudes)"
            Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Importe</strong></td><td><strong>Producto</strong></td></tr>"
            For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
                Mensaje += "<tr><td>" & r.AnexoCon & "</td>"
                Mensaje += "<td>" & r.Descr.Trim & "</td>"
                Mensaje += "<td ALIGN=RIGHT>" & r.Importe.ToString("n2") & "</td>"
                Mensaje += "<td>" & r.TipoCredito & "</td></tr>"
            Next
            Mensaje += "</table>"

            correos.Fill(Tmail, "MESA_CONTROL")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Avio@Finagil.com.mx")
            solicitudAVIO.MC_mail()
        End If

    End Sub

    Private Sub EnviaCorreoAvio_SUC(Sucursal As String)
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillBySUC(tsol, Sucursal)
        If tsol.Rows.Count > 0 Then
            Asunto = "Ministraciones liberadas por MC (" & tsol.Rows.Count & " solicitudes) - " & Sucursal.ToUpper
            Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Importe</strong></td><td><strong>Producto</strong></td></tr>"
            For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
                Mensaje += "<tr><td>" & r.AnexoCon & "</td>"
                Mensaje += "<td>" & r.Descr.Trim & "</td>"
                Mensaje += "<td ALIGN=RIGHT>" & r.Importe.ToString("n2") & "</td>"
                Mensaje += "<td>" & r.TipoCredito & "</td></tr>"
                solicitudAVIO.SUC_mail(r.Anexo)
            Next
            Mensaje += "</table>"

            correos.Fill(Tmail, Sucursal)
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            correos.Fill(Tmail, "MESA_CONTROL")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            correos.Fill(Tmail, "CREDITO")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Avio@Finagil.com.mx")
        End If

    End Sub

    Private Sub EnviaCorreoAvio_SUB()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim DetAvio As New ProduccionDSTableAdapters.AviosVoboTableAdapter
        Dim tAnex As New ProduccionDS.AviosVoboDataTable
        Dim Aux(10) As String
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillSUB(tsol)

        For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
            correos.Fill(Tmail, "SUB_" & r.Nombre_Sucursal.Trim)
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux = rrr.Correo.Split("<")
                Aux = Aux(1).Split("@")
            Next

            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"

            DetAvio.FillByAnexo(tAnex, r.Anexo)
            For Each rr As ProduccionDS.AviosVoboRow In tAnex.Rows
                Mensaje += rr.Documento.Trim & ": " & rr.Importe.ToString("n2") & "<br>"
            Next
            Mensaje += "<br>Importe Total: " & r.Importe.ToString("n2") & "<br>"
            Mensaje += "<A HREF='http://finagil.com.mx/WEBtasas/232db951-Suba.aspx?User=" & Aux(0) & "&Anexo=0&ID=0'>Liga para visto bueno AVIO .</A>"

            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, "Se requiere visto bueno para Solicitar Ministración (" & r.Descr.Trim & ")", "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere visto bueno para Anticipo (" & r.Descr.Trim & ")", "Avio@Finagil.com.mx")
            solicitudAVIO.SUB_mail(Aux(0), r.Anexo)
        Next

    End Sub

    Private Sub EnviaCorreoAvio_DG()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim DetAvio As New ProduccionDSTableAdapters.AviosVoboTableAdapter
        Dim tAnex As New ProduccionDS.AviosVoboDataTable
        Dim Aux(10) As String
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillByDG(tsol)

        For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
            correos.Fill(Tmail, "DG")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                Aux = rrr.Correo.Split("<")
                Aux = Aux(1).Split("@")
            Next

            Mensaje = "Contrato: " & r.AnexoCon & "<br>"
            Mensaje += "Cliente: " & r.Descr.Trim & "<br>"
            Mensaje += "Producto: " & r.TipoCredito & "<br>"

            DetAvio.FillByAnexo(tAnex, r.Anexo)
            For Each rr As ProduccionDS.AviosVoboRow In tAnex.Rows
                Mensaje += rr.Documento.Trim & ": " & rr.Importe.ToString("n2") & "<br>"
            Next
            Mensaje += "<br>Importe Total: " & r.Importe.ToString("n2") & "<br>"
            Mensaje += "<A HREF='http://finagil.com.mx/WEBtasas/232db951-DGxa.aspx?User=" & Aux(0) & "&Anexo=0&ID=0'>Liga para autorización de Anticipo.</A>"

            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, "Se requiere visto bueno para Solicitar Ministración (" & r.Descr.Trim & ")", "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, "Se requiere Autorización para Anticipo (" & r.Descr.Trim & ")", "Avio@Finagil.com.mx")
            solicitudAVIO.DG_mail(Aux(0), r.Anexo)
        Next

    End Sub

    Private Sub EnviaCorreoAvio_FIRA()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.QuitaGastos_CRE()
        solicitudAVIO.FillByFira(tsol)
        If tsol.Rows.Count > 0 Then
            Asunto = "Se requiere descontar Ministración (" & tsol.Rows.Count & " solicitudes)"
            Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Importe</strong></td><td><strong>Producto</strong></td></tr>"
            For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
                Mensaje += "<tr><td>" & r.AnexoCon & "</td>"
                Mensaje += "<td>" & r.Descr.Trim & "</td>"
                Mensaje += "<td ALIGN=RIGHT>" & r.Importe.ToString("n2") & "</td>"
                Mensaje += "<td>" & r.TipoCredito & "</td></tr>"
            Next
            Mensaje += "</table>"

            correos.Fill(Tmail, "FIRA")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Avio@Finagil.com.mx")
            solicitudAVIO.Fira_mail()
        End If

    End Sub

    Private Sub EnviaCorreoAvio_TESO()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.TESO_mail_NoEfectivo()
        solicitudAVIO.FillByTESO(tsol)
        If tsol.Rows.Count > 0 Then
            Asunto = "Se requiere Dispersión de Ministraciones (" & tsol.Rows.Count & " solicitudes)"
            Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Importe</strong></td><td><strong>Producto</strong></td><td><strong>Sucursal</strong></td><td><strong>Ciclo Pagaré</strong></td></tr>"
            For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
                Mensaje += "<tr><td>" & r.AnexoCon & "</td>"
                Mensaje += "<td>" & r.Descr.Trim & "</td>"
                Mensaje += "<td ALIGN=RIGHT>" & r.Importe.ToString("n2") & "</td>"
                Mensaje += "<td>" & r.TipoCredito & "</td>"
                Mensaje += "<td>" & r.Nombre_Sucursal & "</td></tr>"
                Mensaje += "<td>" & r.CicloPagare & "</td></tr>"
            Next
            Mensaje += "</table>"

            correos.Fill(Tmail, "TESORERIA")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Avio@Finagil.com.mx")
            solicitudAVIO.TESO_mail()
        End If

    End Sub

    Private Sub EnviaCorreoAvio_PAG(Sucursal As String)
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillByPAG(tsol, Sucursal)
        If tsol.Rows.Count > 0 Then
            If Sucursal <> "Irapuato" Then
                EnviaCorreoAvio_PAG_FIRA(Sucursal)
            End If
            Asunto = "Ministraciones liberadas por Tesoreria (" & tsol.Rows.Count & " solicitudes) - " & Sucursal.ToUpper
            Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Importe</strong></td><td><strong>Producto</strong></td></tr>"
            For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
                Mensaje += "<tr><td>" & r.AnexoCon & "</td>"
                Mensaje += "<td>" & r.Descr.Trim & "</td>"
                Mensaje += "<td ALIGN=RIGHT>" & r.Importe.ToString("n2") & "</td>"
                Mensaje += "<td>" & r.TipoCredito & "</td></tr>"
                solicitudAVIO.SUC_mail(r.Anexo)
            Next
            Mensaje += "</table>"

            correos.Fill(Tmail, Sucursal)
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            correos.Fill(Tmail, "MESA_CONTROL")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Avio@Finagil.com.mx")
            solicitudAVIO.PAG_mail(Sucursal)
        End If

    End Sub

    Private Sub EnviaCorreoAvio_PAG_FIRA(Sucursal As String)
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillByPAG_FIRA(tsol, Sucursal)
        If tsol.Rows.Count > 0 And tsol.Rows(0).Item("FondeoTit") = "Fira" Then
            Asunto = "Ministraciones liberadas por Tesoreria (Fira) (" & tsol.Rows.Count & " solicitudes) - " & Sucursal.ToUpper
            Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Importe</strong></td><td><strong>Producto</strong></td></tr>"
            For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
                Mensaje += "<tr><td>" & r.AnexoCon & "</td>"
                Mensaje += "<td>" & r.Descr.Trim & "</td>"
                Mensaje += "<td ALIGN=RIGHT>" & r.Importe.ToString("n2") & "</td>"
                Mensaje += "<td>" & r.TipoCredito & "</td></tr>"
            Next
            Mensaje += "</table>"

            correos.Fill(Tmail, "FIRA")
            For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                EnviacORREO(rrr.Correo, Mensaje, Asunto, "Avio@Finagil.com.mx")
            Next
            EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "Avio@Finagil.com.mx")
            solicitudAVIO.PAG_mail(Sucursal)
        End If

    End Sub

    Private Sub EnviaCorreo_PAG_CC()
        '************Solucitud Avio********************
        Dim solicitudAVIO As New ProduccionDSTableAdapters.AviosVoboRESTableAdapter
        Dim tsol As New ProduccionDS.AviosVoboRESDataTable
        Dim Anexo As String = ""
        Dim Mensaje As String = ""
        Dim Asunto As String = ""
        Dim correos As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
        Dim Tmail As New ProduccionDS.CorreosFasesDataTable
        solicitudAVIO.FillByCC(tsol)
        If tsol.Rows.Count > 0 Then

            For Each r As ProduccionDS.AviosVoboRESRow In tsol.Rows
                EnviaCorreoAvio_PAG_FIRA(r.Nombre_Sucursal.Trim)
                Asunto = "Ministraciones liberadas por Tesoreria (" & tsol.Rows.Count & " solicitudes)  "
                Mensaje = "<table BORDER=1><tr><td><strong>Contrato</strong></td><td><strong>Cliente</strong></td><td><strong>Importe</strong></td><td><strong>Producto</strong></td><td><strong>Ciclo Pagaré</strong></td></tr>"
                Mensaje += "<tr><td>" & r.AnexoCon & "</td>"
                Mensaje += "<td>" & r.Descr.Trim & "</td>"
                Mensaje += "<td ALIGN=RIGHT>" & r.Importe.ToString("n2") & "</td>"
                Mensaje += "<td>" & r.TipoCredito & "</td></tr>"
                Mensaje += "<td>" & r.CicloPagare & "</td></tr>"
                Mensaje += "</table>"
                EnviacORREO(solicitudAVIO.CorreoPromo(r.Anexo), Mensaje, Asunto, "CuentaCorriente@Finagil.com.mx")
                EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "CuentaCorriente@Finagil.com.mx")
                correos.Fill(Tmail, "MESA_CONTROL")
                For Each rrr As ProduccionDS.CorreosFasesRow In Tmail.Rows
                    EnviacORREO(rrr.Correo, Mensaje, Asunto, "CuentaCorriente@Finagil.com.mx")
                Next
            Next
            solicitudAVIO.PAG_CC()
        End If
    End Sub


End Module
