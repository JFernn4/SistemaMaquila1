using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SistemaMaquila1.Formularios
{
    public partial class FormularioPedidos : Form
    {
        private GestionPedidos _gestion;
        private GestionClientes _gestionClientes = new GestionClientes();
        private GestionEmpleados _gestionEmpleados = new GestionEmpleados();
        private List<DetallePedido> _detalles = new List<DetallePedido>();

        public event Action<Pedido> PedidoGuardado;

        public FormularioPedidos(GestionPedidos gestion, Form padre = null)
        {
            InitializeComponent();
            _gestion = gestion;
        }

        private void FormularioPedidos_Load(object sender, EventArgs e)
        {
            ConfigurarDataGrid();
            CargarComboClientes();
            CargarComboEmpleados();
            CargarComboEstado();
            ActualizarTotal();
        }

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

        //  Carga de combos 

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
            comboBox2.SelectedIndex = 0;
        }

        //  Tabla de detalles 

        private void ActualizarTabla()
        {
            dataGridView1.Rows.Clear();
            foreach (var d in _detalles)
            {
                double subtotal = d.Cantidad * d.PrecioUnitario;
                dataGridView1.Rows.Add(
                    d.Prenda.Tipo,
                    d.Prenda.Talla,
                    d.Prenda.Descripcion,
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
            foreach (var d in _detalles)
                total += d.Cantidad * d.PrecioUnitario;
            label11.Text = total.ToString("C");
        }

        //  Botones 

        private void button3_Click(object sender, EventArgs e) // "+" añadir prenda
        {
            FormularioPedidoDetallado formDetalle = new FormularioPedidoDetallado();
            if (formDetalle.ShowDialog(this) == DialogResult.OK)
            {
                _detalles.Add(formDetalle.DetalleResultado);
                ActualizarTabla();
            }
        }

        private void button2_Click(object sender, EventArgs e) // Registrar
        {
            try
            {
                if (comboBox1.SelectedItem == null || comboBox3.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text) ||
                    string.IsNullOrWhiteSpace(comboBox2.Text))
                {
                    Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                    return;
                }

                if (_detalles.Count == 0)
                {
                    Toast.Mostrar("Agregue al menos una prenda", esError: true, formPadre: this);
                    return;
                }

                if (!DateTime.TryParse(textBox5.Text, out DateTime fechaInicio))
                {
                    Toast.Mostrar("Fecha de inicio inválida", esError: true, formPadre: this);
                    return;
                }

                if (!DateTime.TryParse(textBox1.Text, out DateTime fechaEntrega))
                {
                    Toast.Mostrar("Fecha de entrega inválida", esError: true, formPadre: this);
                    return;
                }

                if (!double.TryParse(textBox4.Text, out double anticipo) || anticipo < 0)
                {
                    Toast.Mostrar("Anticipo inválido", esError: true, formPadre: this);
                    return;
                }

                double total = 0;
                foreach (var d in _detalles) total += d.Cantidad * d.PrecioUnitario;

                Pedido pedido = new Pedido(
                    0,
                    (int)comboBox1.SelectedValue,
                    (int)comboBox3.SelectedValue,
                    fechaInicio,
                    fechaEntrega,
                    anticipo,
                    total,
                    comboBox2.Text
                );
                pedido.Prendas = _detalles;

                _gestion.GuardarPedido(pedido);
                PedidoGuardado?.Invoke(pedido);
                Toast.Mostrar("Pedido guardado", formPadre: this);
                this.Close();
            }
            catch (Exception ex)
            {
                Toast.Mostrar("Error: " + ex.Message, esError: true, formPadre: this);
            }
        }

        private void button1_Click(object sender, EventArgs e) // Cancelar
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e) { }
    }
}