using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMaquila1
{
    public partial class InterfazUsuarioActual : UserControl
    {
        public InterfazUsuarioActual()
        {
            InitializeComponent();
        }

        private void InterfazUsuarioActual_Load(object sender, EventArgs e)
        {
            ActualizarUI();
        }

        public void ActualizarUI()
        {
            Usuario u = GestionUsuarios.UsuarioActual;

            if (u == null) return;

            label1.Text = u.NombreUsuario;
            label2.Text = u.Rol;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
