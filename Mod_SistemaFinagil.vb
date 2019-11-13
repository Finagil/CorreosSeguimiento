Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Module Mod_SistemaFinagil
    Public Sub CorreosSistemaFinagil(Opcion As String)
        Dim taCorreos As New ProduccionDSTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
        Dim t As New ProduccionDS.GEN_Correos_SistemaFinagilDataTable
        Dim r As ProduccionDS.GEN_Correos_SistemaFinagilRow
        Dim TT As New ProduccionDS.CorreosFasesDataTable
        Dim RR As ProduccionDS.CorreosFasesRow
        Dim Y As Integer
        Dim Para(10) As String
        Dim De As String
        Dim cad() As String
        Dim AUXatt As String = ""
        Dim ASUN As String = ""
        Dim MENSA As String = ""
        Dim MensajAux As String = ""
        Dim Aux() As String
        Dim correo As String = ""
        Dim Correos() As String
        Select Case Opcion.ToUpper
            Case "DG_LIQ"
                taCorreos.FillByDG_LIQ(t)
                EnviacORREO("ecacerest@finagil.com.mx", "Correo Liquidez: " & Date.Now & " - " & t.Rows.Count, "Correo Liquidez", "Correos@finagil.com.mx")
            Case "DG_LIQ_SIN"
                taCorreos.FillByDG_LIQ_sin(t)
            Case "DEYEL"
                taCorreos.FillByDeyel(t)
                For Each r In t.Rows
                    CORREOS_FASE.Fill(TT, "DEYEL_" & r.Para)
                    For Each RR In TT.Rows
                        correo = RR.Correo.Trim
                        If InStr(Correo, "<") Then
                            Aux = correo.Split("<")
                            Aux = Aux(1).Split(">")
                            correo = Aux(0)
                        End If
                        EnviacORREO(correo, r.Mensaje, r.Asunto, r.De)
                    Next
                    CORREOS_FASE.Fill(TT, "DEYEL_SISTEMAS")
                    For Each RR In TT.Rows
                        correo = RR.Correo.Trim
                        If InStr(correo, "<") Then
                            Aux = correo.Split("<")
                            Aux = Aux(1).Split(">")
                            correo = Aux(0)
                        End If
                        EnviacORREO(correo, r.Mensaje, r.Asunto, r.De)
                    Next
                    taCorreos.Enviado(r.id_Correo)
                Next
                Exit Sub
            Case "TODO"
        End Select
        For Each r In t.Rows
            Correos = r.Para.Split(";")
            If Opcion.ToUpper = "DG_LIQ" Then
                cad = r.Para.Split(";")
                Para(Y) = Correos(0)
                Y += 1
                ASUN = "Solicitud de Liquidez Inmediata para Autorización:"
                If MensajAux <> r.Mensaje Then
                    MENSA += r.Mensaje & "<br>"
                End If
                MensajAux = r.Mensaje
                De = r.De
            Else
                For X As Integer = 0 To Correos.Length - 1
                    If Correos(X).Length > 0 Then
                        Dim User As String = "gbello"
                        If InStr(r.Attach, "Autoriza") Then
                            If InStr(r.De.ToLower, User) Then
                                If InStr(r.Attach.ToUpper, ".PDF") Then
                                    If AUXatt <> r.Attach Then
                                        GeneraAutorizacionDG(r.Attach, User)
                                        AUXatt = r.Attach
                                    End If
                                End If
                            End If
                        End If
                        EnviacORREO(Correos(X), r.Mensaje, r.Asunto, r.De, r.Attach)
                    End If
                    If InStr(r.Attach, "Autoriza") Then
                        If InStr(r.Attach, ".Pdf") Then
                            cad = r.Asunto.Split(":")
                            cad(0) = r.Attach.Replace("\LQ\", "")
                            File.Copy(My.Settings.RutaTmp & r.Attach, "\\server-nas\Autorizaciones Credito\Liquidez\" & cad(1).Trim & "-" & cad(0).Trim, True)
                        End If
                    End If
                Next
            End If
            taCorreos.Enviado(r.id_Correo)
        Next
        If Opcion.ToUpper = "DG_LIQ" And ASUN.Length > 3 Then
            For x As Integer = 0 To Y - 1
                EnviacORREO(Para(x), MENSA, ASUN, De, "")
            Next
        End If
    End Sub

    Public Sub CorreosSistemaFinagil_FactSinConta()
        'NOTA SE AGREGAROIN LINEAS MANUEALES EN 
        'Private Sub InitCommandCollection()
        'Me._commandCollection(0).CommandTimeout = 120
        Dim Asunto As String = "FACTURAS SIN MOVIMIENTOS CONTABLES"
        Dim Mensaje As String = "Facturas: <br>"
        Dim ta As New vw_Prod_DSTableAdapters.FactSinContabilidadTableAdapter
        Dim DS As New vw_Prod_DS
        Dim r As vw_Prod_DS.FactSinContabilidadRow
        Try
            ta.Fill(DS.FactSinContabilidad)
            Mensaje += "Serie" & vbTab & "Numero" & vbTab & "Fecha" & vbTab & "Factura" & vbTab & "Anexo" & vbTab & "Letra" & vbTab & "id_historia" & "<br>"
            For Each r In DS.FactSinContabilidad.Rows
                Mensaje += r.Serie & vbTab & r.Numero & vbTab & r.Fecha & vbTab & r.Factura & vbTab & r.Anexo & vbTab & r.Letra & vbTab & r.id_historia & "<br>"
            Next
            If DS.FactSinContabilidad.Rows.Count > 0 Then
                EnviacORREO("ecacerest@finagil.com.mx", Mensaje, Asunto, "ecacerest@finagil.com.mx")
            End If
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, Asunto, "Correos@finagil.com.mx")
        End Try

    End Sub

    Sub GeneraAutorizacionDG(ByVal id_Sol As String, ByVal User As String)
        id_Sol = Mid(id_Sol, 13, id_Sol.Length)
        id_Sol = Mid(id_Sol, 1, InStr(id_Sol, ".") - 1)


        Dim ta1 As New SeguiridadDSTableAdapters.UsuariosFinagilTableAdapter
        Dim DS As New ProduccionDS
        Dim Archivo As String = My.Settings.RutaTmp & "\LQ\Autoriza" & id_Sol & ".Pdf"

        Try
            Dim reporte As New rptAltaLiquidezAutorizacion

            Dim ta As New ProduccionDSTableAdapters.AutorizacionRPTTableAdapter
            ta.Fill(DS.AutorizacionRPT, id_Sol)
            Dim r As ProduccionDS.AutorizacionRPTRow = DS.AutorizacionRPT.Rows(0)
            Dim Antiguedad As Integer = DateDiff(DateInterval.Year, r.FechaIngreso, Date.Now.Date)

            'Console.WriteLine("Datos1")
            reporte.SetDataSource(DS)
            'Console.WriteLine("Datos2")
            reporte.SetParameterValue("var_antiguedad", Antiguedad)
            reporte.SetParameterValue("Autorizo", "C.P. GABRIEL BELLO HERNANDEZ")
            reporte.SetParameterValue("AreaAutorizo", "DIRECCION GENERAL")

            reporte.SetParameterValue("Analista", UCase(Trim(ta1.ScalarNombre(r.UsuarioCredito))))
            reporte.SetParameterValue("FirmaAnalista", Encriptar(r.UsuarioCredito & Date.Now.ToString))
            reporte.SetParameterValue("Firma", Encriptar(User & Date.Now.ToString))
            Dim Aux As String = ta.SacaCorreoPromo(r.Cliente)
            Dim Promo() As String = Aux.Split("@")
            reporte.SetParameterValue("FirmaPromo", Encriptar(Promo(0) & Date.Now.ToString))


            File.Delete(Archivo)
            reporte.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Archivo)
            Console.WriteLine("Exporta")
        Catch ex As Exception
            Console.WriteLine(ex.Message & " Auto")
            EnviacORREO("ecacerest@finagil.com.mx", ex.Message & " - " & Date.Now, "Error de Correos", "Correos@finagil.com.mx")
        End Try

    End Sub

    Public Sub CorreosMasivosSistemaFinagil()
        Dim taCorreos As New ProduccionDSTableAdapters.GEN_CorreoMasivoTableAdapter
        Dim t As New ProduccionDS.GEN_CorreoMasivoDataTable
        Dim r As ProduccionDS.GEN_CorreoMasivoRow
        Dim ASUNTO As String = ""
        Dim MENSAJE As String = ""

        taCorreos.Fill(t)

        For Each r In t.Rows
            ASUNTO = r.Asunto
            ASUNTO = ASUNTO.Replace("|Var1|", r.Var1)
            ASUNTO = ASUNTO.Replace("|Var2|", r.Var2)
            ASUNTO = ASUNTO.Replace("|Var3|", r.Var3)
            ASUNTO = ASUNTO.Replace("|Var4|", r.Var4)
            ASUNTO = ASUNTO.Replace("|Var5|", r.Var5)

            MENSAJE = r.Mensaje
            MENSAJE = MENSAJE.Replace("|Var1|", r.Var1)
            MENSAJE = MENSAJE.Replace("|Var2|", r.Var2)
            MENSAJE = MENSAJE.Replace("|Var3|", r.Var3)
            MENSAJE = MENSAJE.Replace("|Var4|", r.Var4)
            MENSAJE = MENSAJE.Replace("|Var5|", r.Var5)

            EnviacORREO(r.Para, MENSAJE, ASUNTO, r.De, r.Adjunto)
            taCorreos.Procesar(True, r.id_CorreoMasivo)
            Console.WriteLine("Correo: " & r.Para)
        Next

    End Sub

End Module
