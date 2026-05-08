using SistemaDeImportadora;
using SistemaMaquila1.Formularios;
using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System.Windows.Forms;

namespace SistemaMaquila1.Controles_de_Usuario
{
    public partial class InterfazEmpleados : UserControl
    {
        GestionEmpleados gestion = new GestionEmpleados();
        private DetallesEmpleados _detalleAbierto = null;

        public InterfazEmpleados()
        {
            InitializeComponent();
        }

        private void InterfazEmpleados_Load(object sender, EventArgs e)
        {
            panel1.SizeChanged += (s, ev) => RedibujarEmpleados();
            CargarEmpleados();
        }

        private void CargarEmpleados()
        {
            panel1.Controls.Clear();
            GestionListasVisuales.LimpiarCache(panel1);

            if (gestion.ExistenEmpleados())
            {
                label1.Visible = false;
                panel1.Visible = true;

                GestionListasVisuales.CargarItems(
                    panel1,
                    gestion.ObtenerEmpleados(),
                    e => e.Nombre,
                    e => e.ID,
                    Properties.Resources.icons8_employee_50, 
                    e => AbrirEmpleado(e)
                );

                panel1.Refresh();
            }
            else
            {
                label1.Visible = true;
                panel1.Visible = false;
            }
        }

        private void RedibujarEmpleados()
        {
            GestionListasVisuales.Filtrar<Empleado>(panel1, textBox1.Text);
        }

        private void AbrirEmpleado(Empleado e)
        {
            _detalleAbierto?.Close();
            _detalleAbierto = new DetallesEmpleados(e);
            _detalleAbierto.FormClosed += (s, ev) => CargarEmpleados();
            _detalleAbierto.Show(this.FindForm());
        }

        // Botón "+"
        private void button1_Click(object sender, EventArgs e)
        {
            FormularioEmpleados formEmpleados = new FormularioEmpleados(gestion);
            formEmpleados.ShowDialog();
            textBox1.Clear();
            CargarEmpleados();
        }

        // Barra de búsqueda
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label1.Text = "No se ha registrado ningún empleado";
                label1.Visible = false;
                panel1.Visible = true;
                GestionListasVisuales.Filtrar<Empleado>(panel1, "");
            }
            else
            {
                GestionListasVisuales.Filtrar<Empleado>(panel1, textBox1.Text);
                bool sinResultados = panel1.Controls.Count == 0;
                label1.Text = sinResultados ? "No se encontraron empleados." : "";
                label1.Visible = sinResultados;
                panel1.Visible = !sinResultados;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e) { }

    }
}