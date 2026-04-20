using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }

        public Cliente(int iD, string nombre, string telefono, string correo)
        {
            ID = iD;
            Nombre = nombre;
            Telefono = telefono;
            Correo = correo;
        }
    }
}
