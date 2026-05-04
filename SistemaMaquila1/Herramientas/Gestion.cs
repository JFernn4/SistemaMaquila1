using System;
using System.Collections.Generic;
using System.Linq;
using SistemaMaquila1.Objetos;

namespace SistemaMaquila1.Herramientas
{
    public class GestionUsuarios
    {
        public static Usuario UsuarioActual { get; private set; }

        public bool IniciarSesion(string nombreUsuario, string contrasena)
        {
            Usuario usuario = BaseDatos.ValidarUsuario(nombreUsuario, contrasena);
            if (usuario != null)
            {
                UsuarioActual = usuario;
                return true;
            }
            return false;
        }

        public void GuardarUsuario(Usuario usuario) => BaseDatos.InsertarUsuario(usuario);
        public void CerrarSesion() => UsuarioActual = null;
    }

    public class GestionClientes
    {
        public void GuardarCliente(Cliente cliente) => BaseDatos.InsertarCliente(cliente);
        public void ActualizarCliente(Cliente cliente) => BaseDatos.ActualizarCliente(cliente);
        public void EliminarCliente(int id) => BaseDatos.EliminarCliente(id);
        public List<Cliente> ObtenerClientes() => BaseDatos.ObtenerClientes();
        public bool ExistenClientes() => BaseDatos.ObtenerClientes().Any();
    }

    public class GestionEmpleados
    {
        public void GuardarEmpleado(Empleado empleado) => BaseDatos.InsertarEmpleado(empleado);
        public void ActualizarEmpleado(Empleado empleado) => BaseDatos.ActualizarEmpleado(empleado);
        public void EliminarEmpleado(int id) => BaseDatos.EliminarEmpleado(id);
        public List<Empleado> ObtenerEmpleados() => BaseDatos.ObtenerEmpleados();
        public bool ExistenEmpleados() => BaseDatos.ObtenerEmpleados().Any();
    }

    public class GestionMateriales
    {
        public void GuardarMaterial(Material material) => BaseDatos.InsertarMaterial(material);
        public void ActualizarMaterial(Material material) => BaseDatos.ActualizarMaterial(material);
        public void EliminarMaterial(int id) => BaseDatos.EliminarMaterial(id);
        public List<Material> ObtenerMateriales() => BaseDatos.ObtenerMateriales();
        public bool ExistenMateriales() => BaseDatos.ObtenerMateriales().Any();
    }

    public class GestionPrendas
    {
        public void GuardarPrenda(Prenda prenda) => BaseDatos.InsertarPrenda(prenda);
        public void ActualizarPrenda(Prenda prenda) => BaseDatos.ActualizarPrenda(prenda);
        public void EliminarPrenda(int id) => BaseDatos.EliminarPrenda(id);
        public List<Prenda> ObtenerPrendas() => BaseDatos.ObtenerPrendas();
        public bool ExistenPrendas() => BaseDatos.ObtenerPrendas().Any();
    }

    public class GestionPedidos
    {
        public void GuardarPedido(Pedido pedido) => BaseDatos.InsertarPedido(pedido);
        public void ActualizarPedido(Pedido pedido) => BaseDatos.ActualizarPedido(pedido);
        public void EliminarPedido(int id) => BaseDatos.EliminarPedido(id);
        public List<Pedido> ObtenerPedidos() => BaseDatos.ObtenerPedidos();
        public bool ExistenPedidos() => BaseDatos.ObtenerPedidos().Any();

        // Útil para filtrar pedidos por cliente o empleado
        public List<Pedido> ObtenerPedidosPorCliente(int clienteId)
            => BaseDatos.ObtenerPedidos().Where(p => p.ClienteId == clienteId).ToList();

        public List<Pedido> ObtenerPedidosPorEmpleado(int empleadoId)
            => BaseDatos.ObtenerPedidos().Where(p => p.EmpleadoId == empleadoId).ToList();

        public List<Pedido> ObtenerPedidosPorEstado(string estado)
            => BaseDatos.ObtenerPedidos().Where(p => p.Estado == estado).ToList();
    }
}