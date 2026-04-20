using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class Material
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string Color { get; set; }
        public string UnidadMedida { get; set; }
        public double CantidadDisponible { get; set; }
        public double StockMinimo { get; set; }
        public double CostoUnitario { get; set; }

        public Material(int iD, string nombre, string tipo, string color, string unidadMedida, double cantidadDisponible, double stockMinimo, double costoUnitario)
        {
            ID = iD;
            Nombre = nombre;
            Tipo = tipo;
            Color = color;
            UnidadMedida = unidadMedida;
            CantidadDisponible = cantidadDisponible;
            StockMinimo = stockMinimo;
            CostoUnitario = costoUnitario;
        }
    }
}