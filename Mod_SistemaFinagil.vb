Imports System.IO
Module Mod_SistemaFinagil
    Public Sub CorreosSistemaFinagil()
        Dim taCorreos As New ProduccionDSTableAdapters.CorreosSistemaFinagilTableAdapter
        Dim t As New ProduccionDS.CorreosSistemaFinagilDataTable
        Dim r As ProduccionDS.CorreosSistemaFinagilRow
        Dim cad() As String
        Dim Correos() As String
        taCorreos.Fill(t)
        For Each r In t.Rows
            Correos = r.Para.Split(";")
            For X As Integer = 0 To Correos.Length - 1
                If Correos(X).Length > 0 Then EnviacORREO(Correos(X), r.Mensaje, r.Asunto, r.De, r.Attach)
                If InStr(r.Attach, "Autoriza") Then
                    If InStr(r.Attach, ".Pdf") Then
                        cad = r.Asunto.Split(":")
                        File.Copy("\\server-raid\TmpFinagil\" & r.Attach, "\\server-nas\Autorizaciones Credito\Liquidez\" & cad(1).Trim & "-" & r.Attach, True)
                    End If
                End If
            Next
            taCorreos.Enviado(r.id_Correo)
        Next
    End Sub


End Module
