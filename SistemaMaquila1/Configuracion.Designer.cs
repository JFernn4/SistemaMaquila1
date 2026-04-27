namespace SistemaMaquila1
{
    partial class Configuracion
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            SidePanel = new Panel();
            button4 = new Button();
            button3 = new Button();
            button11 = new Button();
            configUsuarios1 = new ConfigUsuarios();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.HotTrack;
            panel1.Controls.Add(SidePanel);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button11);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(164, 264);
            panel1.TabIndex = 114;
            // 
            // SidePanel
            // 
            SidePanel.BackColor = SystemColors.Info;
            SidePanel.Location = new Point(0, 43);
            SidePanel.Name = "SidePanel";
            SidePanel.Size = new Size(10, 54);
            SidePanel.TabIndex = 116;
            // 
            // button4
            // 
            button4.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Century Gothic", 11.25F);
            button4.ForeColor = Color.White;
            button4.ImageAlign = ContentAlignment.MiddleLeft;
            button4.ImeMode = ImeMode.NoControl;
            button4.Location = new Point(0, 157);
            button4.Name = "button4";
            button4.Size = new Size(164, 52);
            button4.TabIndex = 115;
            button4.Text = "Regresar";
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
            button3.ImageAlign = ContentAlignment.MiddleLeft;
            button3.ImeMode = ImeMode.NoControl;
            button3.Location = new Point(0, 100);
            button3.Name = "button3";
            button3.Size = new Size(164, 52);
            button3.TabIndex = 114;
            button3.Text = "Inventario";
            button3.UseVisualStyleBackColor = true;
            // 
            // button11
            // 
            button11.FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
            button11.FlatAppearance.BorderSize = 0;
            button11.FlatStyle = FlatStyle.Flat;
            button11.Font = new Font("Century Gothic", 11.25F);
            button11.ForeColor = Color.White;
            button11.ImageAlign = ContentAlignment.MiddleLeft;
            button11.ImeMode = ImeMode.NoControl;
            button11.Location = new Point(0, 43);
            button11.Name = "button11";
            button11.Size = new Size(161, 52);
            button11.TabIndex = 9;
            button11.Text = "Usuarios";
            button11.UseVisualStyleBackColor = true;
            button11.Click += button11_Click;
            // 
            // configUsuarios1
            // 
            configUsuarios1.Location = new Point(165, 53);
            configUsuarios1.Name = "configUsuarios1";
            configUsuarios1.Size = new Size(586, 194);
            configUsuarios1.TabIndex = 115;
            configUsuarios1.Load += configUsuarios1_Load;
            // 
            // Configuracion
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(configUsuarios1);
            Controls.Add(panel1);
            Name = "Configuracion";
            Size = new Size(748, 264);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel SidePanel;
        private Button button4;
        private Button button3;
        private Button button11;
        private ConfigUsuarios configUsuarios1;
    }
}
