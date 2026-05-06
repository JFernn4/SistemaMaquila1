namespace SistemaMaquila1
{
    partial class FormularioUsuarios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel2 = new Panel();
            panel3 = new Panel();
            label2 = new Label();
            label10 = new Label();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            label3 = new Label();
            label4 = new Label();
            button1 = new Button();
            button2 = new Button();
            comboBox2 = new ComboBox();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(44, 47, 58);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 10);
            panel2.TabIndex = 96;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Black;
            panel3.ForeColor = SystemColors.ButtonHighlight;
            panel3.Location = new Point(429, 10);
            panel3.Name = "panel3";
            panel3.Size = new Size(326, 128);
            panel3.TabIndex = 14;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(46, 60);
            label2.Name = "label2";
            label2.Size = new Size(158, 19);
            label2.TabIndex = 98;
            label2.Text = "Detalles del usuario";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = SystemColors.ButtonShadow;
            label10.Location = new Point(46, 102);
            label10.Name = "label10";
            label10.Size = new Size(157, 21);
            label10.TabIndex = 100;
            label10.Text = "Nombre de usuario";
            // 
            // textBox2
            // 
            textBox2.Font = new Font("Century Gothic", 12F);
            textBox2.Location = new Point(46, 132);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(227, 27);
            textBox2.TabIndex = 120;
            // 
            // textBox1
            // 
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Font = new Font("Century Gothic", 12F);
            textBox1.Location = new Point(325, 132);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(227, 27);
            textBox1.TabIndex = 121;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonShadow;
            label3.Location = new Point(325, 102);
            label3.Name = "label3";
            label3.Size = new Size(103, 21);
            label3.TabIndex = 122;
            label3.Text = "Contraseña";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.ButtonShadow;
            label4.Location = new Point(46, 168);
            label4.Name = "label4";
            label4.Size = new Size(33, 21);
            label4.TabIndex = 123;
            label4.Text = "Rol";
            label4.Click += label4_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(44, 47, 58);
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 12F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(440, 258);
            button1.Name = "button1";
            button1.Size = new Size(112, 37);
            button1.TabIndex = 126;
            button1.Text = "Cancelar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.BackColor = Color.FromArgb(44, 47, 58);
            button2.Cursor = Cursors.Hand;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Century Gothic", 12F);
            button2.ForeColor = Color.White;
            button2.Location = new Point(308, 258);
            button2.Name = "button2";
            button2.Size = new Size(112, 37);
            button2.TabIndex = 125;
            button2.Text = "Aceptar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // comboBox2
            // 
            comboBox2.Font = new Font("Century Gothic", 12F);
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { "Administrador", "Usuario Regular" });
            comboBox2.Location = new Point(46, 198);
            comboBox2.Margin = new Padding(3, 2, 3, 2);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(177, 29);
            comboBox2.TabIndex = 128;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // FormularioUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(598, 355);
            Controls.Add(comboBox2);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(textBox2);
            Controls.Add(label10);
            Controls.Add(label2);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormularioUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormularioUsuarios";
            Load += FormularioUsuarios_Load;
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel2;
        private Panel panel3;
        protected Label label2;
        protected Label label10;
        private TextBox textBox2;
        private TextBox textBox1;
        protected Label label3;
        protected Label label4;
        private Button button1;
        private Button button2;
        private ComboBox comboBox2;
    }
}