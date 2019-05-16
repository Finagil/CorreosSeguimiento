Imports System.Security.Cryptography
Imports System.Text
Imports System.Net.Mail
Module Mod_Global
    Public CORREOS As New SeguiridadDSTableAdapters.UsuariosFinagilTableAdapter
    Public CORREOS_FASE As New ProduccionDSTableAdapters.CorreosFasesTableAdapter
    Public TMAIL As New ProduccionDS.CorreosFasesDataTable
    Public Sub EnviacORREO(ByVal Para As String, ByVal Mensaje As String, ByVal Asunto As String, de As String, Optional Attach As String = "")

        Dim Mensage As New MailMessage(Trim(de), Trim(Para), Trim(Asunto), Mensaje)
        Dim Cliente As New SmtpClient("192.168.110.1", 25)
        Try
            Cliente.Credentials = New System.Net.NetworkCredential("ecacerest", "c4c3r1t0s", "cmoderna")
            Mensage.IsBodyHtml = True
            If Attach.Trim.Length > 0 Then
                Dim Att As New Attachment(My.Settings.RutaTmp & Attach)
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
        f.WriteLine(Date.Now.ToLocalTime & " - " & Mensaje)
        f.Close()
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



End Module
