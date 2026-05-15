using SistemaMaquila1.Formularios;
using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace SistemaMaquila1.Controles_de_Usuario
{
    public partial class InterfazInicio : UserControl
    {
        public InterfazInicio()
        {
            InitializeComponent();
        }

        private void InterfazInicio_Load(object sender, EventArgs e)
        {
            CargarRecientes();
        }

        public void CargarRecientes()
        {
            var recientes = GestionRecientes.ObtenerRecientes();

            if (recientes.Count == 0)
            {
                label1.Visible = true;
                panel1.Visible = false;
                return;
            }

            label1.Visible = false;
            panel1.Visible = true;

            GestionListasVisuales.CargarItems(
                panel1,
                recientes,
                r => r.Nombre,
                r => r.ID,
                ObtenerIcono,
                r => AbrirReciente(r)
            );
        }

        private Image ObtenerIcono(ItemReciente item)
        {
            return item.Tipo switch
            {
                "Cliente" => Properties.Resources.icons8_client_50,
                "Empleado" => Properties.Resources.icons8_employee_50,
                "Material" => Properties.Resources.icons8_blankie_50,
                "Pedido" => Properties.Resources.icons8_books_50,
                _ => Properties.Resources.icons8_user_50
            };
        }


        private void AbrirReciente(ItemReciente item)
        {
            Form1 form1 = this.FindForm() as Form1;

            switch (item.Tipo)
            {
                case "Cliente":
                    var cliente = new GestionClientes().ObtenerClientes()
                        .Find(c => c.ID == item.ElementoId);
                    if (cliente == null) { MostrarNoEncontrado(item); return; }
                    var dc = new DetallesClientes(cliente);
                    dc.FormClosed += (s, ev) => {
                        CargarRecientes();
                        form1?.RefrescarModuloActivo();
                    };
                    dc.Show(this.FindForm());
                    break;

                case "Empleado":
                    var empleado = new GestionEmpleados().ObtenerEmpleados()
                        .Find(e => e.ID == item.ElementoId);
                    if (empleado == null) { MostrarNoEncontrado(item); return; }
                    var de = new DetallesEmpleados(empleado);
                    de.FormClosed += (s, ev) => {
                        CargarRecientes();
                        form1?.RefrescarModuloActivo();
                    };
                    de.Show(this.FindForm());
                    break;

                case "Material":
                    var material = new GestionMateriales().ObtenerMateriales()
                        .Find(m => m.ID == item.ElementoId);
                    if (material == null) { MostrarNoEncontrado(item); return; }
                    var dm = new DetallesMateriales(material);
                    dm.FormClosed += (s, ev) => {
                        CargarRecientes();
                        form1?.RefrescarModuloActivo();
                    };
                    dm.Show(this.FindForm());
                    break;

                case "Pedido":
                    var pedido = new GestionPedidos().ObtenerPedidos()
                        .Find(p => p.ID == item.ElementoId);
                    if (pedido == null) { MostrarNoEncontrado(item); return; }
                    var dp = new DetallesPedidos(pedido);
                    dp.FormClosed += (s, ev) => {
                        CargarRecientes();
                        form1?.RefrescarModuloActivo();
                    };
                    dp.Show(this.FindForm());
                    break;
            }

            CargarRecientes();
        }

        private void MostrarNoEncontrado(ItemReciente item)
        {
            Toast.Mostrar($"{item.Tipo} ya no existe", esError: true, formPadre: this.FindForm());
            // Limpiar el reciente que ya no existe
            BaseDatos.InsertarReciente(
                GestionUsuarios.UsuarioActual.ID,
                item.Tipo, item.ElementoId, item.Nombre
            );
            CargarRecientes();
        }

        private void panel1_Paint(object sender, PaintEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label1.Visible = false;
                panel1.Visible = true;
                GestionListasVisuales.Filtrar<ItemReciente>(panel1, "");
            }
            else
            {
                GestionListasVisuales.Filtrar<ItemReciente>(panel1, textBox1.Text);
                bool sinResultados = panel1.Controls.Count == 0;
                label1.Text = sinResultados ? "No se encontraron resultados." : "";
                label1.Visible = sinResultados;
                panel1.Visible = !sinResultados;
            }
        }
    }
}