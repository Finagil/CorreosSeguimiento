Imports System.Security.Cryptography
Imports System.Text
Imports System.Net.Mail
Module Mod_Global
    Public CORREOS As New SeguiridadDSTableAdapters.UsuariosFinagilTableAdapter
    Public CORREOS_FASE As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
    Public TMAIL As New ProduccionDS.CorreosFasesDataTable
    Dim taMail As New ProduccionDSTableAdapters.GEN_Correos_SistemaFinagilTableAdapter
    Public Sub EnviacORREO(ByVal Para As String, ByVal Mensaje As String, ByVal Asunto As String, de As String, Optional Attach As String = "", Optional RespaldaCorreo As Boolean = False, Optional AsuntoLimitado As Boolean = True)
        de = de.ToLower
        If de.ToLower <> "avisos@finagil.com.mx" Then
            de = de.Replace("@finagil.com.mx", "@cmoderna.com")
            de = de.Replace("@lamoderna.com.mx", "@cmoderna.com")
        End If

        Para = Para.Replace("Ñ", "N")
        Para = Para.Replace("ñ", "n")
        Para = Para.Replace(",", ".")
        Dim Cliente As SmtpClient
        If Mensaje.Length > 2000 And AsuntoLimitado = True Then
            Mensaje = Mid(Mensaje, 1, 2000)
        End If

        Dim Mensage As New MailMessage(Trim(de), Trim(Para), Trim(Asunto), Mensaje)

        If de.ToLower = "avisos@finagil.com.mx" Then
            Mensage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
        End If

        Dim Puerto() As String = My.Settings.SMTP_port.Split(",")
        If RespaldaCorreo = True And AsuntoLimitado = True Then
            taMail.Insert(Trim(de), Trim(Para), Mid(Trim(Asunto), 1, 100), Mensaje, True, Date.Now, "")
        End If

        If InStr(Para, "@lamoderna") > 0 Or InStr(Para, "@cmoderna") > 0 Or InStr(Para, "@finagil") > 0 Or
            InStr(Para, "@pirineos") > 0 Or InStr(Para, "@tamisa") > 0 Or InStr(Para, "@mofesa") > 0 Or
            InStr(Para, "@mosusa") > 0 Or InStr(Para, "@papelesc") > 0 Or InStr(Para, "@peliculasp") > 0 Then
            Cliente = New SmtpClient(My.Settings.SMTP, Puerto(0))
        Else
            Cliente = New SmtpClient(My.Settings.SMTP, Puerto(1))
        End If

        Try
            Dim Credenciales As String() = My.Settings.SMTP_creden.Split(",")
            Cliente.Credentials = New System.Net.NetworkCredential(Credenciales(0), Credenciales(1), Credenciales(2))
            Mensage.IsBodyHtml = True
            If Attach.Trim.Length > 0 Then
                Dim cad As String() = Attach.Trim.Split("|")
                For x As Integer = 0 To cad.Length - 1
                    If cad(x).Trim.Length > 0 Then
                        Dim Att As New Attachment(My.Settings.RutaTmp & cad(x))
                        Mensage.Attachments.Add(Att)
                    End If
                Next
            End If

            Cliente.Send(Mensage)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
        End Try

    End Sub

    Public Sub EscribeLOG(Mensaje)
        'Dim f As New IO.StreamWriter("C:\TMP\Log_Correos.txt", True)
        'f.WriteLine(Date.Now.ToLocalTime & " - " & Mensaje)
        'f.Close()
    End Sub

    Public Function Encriptar(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("Finagil1") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return StrReverse(Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length())))

    End Function

    Public Sub EnviaCorreo365()
        Dim msg = New MailMessage()
        'msg.To.Add(New MailAddress("viapolo@Finagil.com.mx", "Vicente Apolo"))
        msg.To.Add(New MailAddress("delia.jimenez@cmoderna.com", "Delia"))
        msg.From = New MailAddress("ecacerest@cmoderna.com", "Notificaciones")
        msg.Subject = "This is a Test Mail"
        msg.Body = "This is a test message using Exchange OnLine"
        msg.IsBodyHtml = True
        Dim Att As New Attachment(My.Settings.RutaTmp & "\AVISOS\AVISO_421213.PDF")
        msg.Attachments.Add(Att)

        Dim client = New SmtpClient()
        client.UseDefaultCredentials = False
        Dim Credenciales As String() = My.Settings.SMTP_creden.Split(",")
        client.Credentials = New System.Net.NetworkCredential(Credenciales(0) & "@cmoderna.com", Credenciales(1))
        client.Port = 25
        client.Host = "smtp.office365.com"
        client.DeliveryMethod = SmtpDeliveryMethod.Network
        client.EnableSsl = True
        Try

            client.Send(msg)
            Console.WriteLine("Message Sent Succesfully")

        Catch ex As Exception
            Console.WriteLine(ex.ToString())
        End Try
    End Sub

End Module
