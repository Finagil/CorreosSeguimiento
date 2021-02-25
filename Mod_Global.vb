Imports System.Security.Cryptography
Imports System.Text
Imports System.Net.Mail
Imports System.ComponentModel
Module Mod_Global
    Public CORREOS As New SeguiridadDSTableAdapters.UsuariosFinagilTableAdapter
    Public CORREOS_FASE As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
    Public TMAIL As New ProduccionDS.CorreosFasesDataTable
    Public taMail As New ProduccionDSTableAdapters.GEN_Correos_SistemaFinagilTableAdapter

    Public Sub EnviacORREO(ByVal Para As String, ByVal Mensaje As String, ByVal Asunto As String, de As String, Optional Attach As String = "", Optional RespaldaCorreo As Boolean = False, Optional AsuntoLimitado As Boolean = True)
        de = de.ToLower
        de = de.Replace("@finagil.com.mx", "@cmoderna.com")
        de = de.Replace("@lamoderna.com.mx", "@cmoderna.com")
        Para = Para.Replace("Ñ", "N")
        Para = Para.Replace("ñ", "n")
        Para = Para.Replace(",", ".")
        If InStr(Para, "@finagil") > 0 Or
            InStr(Para, "@pirineos") > 0 Or InStr(Para, "@tamisa") > 0 Or InStr(Para, "@mofesa") > 0 Or
            InStr(Para, "@mosusa") > 0 Or InStr(Para, "@papelesc") > 0 Or InStr(Para, "@peliculasp") > 0 Then
            Para = Para.Replace("@finagil.com.mx", "@cmoderna.com")
            Para = Para.Replace("@peliculasplasticas.com.mx", "@cmoderna.com")
            Para = Para.Replace("@papelescorrugados.com.mx", "@cmoderna.com")
            Para = Para.Replace("@pirineos.com.mx", "@cmoderna.com")
            Para = Para.Replace("@mofesa.com.mx", "@cmoderna.com")
            Para = Para.Replace("@tamisa.com.mx", "@cmoderna.com")
            Para = Para.Replace("@mosusa.com.mx", "@cmoderna.com")
        End If

        If Mensaje.Length > 4000 And AsuntoLimitado = True Then
            Mensaje = Mid(Mensaje, 1, 4000)
        End If

        If RespaldaCorreo = True And AsuntoLimitado = True Then
            taMail.Insert(Trim(de), Trim(Para), Mid(Trim(Asunto), 1, 100), Mensaje, True, Date.Now, "")
        End If

        Dim Mensage As New MailMessage(Trim(de), Trim(Para), Trim(Asunto), Mensaje)
        If Asunto.Length > 6 Then
            If Asunto.ToUpper.Substring(0, 6) = "AVISO " Then
                Mensage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess
            End If
        End If

        Try
            Mensage.BodyEncoding = System.Text.Encoding.UTF8
            Mensage.SubjectEncoding = System.Text.Encoding.UTF8
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
            Console.WriteLine(Asunto)
            CLIENTE_SMTP.Send(Mensage)
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            EscribeLOG(ex.Message)
        End Try

    End Sub

    Private Sub SendCompletedCallback(sender As Object, e As AsyncCompletedEventArgs)
        Dim msg As MailMessage = e.UserState
        If e.Cancelled Then
            Console.WriteLine(msg)
        ElseIf IsNothing(e.Error) Then
            Console.WriteLine(msg)
        Else
            Console.WriteLine(msg)
        End If
        msg.Dispose()
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

    Public Function ProcesaCMD()
        Dim taCMD As New ProduccionDSTableAdapters.GEN_ComandosCMDTableAdapter
        Dim tCMD As New ProduccionDS.GEN_ComandosCMDDataTable
        Dim r As ProduccionDS.GEN_ComandosCMDRow
        taCMD.Fill(tCMD)
        For Each r In tCMD.Rows
            Try
                Shell(r.Ruta & r.Comando & r.Parametros, AppWinStyle.NormalNoFocus, False)
                Console.WriteLine(r.Ruta & r.Comando & r.Parametros)
            Catch ex As Exception
                taMail.Insert("ecacerest@cmoderna.com", "ecacerest@cmoderna.com", "Error CMD", ex.Message & "-" & r.Ruta & r.Comando & r.Parametros, False, Today, "")
            End Try
            taCMD.ProcesaCMD(r.id_comando)
        Next
        Return 0
    End Function


End Module
