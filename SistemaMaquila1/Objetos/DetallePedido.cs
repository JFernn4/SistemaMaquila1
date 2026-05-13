using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class DetallePedido
    {
        public int ID { get; set; }
        public int PedidoId { get; set; }
        public int PrendaId { get; set; } //en el formulario primero se registra la prenda, luego se completa el formulario
        public int Cantidad { get; set; }
        public double PrecioUnitario { get; set; }

        // Para mostrar en UI sin consulta extra
        public Prenda Prenda { get; set; }

        public DetallePedido(int id, int pedidoId, int prendaId, int cantidad, double precioUnitario)
        {
            ID = id;
            PedidoId = pedidoId;
            PrendaId = prendaId;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}