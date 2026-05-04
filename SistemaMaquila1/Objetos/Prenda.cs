using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class Prenda
    {
        public int ID { get; set; }
        public string Tipo { get; set; }
        public string Talla { get; set; }
        public string Descripcion { get; set; }

        public Prenda(int id, string tipo, string talla, string descripcion)
        {
            ID = id;
            Tipo = tipo;
            Talla = talla;
            Descripcion = descripcion;
        }
    }
}