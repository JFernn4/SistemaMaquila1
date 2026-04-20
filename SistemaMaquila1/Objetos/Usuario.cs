using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class Usuario
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }

        public Usuario(string nombreUsuario, string contrasena, string rol)
        {
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Rol = rol;
        }
    }
}
