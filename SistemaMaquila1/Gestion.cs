using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaMaquila1.Objetos;

namespace SistemaMaquila1
{
    public class GestionUsuarios
    {
        List<Usuario> listaUsuarios = new List<Usuario>();
        public void GuardarUsuario(Usuario usuario)
        {
            listaUsuarios.Add(usuario);
            BaseDatos.InsertarUsuario(usuario);
        }
        public void Eliminar(int id)
        {

        }
    }
    public class GestionClientes
    {
        List<Cliente> listaClientes = new List<Cliente>();
        public void GuardarCliente(Cliente cliente)
        {
            listaClientes.Add(cliente);
            BaseDatos.InsertarCliente(cliente);
        }
        public void Eliminar(int id)
        {

        }

    }
    public class GestionEmpleados
    {
        List<Empleado> listaEmpleados = new List<Empleado>();
        public void GuardarEmpleado(Empleado empleado)
        {
            listaEmpleados.Add(empleado);
            BaseDatos.InsertarEmpleado(empleado);
        }
    }
    public class GestionMateriales
    {
        List<Material> listaMateriales = new List<Material>();
        public void GuardarMaterial(Material material)
        {
            listaMateriales.Add(material);
            BaseDatos.InsertarMaterial(material);
        }
    }
}