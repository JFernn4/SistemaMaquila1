using SistemaMaquila1.Formularios;
using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System.Windows.Forms;

namespace SistemaMaquila1.Controles_de_Usuario
{
    public partial class InterfazMateriales : UserControl
    {
        GestionMateriales gestion = new GestionMateriales();
        private DetallesMateriales _detalleAbierto = null;

        public InterfazMateriales()
        {
            InitializeComponent();
        }

        private void InterfazMateriales_Load(object sender, EventArgs e)
        {
            panel1.SizeChanged += (s, ev) => RedibujarMateriales();
            CargarMateriales();
        }

        public void CargarMateriales()
        {
            panel1.Controls.Clear();
            GestionListasVisuales.LimpiarCache(panel1);

            if (gestion.ExistenMateriales())
            {
                label1.Visible = false;
                panel1.Visible = true;
                GestionListasVisuales.CargarItems(
                    panel1,
                    gestion.ObtenerMateriales(),
                    m => m.Nombre,
                    m => m.ID,
                    m => Properties.Resources.icons8_blankie_50, 
                    m => AbrirMaterial(m)
                );
                panel1.Refresh();
            }
            else
            {
                label1.Visible = true;
                panel1.Visible = false;
            }
        }

        private void RedibujarMateriales()
        {
            GestionListasVisuales.Filtrar<Material>(panel1, textBox1.Text);
        }

        private void AbrirMaterial(Material m)
        {
            GestionRecientes.Registrar("Material", m.ID, m.Nombre);
            _detalleAbierto?.Close();
            _detalleAbierto = new DetallesMateriales(m);
            _detalleAbierto.FormClosed += (s, ev) => CargarMateriales();
            _detalleAbierto.Show(this.FindForm());
        }

        // Botón "+"
        private void button1_Click(object sender, EventArgs e)
        {
           FormularioMateriales formMateriales = new FormularioMateriales(gestion);
            formMateriales.ShowDialog();
            textBox1.Clear();
            CargarMateriales();
        }
 
        // Barra de búsqueda
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label1.Text = "No se ha registrado ningún material";
                label1.Visible = false;
                panel1.Visible = true;
                GestionListasVisuales.Filtrar<Material>(panel1, "");
            }
            else
            {
                GestionListasVisuales.Filtrar<Material>(panel1, textBox1.Text);
                bool sinResultados = panel1.Controls.Count == 0;
                label1.Text = sinResultados ? "No se encontraron materiales." : "";
                label1.Visible = sinResultados;
                panel1.Visible = !sinResultados;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }
    }
}