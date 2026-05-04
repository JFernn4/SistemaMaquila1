public class Toast : Form
{

public static void Mostrar(string mensaje, bool esError = false, Form formPadre = null)
    {
        Toast toast = new Toast(mensaje, esError);

        if (formPadre != null && !formPadre.IsDisposed)
        {
            toast.Left = formPadre.Left + formPadre.Width - toast.Width - 20;
            toast.Top = formPadre.Top + formPadre.Height - toast.Height - 40;
            toast.Show(formPadre);
        }
        else
        {
            // Fallback: centro de la pantalla
            toast.StartPosition = FormStartPosition.CenterScreen;
            toast.Show();
        }

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        timer.Interval = 2500;
        timer.Tick += (s, e) =>
        {
            timer.Stop();
            if (!toast.IsDisposed)
                toast.Close();
        };
        timer.Start();

        toast.Click += (s, e) => { timer.Stop(); if (!toast.IsDisposed) toast.Close(); };
        foreach (Control c in toast.Controls)
            c.Click += (s, e) => { timer.Stop(); if (!toast.IsDisposed) toast.Close(); };
    }

    public Toast(string mensaje, bool esError)
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(280, 60);
        this.BackColor = esError ? Color.FromArgb(200, 60, 60) : Color.FromArgb(26, 95, 180);
        this.Opacity = 0.95;
        this.TopMost = true;
        this.StartPosition = FormStartPosition.Manual;
        this.ShowInTaskbar = false;


        Label lbl = new Label()
        {
            Text = mensaje,
            ForeColor = Color.White,
            Font = new Font("Century Gothic", 10F),
            Location = new Point(45, 20),
            AutoSize = true,
            Cursor = Cursors.Hand
        };

        Label btnCerrar = new Label()
        {
            Text = "✕",
            ForeColor = Color.FromArgb(180, 255, 255, 255),
            Font = new Font("Century Gothic", 9F),
            Size = new Size(20, 20),
            Location = new Point(255, 5),
            TextAlign = ContentAlignment.MiddleCenter,
            Cursor = Cursors.Hand
        };

        this.Controls.Add(lbl);
        this.Controls.Add(btnCerrar);
    }
}
public class Dialogo : Form
{
    public bool Confirmado { get; private set; } = false;

    public static bool Confirmar(Control padre, string mensaje)
    {
        using (Dialogo d = new Dialogo(mensaje))
        {
            d.ShowDialog(padre.FindForm());
            return d.Confirmado;
        }
    }

    public Dialogo(string mensaje)
    {
        this.FormBorderStyle = FormBorderStyle.None;
        this.Size = new Size(320, 150);
        this.BackColor = Color.White;
        this.StartPosition = FormStartPosition.CenterParent;
        this.ShowInTaskbar = false;

        // Barra de color arriba (igual que tus forms)
        Panel barra = new Panel()
        {
            Dock = DockStyle.Top,
            Height = 8,
            BackColor = SystemColors.HotTrack
        };

        Label lbl = new Label()
        {
            Text = mensaje,
            Font = new Font("Century Gothic", 10F),
            Location = new Point(20, 30),
            Size = new Size(280, 50),
            TextAlign = ContentAlignment.MiddleCenter
        };

        Button btnSi = new Button()
        {
            Text = "Aceptar",
            Font = new Font("Century Gothic", 10F),
            BackColor = SystemColors.HotTrack,
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(100, 34),
            Location = new Point(180, 100),
            Cursor = Cursors.Hand
        };
        btnSi.FlatAppearance.BorderSize = 0;
        btnSi.Click += (s, e) => { Confirmado = true; this.Close(); };

        Button btnNo = new Button()
        {
            Text = "Cancelar",
            Font = new Font("Century Gothic", 10F),
            BackColor = Color.Transparent,
            ForeColor = SystemColors.HotTrack,
            FlatStyle = FlatStyle.Flat,
            Size = new Size(100, 34),
            Location = new Point(50, 100),
            Cursor = Cursors.Hand
        };
        btnNo.FlatAppearance.BorderColor = SystemColors.HotTrack;
        btnNo.Click += (s, e) => { Confirmado = false; this.Close(); };

        this.Controls.AddRange(new Control[] { barra, lbl, btnSi, btnNo });
    }
}