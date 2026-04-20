using SistemaDeImportadora;
using System.Configuration;
using static System.Collections.Specialized.BitVector32;

namespace SistemaMaquila1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void inicioSesion1_Load(object sender, EventArgs e)
        {

        }

        private void SidePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button2.Height;
            SidePanel.Top = button2.Top;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button3.Height;
            SidePanel.Top = button3.Top;
            // temporal!
            GestionEmpleados gestion = new GestionEmpleados();
            FormularioEmpleados formEmpleados = new FormularioEmpleados(gestion);
            formEmpleados.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button4.Height;
            SidePanel.Top = button4.Top;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button11.Height;
            SidePanel.Top = button11.Top;
            // temporal!
            GestionClientes gestion = new GestionClientes();
            FormularioClientes formClientes = new FormularioClientes(gestion);
            formClientes.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //ajustes
            configuracion1.Show();
            configuracion1.BringToFront();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SidePanel.Height = button1.Height;
            SidePanel.Top = button1.Top;
        }

        private void inicioSesion1_Load_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
