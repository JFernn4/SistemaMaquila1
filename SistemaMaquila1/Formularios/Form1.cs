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

        private System.Windows.Forms.Timer reloj = new System.Windows.Forms.Timer();

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
                    { button1,  interfazInicio1 },
                    { button2,  interfazMateriales1 },              // Inventario  pendiente
                    { button3,  interfazEmpleados1 },
                    { button4,  interfazPedidos1 },              // Reportes  pendiente
                    { button11, interfazClientes2 },
                };

            MostrarLogin(); // comentado temporalmente
            reloj.Interval = 1000;

            reloj.Tick += (s, ev) =>
            {
                label1.Text = DateTime.Now.ToString("hh:mm");
                label2.Text = DateTime.Now.ToString("dddd dd MMMM yyyy");
            };

            reloj.Start();
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


            // Mostrar la vista correspondiente
            if (_vistas != null && _vistas.ContainsKey(boton))
                MostrarVista(_vistas[boton]);
        }
        public void RefrescarModuloActivo()
        {
            if (interfazClientes2.Visible) interfazClientes2.CargarClientes();
            if (interfazEmpleados1.Visible) interfazEmpleados1.CargarEmpleados();
            if (interfazMateriales1.Visible) interfazMateriales1.CargarMateriales();
            if (interfazPedidos1.Visible) interfazPedidos1.CargarPedidos();
        }

        //  Botones sidebar 
        private void button1_Click(object sender, EventArgs e) // Inicio
        {
            ActivarBoton(button1);
            interfazInicio1.CargarRecientes(); 
        } // Inicio
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
            inicioSesion1.BringToFront();
            inicioSesion1.Visible = true;
            panel1.Visible = false;
        }

        public void MostrarApp()
        {
            inicioSesion1.Visible = false;
            panel1.Visible = true;
            interfazUsuarioActual1.ActualizarUI();
            ActivarBoton(button1);
            interfazInicio1.CargarRecientes(); // vista por defecto al iniciar sesión
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void interfazUsuarioActual1_Load(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}