using SistemaMaquila1;
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
    public partial class FormularioEmpleados : Form
    {
        GestionEmpleados gestion = new GestionEmpleados();
        public FormularioEmpleados(GestionEmpleados gestion)
        {
            InitializeComponent();
            this.gestion = gestion;
        }

        private void FormularioEmpleados_Load(object sender, EventArgs e)
        {
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(comboBox2.Text))
                {
                    Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                    return;
                }
                if (!int.TryParse(textBox2.Text, out int id1))
                {
                    Toast.Mostrar("Ingrese un ID valido", esError: true, formPadre: this);
                    return;
                }
                if (!double.TryParse(textBox4.Text, out double salario) || salario < 0)
                {
                    MessageBox.Show("Ingrese un salario valido");
                    return;
                }
                Guardar();
                Toast.Mostrar("Empleado guardado", formPadre: this);
                this.Close();
                
            }
            catch (FormatException ex)
            {
                Toast.Mostrar("Error al guardar los datos, intente nuevamente", esError: true, formPadre: this);
            }
            catch (Exception ex)
            {
                Toast.Mostrar("Error inesperado: " + ex.Message, esError: true, formPadre: this);
            }
        }
        public void Guardar()
        {
            if (!double.TryParse(textBox4.Text, out double salario))
            {
                MessageBox.Show("Ingrese un salario válido");
                return;
            }
            Empleado empleado = new Empleado(
                int.Parse(textBox2.Text), //CAMBIAR LUEGO DEJAR EN 0
                textBox1.Text,
                textBox5.Text,
                comboBox2.Text,
                textBox3.Text,
                double.Parse(textBox4.Text)
            );

            gestion.GuardarEmpleado(empleado);
        }

        public void Limpiar()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox2.Text = "";
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
