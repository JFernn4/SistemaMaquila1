using System;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using SistemaMaquila1.Objetos;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaMaquila1
{
    public static class BaseDatos
    {
        private static string dbRuta = "maquila.db";
        private static string connectionString = $"Data Source={dbRuta};Version=3;";

        public static void Inicializar()
        {
            if (!File.Exists(dbRuta))
            {
                SQLiteConnection.CreateFile(dbRuta);
            }

            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string[] tablas = new string[]
{
    //USUARIOS
    @"CREATE TABLE IF NOT EXISTS TUsuarios (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        NombreUsuario TEXT,
        Contrasena TEXT,
        Rol TEXT
    )",
    // CLIENTES 
    @"CREATE TABLE IF NOT EXISTS TClientes (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nombre TEXT,
        Telefono TEXT,
        Correo TEXT
    )",

    // EMPLEADOS 
    @"CREATE TABLE IF NOT EXISTS TEmpleados (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nombre TEXT,
        Telefono TEXT,
        Cargo TEXT, -- Bodeguero, EncargadoVentas, Administrador
        Correo TEXT,
        Salario REAL
    )",

    // MATERIALES 
    @"CREATE TABLE IF NOT EXISTS TMateriales (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Nombre TEXT,
        Tipo TEXT,
        Color TEXT,
        UnidadMedida TEXT,
        CantidadDisponible REAL,
        StockMinimo REAL,
        CostoUnitario REAL
    )",

    // PRENDAS 
    @"CREATE TABLE IF NOT EXISTS TPrendas (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Tipo TEXT,
        Talla TEXT,
        Descripcion TEXT
    )",
    // PEDIDOS 
    @"CREATE TABLE IF NOT EXISTS TPedidos (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        ClienteId INTEGER,
        EmpleadoId INTEGER,
        FechaInicio TEXT,
        FechaEntrega TEXT,
        Anticipo REAL,
        Total REAL,
        Estado TEXT,
        FOREIGN KEY (ClienteId) REFERENCES TClientes(Id),
        FOREIGN KEY (EmpleadoId) REFERENCES TEmpleados(Id)
    )",

    // DETALLE PEDIDO 
    @"CREATE TABLE IF NOT EXISTS TDetallePedido (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        PedidoId INTEGER,
        PrendaId INTEGER,
        Cantidad INTEGER,
        PrecioUnitario REAL,
        FOREIGN KEY (PedidoId) REFERENCES TPedidos(Id),
        FOREIGN KEY (PrendaId) REFERENCES TPrendas(Id)
    )",

    // RELACIÓN PRENDA - MATERIAL
    @"CREATE TABLE IF NOT EXISTS TPrendaMaterial (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        PrendaId INTEGER,
        MaterialId INTEGER,
        Cantidad REAL,
        FOREIGN KEY (PrendaId) REFERENCES TPrendas(Id),
        FOREIGN KEY (MaterialId) REFERENCES TMateriales(Id)
    )",

};
                foreach (var sql in tablas)
                {
                    using (var cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                InsertarUsuarioPorDefecto();
            }
        }
        public static SQLiteConnection AbrirConexion()
        {
            var conn = new SQLiteConnection(connectionString);
            conn.Open();
            return conn;
        }
        //USUARIOS
        public static void InsertarUsuario(Usuario usuario)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO TUsuarios (NombreUsuario, Contrasena, Rol) VALUES (@nombre, @contrasena, @rol)";
                cmd.Parameters.AddWithValue("@nombre", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", usuario.Contrasena);
                cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                cmd.ExecuteNonQuery();
            }
        }
        public static bool ValidarUsuario(string nombreUsuario, string contrasena)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT COUNT(*) 
                            FROM TUsuarios 
                            WHERE NombreUsuario = @nombre 
                            AND Contrasena = @contrasena";

                cmd.Parameters.AddWithValue("@nombre", nombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", contrasena);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        public static bool ExistenUsuarios()
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM TUsuarios";
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        public static void InsertarUsuarioPorDefecto()
        {
            if (!ExistenUsuarios())
            {
                InsertarUsuario(new Usuario("admin", "1234", "Administrador"));
            }
        }
        //CLIENTES
        public static void InsertarCliente(Cliente cliente)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO TClientes (Nombre, Telefono, Correo) VALUES (@Nombre, @Telefono, @Correo)";
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                cmd.ExecuteNonQuery();
            }
        }
        public static List<Cliente> ObtenerClientes()
        {
            var lista = new List<Cliente>();

            using (var conn = AbrirConexion())
            using (var cmd = new SQLiteCommand("SELECT * FROM TClientes", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Cliente(
                        Convert.ToInt32(reader["Id"]),
                        reader["Nombre"].ToString(),
                        reader["Telefono"].ToString(),
                        reader["Correo"].ToString()
                    ));
                }
            }

            return lista;
        }
        //EMPLEADOS
        public static void InsertarEmpleado(Empleado empleado)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO TEmpleados 
                            (Nombre, Telefono, Cargo, Correo, Salario) 
                            VALUES (@Nombre, @Telefono, @Cargo, @Correo, @Salario)";

                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@Salario", empleado.Salario);

                cmd.ExecuteNonQuery();
            }
        }
        //MATERIALES    
        public static void InsertarMaterial(Material material)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO TMateriales 
                            (Nombre, Tipo, Color, UnidadMedida, CantidadDisponible, StockMinimo, CostoUnitario) 
                            VALUES (@Nombre, @Tipo, @Color, @UnidadMedida, @CantidadDisponible, @StockMinimo, @CostoUnitario)";
                cmd.Parameters.AddWithValue("@Nombre", material.Nombre);
                cmd.Parameters.AddWithValue("@Tipo", material.Tipo);
                cmd.Parameters.AddWithValue("@Color", material.Color);
                cmd.Parameters.AddWithValue("@UnidadMedida", material.UnidadMedida);
                cmd.Parameters.AddWithValue("@CantidadDisponible", material.CantidadDisponible);
                cmd.Parameters.AddWithValue("@StockMinimo", material.StockMinimo);
                cmd.Parameters.AddWithValue("@CostoUnitario", material.CostoUnitario);
                cmd.ExecuteNonQuery();
            }
        }
    }
}

