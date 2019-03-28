Imports System.IO
Module Mod_SistemaFinagil
    Public Sub CorreosSistemaFinagil(Opcion As String)
        Dim taCorreos As New ProduccionDSTableAdapters.CorreosSistemaFinagilTableAdapter
        Dim t As New ProduccionDS.CorreosSistemaFinagilDataTable
        Dim r As ProduccionDS.CorreosSistemaFinagilRow
        Dim cad() As String
        Dim ASUN As String = ""
        Dim MENSA As String = ""
        Dim Correos() As String
        Select Case Opcion.ToUpper
            Case "DG_LIQ"
                taCorreos.FillByDG_LIQ(t)
                EnviacORREO("ecacerest@finagil.com.mx", "Correo Liquidez: " & Date.Now, "Correo Liquidez", "Correos@finagil.com.mx")
            Case "DG_LIQ_SIN"
                taCorreos.FillByDG_LIQ_sin(t)
            Case "TODO"
        End Select
        For Each r In t.Rows
            Correos = r.Para.Split(";")
            If Opcion.ToUpper = "DG_LIQ" Then
                cad = r.Para.Split(";")
                ASUN = "Solicitud de Liquidez Inmediata para Autorización:"
                MENSA += r.Mensaje & "<br>"
                cad(0) = r.De
            Else
                For X As Integer = 0 To Correos.Length - 1
                    If Correos(X).Length > 0 Then
                        EnviacORREO(Correos(X), r.Mensaje, r.Asunto, r.De, r.Attach)
                    End If
                    If InStr(r.Attach, "Autoriza") Then
                        If InStr(r.Attach, ".Pdf") Then
                            cad = r.Asunto.Split(":")
                            File.Copy("\\server-raid\TmpFinagil\" & r.Attach, "\\server-nas\Autorizaciones Credito\Liquidez\" & cad(1).Trim & "-" & r.Attach, True)
                        End If
                    End If
                Next
            End If
            taCorreos.Enviado(r.id_Correo)
        Next
        If Opcion.ToUpper = "DG_LIQ" And ASUN.Length > 3 Then
            EnviacORREO(Correos(0), MENSA, ASUN, cad(0), "")
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


End Module
