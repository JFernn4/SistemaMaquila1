using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMaquila1.Objetos
{
    public class Usuario
    {
        public int ID { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
        public string Rol { get; set; }
        public string FotoRuta { get; set; }

        public Usuario(int id, string nombreUsuario, string contrasena, string rol, string fotoRuta = null)
        {
            ID = id;
            NombreUsuario = nombreUsuario;
            Contrasena = contrasena;
            Rol = rol;
            FotoRuta = fotoRuta;
        }
    }
}
