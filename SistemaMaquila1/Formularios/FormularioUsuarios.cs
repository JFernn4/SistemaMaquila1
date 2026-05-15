using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SistemaMaquila1
{
    public partial class FormularioUsuarios : Form
    {
        GestionUsuarios gestionUsuarios = new GestionUsuarios();
        private string _fotoRuta = null;

        public FormularioUsuarios(GestionUsuarios gestionUsuarios)
        {
            InitializeComponent();
            this.gestionUsuarios = gestionUsuarios;
        }

        private void FormularioUsuarios_Load(object sender, EventArgs e)
        {
            pictureBox1.Cursor = Cursors.Hand;
            pictureBox1.Image = Properties.Resources.icons8_user_35;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Selecciona una foto de perfil";
                dialog.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _fotoRuta = dialog.FileName;
                    try
                    {
                        pictureBox1.Image = Image.FromFile(_fotoRuta);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                    catch
                    {
                        _fotoRuta = null;
                        Toast.Mostrar("No se pudo cargar la imagen", esError: true, formPadre: this);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) ||
                    string.IsNullOrEmpty(comboBox2.Text) ||
                    string.IsNullOrEmpty(textBox2.Text))
                {
                    Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                    return;
                }

                Guardar();
                Toast.Mostrar("Usuario guardado", formPadre: this);
                this.Close();
            }
            catch (Exception ex)
            {
                Toast.Mostrar("Error inesperado: " + ex.Message, esError: true, formPadre: this);
            }
        }

        private void Guardar()
        {
            Usuario usuario = new Usuario(
                0,
                textBox2.Text,  // NombreUsuario
                textBox1.Text,  // Contraseña
                comboBox2.Text, // Rol
                _fotoRuta       // null si no se seleccionó foto
            );
            gestionUsuarios.GuardarUsuario(usuario);
        }

        private void Limpiar()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            comboBox2.Text = "";
            _fotoRuta = null;
            pictureBox1.Image = Properties.Resources.icons8_user_35;
        }

        private void button1_Click(object sender, EventArgs e) => this.Close();
        private void button3_Click(object sender, EventArgs e) => Limpiar();
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
    }
}