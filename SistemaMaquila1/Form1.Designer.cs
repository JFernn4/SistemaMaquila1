namespace SistemaMaquila1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            button11 = new Button();
            SidePanel = new Panel();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            button10 = new Button();
            configuracion1 = new Configuracion();
            interfazClientes2 = new InterfazClientes();
            panel3 = new Panel();
            inicioSesion1 = new SistemaDeImportadora.InicioSesion();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(44, 47, 58);
            panel1.Controls.Add(button11);
            panel1.Controls.Add(SidePanel);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(191, 542);
            panel1.TabIndex = 5;
            panel1.Paint += panel1_Paint;
            // 
            // button11
            // 
            button11.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Century Gothic", 11.25F);
            button11.ForeColor = Color.White;
            button11.Image = (Image)resources.GetObject("button11.Image");
            button11.ImageAlign = ContentAlignment.MiddleLeft;
            button11.ImeMode = ImeMode.NoControl;
            button11.Location = new Point(12, 341);
            button11.Name = "button11";
            button11.Size = new Size(176, 52);
            button11.TabIndex = 8;
            button11.Text = "  Clientes";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // SidePanel
            // 
            SidePanel.BackColor = SystemColors.HotTrack;
            SidePanel.Location = new Point(0, 122);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(8, 54);
            SidePanel.TabIndex = 3;
            SidePanel.Paint += SidePanel_Paint;
            // 
            // button4
            // 
            button4.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Century Gothic", 11.25F);
            button4.ForeColor = Color.White;
            button4.Image = (Image)resources.GetObject("button4.Image");
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.ImeMode = ImeMode.NoControl;
            button4.Location = new Point(12, 286);
            button4.Name = "button4";
            button4.Size = new Size(176, 52);
            button4.TabIndex = 6;
            button4.Text = "  Reportes";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Century Gothic", 11.25F);
            button3.ForeColor = Color.White;
            button3.Image = (Image)resources.GetObject("button3.Image");
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.ImeMode = ImeMode.NoControl;
            button3.Location = new Point(12, 231);
            button3.Name = "button3";
            button3.Size = new Size(176, 52);
            button3.TabIndex = 5;
            button3.Text = "   Empleados";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 11.25F);
            button2.ForeColor = Color.White;
            button2.Image = (Image)resources.GetObject("button2.Image");
            button2.ImageAlign = ContentAlignment.MiddleLeft;
            button2.ImeMode = ImeMode.NoControl;
            button2.Location = new Point(12, 176);
            button2.Name = "button2";
            button2.Size = new Size(176, 52);
            button2.TabIndex = 4;
            button2.Text = "  Inventario";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 11.25F);
            button1.ForeColor = Color.White;
            button1.Image = Properties.Resources.icono_inicio;
            button1.ImageAlign = ContentAlignment.MiddleLeft;
            button1.ImeMode = ImeMode.NoControl;
            button1.Location = new Point(12, 121);
            button1.Name = "button1";
            button1.Size = new Size(176, 52);
            button1.TabIndex = 3;
            button1.Text = "  Inicio";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button10
            // 
            button10.BackColor = Color.Transparent;
            button10.Dock = DockStyle.Right;
            button10.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button10.FlatAppearance.BorderSize = 0;
            button10.FlatStyle = FlatStyle.Flat;
            button10.Font = new Font("Century Gothic", 11.25F);
            button10.ForeColor = Color.White;
            button10.Image = (Image)resources.GetObject("button10.Image");
            button10.ImageAlign = ContentAlignment.MiddleLeft;
            button10.ImeMode = ImeMode.NoControl;
            button10.Location = new Point(818, 0);
            button10.Name = "button10";
            button10.Size = new Size(34, 59);
            button10.TabIndex = 17;
            button10.UseVisualStyleBackColor = false;
            button10.Click += button10_Click;
            // 
            // configuracion1
            // 
            configuracion1.Location = new Point(237, 148);
            configuracion1.Name = "configuracion1";
            configuracion1.Size = new Size(748, 264);
            configuracion1.TabIndex = 18;
            // 
            // interfazClientes2
            // 
            interfazClientes2.Location = new Point(194, 65);
            interfazClientes2.Name = "interfazClientes2";
            interfazClientes2.Size = new Size(851, 477);
            interfazClientes2.TabIndex = 19;
            // 
            // panel3
            // 
            panel3.BackColor = SystemColors.Control;
            panel3.Controls.Add(button10);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(191, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(852, 59);
            panel3.TabIndex = 20;
            // 
            // inicioSesion1
            // 
            inicioSesion1.AutoSize = true;
            inicioSesion1.BackgroundImage = (Image)resources.GetObject("inicioSesion1.BackgroundImage");
            inicioSesion1.Location = new Point(0, 0);
            inicioSesion1.Name = "inicioSesion1";
            inicioSesion1.Size = new Size(1043, 542);
            inicioSesion1.TabIndex = 21;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1043, 542);
            Controls.Add(inicioSesion1);
            Controls.Add(interfazClientes2);
            Controls.Add(configuracion1);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistema Maquila";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button button11;
        private Panel SidePanel;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private Button button10;
        private Configuracion configuracion1;
        private InterfazClientes interfazClientes1;
        private InterfazClientes interfazClientes2;
        private Panel panel3;
        private SistemaDeImportadora.InicioSesion inicioSesion1;
    }
}
