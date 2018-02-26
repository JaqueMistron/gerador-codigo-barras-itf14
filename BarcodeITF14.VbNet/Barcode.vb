Imports System.IO

Public Class Barcode

    Public Sub New()
    End Sub

    Public Function GerarBinarioCodigoItf14(ByVal codigo As String) As List(Of String)

        Dim index As Integer

        Dim strCode As String = ""

        Dim strLeft, strRight As String
        codigo = Trim(codigo)

        '' array de pares
        Dim pares As List(Of String) = New List(Of String)
        Dim paresBinarios As List(Of String) = New List(Of String)

        For index = 0 To codigo.Length - 1
            pares.Add(codigo.Substring(index, 2)) ''IIf(index + 2 > codigoEan.Length - 1, codigoEan.Length, index + 2)))
            index += 1
        Next

        For Each par As String In pares
            Select Case CInt(par.Substring(0, 1))
                Case 0
                    strLeft = "00110"
                Case 1
                    strLeft = "10001"
                Case 2
                    strLeft = "01001"
                Case 3
                    strLeft = "11000"
                Case 4
                    strLeft = "00101"
                Case 5
                    strLeft = "10100"
                Case 6
                    strLeft = "01100"
                Case 7
                    strLeft = "00011"
                Case 8
                    strLeft = "10010"
                Case 9
                    strLeft = "01010"
            End Select

            Select Case CInt(par.Substring(1, 1))
                Case 0
                    strRight = "00110"
                Case 1
                    strRight = "10001"
                Case 2
                    strRight = "01001"
                Case 3
                    strRight = "11000"
                Case 4
                    strRight = "00101"
                Case 5
                    strRight = "10100"
                Case 6
                    strRight = "01100"
                Case 7
                    strRight = "00011"
                Case 8
                    strRight = "10010"
                Case 9
                    strRight = "01010"
            End Select

            paresBinarios.Add(strLeft & strRight)
        Next

        Return paresBinarios
    End Function

    Public Function GerarImagemCodigoItf14(ByVal codigo As String,
                              Optional ByVal width As Integer = (-1),
                              Optional ByVal height As Integer = (-1),
                              Optional ByVal sngX1 As Single = (-1),
                              Optional ByVal sngY1 As Single = (-1),
                              Optional ByVal sngX2 As Single = (-1),
                              Optional ByVal sngY2 As Single = (-1),
                              Optional ByVal fontForText As Font = Nothing) As Byte()
        Dim K As Integer
        Dim sngPosX As Single
        Dim sngPosY As Single
        Dim sngScaleX As Single
        Dim strEANBin As List(Of String)
        Dim strFormat As New StringFormat()
        If width = (-1) Then
            width = 265 '204
        End If
        If height = (-1) Then
            height = 125 '70     
        End If
        '*
        '* Convert the code on its binary representation
        '*
        strEANBin = GerarBinarioCodigoItf14(codigo)
        '*
        '* Define the font to be printed
        '*
        If (fontForText Is Nothing) Then
            fontForText = New Font("Arial", 14, FontStyle.Bold)
        End If
        '*
        '* Defines the boundaries to the barcode
        '*
        If sngX1 = (-1) Then sngX1 = 0
        If sngY1 = (-1) Then sngY1 = 0
        If sngX2 = (-1) Then sngX2 = width
        If sngY2 = (-1) Then sngY2 = height
        '*
        '* Defines the boundaries of the barcode
        '*
        sngPosX = sngX1
        sngPosY = sngY2 - CSng(1.0 * fontForText.Height)
        Dim bitMap As New Bitmap(width, height)
        Dim graphics As Graphics = Graphics.FromImage(bitMap)
        '*
        '* Clears the area
        '*
        graphics.FillRectangle(New SolidBrush(Color.White), sngX1, sngY1, sngX2 - sngX1, sngY2 - sngY1)
        '*
        '* Calculates the scale
        '*
        sngScaleX = (sngX2 - sngX1) / String.Join("", strEANBin).Length
        Dim barraLarga, barraEstreita As Single
        barraEstreita = Math.Round(sngScaleX - 1.8, 4)
        barraLarga = Math.Round(sngScaleX + 0.7, 4)
        '* Draw quiet zone and start
        ' quiet zone
        graphics.FillRectangle(New SolidBrush(Color.White), CType(sngPosX.ToString("N4"), Single), sngY1, barraLarga, sngPosY)
        sngPosX += barraLarga * 4
        ' start
        graphics.FillRectangle(New SolidBrush(Color.Black), CType(sngPosX.ToString("N4"), Single), sngY1, barraEstreita, sngPosY)
        sngPosX += barraEstreita
        graphics.FillRectangle(New SolidBrush(Color.White), CType(sngPosX.ToString("N4"), Single), sngY1, barraEstreita, sngPosY)
        sngPosX += barraEstreita
        graphics.FillRectangle(New SolidBrush(Color.Black), CType(sngPosX.ToString("N4"), Single), sngY1, barraEstreita, sngPosY)
        sngPosX += barraEstreita
        graphics.FillRectangle(New SolidBrush(Color.White), CType(sngPosX.ToString("N4"), Single), sngY1, barraEstreita, sngPosY)
        sngPosX += barraEstreita
        '*
        '* Draw the BarCode
        '*
        For Each parBinary As String In strEANBin
            Dim left, right As String
            left = Strings.Left(parBinary, 5)
            right = Strings.Right(parBinary, 5)
            For K = 1 To 5
                'black
                If Mid(left, K, 1) = "1" Then '1 = barra larga
                    graphics.FillRectangle(New SolidBrush(Color.Black), CType(sngPosX.ToString("N4"), Single), sngY1, barraLarga, sngPosY)
                    sngPosX += barraLarga 'sngX1 + (K * barraLarga)
                Else  '0 = barra estreita
                    graphics.FillRectangle(New SolidBrush(Color.Black), CType(sngPosX.ToString("N4"), Single), sngY1, barraEstreita, sngPosY)
                    sngPosX += barraEstreita 'sngX1 + (K * barraEstreita)
                End If

                'white
                If Mid(right, K, 1) = "1" Then '1 = barra larga
                    graphics.FillRectangle(New SolidBrush(Color.White), sngPosX, sngY1, barraLarga, sngPosY)
                    sngPosX += barraLarga 'sngX1 + (K * barraLarga)
                Else  '0 = barra estreita
                    graphics.FillRectangle(New SolidBrush(Color.White), sngPosX, sngY1, barraEstreita, sngPosY)
                    sngPosX += barraEstreita 'sngX1 + (K * barraEstreita)
                End If
            Next K

        Next

        '* Draw stop and quiet zone 
        ' stop
        graphics.FillRectangle(New SolidBrush(Color.Black), CType(sngPosX.ToString("N4"), Single), sngY1, barraLarga, sngPosY)
        sngPosX += barraLarga
        graphics.FillRectangle(New SolidBrush(Color.White), CType(sngPosX.ToString("N4"), Single), sngY1, barraEstreita, sngPosY)
        sngPosX += barraEstreita
        graphics.FillRectangle(New SolidBrush(Color.Black), CType(sngPosX.ToString("N4"), Single), sngY1, barraEstreita, sngPosY)
        sngPosX += barraEstreita
        ' quiet zone
        graphics.FillRectangle(New SolidBrush(Color.White), CType(sngPosX.ToString("N4"), Single), sngY1, barraLarga, sngPosY)
        'sngPosX += barraLarga

        '*
        '* Draw the human-friendly code
        '*
        strFormat.Alignment = StringAlignment.Center
        strFormat.FormatFlags = StringFormatFlags.NoWrap
        graphics.DrawString(codigo, fontForText, New SolidBrush(Color.Black), CSng((sngX2 - sngX1) / 2), CSng(sngY2 - fontForText.Height), strFormat)

        Dim ms As New MemoryStream()
        bitMap.Save(ms, Imaging.ImageFormat.Png)
        Dim byteImage As Byte() = ms.ToArray()

        ms.Close()

        Return byteImage
    End Function

    ''' <summary>
    ''' validação pelo Módulo 10
    ''' </summary>
    ''' <param name="codigo"></param>
    Public Sub ValidarDigitoItf14(ByVal codigo As String)
        Dim digit, dv, checkSum As Integer

        If codigo.Length = 14 Then
            dv = codigo.Substring(codigo.Length - 1, 1)
        End If
        checkSum = 0 'checksum

        For i = 1 To 13
            digit = Mid(codigo, i, 1) - "0"  'get the next digit from bar code text
            If i Mod 2 = 0 Then
                checkSum = checkSum + digit * 1 'multiply each bar code digit by it's weight, 1 or 3
            Else
                checkSum = checkSum + digit * 3
            End If
        Next i
        If checkSum Mod 10 = 0 Then
            checkSum = 0
        Else
            checkSum = 10 - (checkSum Mod 10)
        End If
        If checkSum <> dv Then
            Throw New Exception("O dígito verificador do código DUN-14 está incorreto! Valor digitado: " & dv & " - Valor calculado pelo sistema: " & checkSum)
        End If
    End Sub
End Class
