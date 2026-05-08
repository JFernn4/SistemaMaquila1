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

namespace SistemaMaquila1.Formularios
{
    public partial class DetallesMateriales : Form
    {
        private Material _material;
        public DetallesMateriales(Material material)
        {
            InitializeComponent();
        }

        private void DetallesMateriales_Load(object sender, EventArgs e)
        {

        }
    }
}
