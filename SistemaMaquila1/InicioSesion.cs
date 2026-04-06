using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace SistemaDeImportadora
{
    public partial class InicioSesion : UserControl
    {
        public InicioSesion()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void InicioSesion_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.PasswordChar = '●';
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //ingresar
            if (textBox9.Text == "admin" && textBox1.Text == "2026")
                //
            {
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //mostrar
            button3.BringToFront();
            textBox1.PasswordChar = '\0';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //ocultar
            button1.BringToFront();
            textBox1.PasswordChar = '●';
        }
    }
}
