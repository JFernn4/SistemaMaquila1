namespace SistemaMaquila1.Formularios
{
    partial class FormularioPedidoDetallado
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
            comboBox1 = new ComboBox();
            textBox3 = new TextBox();
            textBox5 = new TextBox();
            textBox4 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label8 = new Label();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label3 = new Label();
            textBox1 = new TextBox();
            label10 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            panel3 = new Panel();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.Font = new Font("Century Gothic", 12F);
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Administrador", "Encargado de Ventas", "Bodega", "Producción" });
            comboBox1.Location = new Point(46, 166);
            comboBox1.Margin = new Padding(3, 2, 3, 2);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(174, 29);
            comboBox1.TabIndex = 162;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // textBox3
            // 
            textBox3.Cursor = Cursors.IBeam;
            textBox3.Font = new Font("Century Gothic", 12F);
            textBox3.Location = new Point(250, 349);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(302, 27);
            textBox3.TabIndex = 159;
            // 
            // textBox5
            // 
            textBox5.Font = new Font("Century Gothic", 12F);
            textBox5.Location = new Point(46, 235);
            textBox5.Margin = new Padding(3, 2, 3, 2);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(506, 56);
            textBox5.TabIndex = 158;
            // 
            // textBox4
            // 
            textBox4.Cursor = Cursors.IBeam;
            textBox4.Font = new Font("Century Gothic", 12F);
            textBox4.Location = new Point(46, 349);
            textBox4.Margin = new Padding(3, 2, 3, 2);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(174, 27);
            textBox4.TabIndex = 157;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(44, 47, 58);
            button1.Cursor = Cursors.Hand;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Century Gothic", 12F);
            button1.ForeColor = Color.White;
            button1.Location = new Point(440, 431);
            button1.Name = "button1";
            button1.Size = new Size(112, 37);
            button1.TabIndex = 155;
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
            button2.Location = new Point(310, 431);
            button2.Name = "button2";
            button2.Size = new Size(112, 37);
            button2.TabIndex = 154;
            button2.Text = "Registrar";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = SystemColors.ActiveCaptionText;
            label8.Location = new Point(46, 363);
            label8.Name = "label8";
            label8.Size = new Size(0, 21);
            label8.TabIndex = 153;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = SystemColors.ButtonShadow;
            label7.Location = new Point(46, 317);
            label7.Name = "label7";
            label7.Size = new Size(87, 21);
            label7.TabIndex = 152;
            label7.Text = "Cantidad";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = SystemColors.ButtonShadow;
            label6.Location = new Point(250, 315);
            label6.Name = "label6";
            label6.Size = new Size(120, 21);
            label6.TabIndex = 151;
            label6.Text = "Precio unitario";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = SystemColors.ButtonShadow;
            label5.Location = new Point(46, 204);
            label5.Name = "label5";
            label5.Size = new Size(100, 21);
            label5.TabIndex = 150;
            label5.Text = "Descripción";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.ButtonShadow;
            label3.Location = new Point(250, 134);
            label3.Name = "label3";
            label3.Size = new Size(46, 21);
            label3.TabIndex = 148;
            label3.Text = "Talla";
            // 
            // textBox1
            // 
            textBox1.Cursor = Cursors.IBeam;
            textBox1.Font = new Font("Century Gothic", 12F);
            textBox1.Location = new Point(250, 166);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(302, 27);
            textBox1.TabIndex = 147;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Century Gothic", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = SystemColors.ButtonShadow;
            label10.Location = new Point(46, 134);
            label10.Name = "label10";
            label10.Size = new Size(42, 21);
            label10.TabIndex = 146;
            label10.Text = "Tipo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Century Gothic", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = SystemColors.ActiveCaptionText;
            label2.Location = new Point(46, 55);
            label2.Name = "label2";
            label2.Size = new Size(175, 19);
            label2.TabIndex = 145;
            label2.Text = "Detalles de la prenda";
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(44, 47, 58);
            panel2.Controls.Add(panel3);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 10);
            panel2.TabIndex = 163;
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
            // FormularioPedidoDetallado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(598, 523);
            Controls.Add(panel2);
            Controls.Add(comboBox1);
            Controls.Add(textBox3);
            Controls.Add(textBox5);
            Controls.Add(textBox4);
            Controls.Add(button1);
            Controls.Add(button2);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label10);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "FormularioPedidoDetallado";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormularioPedidoDetallado";
            Load += FormularioPedidoDetallado_Load;
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private TextBox textBox3;
        private TextBox textBox5;
        private TextBox textBox4;
        private Button button1;
        private Button button2;
        protected Label label8;
        protected Label label7;
        protected Label label6;
        protected Label label5;
        protected Label label3;
        private TextBox textBox1;
        protected Label label10;
        protected Label label2;
        private Panel panel2;
        private Panel panel3;
    }
}