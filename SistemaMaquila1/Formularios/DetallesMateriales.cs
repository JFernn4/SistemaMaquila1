using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Windows.Forms;

namespace SistemaMaquila1.Formularios
{
    public partial class DetallesMateriales : Form
    {
        private Material _material;
        private GestionMateriales _gestion = new GestionMateriales();

        private Control[] _modoEdicion;
        private Control[] _modoDetalle;

        public DetallesMateriales(Material material)
        {
            InitializeComponent();
            _material = material;

            _modoEdicion = new Control[] { button3, textBox1, textBox5, textBox3, textBox4, textBox6, comboBox1, comboBox2 };
            _modoDetalle = new Control[] { button2, button4, label9, label11, label12, label13, label14, label15, label16 };
        }

        private void DetallesMateriales_Load(object sender, EventArgs e)
        {
            MostrarDetalles();
        }

        // ─── Modos ────────────────────────────────────────────────────────

        private void MostrarDetalles()
        {
            label11.Text = _material.Nombre;
            label9.Text = _material.Tipo;
            label12.Text = _material.Color;
            label13.Text = _material.UnidadMedida;
            label14.Text = _material.StockMinimo.ToString();
            label16.Text = _material.CantidadDisponible.ToString();
            label15.Text = _material.CostoUnitario.ToString();

            foreach (var c in _modoDetalle) c.Visible = true;
            foreach (var c in _modoEdicion) c.Visible = false;
        }

        private void MostrarEdicion()
        {
            textBox1.Text = _material.Nombre;
            comboBox1.Text = _material.Tipo;
            textBox5.Text = _material.Color;
            comboBox2.Text = _material.UnidadMedida;
            textBox4.Text = _material.StockMinimo.ToString();
            textBox3.Text = _material.CantidadDisponible.ToString();
            textBox6.Text = _material.CostoUnitario.ToString();

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
                string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox6.Text))
            {
                Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                return;
            }

            if (!double.TryParse(textBox3.Text, out double cantidad) || cantidad < 0)
            {
                Toast.Mostrar("Cantidad disponible inválida", esError: true, formPadre: this);
                return;
            }

            if (!double.TryParse(textBox4.Text, out double stockMinimo) || stockMinimo < 0)
            {
                Toast.Mostrar("Stock mínimo inválido", esError: true, formPadre: this);
                return;
            }

            if (!double.TryParse(textBox6.Text, out double costoUnitario) || costoUnitario < 0)
            {
                Toast.Mostrar("Costo unitario inválido", esError: true, formPadre: this);
                return;
            }

            _material.Nombre = textBox1.Text;
            _material.Tipo = comboBox1.Text;
            _material.Color = textBox5.Text;
            _material.UnidadMedida = comboBox2.Text;
            _material.CantidadDisponible = cantidad;
            _material.StockMinimo = stockMinimo;
            _material.CostoUnitario = costoUnitario;

            _gestion.ActualizarMaterial(_material);
            Toast.Mostrar("Material actualizado", formPadre: this);
            MostrarDetalles();
        }

        private void button4_Click(object sender, EventArgs e) // Eliminar
        {
            if (Dialogo.Confirmar(this, $"¿Eliminar {_material.Nombre}?"))
            {
                _gestion.EliminarMaterial(_material.ID);
                Toast.Mostrar("Material eliminado", formPadre: this);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Cancelar / Cerrar
        {
            this.Close();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }
    }
}