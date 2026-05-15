using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class ItemReciente
    {
        public int ID { get; set; }
        public string Tipo { get; set; }
        public int ElementoId { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaAcceso { get; set; }

        public ItemReciente(int id, string tipo, int elementoId, string nombre, DateTime fechaAcceso)
        {
            ID = id;
            Tipo = tipo;
            ElementoId = elementoId;
            Nombre = nombre;
            FechaAcceso = fechaAcceso;
        }
    }
}