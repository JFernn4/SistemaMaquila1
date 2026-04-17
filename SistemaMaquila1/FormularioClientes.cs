using SistemaMaquila1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaDeImportadora
{
    public partial class FormularioClientes : Form
    {
        GestionClientes datoscliente = new GestionClientes();
        public FormularioClientes(GestionClientes datoscliente)
        {
            InitializeComponent();
            this.datoscliente = datoscliente;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(maskedTextBox1.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox2.Text))
                {
                    MessageBox.Show("Complete todos los datos");
                    return;
                }
                if (!int.TryParse(textBox2.Text, out int id1))
                {
                    MessageBox.Show("Ingrese un ID valido");
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
        private void Guardar()
        {
            Cliente cliente = new Cliente()
            {
                ID = int.Parse(textBox2.Text),
                Nombre = textBox1.Text,
                Telefono = maskedTextBox1.Text,
                Correo = textBox3.Text
            };
            datoscliente.GuardarCliente(cliente);
        }
        private void Cancelar()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            textBox3.Text = "";
        }
        private void Limpiar()
        {
            textBox2.Text = "";
            textBox1.Text = "";
            maskedTextBox1.Text = "";
            textBox3.Text = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void FormularioClientes_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
