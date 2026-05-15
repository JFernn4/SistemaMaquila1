using SistemaDeImportadora;
using SistemaMaquila1.Formularios;
using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SistemaMaquila1
{
    public partial class InterfazClientes : UserControl
    {
        GestionClientes gestion = new GestionClientes();
        private DetallesClientes _detalleAbierto = null;

        public InterfazClientes()
        {
            InitializeComponent();
        }

        private void InterfazClientes_Load(object sender, EventArgs e)
        {
            CargarClientes();

            // Rerenderizar cuando el panel cambie de tamaño 
            panel1.Resize += (s, ev) =>
            {
                GestionListasVisuales.Filtrar<Cliente>(panel1, textBox1.Text);
            };
        }

        public void CargarClientes()
        {
            panel1.Controls.Clear();
            GestionListasVisuales.LimpiarCache(panel1);

            if (gestion.ExistenClientes())
            {
                label1.Visible = false;
                panel1.Visible = true;
                if (!panel1.Visible)
                {
                    panel1.Visible = true;
                    panel1.Parent?.PerformLayout();
                    panel1.PerformLayout();
                }
                GestionListasVisuales.CargarItems(
                    panel1,
                    gestion.ObtenerClientes(),
                    c => c.Nombre,
                    c => c.ID,
                    c => Properties.Resources.icons8_client_50,  
                    c => AbrirCliente(c)
                );
                panel1.Refresh();
            }
            else
            {
                label1.Visible = true;
                panel1.Visible = false;
            }
        }

        private void AbrirCliente(Cliente c)
        {
            GestionRecientes.Registrar("Cliente", c.ID, c.Nombre);
            _detalleAbierto?.Close();
            _detalleAbierto = new DetallesClientes(c);
            _detalleAbierto.FormClosed += (s, e) => CargarClientes(); // refresca al cerrar
            _detalleAbierto.Show(this.FindForm());
        }

        // Botón "+" — agregar nuevo cliente
        private void button2_Click(object sender, EventArgs e)
        {
            FormularioClientes formClientes = new FormularioClientes(gestion);
            formClientes.ShowDialog();
            textBox1.Clear();
            CargarClientes();
        }
        // Barra de búsqueda
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Restaurar estado normal
                label1.Text = "No se ha registrado ningún cliente";
                label1.Visible = false;
                panel1.Visible = true; 
                GestionListasVisuales.Filtrar<Cliente>(panel1, "");
            }
            else
            {
                GestionListasVisuales.Filtrar<Cliente>(panel1, textBox1.Text);

                bool sinResultados = panel1.Controls.Count == 0;
                label1.Text = sinResultados ? "No se encontraron clientes." : "";
                label1.Visible = sinResultados;
                panel1.Visible = !sinResultados; 
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }

        private void button1_Click_1(object sender, EventArgs e)
        {
            FormularioClientes formClientes = new FormularioClientes(gestion);
            formClientes.ShowDialog();
            textBox1.Clear();
            CargarClientes();  
        }
    }
}