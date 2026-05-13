using SistemaDeImportadora;
using System.Configuration;

namespace SistemaMaquila1
{
    public partial class Form1 : Form
    {
        private readonly Dictionary<Button, (Image inactivo, Image activo)> _iconos;
        private Button _botonActivo = null;

        // Mapea cada botón con su UserControl correspondiente
        private Dictionary<Button, Control> _vistas;

        public Form1()
        {
            InitializeComponent();

            _iconos = new Dictionary<Button, (Image, Image)>
            {
                { button1,  (Properties.Resources.icono_inicio,          Properties.Resources.icono_inicio_activo) },
                { button2,  (Properties.Resources.icono_inventario,       Properties.Resources.icono_inventario_activo) },
                { button3,  (Properties.Resources.icono_empleado,         Properties.Resources.icono_empleados_activo) },
                { button4,  (Properties.Resources.icono_reportes,         Properties.Resources.icono_reportes_activo) },
                { button11, (Properties.Resources.icono_clientes,         Properties.Resources.icono_clientes_activo) },
            };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Dock a todos los UserControls
            interfazClientes2.Dock = DockStyle.Fill;
            interfazEmpleados1.Dock = DockStyle.Fill;
            // agrega aquí los demás cuando los tengas

            // Mapea botones con sus vistas
            _vistas = new Dictionary<Button, Control>
            {
                { button1,  interfazPedidos1 },              // Inicio  sin vista por ahora
                { button2,  interfazMateriales1 },              // Inventario  pendiente
                { button3,  interfazEmpleados1 },
                { button4,  null },              // Reportes  pendiente
                { button11, interfazClientes2 },
            };

            // MostrarLogin(); // comentado temporalmente
            ActivarBoton(button1); // vista por defecto al arrancar
        }

        //  Navegación 

        private void MostrarVista(Control vista)
        {
            // Ocultar todas las vistas
            foreach (var v in _vistas.Values)
                if (v != null) v.Visible = false;

            // Mostrar la seleccionada
            if (vista != null)
            {
                vista.Visible = true;
                vista.BringToFront();
            }
        }

        private void ActivarBoton(Button boton)
        {
            if (_botonActivo != null)
            {
                _botonActivo.ForeColor = Color.White;
                if (_iconos.ContainsKey(_botonActivo))
                    _botonActivo.Image = _iconos[_botonActivo].inactivo;
            }

            _botonActivo = boton;
            boton.ForeColor = Color.FromArgb(0, 102, 204);
            if (_iconos.ContainsKey(boton))
                boton.Image = _iconos[boton].activo;

            SidePanel.Height = boton.Height;
            SidePanel.Top = boton.Top;

            // Mostrar la vista correspondiente
            if (_vistas != null && _vistas.ContainsKey(boton))
                MostrarVista(_vistas[boton]);
        }

        //  Botones sidebar 
        private void button1_Click(object sender, EventArgs e) => ActivarBoton(button1);  // Inicio
        private void button2_Click(object sender, EventArgs e) => ActivarBoton(button2);  // Inventario
        private void button3_Click(object sender, EventArgs e) => ActivarBoton(button3);  // Empleados
        private void button4_Click(object sender, EventArgs e) => ActivarBoton(button4);  // Reportes
        private void button11_Click(object sender, EventArgs e) => ActivarBoton(button11); // Clientes

        private void button10_Click(object sender, EventArgs e)
        {
            configuracion1.Show();
            configuracion1.BringToFront();
        }

        //  Login 
        public void MostrarLogin()
        {
            inicioSesion1.Visible = true;
            panel1.Visible = false;
        }

        public void MostrarApp()
        {
            inicioSesion1.Visible = false;
            panel1.Visible = true;
            interfazUsuarioActual1.ActualizarUI();
            ActivarBoton(button1); // vista por defecto al iniciar sesión
        }

        private void SidePanel_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void inicioSesion1_Load(object sender, EventArgs e) { }
        private void inicioSesion1_Load_1(object sender, EventArgs e) { }
        private void inicioSesion1_Load_2(object sender, EventArgs e) { }
        private void inicioSesion1_Load_3(object sender, EventArgs e) { }
        private void inicioSesion1_Load_4(object sender, EventArgs e) { }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void interfazEmpleados1_Load(object sender, EventArgs e)
        {

        }

        private void configuracion1_Load(object sender, EventArgs e)
        {

        }

        private void interfazMateriales1_Load(object sender, EventArgs e)
        {

        }
    }
}