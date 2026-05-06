using SistemaMaquila1;
using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Windows.Forms;

namespace SistemaMaquila1.Formularios
{
    public partial class DetallesClientes : Form
    {
        private Cliente _cliente;
        private GestionClientes _gestion = new GestionClientes();

        // Elementos modo edición
        private Control[] _modoEdicion;
        // Elementos modo detalle
        private Control[] _modoDetalle;

        public DetallesClientes(Cliente cliente)
        {
            InitializeComponent();
            _cliente = cliente;

            _modoEdicion = new Control[] { button2, textBox1, textBox2, textBox3, maskedTextBox1 };
            _modoDetalle = new Control[] { button4, button3, label4, label7, label8, label9 };
        }

        private void DetallesClientes_Load(object sender, EventArgs e)
        {
            MostrarDetalles();
        }

        // ─── Modos ────────────────────────────────────────────────────────

        private void MostrarDetalles()
        {
            // Cargar datos en labels
            label7.Text = _cliente.Nombre;
            label4.Text = _cliente.DPI;
            label8.Text = _cliente.Telefono;
            label9.Text = _cliente.Correo;

            // Visibilidad
            foreach (var c in _modoDetalle) c.Visible = true;
            foreach (var c in _modoEdicion) c.Visible = false;
        }

        private void MostrarEdicion()
        {
            // Precargar textboxes con datos actuales del cliente
            textBox1.Text = _cliente.Nombre;
            textBox2.Text = _cliente.DPI;
            maskedTextBox1.Text = _cliente.Telefono;
            textBox3.Text = _cliente.Correo;

            // Visibilidad
            foreach (var c in _modoEdicion) c.Visible = true;
            foreach (var c in _modoDetalle) c.Visible = false;
        }

        // ─── Botones ──────────────────────────────────────────────────────

        private void button3_Click(object sender, EventArgs e) // Editar
        {
            MostrarEdicion();
        }

        private void button2_Click(object sender, EventArgs e) // Aceptar (guardar edición)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(maskedTextBox1.Text))
            {
                Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                return;
            }

            // Actualizar el objeto cliente
            _cliente.Nombre = textBox1.Text;
            _cliente.DPI = textBox2.Text;
            _cliente.Telefono = maskedTextBox1.Text;
            _cliente.Correo = textBox3.Text;

            _gestion.ActualizarCliente(_cliente);
            Toast.Mostrar("Cliente actualizado", formPadre: this);
            MostrarDetalles();
        }

        private void button4_Click(object sender, EventArgs e) // Eliminar
        {
            if (Dialogo.Confirmar(this, $"¿Eliminar a {_cliente.Nombre}?"))
            {
                _gestion.EliminarCliente(_cliente.ID);
                Toast.Mostrar("Cliente eliminado", formPadre: this);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Cancelar / Cerrar
        {
            this.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}