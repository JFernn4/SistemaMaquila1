using SistemaMaquila1;
using SistemaMaquila1.Herramientas;
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
            string usuario = textBox9.Text;
            string contrasena = textBox1.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this.FindForm());
                return;
            }

            GestionUsuarios gestion = new GestionUsuarios();

            if (gestion.IniciarSesion(usuario, contrasena))
            {
                // Subir al Form1 y llamar MostrarApp()
                Form1 form1 = this.FindForm() as Form1;
                form1?.MostrarApp();
            }
            else
            {
                Toast.Mostrar("Credenciales incorrectas", esError: true, formPadre: this.FindForm());
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
