using SistemaMaquila1.Formularios;
using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System.Windows.Forms;

namespace SistemaMaquila1
{
    public partial class InterfazPedidos : UserControl
    {
        GestionPedidos gestion = new GestionPedidos();
        private DetallesPedidos _detalleAbierto = null;

        public InterfazPedidos()
        {
            InitializeComponent();
        }

        private void InterfazPedidos_Load(object sender, EventArgs e)
        {
            panel1.SizeChanged += (s, ev) => RedibujarPedidos();
            CargarPedidos();
        }

        public void CargarPedidos()
        {
            panel1.Controls.Clear();
            GestionListasVisuales.LimpiarCache(panel1);

            if (gestion.ExistenPedidos())
            {
                label1.Visible = false;
                panel1.Visible = true;
                GestionListasVisuales.CargarItems(
                    panel1,
                    gestion.ObtenerPedidos(),
                    p => $"Pedido #{p.ID}",
                    p => p.ID,
                    p => Properties.Resources.icons8_books_50, 
                    p => AbrirPedido(p)
                );
                panel1.Refresh();
            }
            else
            {
                label1.Visible = true;
                panel1.Visible = false;
            }
        }

        private void RedibujarPedidos()
        {
            GestionListasVisuales.Filtrar<Pedido>(panel1, textBox1.Text);
        }

        private void AbrirPedido(Pedido p)
        {
            GestionRecientes.Registrar("Pedido", p.ID, $"Pedido #{p.ID}");
            _detalleAbierto?.Close();
            _detalleAbierto = new DetallesPedidos(p);
            _detalleAbierto.FormClosed += (s, ev) => CargarPedidos();
            _detalleAbierto.Show(this.FindForm());
        }

        private void button1_Click(object sender, EventArgs e) // "+"
        {
            FormularioPedidos formPedidos = new FormularioPedidos(gestion);
            formPedidos.ShowDialog();
            textBox1.Clear();
            CargarPedidos();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label1.Text = "No se ha registrado ningún pedido";
                label1.Visible = false;
                panel1.Visible = true;
                GestionListasVisuales.Filtrar<Pedido>(panel1, "");
            }
            else
            {
                GestionListasVisuales.Filtrar<Pedido>(panel1, textBox1.Text);
                bool sinResultados = panel1.Controls.Count == 0;
                label1.Text = sinResultados ? "No se encontraron pedidos." : "";
                label1.Visible = sinResultados;
                panel1.Visible = !sinResultados;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}