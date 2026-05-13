using SistemaMaquila1.Objetos;
using System;
using System.Windows.Forms;

namespace SistemaMaquila1.Formularios
{
    public partial class FormularioPedidoDetallado : Form
    {
        public DetallePedido DetalleResultado { get; private set; }

        public FormularioPedidoDetallado()
        {
            InitializeComponent();
        }

        private void FormularioPedidoDetallado_Load(object sender, EventArgs e)
        {
            // Cargar tipos de prenda disponibles
            comboBox1.Items.AddRange(new string[] { "Camisa", "Pantalón", "Vestido", "Falda", "Chaqueta", "Otro" });
        }

        private void button2_Click(object sender, EventArgs e) // Registrar
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox5.Text) ||
                string.IsNullOrWhiteSpace(textBox4.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text))
            {
                Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                return;
            }

            if (!int.TryParse(textBox4.Text, out int cantidad) || cantidad <= 0)
            {
                Toast.Mostrar("Ingrese una cantidad válida", esError: true, formPadre: this);
                return;
            }

            if (!double.TryParse(textBox3.Text, out double precioUnitario) || precioUnitario < 0)
            {
                Toast.Mostrar("Ingrese un precio unitario válido", esError: true, formPadre: this);
                return;
            }

            // Crear prenda temporal (ID 0, se asignará al guardar el pedido)
            Prenda prenda = new Prenda(0, comboBox1.Text, textBox1.Text, textBox5.Text);

            DetalleResultado = new DetallePedido(0, 0, 0, cantidad, precioUnitario);
            DetalleResultado.Prenda = prenda;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) // Cancelar
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
    }
}