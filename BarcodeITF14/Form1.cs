using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace BarcodeITF14
{
    public partial class Form1 : Form
    {
        private Barcode dun14 = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnGerar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text))
                    throw new Exception("Informe um código de 14 dígitos numéricos!");

                string path = Path.Combine(Path.GetTempPath(), "barcode.png");
                dun14 = new Barcode();

                try
                {
                    dun14.ValidarDigitoItf14(textBox1.Text);
               }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                byte[] imagem = dun14.GerarImagemCodigoItf14(textBox1.Text);

                if (File.Exists(path))
                    File.Delete(path);

                File.WriteAllBytes(path, imagem);
                pbCodigo.ImageLocation = path;            
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erro ao gerar código: " + ex.Message);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            pbCodigo.ImageLocation = string.Empty;
            textBox1.Focus();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pbCodigo_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
