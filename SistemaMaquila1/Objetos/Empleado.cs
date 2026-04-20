using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class Empleado
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Cargo { get; set; }
        public string Correo { get; set; }
        public double Salario { get; set; }

        public Empleado(int iD, string nombre, string telefono, string cargo, string correo, double salario)
        {
            ID = iD;
            Nombre = nombre;
            Telefono = telefono;
            Cargo = cargo;
            Correo = correo;
            Salario = salario;
        }
    }
}
