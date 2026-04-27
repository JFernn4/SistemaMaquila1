using SistemaDeImportadora;
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

        public InterfazClientes()
        {
            InitializeComponent();
        }

        private void InterfazClientes_Load(object sender, EventArgs e)
        {
            CargarClientes();
        }

        private void CargarClientes()
        {
            if (gestion.ExistenClientes())
            {
                label1.Visible = false;
                panel1.Visible = true;

                GestionListasVisuales.CargarItems(
                    panel1,
                    gestion.ObtenerClientes(),
                    c => c.Nombre,
                    c => c.ID,
                    Properties.Resources.icons8_client_50,
                    c => AbrirCliente(c)
                );
            }
            else
            {
                label1.Visible = true;
                panel1.Visible = false;
            }
        }

        private void AbrirCliente(Cliente c)
        {
            //USERCONTROL DETALLADO DE CLIENTE (PENDIENTE)
            MessageBox.Show("Cliente: " + c.Nombre);
        }

        // Botón "+" — agregar nuevo cliente
        private void button2_Click(object sender, EventArgs e)
        {
            FormularioClientes formClientes = new FormularioClientes(gestion);
            formClientes.FormClosed += (s, ev) => CargarClientes();
            formClientes.Show(this.FindForm());
        }

        // Barra de búsqueda
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                // Sin texto: mostrar todos
                GestionListasVisuales.Filtrar<Cliente>(panel1, "");
            }
            else
            {
                GestionListasVisuales.Filtrar<Cliente>(panel1, textBox1.Text);

                // Mostrar aviso si no hay resultados
                bool sinResultados = panel1.Controls.Count == 0;
                label1.Text = sinResultados ? "No se encontraron clientes." : "";
                label1.Visible = sinResultados;
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GestionClientes gestion = new GestionClientes();
            FormularioClientes formEmpleados = new FormularioClientes(gestion);
            formEmpleados.Show();
        }
    }
}