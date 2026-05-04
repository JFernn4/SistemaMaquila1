using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class Pedido
    {
        public int ID { get; set; }
        public int ClienteId { get; set; }
        public int EmpleadoId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaEntrega { get; set; }
        public double Anticipo { get; set; }
        public double Total { get; set; }
        public string Estado { get; set; } // Ej: "Pendiente", "En proceso", "Completado"

        public Pedido(int id, int clienteId, int empleadoId, DateTime fechaInicio,
                      DateTime fechaEntrega, double anticipo, double total, string estado)
        {
            ID = id;
            ClienteId = clienteId;
            EmpleadoId = empleadoId;
            FechaInicio = fechaInicio;
            FechaEntrega = fechaEntrega;
            Anticipo = anticipo;
            Total = total;
            Estado = estado;
        }
    }
}