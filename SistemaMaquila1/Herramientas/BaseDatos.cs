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
        Rol TEXT,
        FotoRuta TEXT   
    )",

    @"CREATE TABLE IF NOT EXISTS TRecientes (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        UsuarioId INTEGER,
        Tipo TEXT,
        ElementoId INTEGER,
        Nombre TEXT,
        FechaAcceso TEXT,
        FOREIGN KEY (UsuarioId) REFERENCES TUsuarios(Id)
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
                cmd.CommandText = "INSERT INTO TUsuarios (NombreUsuario, Contrasena, Rol, FotoRuta) VALUES (@nombre, @contrasena, @rol, @fotoRuta)";
                cmd.Parameters.AddWithValue("@nombre", usuario.NombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", Seguridad.HashSHA256(usuario.Contrasena));
                cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@fotoRuta", (object)usuario.FotoRuta ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public static Usuario ValidarUsuario(string nombreUsuario, string contrasena)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT Id, NombreUsuario, Rol, FotoRuta FROM TUsuarios 
                            WHERE NombreUsuario = @nombre 
                            AND Contrasena = @contrasena";
                cmd.Parameters.AddWithValue("@nombre", nombreUsuario);
                cmd.Parameters.AddWithValue("@contrasena", Seguridad.HashSHA256(contrasena));

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Usuario(
                            Convert.ToInt32(reader["Id"]),
                            reader["NombreUsuario"].ToString(),
                            "",
                            reader["Rol"].ToString(),
                            reader["FotoRuta"]?.ToString()
                        );
                    }
                }
                return null;
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
                InsertarUsuario(new Usuario(0,"admin", "1234", "Administrador",null));
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

        // InsertarPedido actualizado — también guarda las prendas
        public static void InsertarPedido(Pedido pedido)
        {
            using (var conn = AbrirConexion())
            {
                // 1. Insertar el pedido
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

                    // Obtener ID del pedido recién insertado
                    cmd.CommandText = "SELECT last_insert_rowid()";
                    int pedidoId = Convert.ToInt32(cmd.ExecuteScalar());

                    // 2. Para cada detalle: guardar la prenda primero, luego el detalle
                    foreach (var detalle in pedido.Prendas)
                    {
                        // Insertar la prenda y obtener su ID
                        cmd.CommandText = "INSERT INTO TPrendas (Tipo, Talla, Descripcion) VALUES (@Tipo, @Talla, @Descripcion)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Tipo", detalle.Prenda.Tipo);
                        cmd.Parameters.AddWithValue("@Talla", detalle.Prenda.Talla);
                        cmd.Parameters.AddWithValue("@Descripcion", detalle.Prenda.Descripcion);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT last_insert_rowid()";
                        int prendaId = Convert.ToInt32(cmd.ExecuteScalar());

                        // Insertar el detalle con IDs reales
                        cmd.CommandText = @"INSERT INTO TDetallePedido 
                                    (PedidoId, PrendaId, Cantidad, PrecioUnitario)
                                    VALUES (@PedidoId, @PrendaId, @Cantidad, @PrecioUnitario)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@PedidoId", pedidoId);
                        cmd.Parameters.AddWithValue("@PrendaId", prendaId);
                        cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                        cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                        cmd.ExecuteNonQuery();
                    }   
                }
            }
        }
        // ObtenerPedidos actualizado — carga las prendas de cada pedido
        public static List<Pedido> ObtenerPedidos()
        {
            var lista = new List<Pedido>();
            using (var conn = AbrirConexion())
            using (var cmd = new SQLiteCommand("SELECT * FROM TPedidos", conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var pedido = new Pedido(
                        Convert.ToInt32(reader["Id"]),
                        Convert.ToInt32(reader["ClienteId"]),
                        Convert.ToInt32(reader["EmpleadoId"]),
                        DateTime.Parse(reader["FechaInicio"].ToString()),
                        DateTime.Parse(reader["FechaEntrega"].ToString()),
                        Convert.ToDouble(reader["Anticipo"]),
                        Convert.ToDouble(reader["Total"]),
                        reader["Estado"].ToString()
                    );
                    // Cargar prendas del pedido
                    pedido.Prendas = ObtenerDetallesPorPedido(pedido.ID);
                    lista.Add(pedido);
                }
            }
            return lista;
        }

        // ActualizarPedido actualizado — reemplaza las prendas
        public static void ActualizarPedido(Pedido pedido)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                // 1. Actualizar el pedido
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

                // 2. Eliminar detalles viejos
                cmd.CommandText = "DELETE FROM TDetallePedido WHERE PedidoId = @PedidoId";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PedidoId", pedido.ID);
                cmd.ExecuteNonQuery();

                // 3. Reinsertar cada prenda y su detalle
                foreach (var detalle in pedido.Prendas)
                {
                    int prendaId;

                    if (detalle.Prenda.ID == 0)
                    {
                        // Prenda nueva — insertarla primero
                        cmd.CommandText = "INSERT INTO TPrendas (Tipo, Talla, Descripcion) VALUES (@Tipo, @Talla, @Descripcion)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@Tipo", detalle.Prenda.Tipo);
                        cmd.Parameters.AddWithValue("@Talla", detalle.Prenda.Talla);
                        cmd.Parameters.AddWithValue("@Descripcion", detalle.Prenda.Descripcion);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "SELECT last_insert_rowid()";
                        prendaId = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    else
                    {
                        // Prenda existente — usar su ID
                        prendaId = detalle.Prenda.ID;
                    }

                    // Insertar detalle con ID real
                    cmd.CommandText = @"INSERT INTO TDetallePedido 
                                (PedidoId, PrendaId, Cantidad, PrecioUnitario)
                                VALUES (@PedidoId, @PrendaId, @Cantidad, @PrecioUnitario)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@PedidoId", pedido.ID);
                    cmd.Parameters.AddWithValue("@PrendaId", prendaId);
                    cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                    cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                    cmd.ExecuteNonQuery();
                }
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
        // ─── DETALLE PEDIDO 

        public static void InsertarDetallePedido(DetallePedido detalle)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"INSERT INTO TDetallePedido 
                            (PedidoId, PrendaId, Cantidad, PrecioUnitario)
                            VALUES (@PedidoId, @PrendaId, @Cantidad, @PrecioUnitario)";
                cmd.Parameters.AddWithValue("@PedidoId", detalle.PedidoId);
                cmd.Parameters.AddWithValue("@PrendaId", detalle.PrendaId);
                cmd.Parameters.AddWithValue("@Cantidad", detalle.Cantidad);
                cmd.Parameters.AddWithValue("@PrecioUnitario", detalle.PrecioUnitario);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<DetallePedido> ObtenerDetallesPorPedido(int pedidoId)
        {
            var lista = new List<DetallePedido>();
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT d.*, p.Tipo, p.Talla, p.Descripcion 
                            FROM TDetallePedido d
                            JOIN TPrendas p ON d.PrendaId = p.Id
                            WHERE d.PedidoId = @PedidoId";
                cmd.Parameters.AddWithValue("@PedidoId", pedidoId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var detalle = new DetallePedido(
                            Convert.ToInt32(reader["Id"]),
                            Convert.ToInt32(reader["PedidoId"]),
                            Convert.ToInt32(reader["PrendaId"]),
                            Convert.ToInt32(reader["Cantidad"]),
                            Convert.ToDouble(reader["PrecioUnitario"])
                        );
                        // Adjuntar el objeto Prenda directamente
                        detalle.Prenda = new Prenda(
                            Convert.ToInt32(reader["PrendaId"]),
                            reader["Tipo"].ToString(),
                            reader["Talla"].ToString(),
                            reader["Descripcion"].ToString()
                        );
                        lista.Add(detalle);
                    }
                }
            }
            return lista;
        }

        public static void EliminarDetallesPorPedido(int pedidoId)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM TDetallePedido WHERE PedidoId = @PedidoId";
                cmd.Parameters.AddWithValue("@PedidoId", pedidoId);
                cmd.ExecuteNonQuery();
            }
        }

        // ─── RECIENTES ────────────────────────────────────────────────────────────

        public static void InsertarReciente(int usuarioId, string tipo, int elementoId, string nombre)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                // Evitar duplicados — si ya existe, actualizar la fecha
                cmd.CommandText = @"INSERT INTO TRecientes (UsuarioId, Tipo, ElementoId, Nombre, FechaAcceso)
                            VALUES (@UsuarioId, @Tipo, @ElementoId, @Nombre, @FechaAcceso)
                            ON CONFLICT DO NOTHING";
                cmd.CommandText = @"DELETE FROM TRecientes 
                            WHERE UsuarioId = @UsuarioId AND Tipo = @Tipo AND ElementoId = @ElementoId";
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@ElementoId", elementoId);
                cmd.ExecuteNonQuery();

                cmd.CommandText = @"INSERT INTO TRecientes (UsuarioId, Tipo, ElementoId, Nombre, FechaAcceso)
                            VALUES (@UsuarioId, @Tipo, @ElementoId, @Nombre, @FechaAcceso)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@ElementoId", elementoId);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@FechaAcceso", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                cmd.ExecuteNonQuery();

                // Mantener solo los últimos 10 por usuario
                cmd.CommandText = @"DELETE FROM TRecientes WHERE UsuarioId = @UsuarioId
                            AND Id NOT IN (
                                SELECT Id FROM TRecientes 
                                WHERE UsuarioId = @UsuarioId
                                ORDER BY FechaAcceso DESC LIMIT 10
                            )";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);
                cmd.ExecuteNonQuery();
            }
        }

        public static List<ItemReciente> ObtenerRecientes(int usuarioId)
        {
            var lista = new List<ItemReciente>();
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"SELECT * FROM TRecientes 
                            WHERE UsuarioId = @UsuarioId 
                            ORDER BY FechaAcceso DESC";
                cmd.Parameters.AddWithValue("@UsuarioId", usuarioId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ItemReciente(
                            Convert.ToInt32(reader["Id"]),
                            reader["Tipo"].ToString(),
                            Convert.ToInt32(reader["ElementoId"]),
                            reader["Nombre"].ToString(),
                            DateTime.Parse(reader["FechaAcceso"].ToString())
                        ));
                    }
                }
            }
            return lista;
        }

        // ─── FOTO USUARIO/EMPLEADO ────────────────────────────────────────────────

        public static void ActualizarFotoUsuario(int id, string fotoRuta)
        {
            using (var conn = AbrirConexion())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = "UPDATE TUsuarios SET FotoRuta = @FotoRuta WHERE Id = @Id";
                cmd.Parameters.AddWithValue("@FotoRuta", (object)fotoRuta ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }


    }
}

