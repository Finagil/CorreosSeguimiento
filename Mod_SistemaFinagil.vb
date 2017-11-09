Module Mod_SistemaFinagil
    Public Sub CorreosSistemaFinagil()
        Dim taCorreos As New ProduccionDSTableAdapters.CorreosSistemaFinagilTableAdapter
        Dim t As New ProduccionDS.CorreosSistemaFinagilDataTable
        Dim r As ProduccionDS.CorreosSistemaFinagilRow
        Dim Correos() As String
        taCorreos.Fill(t)
        For Each r In t.Rows
            Correos = r.Para.Split(";")
            For X As Integer = 0 To Correos.Length - 1
                If Correos(X).Length > 0 Then EnviacORREO(Correos(X), r.Mensaje, r.Asunto, r.De, r.Attach)
            Next
            taCorreos.Enviado(r.id_Correo)
        Next
    End Sub


End Module
