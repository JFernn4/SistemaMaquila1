using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SistemaMaquila1.Herramientas
{
    internal class GestionListasVisuales
    {
        private static Dictionary<Panel, object> _cacheListas = new Dictionary<Panel, object>();

        // ─── CargarItems ──────────────────────────────────────────────────

        public static void CargarItems<T>(
            Panel panel,
            List<T> lista,
            Func<T, string> obtenerTexto,
            Func<T, int> obtenerId,
            Func<T, Image> obtenerImagen,
            Action<T> onClick = null
        )
        {
            _cacheListas[panel] = new ItemsCache<T>
            {
                Lista = lista,
                ObtenerTexto = obtenerTexto,
                ObtenerID = obtenerId,
                ObtenerImagen = obtenerImagen,
                Imagen = null,
                OnClick = onClick
            };

            panel.AutoScroll = true;
            panel.AutoScrollMinSize = new Size(0, 0);
            RenderizarItems(panel, lista, obtenerTexto, obtenerId, obtenerImagen, onClick);
        }

        // Sobrecarga con imagen estática — convierte a Func internamente
        public static void CargarItems<T>(
            Panel panel,
            List<T> lista,
            Func<T, string> obtenerTexto,
            Func<T, int> obtenerId,
            Image imagen,
            Action<T> onClick = null
        )
        {
            CargarItems(panel, lista, obtenerTexto, obtenerId, _ => imagen, onClick);
        }

        // ─── Filtrar ──────────────────────────────────────────────────────

        public static void Filtrar<T>(Panel panel, string textoBusqueda)
        {
            if (!_cacheListas.ContainsKey(panel)) return;

            var cache = _cacheListas[panel] as ItemsCache<T>;
            if (cache == null) return;

            var listaFiltrada = string.IsNullOrWhiteSpace(textoBusqueda)
                ? cache.Lista
                : cache.Lista
                    .Where(item => cache.ObtenerTexto(item)
                        .IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

            Func<T, Image> obtenerImagen = cache.ObtenerImagen ?? (_ => cache.Imagen);
            RenderizarItems(panel, listaFiltrada, cache.ObtenerTexto, cache.ObtenerID, obtenerImagen, cache.OnClick);
        }

        // ─── Renderizado interno ──────────────────────────────────────────

        private static void RenderizarItems<T>(
            Panel panel,
            List<T> lista,
            Func<T, string> obtenerTexto,
            Func<T, int> obtenerId,
            Func<T, Image> obtenerImagen,
            Action<T> onClick)
        {
            panel.SuspendLayout();
            panel.Controls.Clear();

            int columnas = Math.Max(1, (panel.Width - 20) / 110);
            int x = 20, y = 20, col = 0;

            foreach (var item in lista)
            {
                var itemLocal = item;
                Button btn = CrearBoton(
                    obtenerTexto(item),
                    obtenerId(item),
                    obtenerImagen(item),
                    x, y
                );

                if (onClick != null)
                    btn.Click += (s, e) => onClick(itemLocal);

                panel.Controls.Add(btn);

                col++;
                if (col >= columnas) { col = 0; x = 20; y += 110; }
                else x += 110;
            }

            panel.AutoScrollMinSize = new Size(0, y + 110 + 20);
            panel.ResumeLayout();
        }

        // ─── Helpers ──────────────────────────────────────────────────────

        private static Button CrearBoton(string texto, int id, Image imagen, int x, int y)
        {
            Button btn = new Button
            {
                BackColor = SystemColors.ButtonHighlight,
                Cursor = Cursors.Hand,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Century Gothic", 9F),
                ForeColor = Color.Black,
                ImageAlign = ContentAlignment.TopCenter,
                TextAlign = ContentAlignment.BottomCenter,
                TextImageRelation = TextImageRelation.ImageAboveText,
                Padding = new Padding(0, 5, 0, 0),
                Size = new Size(96, 102),
                Location = new Point(x, y),
                Text = AjustarTexto(texto),
                Tag = id,
                Image = imagen
            };
            btn.FlatAppearance.BorderColor = SystemColors.ButtonHighlight;
            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(220, 235, 255);
            btn.MouseLeave += (s, e) => btn.BackColor = SystemColors.ButtonHighlight;

            return btn;
        }

        private static string AjustarTexto(string texto)
        {
            const int max = 10;
            if (texto.Length <= max) return texto;
            if (texto.Length <= max * 2) return texto.Insert(max, "\n");
            return texto.Substring(0, max) + "\n" + texto.Substring(max, max - 3) + "...";
        }

        // ─── Caché ────────────────────────────────────────────────────────

        private class ItemsCache<T>
        {
            public List<T> Lista { get; set; }
            public Func<T, string> ObtenerTexto { get; set; }
            public Func<T, int> ObtenerID { get; set; }
            public Image Imagen { get; set; }
            public Func<T, Image> ObtenerImagen { get; set; }
            public Action<T> OnClick { get; set; }
        }

        public static void LimpiarCache(Panel panel)
        {
            if (_cacheListas.ContainsKey(panel))
                _cacheListas.Remove(panel);
        }
    }
}