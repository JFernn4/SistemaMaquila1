using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
            this.Resize += (s, ev) => ActualizarUI();
            ActualizarUI();

            // Cursor de mano al pasar sobre la foto
            pictureBox1.Cursor = Cursors.Hand;
        }

        public void ActualizarUI()
        {
            Usuario u = GestionUsuarios.UsuarioActual;
            if (u == null) return;

            label1.Text = u.NombreUsuario;
            label2.Text = u.Rol;

            CargarFoto(u.FotoRuta);

            int margenDerecho = this.Width - 20;
            int anchoBloque = pictureBox1.Width + 8 + Math.Max(label1.Width, label2.Width);

            pictureBox1.Left = margenDerecho - anchoBloque;

            label1.Left = pictureBox1.Right + 8;

            label2.Left = pictureBox1.Right + 8;
        }

        private void CargarFoto(string fotoRuta)
        {
            try
            {
                if (!string.IsNullOrEmpty(fotoRuta) && File.Exists(fotoRuta))
                {
                    Image original = Image.FromFile(fotoRuta);
                    pictureBox1.Image = HacerRedonda(original, pictureBox1.Size);
                }
                else
                {
                    pictureBox1.Image = HacerRedonda(Properties.Resources.icons8_user_35, pictureBox1.Size);
                }
            }
            catch
            {
                pictureBox1.Image = HacerRedonda(Properties.Resources.icons8_user_35, pictureBox1.Size);
            }
        }

        private Image HacerRedonda(Image imagen, Size tamaño)
        {
            Bitmap resultado = new Bitmap(tamaño.Width, tamaño.Height);
            using (Graphics g = Graphics.FromImage(resultado))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(0, 0, tamaño.Width, tamaño.Height);
                    g.SetClip(path);
                    g.DrawImage(imagen, 0, 0, tamaño.Width, tamaño.Height);
                }
            }
            return resultado;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Usuario u = GestionUsuarios.UsuarioActual;
            if (u == null) return;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = "Selecciona una foto de perfil";
                dialog.Filter = "Imágenes|*.jpg;*.jpeg;*.png;*.bmp";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string ruta = dialog.FileName;

                    // Guardar en BD
                    new GestionUsuarios().ActualizarFotoUsuario(u.ID, ruta);

                    // Actualizar el objeto en memoria
                    u.FotoRuta = ruta;

                    // Refrescar la foto
                    CargarFoto(ruta);
                    Toast.Mostrar("Foto actualizada", formPadre: this.FindForm());
                }
            }
        }

        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}