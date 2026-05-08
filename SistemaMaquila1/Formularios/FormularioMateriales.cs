using SistemaMaquila1.Herramientas;
using SistemaMaquila1.Objetos;
using System;
using System.Windows.Forms;

namespace SistemaMaquila1.Formularios
{
    public partial class FormularioMateriales : Form
    {
        private GestionMateriales gestion;

        public event Action<Material> MaterialGuardado;

        public FormularioMateriales(GestionMateriales gestion)
        {
            InitializeComponent();
            this.gestion = gestion;
        }

        private void FormularioMateriales_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e) // Cancelar
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) // Registrar
        {
            try
            {
                if (string.IsNullOrEmpty(textBox1.Text) ||
                    string.IsNullOrEmpty(comboBox1.Text) ||
                    string.IsNullOrEmpty(textBox5.Text) ||
                    string.IsNullOrEmpty(comboBox2.Text) ||
                    string.IsNullOrEmpty(textBox3.Text) ||
                    string.IsNullOrEmpty(textBox4.Text) ||
                    string.IsNullOrEmpty(textBox6.Text))
                {
                    Toast.Mostrar("Complete todos los datos", esError: true, formPadre: this);
                    return;
                }

                if (!double.TryParse(textBox3.Text, out double cantidad) || cantidad < 0)
                {
                    Toast.Mostrar("Ingrese una cantidad disponible válida", esError: true, formPadre: this);
                    return;
                }

                if (!double.TryParse(textBox4.Text, out double stockMinimo) || stockMinimo < 0)
                {
                    Toast.Mostrar("Ingrese un stock mínimo válido", esError: true, formPadre: this);
                    return;
                }

                if (!double.TryParse(textBox6.Text, out double costoUnitario) || costoUnitario < 0)
                {
                    Toast.Mostrar("Ingrese un costo unitario válido", esError: true, formPadre: this);
                    return;
                }

                Material materialCreado = Guardar(cantidad, stockMinimo, costoUnitario);
                MaterialGuardado?.Invoke(materialCreado);
                Toast.Mostrar("Material guardado", formPadre: this);
                this.Close();
            }
            catch (Exception ex)
            {
                Toast.Mostrar("Error inesperado: " + ex.Message, esError: true, formPadre: this);
            }
        }

        private Material Guardar(double cantidad, double stockMinimo, double costoUnitario)
        {
            Material material = new Material(
                0,                  // ID autoincrement
                textBox1.Text,      // Nombre
                comboBox1.Text,     // Tipo
                textBox5.Text,      // Color
                comboBox2.Text,     // Unidad de medida
                cantidad,           // Cantidad disponible
                stockMinimo,        // Stock mínimo
                costoUnitario       // Costo unitario
            );
            gestion.GuardarMaterial(material);
            return material;
        }

        private void Limpiar()
        {
            textBox1.Clear(); // Nombre
            textBox5.Clear(); // Color
            textBox3.Clear(); // Cantidad disponible
            textBox4.Clear(); // Stock mínimo
            textBox6.Clear(); // Costo unitario
            comboBox1.Text = ""; // Tipo
            comboBox2.Text = ""; // Unidad de medida
        }

        private void btnLimpiar_Click(object sender, EventArgs e) => Limpiar();

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void textBox6_TextChanged(object sender, EventArgs e) { }
    }
}