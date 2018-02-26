using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

/*
 * Desenvolvido por Jaqueline Mistron
 * Janeiro de 2016
 * Fontes de conhecimento: 
 *      validação do dígito verificador: https://strokescribe.com/en/ean-13.html#ean-13-check-digitvba
 *      projeto EAN-13 (VB.Net e C#): https://www.ibm.com/developerworks/community/blogs/fd26864d-cb41-49cf-b719-d89c6b072893/entry/net_c_C3_B3digo_de_barras_ean_8_e_13_parte_021?lang=en
 *      especificações do padrão ITF-14: http://www.gs1.org/docs/barcodes/GS1_General_Specifications.pdf
 *      
 */

namespace BarcodeITF14
{
    public class Barcode
    {
        public Barcode() { }

        public List<string> GerarBinarioCodigoItf14(string codigo)
        {
            string strLeft, strRight = string.Empty;
            List<string> pares = new List<string>();
            List<string> paresBinarios = new List<string>();
            try
            {
                strLeft = "";
                codigo = codigo.Trim();

                for (int index = 0; index < codigo.Length; )
                {
                    pares.Add(codigo.Substring(index, 2)); // é ncessário formar pares com os dígitos do código. Então, o código de 14 dígitos terá 7 pares
                    index += 2;
                }

                foreach (var par in pares)
                {
                    switch (int.Parse(par.Substring(0, 1)))
                    {
                        case 0:
                            strLeft = "00110";
                            break;
                        case 1:
                            strLeft = "10001";
                            break;
                        case 2:
                            strLeft = "01001";
                            break;
                        case 3:
                            strLeft = "11000";
                            break;
                        case 4:
                            strLeft = "00101";
                            break;
                        case 5:
                            strLeft = "10100";
                            break;
                        case 6:
                            strLeft = "01100";
                            break;
                        case 7:
                            strLeft = "00011";
                            break;
                        case 8:
                            strLeft = "10010";
                            break;
                        case 9:
                            strLeft = "01010";
                            break;
                    }

                    switch (int.Parse(par.Substring(1, 1)))
                    {
                        case 0:
                            strRight = "00110";
                            break;
                        case 1:
                            strRight = "10001";
                            break;
                        case 2:
                            strRight = "01001";
                            break;
                        case 3:
                            strRight = "11000";
                            break;
                        case 4:
                            strRight = "00101";
                            break;
                        case 5:
                            strRight = "10100";
                            break;
                        case 6:
                            strRight = "01100";
                            break;
                        case 7:
                            strRight = "00011";
                            break;
                        case 8:
                            strRight = "10010";
                            break;
                        case 9:
                            strRight = "01010";
                            break;
                    }

                    paresBinarios.Add(strLeft + strRight);
                }

                return paresBinarios;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erro ao gerar binário: " + ex.Message);
            }
            return paresBinarios;
        }

        public byte[] GerarImagemCodigoItf14(string codigo, int width = -1, int height = -1, float sngX1 = -1, float sngY1 = -1, float sngX2 = -1, float sngY2 = -1, System.Drawing.Font fontForText = null)
        {
            try
            {
                int k;
                float sngPosX, sngPosY, sngScaleX;
                List<string> strEanBin;
                StringFormat strFormat = new StringFormat();

                if (width == -1)
                    width = 285;
                if (height == -1)
                    height = 125;

                // Converte o código em uma representação binária
                strEanBin = GerarBinarioCodigoItf14(codigo);

                if (fontForText == null)
                    fontForText = new Font("Arial", 14, FontStyle.Bold);

                if (sngX1 == -1)
                    sngX1 = 0;
                if (sngY1 == -1)
                    sngY1 = 0;
                if (sngX2 == -1)
                    sngX2 = width;
                if (sngY2 == -1)
                    sngY2 = height;

                sngPosX = sngX1;
                sngPosY = sngY2 - (float)(1.5 * fontForText.Height);

                Bitmap bitMap = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(bitMap);

                // Limpa a área
                graphics.FillRectangle(new SolidBrush(Color.White), sngX1, sngY1, sngX2 - sngX1, sngY2 - sngY1);

                sngScaleX = (sngX2 - sngX1) / string.Join("", strEanBin).Length;

                float barraLarga, barraEstreita;
                barraEstreita = (float)Math.Round(sngScaleX - 1.8, 4);
                barraLarga = (float)Math.Round(sngScaleX + 0.7, 4);

                // desenha a quiet zone
                graphics.FillRectangle(new SolidBrush(Color.White), (float)Math.Round(sngPosX, 4), sngY1, barraLarga, sngPosY);
                sngPosX += barraLarga * 4;
                // start
                graphics.FillRectangle(new SolidBrush(Color.Black), (float)Math.Round(sngPosX, 4), sngY1, barraEstreita, sngPosY);
                sngPosX += barraEstreita;
                graphics.FillRectangle(new SolidBrush(Color.White), (float)Math.Round(sngPosX, 4), sngY1, barraEstreita, sngPosY);
                sngPosX += barraEstreita;
                graphics.FillRectangle(new SolidBrush(Color.Black), (float)Math.Round(sngPosX, 4), sngY1, barraEstreita, sngPosY);
                sngPosX += barraEstreita;
                graphics.FillRectangle(new SolidBrush(Color.White), (float)Math.Round(sngPosX, 4), sngY1, barraEstreita, sngPosY);
                sngPosX += barraEstreita;

                // desenha o código de barras
                foreach (var parBinary in strEanBin)
                {
                    string left, right;
                    left = parBinary.Substring(0, 5);
                    right = parBinary.Substring(5, 5);

                    for (k = 0; k < 5; k++)
                    {
                        // black
                        if (left.Substring(k, 1) == "1")
                        {
                            graphics.FillRectangle(new SolidBrush(Color.Black), (float)Math.Round(sngPosX, 4), sngY1, barraLarga, sngPosY);
                            sngPosX += barraLarga;
                        }
                        else
                        {
                            graphics.FillRectangle(new SolidBrush(Color.Black), (float)Math.Round(sngPosX, 4), sngY1, barraEstreita, sngPosY);
                            sngPosX += barraEstreita;
                        }

                        // white
                        if (right.Substring(k, 1) == "1")
                        {
                            graphics.FillRectangle(new SolidBrush(Color.White), sngPosX, sngY1, barraLarga, sngPosY);
                            sngPosX += barraLarga;
                        }
                        else
                        {
                            graphics.FillRectangle(new SolidBrush(Color.White), sngPosX, sngY1, barraEstreita, sngPosY);
                            sngPosX += barraEstreita;
                        }
                    }
                }
                // desenha a quiet zone  
                graphics.FillRectangle(new SolidBrush(Color.Black), (float)Math.Round(sngPosX, 4), sngY1, barraLarga, sngPosY);
                sngPosX += barraLarga;
                graphics.FillRectangle(new SolidBrush(Color.White), (float)Math.Round(sngPosX, 4), sngY1, barraEstreita, sngPosY);
                sngPosX += barraEstreita;
                graphics.FillRectangle(new SolidBrush(Color.Black), (float)Math.Round(sngPosX, 4), sngY1, barraEstreita, sngPosY);
                sngPosX += barraEstreita;
                // quiet zone
                graphics.FillRectangle(new SolidBrush(Color.White), (float)Math.Round(sngPosX, 4), sngY1, barraLarga, sngPosY);

                // desenha o código amigável (numérico)
                strFormat.Alignment = StringAlignment.Center;
                strFormat.FormatFlags = StringFormatFlags.NoWrap;
                graphics.DrawString(codigo, fontForText, new SolidBrush(Color.Black), (sngX2 - sngX1) / 2, sngY2 - fontForText.Height, strFormat);

                MemoryStream ms = new MemoryStream();
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                ms.Close();

                return byteImage;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erro ao gerar imagem: " + ex.Message);
            }
            return null;
        }

        /// <summary>
        /// validação pelo Módulo 10
        /// </summary>
        /// <param name="codigo"></param>
        public void ValidarDigitoItf14(string codigo)
        {
            int digit, dv, checkSum;

            dv = -1;

            if (codigo.Length == 14)
                dv = int.Parse(codigo.Substring(codigo.Length - 1, 1));

            checkSum = 0;

            for (int index = 0; index < 13; index++)
            {
                digit = int.Parse(codigo.Substring(index, 1));

                if ((index + 1) % 2 == 0)
                    checkSum = checkSum + digit * 1;
                else
                    checkSum = checkSum + digit * 3;
            }

            if (checkSum % 10 == 0)
                checkSum = 0;
            else
                checkSum = 10 - (checkSum % 10);

            if (checkSum != dv)
                throw new Exception("O dígito verificador do código ITF-14 está incorreto! Valor digitado: " + (dv >= 0 ? dv.ToString() : string.Empty) + " - Valor calculado: " + checkSum);

        }
    }
}
