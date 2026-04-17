using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMaquila1
{
    public partial class FormularioUsuarios : Form
    {
        GestionUsuarios gestionUsuarios = new GestionUsuarios();
        public FormularioUsuarios(GestionUsuarios gestionUsuarios)
        {
            InitializeComponent();
            this.gestionUsuarios = gestionUsuarios;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) ||  string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Complete todos los datos");
                    return;
                }

                Guardar();
                MessageBox.Show("Datos Guardados");
                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Error al guardar los datos, intente nuevamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message);
            }
        }

        private void FormularioUsuarios_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Guardar()
        {
            string nombreUsuario = textBox2.Text;
            string contrasena = textBox1.Text;
            string rol = textBox3.Text;
            Usuario usuario = new Usuario(nombreUsuario, contrasena, rol);

            gestionUsuarios.GuardarUsuario(usuario);
        }
        private void Cancelar()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
        }
        private void Limpiar()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            textBox3.Text = "";
        }
    }
}
