Imports System.IO

Public Class Form1

    Public dun14 As Barcode = Nothing

    Private Sub btnGerar_Click(sender As Object, e As EventArgs) Handles btnGerar.Click
        Try
            If String.IsNullOrEmpty(textBox1.Text) Then
                Throw New Exception("Informe um código de 14 dígitos numéricos!")
            End If

            Dim pathFile As String = Path.Combine(Path.GetTempPath(), "barcode.png")
            dun14 = New Barcode()

            Try
                dun14.ValidarDigitoItf14(textBox1.Text)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

            Dim imagem As Byte() = dun14.GerarImagemCodigoItf14(textBox1.Text)

            If (File.Exists(pathFile)) Then
                File.Delete(pathFile)
            End If

            File.WriteAllBytes(pathFile, imagem)
            pbCodigo.ImageLocation = pathFile

        Catch ex As Exception
            MessageBox.Show("Erro ao gerar código: " + ex.Message)
        End Try
    End Sub

    Private Sub btnLimpar_Click(sender As Object, e As EventArgs) Handles btnLimpar.Click
        textBox1.Text = String.Empty
        pbCodigo.ImageLocation = String.Empty
        textBox1.Focus()
    End Sub
End Class
