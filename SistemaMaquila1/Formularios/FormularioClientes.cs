using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
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
        private GestionClientes gestion;
        Form _padre;
        public event Action<Cliente> ClienteGuardado;
        public FormularioClientes(GestionClientes gestion, Form padre = null)
        {
            InitializeComponent();
            this.gestion = gestion;
            _padre = padre;
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
                if (string.IsNullOrEmpty(textBox1.Text) ||
                    string.IsNullOrEmpty(maskedTextBox1.Text) ||
                    string.IsNullOrEmpty(textBox3.Text) ||
                    string.IsNullOrEmpty(textBox2.Text))
                {
                    Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                    return;
                }

                Cliente clienteCreado = Guardar();
                ClienteGuardado?.Invoke(clienteCreado);
                this.Close();
                Toast.Mostrar("Cliente guardado", formPadre: this);
            }
            catch (Exception ex)
            {
                Toast.Mostrar("Error inesperado: " + ex.Message, esError: true, formPadre: this);
            }
        }

        private Cliente Guardar()
        {
            Cliente cliente = new Cliente(
                0,
                textBox2.Text,
                textBox1.Text,
                maskedTextBox1.Text,
                textBox3.Text
            );

            gestion.GuardarCliente(cliente);

            return cliente;
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
