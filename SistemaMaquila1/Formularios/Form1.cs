using SistemaDeImportadora;
using System.Configuration;

namespace SistemaMaquila1
{
    public partial class Form1 : Form
    {
        // Cambia los recursos por los que tengas en tu proyecto
        private readonly Dictionary<Button, (Image inactivo, Image activo)> _iconos;

        // Botón actualmente seleccionado
        private Button _botonActivo = null;

        public Form1()
        {
            InitializeComponent();

            // Mapea cada botón con su ícono inactivo y activo
            _iconos = new Dictionary<Button, (Image, Image)>
            {
                { button1,  (Properties.Resources.icono_inicio,      Properties.Resources.icono_inicio_activo) },
                { button2,  (Properties.Resources.icono_inventario,   Properties.Resources.icono_inventario_activo) },
                { button3,  (Properties.Resources.icono_empleado,    Properties.Resources.icono_empleados_activo) },
                { button4,  (Properties.Resources.icono_reportes,     Properties.Resources.icono_reportes_activo) },
                { button11, (Properties.Resources.icono_clientes,     Properties.Resources.icono_clientes_activo) },
            };
        }

        private void ActivarBoton(Button boton)
        {
            // Resetear el botón anterior
            if (_botonActivo != null)
            {
                _botonActivo.ForeColor = Color.White; // color inactivo
                if (_iconos.ContainsKey(_botonActivo))
                    _botonActivo.Image = _iconos[_botonActivo].inactivo;
            }


            _botonActivo = boton;
            boton.ForeColor = Color.FromArgb(0, 102, 204); // azul activo
            if (_iconos.ContainsKey(boton))
                boton.Image = _iconos[boton].activo;


            SidePanel.Height = boton.Height;
            SidePanel.Top = boton.Top;
        }

        private void button1_Click(object sender, EventArgs e) => ActivarBoton(button1); // Inicio
        private void button2_Click(object sender, EventArgs e) => ActivarBoton(button2); // Inventario
        private void button3_Click(object sender, EventArgs e) => ActivarBoton(button3); // Empleados
        private void button4_Click(object sender, EventArgs e) => ActivarBoton(button4); // Reportes
        private void button11_Click(object sender, EventArgs e) => ActivarBoton(button11); // Clientes

        private void button10_Click(object sender, EventArgs e)
        {
            configuracion1.Show();
            configuracion1.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ActivarBoton(button1);
            MostrarLogin();
        }

        public void MostrarLogin()
        {
            inicioSesion1.Visible = true;      // panel que contiene InicioSesion
            panel1.Visible = false; // panel con sidebar + módulos
        }

        public void MostrarApp()
        {
            inicioSesion1.Visible = false;
            panel1.Visible = true;
            interfazUsuarioActual1.ActualizarUI(); // refresca nombre y rol
        }


        private void SidePanel_Paint(object sender, PaintEventArgs e) { }
        private void panel1_Paint(object sender, PaintEventArgs e) { }
        private void inicioSesion1_Load(object sender, EventArgs e) { }
        private void inicioSesion1_Load_1(object sender, EventArgs e) { }

        private void inicioSesion1_Load_2(object sender, EventArgs e)
        {

        }
    }
}