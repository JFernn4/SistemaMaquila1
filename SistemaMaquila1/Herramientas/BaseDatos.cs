using System;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography.Xml;
using System.Windows.Forms;
using SistemaMaquila1.Objetos;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaMaquila1.Herramientas
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
        DPI TEXT,
        Nombre TEXT,
        Telefono TEXT,
        Correo TEXT
    )",

    // EMPLEADOS
    @"CREATE TABLE IF NOT EXISTS TEmpleados (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        DPI TEXT,
        Nombre TEXT,
        Telefono TEXT,
        Cargo TEXT,
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
                cmd.Parameters.AddWithValue("@contrasena", Seguridad.HashSHA256(usuario.Contrasena)); //SHA256
                cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                cmd.ExecuteNonQuery();
            }
        }

        public static Usuario ValidarUsuario(string nombreUsuario, string contrasena)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, NombreUsuario, Rol FROM TUsuarios 
                            WHERE NombreUsuario = @nombre 
                            AND Contrasena = @contrasena";

                cmd.Parameters.AddWithValue("@nombre", nombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", Seguridad.HashSHA256(contrasena));

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario(
                            reader["NombreUsuario"].ToString(),
                            "", // no retornamos la contraseña
                            reader["Rol"].ToString()
                        );
                    }
                }
                return null; // credenciales inválidas
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
                cmd.CommandText = "INSERT INTO TClientes (DPI, Nombre, Telefono, Correo) VALUES (@DPI, @Nombre, @Telefono, @Correo)";
                cmd.Parameters.AddWithValue("@DPI", cliente.DPI);
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
                        reader["DPI"].ToString(),
                        reader["Nombre"].ToString(),
                        reader["Telefono"].ToString(),
                        reader["Correo"].ToString()
                    ));
                }
            }
            return lista;
        }
        public static void ActualizarCliente(Cliente cliente)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE TClientes 
                            SET DPI = @DPI, Nombre = @Nombre, 
                                Telefono = @Telefono, Correo = @Correo
                            WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@DPI", cliente.DPI);
                cmd.Parameters.AddWithValue("@Nombre", cliente.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                cmd.Parameters.AddWithValue("@Correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@Id", cliente.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarCliente(int id)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TClientes WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }
        //EMPLEADOS
        public static void InsertarEmpleado(Empleado empleado)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO TEmpleados 
                    (DPI, Nombre, Telefono, Cargo, Correo, Salario) 
                    VALUES (@DPI, @Nombre, @Telefono, @Cargo, @Correo, @Salario)";
                cmd.Parameters.AddWithValue("@DPI", empleado.DPI);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@Salario", empleado.Salario);
                cmd.ExecuteNonQuery();
            }
        }
        public static List<Empleado> ObtenerEmpleados()
        {
            var lista = new List<Empleado>();
            using (var conn = AbrirConexion())
            using (var cmd = new SQLiteCommand("SELECT * FROM TEmpleados", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Empleado(
                        Convert.ToInt32(reader["Id"]),
                        reader["DPI"].ToString(),
                        reader["Nombre"].ToString(),
                        reader["Telefono"].ToString(),
                        reader["Cargo"].ToString(),
                        reader["Correo"].ToString(),
                        Convert.ToDouble(reader["Salario"])
                    ));
                }
            }
            return lista;
        }

        public static void ActualizarEmpleado(Empleado empleado)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE TEmpleados 
                            SET DPI = @DPI, Nombre = @Nombre, Telefono = @Telefono,
                                Cargo = @Cargo, Correo = @Correo, Salario = @Salario
                            WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@DPI", empleado.DPI);
                cmd.Parameters.AddWithValue("@Nombre", empleado.Nombre);
                cmd.Parameters.AddWithValue("@Telefono", empleado.Telefono);
                cmd.Parameters.AddWithValue("@Cargo", empleado.Cargo);
                cmd.Parameters.AddWithValue("@Correo", empleado.Correo);
                cmd.Parameters.AddWithValue("@Salario", empleado.Salario);
                cmd.Parameters.AddWithValue("@Id", empleado.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarEmpleado(int id)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TEmpleados WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
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

        public static List<Material> ObtenerMateriales()
        {
            var lista = new List<Material>();
            using (var conn = AbrirConexion())
            using (var cmd = new SQLiteCommand("SELECT * FROM TMateriales", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Material(
                        Convert.ToInt32(reader["Id"]),
                        reader["Nombre"].ToString(),
                        reader["Tipo"].ToString(),
                        reader["Color"].ToString(),
                        reader["UnidadMedida"].ToString(),
                        Convert.ToDouble(reader["CantidadDisponible"]),
                        Convert.ToDouble(reader["StockMinimo"]),
                        Convert.ToDouble(reader["CostoUnitario"])
                    ));
                }
            }
            return lista;
        }

        public static void ActualizarMaterial(Material material)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE TMateriales 
                            SET Nombre = @Nombre, Tipo = @Tipo, Color = @Color,
                                UnidadMedida = @UnidadMedida, CantidadDisponible = @CantidadDisponible,
                                StockMinimo = @StockMinimo, CostoUnitario = @CostoUnitario
                            WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Nombre", material.Nombre);
                cmd.Parameters.AddWithValue("@Tipo", material.Tipo);
                cmd.Parameters.AddWithValue("@Color", material.Color);
                cmd.Parameters.AddWithValue("@UnidadMedida", material.UnidadMedida);
                cmd.Parameters.AddWithValue("@CantidadDisponible", material.CantidadDisponible);
                cmd.Parameters.AddWithValue("@StockMinimo", material.StockMinimo);
                cmd.Parameters.AddWithValue("@CostoUnitario", material.CostoUnitario);
                cmd.Parameters.AddWithValue("@Id", material.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarMaterial(int id)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TMateriales WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        //  PRENDAS 

        public static void InsertarPrenda(Prenda prenda)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO TPrendas (Tipo, Talla, Descripcion) VALUES (@Tipo, @Talla, @Descripcion)";
                cmd.Parameters.AddWithValue("@Tipo", prenda.Tipo);
                cmd.Parameters.AddWithValue("@Talla", prenda.Talla);
                cmd.Parameters.AddWithValue("@Descripcion", prenda.Descripcion);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Prenda> ObtenerPrendas()
        {
            var lista = new List<Prenda>();
            using (var conn = AbrirConexion())
            using (var cmd = new SQLiteCommand("SELECT * FROM TPrendas", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Prenda(
                        Convert.ToInt32(reader["Id"]),
                        reader["Tipo"].ToString(),
                        reader["Talla"].ToString(),
                        reader["Descripcion"].ToString()
                    ));
                }
            }
            return lista;
        }

        public static void ActualizarPrenda(Prenda prenda)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE TPrendas 
                            SET Tipo = @Tipo, Talla = @Talla, Descripcion = @Descripcion
                            WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Tipo", prenda.Tipo);
                cmd.Parameters.AddWithValue("@Talla", prenda.Talla);
                cmd.Parameters.AddWithValue("@Descripcion", prenda.Descripcion);
                cmd.Parameters.AddWithValue("@Id", prenda.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarPrenda(int id)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TPrendas WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        //  PEDIDOS 

        public static void InsertarPedido(Pedido pedido)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO TPedidos 
                            (ClienteId, EmpleadoId, FechaInicio, FechaEntrega, Anticipo, Total, Estado)
                            VALUES (@ClienteId, @EmpleadoId, @FechaInicio, @FechaEntrega, @Anticipo, @Total, @Estado)";
                cmd.Parameters.AddWithValue("@ClienteId", pedido.ClienteId);
                cmd.Parameters.AddWithValue("@EmpleadoId", pedido.EmpleadoId);
                cmd.Parameters.AddWithValue("@FechaInicio", pedido.FechaInicio.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@FechaEntrega", pedido.FechaEntrega.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Anticipo", pedido.Anticipo);
                cmd.Parameters.AddWithValue("@Total", pedido.Total);
                cmd.Parameters.AddWithValue("@Estado", pedido.Estado);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<Pedido> ObtenerPedidos()
        {
            var lista = new List<Pedido>();
            using (var conn = AbrirConexion())
            using (var cmd = new SQLiteCommand("SELECT * FROM TPedidos", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    lista.Add(new Pedido(
                        Convert.ToInt32(reader["Id"]),
                        Convert.ToInt32(reader["ClienteId"]),
                        Convert.ToInt32(reader["EmpleadoId"]),
                        DateTime.Parse(reader["FechaInicio"].ToString()),
                        DateTime.Parse(reader["FechaEntrega"].ToString()),
                        Convert.ToDouble(reader["Anticipo"]),
                        Convert.ToDouble(reader["Total"]),
                        reader["Estado"].ToString()
                    ));
                }
            }
            return lista;
        }

        public static void ActualizarPedido(Pedido pedido)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"UPDATE TPedidos 
                            SET ClienteId = @ClienteId, EmpleadoId = @EmpleadoId,
                                FechaInicio = @FechaInicio, FechaEntrega = @FechaEntrega,
                                Anticipo = @Anticipo, Total = @Total, Estado = @Estado
                            WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@ClienteId", pedido.ClienteId);
                cmd.Parameters.AddWithValue("@EmpleadoId", pedido.EmpleadoId);
                cmd.Parameters.AddWithValue("@FechaInicio", pedido.FechaInicio.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@FechaEntrega", pedido.FechaEntrega.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Anticipo", pedido.Anticipo);
                cmd.Parameters.AddWithValue("@Total", pedido.Total);
                cmd.Parameters.AddWithValue("@Estado", pedido.Estado);
                cmd.Parameters.AddWithValue("@Id", pedido.ID);
                cmd.ExecuteNonQuery();
            }
        }

        public static void EliminarPedido(int id)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TPedidos WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}

