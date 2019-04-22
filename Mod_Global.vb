Imports System.Net.Mail
Module Mod_Global
    Public CORREOS As New SeguiridadDSTableAdapters.UsuariosFinagilTableAdapter
    Public CORREOS_FASE As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
    Public TMAIL As New ProduccionDS.CorreosFasesDataTable
    Public Sub EnviacORREO(ByVal Para As String, ByVal Mensaje As String, ByVal Asunto As String, de As String, Optional Attach As String = "")

        Dim Mensage As New MailMessage(Trim(de), Trim(Para), Trim(Asunto), Mensaje)
        Dim Cliente As New SmtpClient("192.168.110.1", 25)
        Try
            Mensage.IsBodyHtml = True
            If Attach.Trim.Length > 0 Then
                Dim Att As New Attachment("\\server-raid\TmpFinagil\" & Attach)
                Mensage.Attachments.Add(Att)
            End If
            Cliente.Send(Mensage)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
        End Try

    End Sub

    Public Sub EscribeLOG(Mensaje)
        Dim f As New IO.StreamWriter("C:\TMP\Log_Correos.txt", True)
        f.WriteLine(Mensaje)
        f.Close()
    End Sub



End Module
