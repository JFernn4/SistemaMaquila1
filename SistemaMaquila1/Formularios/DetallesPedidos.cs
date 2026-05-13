using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaMaquila1.Formularios
{
    public partial class DetallesPedidos : Form
    {
        private Pedido _pedido;
        private GestionPedidos _gestion = new GestionPedidos();
        private GestionClientes _gestionClientes = new GestionClientes();
        private GestionEmpleados _gestionEmpleados = new GestionEmpleados();
        private List<DetallePedido> _detalles;

        private Control[] _modoEdicion;
        private Control[] _modoDetalle;

        public DetallesPedidos(Pedido pedido)
        {
            InitializeComponent();
            _pedido = pedido;
            _detalles = new List<DetallePedido>(_pedido.Prendas);

            _modoEdicion = new Control[] { button2, comboBox1, comboBox3, textBox5, textBox1, textBox4, comboBox2, button3, dataGridView1 };
            _modoDetalle = new Control[] { button5, button4, label12, label13, label14, label15, label16, label17, dataGridView1 };
        }

        private void DetallesPedidos_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid();
            CargarComboClientes();
            CargarComboEmpleados();
            CargarComboEstado();
            MostrarDetalles();
        }

        // Configuración

        private void ConfigurarDataGrid()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Tipo", "Tipo");
            dataGridView1.Columns.Add("Talla", "Talla");
            dataGridView1.Columns.Add("Descripcion", "Descripción");
            dataGridView1.Columns.Add("Cantidad", "Cantidad");
            dataGridView1.Columns.Add("PrecioUnitario", "Precio Unit.");
            dataGridView1.Columns.Add("Subtotal", "Subtotal");
            dataGridView1.ReadOnly = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.RowHeadersVisible = false; 
        }

        private void CargarComboClientes()
        {
            comboBox1.DataSource = _gestionClientes.ObtenerClientes();
            comboBox1.DisplayMember = "Nombre";
            comboBox1.ValueMember = "ID";
        }

        private void CargarComboEmpleados()
        {
            comboBox3.DataSource = _gestionEmpleados.ObtenerEmpleados();
            comboBox3.DisplayMember = "Nombre";
            comboBox3.ValueMember = "ID";
        }

        private void CargarComboEstado()
        {
            comboBox2.Items.AddRange(new string[] { "Pendiente", "En proceso", "Completado", "Cancelado" });
        }

        // ─── Modos ────────────────────────────────────────────────────────

        private void MostrarDetalles()
        {
            // Buscar nombre de cliente y empleado
            var clientes = _gestionClientes.ObtenerClientes();
            var empleados = _gestionEmpleados.ObtenerEmpleados();

            var cliente = clientes.Find(c => c.ID == _pedido.ClienteId);
            var empleado = empleados.Find(e => e.ID == _pedido.EmpleadoId);

            label12.Text = cliente?.Nombre ?? _pedido.ClienteId.ToString();
            label13.Text = empleado?.Nombre ?? _pedido.EmpleadoId.ToString();
            label14.Text = _pedido.FechaInicio.ToString("dd/MM/yyyy");
            label15.Text = _pedido.FechaEntrega.ToString("dd/MM/yyyy");
            label16.Text = _pedido.Anticipo.ToString("C");
            label17.Text = _pedido.Estado;
            label11.Text = _pedido.Total.ToString("C");

            ActualizarTabla();

            foreach (var c in _modoDetalle) c.Visible = true;
            foreach (var c in _modoEdicion) c.Visible = false;
            dataGridView1.Visible = true;
            dataGridView1.ReadOnly = true;
        }

        private void MostrarEdicion()
        {
            comboBox1.SelectedValue = _pedido.ClienteId;
            comboBox3.SelectedValue = _pedido.EmpleadoId;
            textBox5.Text = _pedido.FechaInicio.ToString("yyyy-MM-dd");
            textBox1.Text = _pedido.FechaEntrega.ToString("yyyy-MM-dd");
            textBox4.Text = _pedido.Anticipo.ToString();
            comboBox2.Text = _pedido.Estado;

            ActualizarTabla();

            foreach (var c in _modoEdicion) c.Visible = true;
            foreach (var c in _modoDetalle) c.Visible = false;
            dataGridView1.Visible = true;
            dataGridView1.ReadOnly = true;
        }

        private void ActualizarTabla()
        {
            dataGridView1.Rows.Clear();
            foreach (var d in _detalles)
            {
                double subtotal = d.Cantidad * d.PrecioUnitario;
                dataGridView1.Rows.Add(
                    d.Prenda?.Tipo,
                    d.Prenda?.Talla,
                    d.Prenda?.Descripcion,
                    d.Cantidad,
                    d.PrecioUnitario.ToString("C"),
                    subtotal.ToString("C")
                );
            }
            ActualizarTotal();
        }

        private void ActualizarTotal()
        {
            double total = 0;
            foreach (var d in _detalles) total += d.Cantidad * d.PrecioUnitario;
            label11.Text = total.ToString("C");
        }

        // ─── Botones ──────────────────────────────────────────────────────

        private void button5_Click(object sender, EventArgs e) // Editar
        {
            MostrarEdicion();
        }

        private void button2_Click(object sender, EventArgs e) // Aceptar
        {
            if (!DateTime.TryParse(textBox5.Text, out DateTime fechaInicio) ||
                !DateTime.TryParse(textBox1.Text, out DateTime fechaEntrega) ||
                !double.TryParse(textBox4.Text, out double anticipo))
            {
                Toast.Mostrar("Verifique los datos ingresados", esError: true, formPadre: this);
                return;
            }

            _pedido.ClienteId = (int)comboBox1.SelectedValue;
            _pedido.EmpleadoId = (int)comboBox3.SelectedValue;
            _pedido.FechaInicio = fechaInicio;
            _pedido.FechaEntrega = fechaEntrega;
            _pedido.Anticipo = anticipo;
            _pedido.Estado = comboBox2.Text;
            _pedido.Prendas = _detalles;
            _pedido.Total = 0;
            foreach (var d in _detalles) _pedido.Total += d.Cantidad * d.PrecioUnitario;

            _gestion.ActualizarPedido(_pedido);
            Toast.Mostrar("Pedido actualizado", formPadre: this);
            MostrarDetalles();
        }

        private void button3_Click(object sender, EventArgs e) // "+" añadir prenda en edición
        {
            FormularioPedidoDetallado formDetalle = new FormularioPedidoDetallado();
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                _detalles.Add(formDetalle.DetalleResultado);
                ActualizarTabla();
            }
        }

        private void button4_Click(object sender, EventArgs e) // Eliminar pedido
        {
            if (Dialogo.Confirmar(this, $"¿Eliminar el pedido #{_pedido.ID}?"))
            {
                _gestion.EliminarPedido(_pedido.ID);
                Toast.Mostrar("Pedido eliminado", formPadre: this);
                this.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Cancelar / Cerrar
        {
            this.Close();
        }
    }
}