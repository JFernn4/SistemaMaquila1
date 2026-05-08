using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Windows.Forms;

namespace SistemaMaquila1.Formularios
{
    public partial class DetallesEmpleados : Form
    {
        private Empleado _empleado;
        private GestionEmpleados _gestion = new GestionEmpleados();

        private Control[] _modoEdicion;
        private Control[] _modoDetalle;

        public DetallesEmpleados(Empleado empleado)
        {
            InitializeComponent();
            _empleado = empleado;

            _modoEdicion = new Control[] { button3, textBox1, textBox2, textBox3, maskedTextBox1, comboBox2, textBox4, label7 };
            _modoDetalle = new Control[] { button2, button4, label9, label1, label11, label8, label12 };
        }

        private void DetallesEmpleados_Load(object sender, EventArgs e)
        {
            MostrarDetalles();
        }

        // ─── Modos ────────────────────────────────────────────────────────

        private void MostrarDetalles()
        {
            label9.Text = _empleado.Nombre;
            label1.Text = _empleado.DPI;
            label8.Text = _empleado.Telefono;
            label11.Text = _empleado.Correo;
            label12.Text = _empleado.Cargo;

            foreach (var c in _modoDetalle) c.Visible = true;
            foreach (var c in _modoEdicion) c.Visible = false;
        }

        private void MostrarEdicion()
        {
            textBox1.Text = _empleado.Nombre;
            textBox2.Text = _empleado.DPI;
            maskedTextBox1.Text = _empleado.Telefono;
            textBox3.Text = _empleado.Correo;
            comboBox2.Text = _empleado.Cargo;
            textBox4.Text = _empleado.Salario.ToString();

            foreach (var c in _modoEdicion) c.Visible = true;
            foreach (var c in _modoDetalle) c.Visible = false;
        }

        // ─── Botones ──────────────────────────────────────────────────────

        private void button2_Click(object sender, EventArgs e) // Editar
        {
            MostrarEdicion();
        }

        private void button3_Click(object sender, EventArgs e) // Aceptar
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(maskedTextBox1.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                return;
            }

            if (!double.TryParse(textBox4.Text, out double salario) || salario < 0)
            {
                Toast.Mostrar("Ingrese un salario válido", esError: true, formPadre: this);
                return;
            }

            _empleado.Nombre = textBox1.Text;
            _empleado.DPI = textBox2.Text;
            _empleado.Telefono = maskedTextBox1.Text;
            _empleado.Correo = textBox3.Text;
            _empleado.Cargo = comboBox2.Text;
            _empleado.Salario = salario;

            _gestion.ActualizarEmpleado(_empleado);
            Toast.Mostrar("Empleado actualizado", formPadre: this);
            MostrarDetalles();
        }

        private void button4_Click(object sender, EventArgs e) // Eliminar
        {
            if (Dialogo.Confirmar(this, $"¿Eliminar a {_empleado.Nombre}?"))
            {
                _gestion.EliminarEmpleado(_empleado.ID);
                Toast.Mostrar("Empleado eliminado", formPadre: this);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Cancelar / Cerrar
        {
            this.Close();
        }
    }
}